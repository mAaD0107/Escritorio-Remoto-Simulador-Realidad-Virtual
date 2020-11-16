
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization.Formatters.Binary


Public Class Cliente

    '----------------------------------------------------------------------------------------
    '-------------------------------- Control de Mouse --------------------------------------
    '----------------------------------------------------------------------------------------
    ''Declare Function mouse_event Lib "user32.dll" Alias "mouse_event" (flag, x, y, button, extra)
    Private Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    'LEFTDOWN = &H2
    'LEFTUP = &H4
    'RIGHTDOWN = &H8
    'RIGHTUP = &H10
    'MIDDLEUP = &H40
    'MIDDLEDOWN = &H20
    '----------------------------------------------------------------------------------------

    Public Declare Function BlockInput Lib "user32" (ByVal fBlock As Long) As Long
    Private Declare Function ShowCursor Lib "user32" (ByVal lShow As Long) As Long
    <DllImport("user32.dll", EntryPoint:="keybd_event")>
    Public Shared Sub keybd_event(ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As UInteger)
    End Sub
    '----------------------------------------------------------------------------------------
    '-------------------------------- Variables Globales ------------------------------------
    '----------------------------------------------------------------------------------------
    Dim WithEvents socketCliente As New ClaseClienteSocket              ' Crea el socket para conectarse con el servidor
    ' Cambiar IP al terminar
    Dim IPservidor As String = "192.168.0.220"                            ' Especifica la ip del servidor    
    ''Dim IPservidor As String = "127.0.0.1"
    Dim PuertoServidor As Integer = 20000                                ' Especifica el puerto del servidor 

    Dim direccionRaiz As String                                         ' Variable para guardar la ubicacion del juego
    Dim IDapp As Integer                                                ' Variable para guardar el Identicador del proceso
    Dim separadorDirRaiz(), cerrarApp() As String                       ' Variables para ubicar nombres de los procesos 

    Const direccionRaizSteam = "C:\Program Files (x86)\Steam\steamapps\common\SteamVR\bin\win64\vrmonitor.exe"  ' Ubicacion de steamVR
    '----------------------------------------------------------------------------------------
    Dim conectado As Boolean = False

    Dim pantalla As System.Windows.Forms.Screen = System.Windows.Forms.Screen.PrimaryScreen

    '----------------------------------------------------------------------------------------
    '-------------------------------- Variables para Streaming ------------------------------
    '----------------------------------------------------------------------------------------

    Dim cliente As New TcpClient
    Dim ns As NetworkStream
    Dim puertoStreaming As Integer = 50000  '' cambiar por cada PC
    Dim esperaPausaJ1 As Integer = 45000
    Dim esperaPausaJ2 As Integer = 54750
    Dim esperaPausaJ3 As Integer = 46750

    Dim pausaJuego As Integer = 0


    '----------------------------------------------------------------------------------------
    '----------------------------- Evento al cargar ventana ---------------------------------
    '----------------------------------------------------------------------------------------

    Private Sub Cliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Try                                                         ' Intenta conectarse al servidor
            socketCliente.IP = IPservidor                           ' Especifica la IP y el Puerto
            socketCliente.Puerto = PuertoServidor

            socketCliente.Conectar()                                ' Conecta con el servidor


            ToolStripStatusLabel1.Text = "Conectado al Servidor " & IPservidor & ":" & PuertoServidor   ' Especifica que se ha conectado
            ToolStripProgressBar1.Value = 100

            Button1.Enabled = True                                  ' Habilita el boton para el envio de mensajes 
            conectado = True

        Catch ex As Exception

            MsgBox("Error al conectar al servidor " & IPservidor &  ' Ventana mensaje especificando que no se ha conectado 
                      vbCrLf & vbCrLf & ex.Message,
                      MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

            Button1.Enabled = False                                 ' Deshabilita el boton enviar

        End Try

        Listar()                                                    ' Carga una lista de todos los procesos que se enstan ejecutando 


        Try

            socketCliente.EnviarDatos("Listo!")                          ' Enviar mensaje al servidor
        Catch err As Exception
            MsgBox("Error al enviar mensaje al servidor." &                   ' Si hay un error lo pone en un cuadro de texto 
                   vbCrLf & vbCrLf & err.Message)
        End Try


        Try
            cliente.Connect(IPservidor, puertoStreaming)

        Catch ex As Exception

        End Try

        Timer2.Start()

    End Sub
    '----------------------------------------------------------------------------------------




    '----------------------------------------------------------------------------------------
    '----------------------------- Evento  Enviar Mensaje -----------------------------------
    '----------------------------------------------------------------------------------------
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try

            socketCliente.EnviarDatos(TextBox2.Text)                          ' Enviar mensaje al servidor
        Catch err As Exception
            MsgBox("Error al enviar mensaje al servidor." &                   ' Si hay un error lo pone en un cuadro de texto 
                   vbCrLf & vbCrLf & err.Message)
        End Try
    End Sub


    '----------------------------------------------------------------------------------------
    '--------------------------- Evento Recepcion de Datos ----------------------------------
    '----------------------------------------------------------------------------------------
    Private Sub socketCliente_DatosRecibidos(
            ByVal datos As String) Handles socketCliente.DatosRecibidos

        CheckForIllegalCrossThreadCalls = False                         ' Deshabilita la deteccion de colision de datos

        Dim vecese As Integer = 0

        TextBox1.Text = TextBox1.Text + Now + " Servidor: " & datos     ' Especifica el mensaje que ha llegado 
        TextBox1.Text = TextBox1.Text & vbCrLf

        TextBox1.Select(TextBox1.Text.Length, 0)                        ' Ubica al textBox en la ultima linea 
        TextBox1.ScrollToCaret()

        '-------------------------------------------
        direccionRaiz += datos                                          ' Concatena los datos que acaban de llegar 
        '-------------------------------------------


        ' ------------------------------------------------------------------------------------------------------------------------
        If datos.Contains("AbrirSteam") Then
            keybd_event(CByte(Keys.LWin), 0, &H0, 0)
            keybd_event(CByte(Keys.D), 0, &H0, 0)
            keybd_event(CByte(Keys.D), 0, &H2, 0)
            keybd_event(CByte(Keys.LWin), 0, &H2, 0)
            Try
                Shell("C:\Program Files (x86)\Steam\Steam.exe", vbNormalFocus)
                Me.WindowState = FormWindowState.Minimized
                System.Threading.Thread.Sleep(15000)
                ''Cursor.Position = New Point(750, 400)
                Cursor.Position = New Point(pantalla.Bounds.Width.ToString * 56.6 / 100, pantalla.Bounds.Height.ToString * 54.6 / 100)
                mouse_event(&H2, 0, 0, 0, 0)
                mouse_event(&H4, 0, 0, 0, 0)

                socketCliente.EnviarDatos("Steam Abierto")

                ' ------------------------
                direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
                ' ------------------------
            Catch ex As Exception
                MsgBox("Steam no pudo iniciar: " & ex.Message, MsgBoxStyle.Critical)
            End Try



        End If
        ' ------------------------------------------------------------------------------------------------------------------------



        If datos.Contains("iniciarUltimateBooster") Then
            ''Cursor.Position = New Point(162, 323)
            Cursor.Position = New Point(pantalla.Bounds.Width.ToString * 16.6 / 100, pantalla.Bounds.Height.ToString * 44.3 / 100 - 20)
            mouse_event(&H2, 0, 0, 0, 0)
            mouse_event(&H4, 0, 0, 0, 0)

            socketCliente.EnviarDatos(cerrarApp(0) & " Iniciado")
            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If


        If datos.Contains("iniciarDreadEye") Then


            keybd_event(CByte(Keys.Space), 0, &H0, 0)
            keybd_event(CByte(Keys.Space), 0, &H2, 0)

            keybd_event(CByte(Keys.Space), 0, &H0, 0)
            keybd_event(CByte(Keys.Space), 0, &H2, 0)

            keybd_event(CByte(Keys.Space), 0, &H0, 0)
            keybd_event(CByte(Keys.Space), 0, &H2, 0)

            socketCliente.EnviarDatos("DreadEye Iniciado")

            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If



        If datos.Contains("iniciaForestHill") Then


            keybd_event(CByte(Keys.Enter), 0, &H0, 0)
            keybd_event(CByte(Keys.Enter), 0, &H2, 0)
            ''System.Threading.Thread.Sleep(500)
            For veces = 1 To 6
                keybd_event(CByte(Keys.Tab), 0, &H0, 0)
                keybd_event(CByte(Keys.Tab), 0, &H2, 0)
                ''System.Threading.Thread.Sleep(500)
            Next
            keybd_event(CByte(Keys.Down), 0, &H0, 0)
            keybd_event(CByte(Keys.Down), 0, &H2, 0)
            ''System.Threading.Thread.Sleep(500)

            keybd_event(CByte(Keys.Enter), 0, &H0, 0)
            keybd_event(CByte(Keys.Enter), 0, &H2, 0)


            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------


        End If


        If datos.Contains("iniciaHybris") Then


            keybd_event(CByte(Keys.Enter), 0, &H0, 0)
            keybd_event(CByte(Keys.Enter), 0, &H2, 0)
            ''System.Threading.Thread.Sleep(500)
            For veces = 1 To 6
                keybd_event(CByte(Keys.Tab), 0, &H0, 0)
                keybd_event(CByte(Keys.Tab), 0, &H2, 0)
                ''System.Threading.Thread.Sleep(500)
            Next
            keybd_event(CByte(Keys.Down), 0, &H0, 0)
            keybd_event(CByte(Keys.Down), 0, &H2, 0)
            keybd_event(CByte(Keys.Down), 0, &H0, 0)
            keybd_event(CByte(Keys.Down), 0, &H2, 0)
            ''System.Threading.Thread.Sleep(500)

            keybd_event(CByte(Keys.Enter), 0, &H0, 0)
            keybd_event(CByte(Keys.Enter), 0, &H2, 0)
            ''System.Threading.Thread.Sleep(500)

            ''System.Threading.Thread.Sleep(esperaPausaJ2)                          ' Cambiar por cada PC
            ''keybd_event(CByte(Keys.P), 0, &H0, 0)
            ''keybd_event(CByte(Keys.P), 0, &H2, 0)

            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If

        If datos.Contains("iniciaWillderness") Then


            keybd_event(CByte(Keys.Enter), 0, &H0, 0)
            keybd_event(CByte(Keys.Enter), 0, &H2, 0)
            ''System.Threading.Thread.Sleep(500)
            For veces = 1 To 6
                keybd_event(CByte(Keys.Tab), 0, &H0, 0)
                keybd_event(CByte(Keys.Tab), 0, &H2, 0)
                ''System.Threading.Thread.Sleep(500)
            Next
            keybd_event(CByte(Keys.Down), 0, &H0, 0)
            keybd_event(CByte(Keys.Down), 0, &H2, 0)
            keybd_event(CByte(Keys.Down), 0, &H0, 0)
            keybd_event(CByte(Keys.Down), 0, &H2, 0)
            keybd_event(CByte(Keys.Down), 0, &H0, 0)
            keybd_event(CByte(Keys.Down), 0, &H2, 0)
            keybd_event(CByte(Keys.Down), 0, &H0, 0)
            keybd_event(CByte(Keys.Down), 0, &H2, 0)
            ''System.Threading.Thread.Sleep(500)

            keybd_event(CByte(Keys.Enter), 0, &H0, 0)
            keybd_event(CByte(Keys.Enter), 0, &H2, 0)
            ''System.Threading.Thread.Sleep(500)

            '' System.Threading.Thread.Sleep(esperaPausaJ3)                          ' Cambiar por cada PC
            ''keybd_event(CByte(Keys.P), 0, &H0, 0)
            ''keybd_event(CByte(Keys.P), 0, &H2, 0)

            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If


        If datos.Contains("restaurar") Then                             ' Restaurar imagen 
            ''AppActivate("NoLimits 2 Demo")
            AppActivate("Forest Hills Park.nl2park (ReadOnly) - NoLimits 2 Demo")
            '' keybd_event(CByte(Keys.P), 0, &H0, 0)
            '' keybd_event(CByte(Keys.P), 0, &H2, 0)


            keybd_event(CByte(Keys.R), 0, &H0, 0)
            keybd_event(CByte(Keys.R), 0, &H2, 0)
            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If


        If datos.Contains("GO") Then                             ' Restaurar imagen 
            AppActivate("Forest Hills Park.nl2park (ReadOnly) - NoLimits 2 Demo")
            ''keybd_event(CByte(Keys.R), 0, &H0, 0)
            ''keybd_event(CByte(Keys.R), 0, &H2, 0)

            ''keybd_event(CByte(Keys.P), 0, &H0, 0)
            ''keybd_event(CByte(Keys.P), 0, &H2, 0)


            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If



        ' ------------------------------------------------------------------------------------------------------------------------
        If datos.Contains("cerrarUltimateBooster") Then                                ' Si los datos contienen la palabra cerrar 



            Dim p As Process                                            ' Genera una variable de proceso 



            For Each p In Process.GetProcesses()                        ' Genera un for con todos los procesos abiertos 
                If Not p Is Nothing Then                                ' Si no hay proceos sale del bucle
                    If p.ProcessName.ToString = cerrarApp(0) Then       ' Si el nombre del proceso tiene el mismo nombre que el proceso previo (abierto)
                            Try
                                p.Kill()                                    ' Cierra proceso   
                                Listar()                                    ' Actualiza la lista de procesos  
                                socketCliente.EnviarDatos(cerrarApp(0) & " Cerrada")
                                Exit For                                    ' Sale del bucle
                            Catch msg As Exception
                                MsgBox(msg.Message.ToString, MsgBoxStyle.Critical)  ' Genera una ventana de mensaje especificando el error por el que no pudo cerrar el proceso 
                            Exit Sub
                        End Try
                    End If
                End If
            Next

            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If
        ' ------------------------------------------------------------------------------------------------------------------------



        If datos.Contains("cerrarDreadEye") Then


            Dim p As Process                                            ' Genera una variable de proceso 



            For Each p In Process.GetProcesses()                        ' Genera un for con todos los procesos abiertos 
                If Not p Is Nothing Then                                ' Si no hay proceos sale del bucle
                    If p.ProcessName.ToString = "DreadoutOculus-Win64-Shipping" Then       ' Si el nombre del proceso tiene el mismo nombre que el proceso previo (abierto)
                        Try
                            p.Kill()                                    ' Cierra proceso   
                            Listar()                                    ' Actualiza la lista de procesos  
                            socketCliente.EnviarDatos(cerrarApp(0) & " Cerrada")
                            Exit For                                    ' Sale del bucle
                        Catch msg As Exception
                            MsgBox(msg.Message.ToString, MsgBoxStyle.Critical)  ' Genera una ventana de mensaje especificando el error por el que no pudo cerrar el proceso 
                            Exit Sub
                        End Try
                    End If
                End If
            Next





            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If




        ' ------------------------------------------------------------------------------------------------------------------------
        If datos.Contains("cerrarNoLimits") Then                                ' Si los datos contienen la palabra cerrar 



            Dim p As Process                                            ' Genera una variable de proceso 



            For Each p In Process.GetProcesses()                        ' Genera un for con todos los procesos abiertos 
                If Not p Is Nothing Then                                ' Si no hay proceos sale del bucle
                    If p.ProcessName.ToString = "nolimits2app" Then       ' Si el nombre del proceso tiene el mismo nombre que el proceso previo (abierto)
                        Try
                            p.Kill()                                    ' Cierra proceso   
                            Listar()                                    ' Actualiza la lista de procesos  
                            socketCliente.EnviarDatos(cerrarApp(0) & " Cerrada")
                            Exit For                                    ' Sale del bucle
                        Catch msg As Exception
                            MsgBox(msg.Message.ToString, MsgBoxStyle.Critical)  ' Genera una ventana de mensaje especificando el error por el que no pudo cerrar el proceso 
                            Exit Sub
                        End Try
                    End If
                End If
            Next

            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If
        ' ------------------------------------------------------------------------------------------------------------------------





        If datos.Contains("ApagarPC") Then

            socketCliente.EnviarDatos("Apagando")

            Shell("cmd.exe /k" & "shutdown -s -t 5")

            ' ------------------------
            direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
            ' ------------------------
        End If


        ' ------------------------------------------------------------------------------------------------------------------------
        If direccionRaiz.Contains(".exe") Then                          ' Si la direccion esta completa 




            Try
                IDapp = Shell(direccionRaizSteam, vbMinimizedFocus)      ' Abre SteamVR (Para que las gafas no tengan problemas) 
                keybd_event(CByte(Keys.LWin), 0, &H0, 0)                 ' Minimiza todas las ventanas 
                keybd_event(CByte(Keys.D), 0, &H0, 0)
                keybd_event(CByte(Keys.D), 0, &H2, 0)
                keybd_event(CByte(Keys.LWin), 0, &H2, 0)
                System.Threading.Thread.Sleep(8000)                     ' Genera un tiempo muerto hasta que el progama se ejecute y cargue sus procesos internos
                Shell(direccionRaiz, vbMaximizedFocus)                   ' Abre el juego que se especifica en la direccion raiz 

                If direccionRaiz.Contains("nolimits2app") Then          '' Sie el juego es montaña rusa necesita un enter para iniciar 
                    System.Threading.Thread.Sleep(5000)
                    keybd_event(CByte(Keys.Enter), 0, &H0, 0)
                    keybd_event(CByte(Keys.Enter), 0, &H2, 0)
                End If


                Label1.Text = IDapp.ToString                            ' Borrar al terminar 

                separadorDirRaiz = Split(direccionRaiz, "\")            ' Separa las rutas y cuenta el numero de carpetas
                Label3.Text = separadorDirRaiz.Length.ToString          ' Borrar al terminar 


                cerrarApp = Split(separadorDirRaiz(separadorDirRaiz.Length - 1), ".exe")    'Sepra el nombre del juego del .exe  
                socketCliente.EnviarDatos(cerrarApp(0) & " Abierto")                          ' Enviar mensaje al servidor
                Label2.Text = cerrarApp(0)                              ' Borrar al terminar 
                ' ------------------------
                direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
                ' ------------------------

            Catch ex As Exception
                MsgBox("El juego no fue encontrado, verifique que el juego se encuentre en la direccion raiz: " + direccionRaiz, MsgBoxStyle.Critical, "ALERTA") ' Ventana de mensaje Juego no encontrado
            End Try



        End If
        ' ------------------------------------------------------------------------------------------------------------------------


        If direccionRaiz.Contains(".lnk") Then

            Try
                Process.Start(direccionRaiz, vbMaximizedFocus)      ' Abre el juego
                keybd_event(CByte(Keys.LWin), 0, &H0, 0)                 ' Minimiza todas las ventanas 
                keybd_event(CByte(Keys.D), 0, &H0, 0)
                keybd_event(CByte(Keys.D), 0, &H2, 0)
                keybd_event(CByte(Keys.LWin), 0, &H2, 0)
                System.Threading.Thread.Sleep(1000)                     ' Genera un tiempo muerto hasta que el progama se ejecute y cargue sus procesos internos
                Process.Start("C:\Program Files (x86)\Steam\steamapps\common\Accesos Directos\Portal de realidad mixta.lnk")

                ' Abre el juego que se especifica en la direccion raiz 



                Label1.Text = IDapp.ToString                            ' Borrar al terminar 

                separadorDirRaiz = Split(direccionRaiz, "\")            ' Separa las rutas y cuenta el numero de carpetas
                Label3.Text = separadorDirRaiz.Length.ToString          ' Borrar al terminar 


                cerrarApp = Split(separadorDirRaiz(separadorDirRaiz.Length - 1), ".lnk")    'Sepra el nombre del juego del .exe  
                socketCliente.EnviarDatos(cerrarApp(0) & " Abierto")                          ' Enviar mensaje al servidor
                Label2.Text = cerrarApp(0)                              ' Borrar al terminar 
                ' ------------------------
                direccionRaiz = ""                                          ' Encera la variable de la ubicacion raiz
                ' ------------------------
            Catch ex As Exception

            End Try

        End If
    End Sub
    '----------------------------------------------------------------------------------------


    '----------------------------------------------------------------------------------------
    '--------------------------- Evento Servidor Desconectado -------------------------------
    '----------------------------------------------------------------------------------------
    Private Sub socketCliente_ConexionTerminada() Handles _
            socketCliente.ConexionTerminada

        CheckForIllegalCrossThreadCalls = False                         ' Deshabilita la deteccion de colision de datos 
        ToolStripStatusLabel1.Text = "Servidor Desconectado"            ' Especifica que el servidor se ha desconectado en la barra Status 
        ToolStripProgressBar1.Value = 0                                 ' Barra de progreso = 0


        conectado = False
        Label1.Text = "intentando conectar"

    End Sub
    '----------------------------------------------------------------------------------------




    '----------------------------------------------------------------------------------------
    '----------------------------- Evento Cerrar Formulario ---------------------------------
    '----------------------------------------------------------------------------------------
    Private Sub Cliente_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        Try
            socketCliente.Desconectar()                                 ' Se desconecta del servidor 
        Catch err As Exception
            MsgBox("Error al desconectar del servidor " & IPservidor &  ' Si ha habido un error al desconertarse del servidor lo muestra en un MsgBox
                 vbCrLf & vbCrLf & err.Message,
                 MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
        End Try
        End                                                             ' Finaliza y cierra la aplicacion 
    End Sub
    '----------------------------------------------------------------------------------------



    '----------------------------------------------------------------------------------------
    '--------------------------------- Funcion Listar() -------------------------------------
    '----------------------------------------------------------------------------------------
    Sub Listar()

        ListBox1.Items.Clear()                                          ' Borra los elementos que se encuentran en el ListBox
        Dim p As Process                                                ' Genera una variable de proceso 
        For Each p In Process.GetProcesses()                            ' Genera un For para cada proceso abierto 
            If Not p Is Nothing Then                                    ' Si no hay procesos sale del bucle
                ListBox1.Items.Add(p.ProcessName)                       ' Carga el ListBox con el nombre de cada proceso hallado 
                ListBox2.Items.Add(p.Id)                                ' Carga el ListBox2 con el ID de los procesos 
            End If
        Next
        Me.Text = "cantidad de procesos : " & CStr(ListBox1.Items.Count + 1)    ' Nombra la ventana o Form con el numero de procesos 
    End Sub
    '----------------------------------------------------------------------------------------

    '----------------------------------------------------------------------------------------
    '-------------------------------- Evento Boton Listar -----------------------------------
    '----------------------------------------------------------------------------------------
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call Listar()                                                   ' Llama a la funcion listar                
    End Sub
    '----------------------------------------------------------------------------------------


    '----------------------------------------------------------------------------------------
    '-------------------------------- Borrar al Terminar  -----------------------------------
    '----------------------------------------------------------------------------------------
    Private Sub ListBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseClick
        Label1.Text = ListBox1.SelectedItem.ToString        ' Borrar al terminar
        Label2.Text = ListBox1.SelectedIndex.ToString       ' Borrar al terminar
    End Sub



    Private Sub Cliente_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        Label3.Text = Cursor.Position.X.ToString
        Label2.Text = Cursor.Position.Y.ToString
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        EnviarEscritorio()
    End Sub

    Private Sub EnviarEscritorio()
        Dim bf As New BinaryFormatter
        If cliente.Connected = True Then
            Try
                ns = cliente.GetStream
                bf.Serialize(ns, Escritorio())
            Catch ex As Exception

            End Try
        End If
    End Sub


    Public Function Escritorio() As Image
        Dim dimensiones As Rectangle = Nothing
        Dim screenshot As System.Drawing.Bitmap = Nothing
        Dim graph As Graphics = Nothing
        dimensiones = Screen.PrimaryScreen.Bounds
        screenshot = New Bitmap(dimensiones.Width, dimensiones.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555)
        graph = Graphics.FromImage(screenshot)
        graph.CopyFromScreen(dimensiones.X, dimensiones.Y, 0, 0, dimensiones.Size, CopyPixelOperation.SourceCopy)
        Return screenshot
    End Function


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If conectado = False Then
            Try
                socketCliente.IP = IPservidor                           ' Especifica la IP y el Puerto
                socketCliente.Puerto = PuertoServidor

                socketCliente.Conectar()
                socketCliente.EnviarDatos("Listo!")

                ToolStripStatusLabel1.Text = "Conectado al Servidor " & IPservidor & ":" & PuertoServidor   ' Especifica que se ha conectado
                ToolStripProgressBar1.Value = 100

                conectado = True
            Catch ex As Exception

            End Try
        End If

        If cliente.Connected = False Then

            Try
                cliente.Connect(IPservidor, puertoStreaming)

            Catch ex As Exception

            End Try
        End If
    End Sub















    '----------------------------------------------------------------------------------------


    '----------------------------------------------------------------------------------------
    '-------------------------------- Borrar al Terminar  -----------------------------------
    '----------------------------------------------------------------------------------------
    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        Dim texto As String = "hola\miguel\como\estas.exe"                          ' Pruebas
        Dim salida() As String                                                      ' Pruebas

        cerrarApp = Split(texto, "\")                                               ' Pruebas
        Label3.Text = cerrarApp.Length.ToString                                     ' Pruebas


        salida = Split(cerrarApp(cerrarApp.Length - 1).ToString, ".exe")            ' Pruebas
        Label2.Text = salida(0)                                                     ' Pruebas
    End Sub
    '----------------------------------------------------------------------------------------
End Class
