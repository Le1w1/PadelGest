# PadelGest - operaciones SQL para Inno Setup.
# Se utiliza autenticacion integrada de Windows; no guarda contrasenas.
# SQL Server y sqlcmd deben existir previamente. Exclusivamente PadelGestDB.
param(
    [Parameter(Mandatory=$true)][ValidateSet('Consultar','Instalar','Eliminar','BackupEliminar')][string]$Accion,
    [Parameter(Mandatory=$true)][ValidateSet('.','localhost\SQLEXPRESS')][string]$Servidor,
    [string]$ScriptSQL = ''
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

    if ($Accion -eq 'Instalar') {
        if ($existe) {
            Write-Output 'La base existente se conserva; no se ejecuta el script de instalacion.'
            exit 0
        }
        if (-not (Test-Path -LiteralPath $ScriptSQL -PathType Leaf)) {
            throw 'No se encontro el script de instalacion limpia.'
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
        Write-Output 'Base de datos creada y verificada.'
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
