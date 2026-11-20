; PadelGest - instalador/reinstalador basado en CineGest.
; Requiere Windows x64, SQL Server 2019+ y sqlcmd (Windows Authentication).
; Instalar crea una BD nueva (con reemplazo confirmado); Reinstalar conserva la BD.
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
PrivilegesRequired=lowest
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
UninstallDisplayIcon={app}\{#AppExe}
CloseApplications=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
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
; El instalador se ejecuta sin elevacion: la configuracion es por usuario.
Root: HKCU; Subkey: "Environment"; ValueType: string; ValueName: "PADELGEST_ENTORNO"; ValueData: "{code:EntornoSeleccionado}"; Flags: uninsdeletevalue

[Run]
Filename: "{app}\{#AppExe}"; Description: "Iniciar PadelGest"; BeforeInstall: ConfigurarEntornoProceso; Flags: nowait postinstall skipifsilent

[Code]
function SetEnvironmentVariableW(Nombre: String; Valor: String): Boolean;
  external 'SetEnvironmentVariableW@kernel32.dll stdcall';

var
  PaginaModo: TInputOptionWizardPage;
  PaginaServidor: TInputOptionWizardPage;
  ReemplazoConfirmado: Boolean;
  ServidorConfirmado: String;
  ModoConfirmado: Integer;
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
  if (Accion = 'Instalar') and ReemplazoConfirmado then
    Parametros := Parametros + ' -PermitirReemplazo';
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
  ReemplazoConfirmado := False;
  ServidorConfirmado := '';
  ModoConfirmado := -1;

  // La operacion aparece ANTES de elegir el directorio de instalacion.
  PaginaModo := CreateInputOptionPage(wpWelcome,
    'Tipo de instalacion', 'Selecciona la operacion que deseas realizar',
    'Instalar: instala PadelGest y crea una base nueva. Si ya existe PadelGestDB, ' +
    'podras autorizar su reemplazo tras una advertencia.' + Chr(13) + Chr(10) +
    'Reinstalar: actualiza solo el programa; requiere una base PadelGestDB existente.',
    True, False);
  PaginaModo.Add('Instalar (programa + base de datos nueva)');
  PaginaModo.Add('Reinstalar (solo programa; conservar base de datos)');
  PaginaModo.SelectedValueIndex := 1;

  PaginaServidor := CreateInputOptionPage(PaginaModo.ID,
    'Conexion con SQL Server', 'Selecciona la instancia de la base de datos',
    'La instancia seleccionada debe ser local. SQL Server y sqlcmd deben estar ' +
    'instalados y accesibles con tu cuenta de Windows.', True, False);
  PaginaServidor.Add('SQL Server Express (localhost\\SQLEXPRESS)');
  PaginaServidor.Add('SQL Server predeterminado (.)');
  PaginaServidor.SelectedValueIndex := 0;
  if RegQueryStringValue(HKCU,
    'Environment', 'PADELGEST_ENTORNO', EntornoGuardado) then
    if EntornoGuardado = 'UAI' then
      PaginaServidor.SelectedValueIndex := 1;
end;

function NextButtonClick(CurPageID: Integer): Boolean;
var
  EstadoBase: Integer;
  Servidor: String;
begin
  Result := True;
  if CurPageID <> PaginaServidor.ID then
    Exit;

  // No se modifica ni se elimina ninguna BD en esta comprobacion.
  ReemplazoConfirmado := False;
  ServidorConfirmado := '';
  ModoConfirmado := -1;
  ExtractTemporaryFile('OperacionesSQL.ps1');
  Servidor := ServidorSeleccionado();
  EstadoBase := EjecutarHelper('Consultar', Servidor, '');

  if (EstadoBase <> 10) and (EstadoBase <> 11) then
  begin
    MsgBox('No se pudo comprobar PadelGestDB en ' + Servidor +
      '. Verifica SQL Server, sqlcmd y los permisos de Windows.',
      mbError, MB_OK);
    Result := False;
    Exit;
  end;

  if PaginaModo.SelectedValueIndex = 1 then
  begin
    // La reinstalacion NUNCA crea ni elimina la base.
    if EstadoBase <> 10 then
    begin
      MsgBox('No se puede reinstalar: no existe PadelGestDB en ' + Servidor +
        '. Selecciona Instalar para crear una base nueva.',
        mbError, MB_OK);
      Result := False;
      Exit;
    end;
    if EjecutarHelper('Reinstalar', Servidor, '') <> 0 then
    begin
      MsgBox('La base encontrada no esta accesible o no contiene las tablas ' +
        'principales de PadelGest. No se puede reinstalar.',
        mbError, MB_OK);
      Result := False;
      Exit;
    end;
  end
  else if EstadoBase = 10 then
  begin
    Result := MsgBox(
      'ATENCION: ya existe PadelGestDB en ' + Servidor + '.' + #13#10 + #13#10 +
      'Si continuas con INSTALAR, se eliminara la base actual y se creara ' +
      'una nueva. Se perderan los usuarios, clientes, reservas, productos, ' +
      'ventas e historiales actuales.' + #13#10 +
      'Antes de reemplazarla se generara y verificara un respaldo .bak ' +
      'en la carpeta predeterminada de SQL Server.' + #13#10 + #13#10 +
      '¿Confirmas que deseas REEMPLAZAR la base de datos existente?',
      mbConfirmation, MB_YESNO or MB_DEFBUTTON2) = IDYES;
    if not Result then
      Exit;
    ReemplazoConfirmado := True;
  end;

  // La confirmacion solo es valida para la instancia y modo consultados.
  ServidorConfirmado := Servidor;
  ModoConfirmado := PaginaModo.SelectedValueIndex;
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
var
  EstadoBase, ResultadoSQL: Integer;
begin
  Result := '';
  if (ServidorConfirmado <> ServidorSeleccionado()) or
     (ModoConfirmado <> PaginaModo.SelectedValueIndex) then
  begin
    Result := 'Se modifico la seleccion de instalacion. Vuelve a la pantalla ' +
      'de SQL Server y confirma la operacion nuevamente.';
    Exit;
  end;

  ExtractTemporaryFile('OperacionesSQL.ps1');
  EstadoBase := EjecutarHelper('Consultar', ServidorSeleccionado(), '');
  if (EstadoBase <> 10) and (EstadoBase <> 11) then
  begin
    Result := 'No se pudo verificar el estado de PadelGestDB. No se instalaron archivos.';
    Exit;
  end;

  if PaginaModo.SelectedValueIndex = 1 then
  begin
    if EstadoBase <> 10 then
    begin
      Result := 'La base ya no existe. La reinstalacion solo actualiza el programa.';
      Exit;
    end;
    ResultadoSQL := EjecutarHelper('Reinstalar', ServidorSeleccionado(), '');
  end
  else
  begin
    // Puede haberse creado otra BD mientras se navegaba el asistente.
    // Sin consentimiento explicito, no permitimos reemplazarla.
    if (EstadoBase = 10) and (not ReemplazoConfirmado) then
    begin
      Result := 'Se detecto una base nueva en el servidor. Retrocede y autoriza ' +
        'su reemplazo antes de continuar con Instalar.';
      Exit;
    end;
    ExtractTemporaryFile('PadelGest_Instalacion_Limpia_SQL2019.sql');
    ResultadoSQL := EjecutarHelper('Instalar', ServidorSeleccionado(),
      ExpandConstant('{tmp}\\PadelGest_Instalacion_Limpia_SQL2019.sql'));
  end;

  if ResultadoSQL <> 0 then
    Result := 'No se pudo completar la operacion SQL. No se instalaron archivos ' +
      'nuevos. Revisa el mensaje de error y comprueba SQL Server, sqlcmd, ' +
      'el acceso a PadelGestDB y los permisos de tu usuario.';
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
  if not RegQueryStringValue(HKCU,
    'Environment',
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
