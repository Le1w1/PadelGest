; PadelGest - instalador/reinstalador basado en CineGest.
; Requiere Windows x64, SQL Server 2019+ y sqlcmd (Windows Authentication).
; El script SQL se aplica SOLO si PadelGestDB no existe.
#define AppName "PadelGest"
#define AppVersion "1.0"
#define AppExe "UI.exe"

[Setup]
AppId={{A1F3C0E2-8D4C-4E5A-9B7D-504144454C47}
AppName={#AppName}
AppVersion={#AppVersion}
DefaultDirName={autopf}\{#AppName}
DefaultGroupName={#AppName}
DisableProgramGroupPage=yes
OutputDir=Output
OutputBaseFilename=PadelGest_Setup_{#AppVersion}
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
UninstallDisplayIcon={app}\{#AppExe}
CloseApplications=yes

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "Crear acceso directo en el escritorio"; GroupDescription: "Accesos directos:"

[Files]
; Primero ejecutar: dotnet publish UI/UI.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o Instalador/publish
Source: "publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "OperacionesSQL.ps1"; DestDir: "{app}"; Flags: ignoreversion
Source: "OperacionesSQL.ps1"; Flags: dontcopy
Source: "..\Script\PadelGest_Instalacion_Limpia_SQL2019.sql"; Flags: dontcopy

[Icons]
Name: "{group}\PadelGest"; Filename: "{app}\{#AppExe}"
Name: "{group}\Desinstalar PadelGest"; Filename: "{uninstallexe}"
Name: "{autodesktop}\PadelGest"; Filename: "{app}\{#AppExe}"; Tasks: desktopicon

[Registry]
Root: HKLM; Subkey: "SYSTEM\CurrentControlSet\Control\Session Manager\Environment"; ValueType: string; ValueName: "PADELGEST_ENTORNO"; ValueData: "{code:EntornoSeleccionado}"; Flags: uninsdeletevalue

[Run]
Filename: "{app}\{#AppExe}"; Description: "Iniciar PadelGest"; BeforeInstall: ConfigurarEntornoProceso; Flags: nowait postinstall skipifsilent

[Code]
function SetEnvironmentVariableW(Nombre: String; Valor: String): Boolean;
  external 'SetEnvironmentVariableW@kernel32.dll stdcall';

var
  PaginaServidor: TInputOptionWizardPage;
  EleccionEliminar: Integer;
  VentanaEleccion: TSetupForm;

function EntornoSeleccionado(Parametro: String): String;
begin
  if PaginaServidor.SelectedValueIndex = 0 then
    Result := 'LEO'
  else
    Result := 'UAI';
end;

procedure ConfigurarEntornoProceso();
begin
  // El exe iniciado desde Setup hereda su entorno, que puede no reflejar
  // todavia el valor recien escrito en el registro.
  SetEnvironmentVariableW('PADELGEST_ENTORNO', EntornoSeleccionado(''));
end;

function ServidorSeleccionado: String;
begin
  if PaginaServidor.SelectedValueIndex = 0 then
    Result := 'localhost\SQLEXPRESS'
  else
    Result := '.';
end;

function EjecutarHelper(Accion, Servidor, RutaSQL: String): Integer;
var
  Codigo: Integer;
  Parametros: String;
begin
  Parametros := '-NoProfile -STA -ExecutionPolicy Bypass -File "' +
    ExpandConstant('{tmp}\OperacionesSQL.ps1') +
    '" -Accion "' + Accion + '" -Servidor "' + Servidor + '"';
  if RutaSQL <> '' then
    Parametros := Parametros + ' -ScriptSQL "' + RutaSQL + '"';
  if Exec(ExpandConstant('{sys}\WindowsPowerShell\v1.0\powershell.exe'),
          Parametros, '', SW_HIDE, ewWaitUntilTerminated, Codigo) then
    Result := Codigo
  else
    Result := -1;
end;

function EjecutarHelperDesinstalacion(Accion, Servidor: String): Integer;
var
  Codigo: Integer;
begin
  if Exec(ExpandConstant('{sys}\WindowsPowerShell\v1.0\powershell.exe'),
    '-NoProfile -STA -ExecutionPolicy Bypass -File "' +
    ExpandConstant('{app}\OperacionesSQL.ps1') + '" -Accion "' +
    Accion + '" -Servidor "' + Servidor + '"',
    '', SW_HIDE, ewWaitUntilTerminated, Codigo) then
    Result := Codigo
  else
    Result := -1;
end;

procedure InitializeWizard();
var
  EntornoGuardado: String;
begin
  PaginaServidor := CreateInputOptionPage(wpSelectDir,
    'Conexion con SQL Server', 'Selecciona donde se guardaran los datos',
    'El sistema utilizara la cuenta de Windows para conectarse. ' +
    'SQL Server y sqlcmd deben estar instalados en este equipo.', True, False);
  PaginaServidor.Add('SQL Server Express (localhost\SQLEXPRESS)');
  PaginaServidor.Add('SQL Server predeterminado (.)');
  PaginaServidor.SelectedValueIndex := 0;
  if RegQueryStringValue(HKLM,
    'SYSTEM\CurrentControlSet\Control\Session Manager\Environment',
    'PADELGEST_ENTORNO', EntornoGuardado) then
    if EntornoGuardado = 'UAI' then
      PaginaServidor.SelectedValueIndex := 1;
end;

// Antes de comenzar, informar si es instalacion nueva o reinstalacion.
// Esta consulta solo lee SQL Server; la decision no modifica registros.
function NextButtonClick(CurPageID: Integer): Boolean;
var
  EstadoBase: Integer;
begin
  Result := True;
  if CurPageID <> wpReady then
    Exit;

  ExtractTemporaryFile('OperacionesSQL.ps1');
  EstadoBase := EjecutarHelper('Consultar', ServidorSeleccionado, '');

  if EstadoBase = 10 then
  begin
    Result := MsgBox(
      'Se detecto una base de datos PadelGestDB existente en la instancia ' +
      ServidorSeleccionado + '.' + Chr(13) + Chr(10) + Chr(13) + Chr(10) +
      'REINSTALACION DE PADELGEST' + Chr(13) + Chr(10) +
      'Se actualizaran los archivos del programa.' + Chr(13) + Chr(10) +
      'Se conservaran clientes, usuarios, reservas, ventas, productos, ' +
      'historiales y demas datos de la base.' + Chr(13) + Chr(10) + Chr(13) + Chr(10) +
      '¿Deseas continuar con la reinstalacion?',
      mbConfirmation, MB_YESNO or MB_DEFBUTTON2) = IDYES;
  end
  else if EstadoBase = 11 then
  begin
    Result := MsgBox(
      'No se encontro PadelGestDB en la instancia ' +
      ServidorSeleccionado + '.' + Chr(13) + Chr(10) + Chr(13) + Chr(10) +
      'INSTALACION INICIAL' + Chr(13) + Chr(10) +
      'Se instalaran los archivos del programa y se creara una ' +
      'base de datos nueva con la configuracion inicial.' + Chr(13) + Chr(10) + Chr(13) + Chr(10) +
      '¿Deseas continuar?',
      mbConfirmation, MB_YESNO or MB_DEFBUTTON2) = IDYES;
  end
  else
  begin
    MsgBox(
      'No se pudo verificar si PadelGestDB existe.' + Chr(13) + Chr(10) +
      'Comprueba la instancia seleccionada, SQL Server, sqlcmd y los permisos.' +
      Chr(13) + Chr(10) +
      'La instalacion no continuara hasta resolver la conexion.',
      mbError, MB_OK);
    Result := False;
  end;
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
begin
  Result := '';
  ExtractTemporaryFile('OperacionesSQL.ps1');
  ExtractTemporaryFile('PadelGest_Instalacion_Limpia_SQL2019.sql');
  if EjecutarHelper('Instalar', ServidorSeleccionado,
       ExpandConstant('{tmp}\PadelGest_Instalacion_Limpia_SQL2019.sql')) <> 0 then
  begin
    Result := 'No se pudo preparar la base de datos. Verifica que SQL Server ' +
      'este iniciado, que sqlcmd este instalado y que tu usuario de Windows ' +
      'tenga permisos. La instalacion se detuvo para evitar un estado incompleto.';
  end;
end;

// ModalResult pertenece a TNewButton, no a TSetupForm.
// ShowModal devuelve el ModalResult del boton elegido.
procedure AgregarBoton(Caption: String; X, Ancho, ResultadoModal: Integer);
var
  Boton: TNewButton;
begin
  Boton := TNewButton.Create(VentanaEleccion);
  Boton.Parent := VentanaEleccion;
  Boton.Caption := Caption;
  Boton.Left := ScaleX(X);
  Boton.Top := ScaleY(116);
  Boton.Width := ScaleX(Ancho);
  Boton.Height := ScaleY(29);
  Boton.ModalResult := ResultadoModal;
end;


function ObtenerServidorDesinstalacion(var Servidor: String): Boolean;
var
  Entorno: String;
begin
  Result := False;
  if not RegQueryStringValue(HKLM,
    'SYSTEM\CurrentControlSet\Control\Session Manager\Environment',
    'PADELGEST_ENTORNO', Entorno) then
  begin
    MsgBox('No se encontro la configuracion de SQL Server. ' +
      'No se eliminara la base de datos.', mbError, MB_OK);
    Exit;
  end;

  if Entorno = 'LEO' then
    Servidor := 'localhost\SQLEXPRESS'
  else if Entorno = 'UAI' then
    Servidor := '.'
  else
  begin
    MsgBox('Instancia SQL desconocida. No se eliminara la base de datos.',
      mbError, MB_OK);
    Exit;
  end;

  Result := True;
end;

// Todas las operaciones SQL se ejecutan ANTES de iniciar la desinstalacion.
// Si el backup falla, ni el programa ni la base se eliminan.
// El usuario puede regresar al menu y elegir otra opcion.
function InitializeUninstall(): Boolean;
var
  Texto: TNewStaticText;
  Servidor, Accion: String;
  Codigo, Respuesta: Integer;
begin
  Result := False;
  VentanaEleccion := CreateCustomForm(ScaleX(560), ScaleY(165), False, False);
  try
    VentanaEleccion.Caption := 'Desinstalar PadelGest';
    Texto := TNewStaticText.Create(VentanaEleccion);
    Texto.Parent := VentanaEleccion;
    Texto.Left := ScaleX(16);
    Texto.Top := ScaleY(14);
    Texto.Width := ScaleX(528);
    Texto.Height := ScaleY(85);
    Texto.AutoSize := False;
    Texto.WordWrap := True;
    Texto.Caption :=
      '¿Que deseas eliminar?' + Chr(13) + Chr(10) + Chr(13) + Chr(10) +
      'Solo el programa: desinstala PadelGest y conserva la base de datos.' + Chr(13) + Chr(10) +
      'Eliminar todo: tambien borra clientes, reservas, ventas y usuarios.' + Chr(13) + Chr(10) +
      'Backup y eliminar: guarda y verifica un archivo .bak antes de borrar.';

    AgregarBoton('Solo el programa', 12, 150, mrOk);
    AgregarBoton('Eliminar todo', 174, 143, mrYes);
    AgregarBoton('Backup y eliminar todo', 329, 219, mrNo);

    // Ante cualquier error se permanece en el menu, nunca en la pantalla
    // de progreso de desinstalacion.
    while True do
    begin
      case VentanaEleccion.ShowModal() of
        mrOk:
          begin
            // Desinstalar solo archivos del programa; no tocar SQL Server.
            Result := True;
            Exit;
          end;
        mrYes: Accion := 'Eliminar';
        mrNo: Accion := 'BackupEliminar';
      else
        // La X de la ventana cancela toda la desinstalacion.
        Exit;
      end;

      Respuesta := MsgBox(
        '¿Estas seguro de que deseas eliminar tambien la base de datos PadelGestDB?' +
        Chr(13) + Chr(10) +
        'Esta accion no se puede deshacer sin un respaldo.',
        mbConfirmation, MB_YESNO or MB_DEFBUTTON2);
      if Respuesta <> IDYES then
        Continue;

      if not ObtenerServidorDesinstalacion(Servidor) then
        Continue;

      if Accion = 'BackupEliminar' then
        MsgBox(
          'Selecciona una carpeta para guardar el respaldo .bak.' +
          Chr(13) + Chr(10) +
          'SQL Server debe tener permiso de escritura en esa carpeta.' +
          Chr(13) + Chr(10) +
          'Evita ubicaciones protegidas como la raiz de C:\.' +
          Chr(13) + Chr(10) +
          'Si la copia falla, no se eliminara el programa ni la base.',
          mbInformation, MB_OK);

      Codigo := EjecutarHelperDesinstalacion(Accion, Servidor);
      if Codigo = 0 then
      begin
        // Solo se permite desinstalar los archivos cuando la operacion SQL
        // solicitada termino correctamente.
        Result := True;
        Exit;
      end;

      if Codigo = 2 then
        MsgBox('Se cancelo la copia de seguridad. No se elimino nada.' +
          Chr(13) + Chr(10) + 'Podes elegir otra opcion.',
          mbInformation, MB_OK)
      else
        MsgBox('No se pudo completar la operacion solicitada.' +
          Chr(13) + Chr(10) +
          'Se conservaron el programa y la base de datos, si la operacion ' +
          'de SQL Server no llego a eliminarlos.' + Chr(13) + Chr(10) +
          'Podes elegir otra opcion o cerrar la ventana para cancelar.',
          mbError, MB_OK);
    end;
  finally
    VentanaEleccion.Free();
  end;
end;
