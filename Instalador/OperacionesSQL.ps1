# PadelGest - operaciones SQL para Inno Setup.
# Se utiliza autenticacion integrada de Windows; no guarda contrasenas.
# SQL Server y sqlcmd deben existir previamente. Exclusivamente PadelGestDB.
param(
    [Parameter(Mandatory=$true)][ValidateSet('Consultar','Instalar','Reinstalar','Eliminar','BackupEliminar')][string]$Accion,
    [Parameter(Mandatory=$true)][ValidateSet('.','localhost\SQLEXPRESS')][string]$Servidor,
    [string]$ScriptSQL = '',
    [string]$ArchivoDestinoRespaldo = '',
    [switch]$PermitirReemplazo
)
$ErrorActionPreference = 'Stop'
$base = 'PadelGestDB'

function Ejecutar-Sql([string]$Consulta, [string]$Archivo = '') {
    # SQL Server local usa certificado autofirmado. -C omite la validacion
    # del certificado, como TrustServerCertificate=True de PadelGest.
    $argumentos = @('-S', $Servidor, '-E', '-C', '-b', '-l', '10', '-h', '-1', '-W')
    if ($Archivo) { $argumentos += @('-i', $Archivo) }
    else { $argumentos += @('-Q', $Consulta) }
    $salida = & sqlcmd.exe @argumentos 2>&1 | Out-String
    if ($LASTEXITCODE -ne 0) { throw "SQL Server no pudo completar la operacion: $salida" }
    return $salida.Trim()
}

try {
    if (-not (Get-Command sqlcmd.exe -ErrorAction SilentlyContinue)) {
        throw 'No se encontro sqlcmd. Instale las utilidades de linea de comandos de SQL Server y vuelva a intentar.'
    }
    $existe = (Ejecutar-Sql "SET NOCOUNT ON; SELECT CASE WHEN DB_ID(N'$base') IS NULL THEN 0 ELSE 1 END;").Trim() -eq '1'

    # Codigos usados por el asistente antes de instalar:
    # 10 = existe la base, 11 = instalacion nueva; 1 = fallo de conexion.
    # Consultar es exclusivamente lectura: nunca modifica ni crea la base.
    if ($Accion -eq 'Consultar') {
        if ($existe) { exit 10 }
        exit 11
    }

    if ($Accion -eq 'Reinstalar') {
        if (-not $existe) {
            throw 'No existe PadelGestDB en el servidor seleccionado. Reinstalar solo es posible con una base existente.'
        }
        # DB_ID por si solo no garantiza que se pueda usar la base.
        $valida = (Ejecutar-Sql "SET NOCOUNT ON; SELECT CASE WHEN HAS_DBACCESS(N'$base') = 1 AND (SELECT COUNT(*) FROM [PadelGestDB].sys.tables WHERE name IN (N'Usuario', N'Cliente', N'Producto')) = 3 THEN 1 ELSE 0 END;").Trim()
        if ($valida -ne '1') {
            throw 'La base existente no esta accesible o no contiene las tablas principales de PadelGest. No se modifico el programa.'
        }
        Write-Output 'Base existente verificada. Se actualizaran solo los archivos del programa.'
        exit 0
    }

    if ($Accion -eq 'Instalar') {
        if (-not (Test-Path -LiteralPath $ScriptSQL -PathType Leaf)) {
            throw 'No se encontro el script de instalacion limpia.'
        }
        if ($existe) {
            if (-not $PermitirReemplazo) {
                throw 'Ya existe PadelGestDB. No se permite reemplazarla sin confirmacion explicita del asistente.'
            }

            # La carpeta elegida no se recibe como texto en la linea de comandos:
            # Inno Setup la escribe en UTF-8 en un archivo temporal controlado.
            if ([string]::IsNullOrWhiteSpace($ArchivoDestinoRespaldo) -or
                -not (Test-Path -LiteralPath $ArchivoDestinoRespaldo -PathType Leaf)) {
                throw 'No se recibio de forma valida la carpeta elegida para el respaldo. Se conserva la base.'
            }

            $carpetaRespaldo = [string](Get-Content -LiteralPath $ArchivoDestinoRespaldo -Encoding UTF8 | Select-Object -First 1)
            if ([string]::IsNullOrWhiteSpace($carpetaRespaldo)) {
                throw 'La carpeta elegida para el respaldo esta vacia. Se conserva la base.'
            }
            try {
                $rutaRespaldo = [System.IO.Path]::GetFullPath($carpetaRespaldo)
            }
            catch {
                throw 'La carpeta elegida para el respaldo no es una ruta valida. Se conserva la base.'
            }
            if (-not [System.IO.Path]::IsPathRooted($rutaRespaldo) -or
                -not (Test-Path -LiteralPath $rutaRespaldo -PathType Container)) {
                throw 'La carpeta elegida para el respaldo no existe o no es una ruta absoluta. Se conserva la base.'
            }

            $archivoRespaldo = Join-Path $rutaRespaldo ('PadelGestDB_AntesDeInstalar_' + (Get-Date -Format 'yyyyMMdd_HHmmss') + '_' + [guid]::NewGuid().ToString('N') + '.bak')
            $sqlArchivo = $archivoRespaldo.Replace("'", "''")
            Ejecutar-Sql "BACKUP DATABASE [$base] TO DISK = N'$sqlArchivo' WITH COPY_ONLY, CHECKSUM;" | Out-Null
            if (-not (Test-Path -LiteralPath $archivoRespaldo -PathType Leaf) -or
                (Get-Item -LiteralPath $archivoRespaldo).Length -le 0) {
                throw 'SQL Server indico que termino el backup, pero no se encontro un archivo .bak valido. Se conserva la base.'
            }
            Ejecutar-Sql "RESTORE VERIFYONLY FROM DISK = N'$sqlArchivo' WITH CHECKSUM;" | Out-Null
            Write-Output "Respaldo SQL verificado en: $archivoRespaldo"

            # Solo eliminar despues de verificar correctamente el respaldo.
            Ejecutar-Sql "USE [master]; ALTER DATABASE [$base] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$base];" | Out-Null
            if ((Ejecutar-Sql "SET NOCOUNT ON; SELECT CASE WHEN DB_ID(N'$base') IS NULL THEN 0 ELSE 1 END;").Trim() -ne '0') {
                throw 'No se pudo confirmar el borrado de la base antigua. La instalacion se detuvo.'
            }
        }

        # sqlcmd puede interpretar un archivo UTF-8 sin BOM como ANSI.
        # La copia temporal UTF-16 LE con BOM preserva nombres y acentos.
        $scriptTemporal = Join-Path $env:TEMP ('PadelGest_SQL_' + [guid]::NewGuid().ToString('N') + '.sql')
        try {
            $textoSql = [System.IO.File]::ReadAllText($ScriptSQL, [System.Text.Encoding]::UTF8)
            [System.IO.File]::WriteAllText($scriptTemporal, $textoSql, [System.Text.Encoding]::Unicode)
            Ejecutar-Sql '' $scriptTemporal | Out-Null
        }
        finally {
            if (Test-Path -LiteralPath $scriptTemporal) { Remove-Item -LiteralPath $scriptTemporal -Force }
        }
        if ((Ejecutar-Sql "SET NOCOUNT ON; SELECT CASE WHEN DB_ID(N'$base') IS NULL THEN 0 ELSE 1 END;").Trim() -ne '1') {
            throw 'No se pudo comprobar la creacion de la base de datos.'
        }
        Write-Output 'Base de datos nueva creada y verificada.'
        exit 0
    }

    if (-not $existe) {
        Write-Output 'La base de datos ya no existe. No hay informacion para eliminar.'
        exit 0
    }

    if ($Accion -eq 'BackupEliminar') {
        Add-Type -AssemblyName System.Windows.Forms
        $dialogo = New-Object System.Windows.Forms.FolderBrowserDialog
        $dialogo.Description = 'Seleccione donde guardar el respaldo de PadelGest (.bak). SQL Server debe tener permiso de escritura en esa carpeta.'
        $dialogo.ShowNewFolderButton = $true
        if ($dialogo.ShowDialog() -ne [System.Windows.Forms.DialogResult]::OK) {
            Write-Output 'El usuario cancelo la eleccion de carpeta. No se elimino la base.'
            exit 2
        }
        $archivo = Join-Path $dialogo.SelectedPath ('PadelGestDB_Backup_' + (Get-Date -Format 'yyyyMMdd_HHmmss') + '.bak')
        $escapado = $archivo.Replace("'", "''")
        Ejecutar-Sql "BACKUP DATABASE [$base] TO DISK = N'$escapado' WITH INIT, CHECKSUM;" | Out-Null
        Ejecutar-Sql "RESTORE VERIFYONLY FROM DISK = N'$escapado' WITH CHECKSUM;" | Out-Null
        if (-not (Test-Path -LiteralPath $archivo -PathType Leaf)) {
            throw 'SQL Server indico que el backup termino, pero el archivo no se encontro en la carpeta elegida. No se elimino la base.'
        }
        Write-Output "Respaldo realizado y verificado: $archivo"
    }

    # Nombre de base fijo; nunca se reciben identificadores SQL del usuario.
    Ejecutar-Sql "USE [master]; ALTER DATABASE [$base] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$base];" | Out-Null
    if ((Ejecutar-Sql "SET NOCOUNT ON; SELECT CASE WHEN DB_ID(N'$base') IS NULL THEN 0 ELSE 1 END;").Trim() -ne '0') {
        throw 'No se pudo confirmar la eliminacion de la base.'
    }
    Write-Output 'Base de datos eliminada.'
    exit 0
}
catch {
    Add-Type -AssemblyName System.Windows.Forms
    [System.Windows.Forms.MessageBox]::Show(
        $_.Exception.Message + [Environment]::NewLine + [Environment]::NewLine +
        'Si solicito un respaldo, NO se borro la base cuando el respaldo fallo.',
        'PadelGest - Operacion SQL', 'OK', 'Error'
    ) | Out-Null
    Write-Error $_
    exit 1
}
