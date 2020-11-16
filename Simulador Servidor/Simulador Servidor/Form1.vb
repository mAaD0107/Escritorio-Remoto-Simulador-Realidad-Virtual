Imports System.Net.Sockets


Public Class Form1

    Dim bandera As Byte = 0                                                 ' Bandera para boton ecendido
    Dim steam As Boolean = False

    ' ----------------------------------------------------------------------------------------------------
    ' ------------------------------ Variables para SOCKETS SERVIDOR -------------------------------------
    ' ----------------------------------------------------------------------------------------------------
    Dim WithEvents socketServidor As New ClaseServidorSocket()              ' Llama a la clase socketServidor
    Dim puerto As Integer = 1010                                            ' Elige el puerto de comunicacion

    ' ----------------------------------------------------------------------------------------------------
    ' ------------------------ Variables para valor de celdas de Base de datos ---------------------------
    ' ----------------------------------------------------------------------------------------------------
    Dim id As Int16 = 0                                                     ' Variable id del juego 
    Dim nombreJuego As String = vbNull                                      ' Variable para nombre del juego (envia al cliente)
    Dim direccionRaiz As String = vbNull                                    ' Variable para ruta del juego (envia al cliente)



    Dim sonido As System.Media.SoundPlayer = New Media.SoundPlayer(My.Resources.Juego)



    ' ----------------------------------------------------------------------------------------------------


    ' ----------------------------------------------------------------------------------------------------
    ' --------------------- Evento para colorear boton de encendido al acercar mouse ---------------------
    ' ----------------------------------------------------------------------------------------------------
    Private Sub PictureBox8_MouseHover(sender As Object, e As EventArgs) Handles PictureBox8.MouseHover
        If bandera = 0 Then
            PictureBox8.Image = My.Resources.boton2
        End If
    End Sub

    Private Sub PictureBox8_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox8.MouseLeave

        If bandera = 0 Then
            PictureBox8.Image = My.Resources.boton4
        End If
    End Sub
    ' ----------------------------------------------------------------------------------------------------



    ' ----------------------------------------------------------------------------------------------------
    ' ---------------------------- Evento para enviar Paquete Magico  ------------------------------------
    ' ----------------------------------------------------------------------------------------------------


    Private Sub PictureBox8_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox8.DoubleClick


        bandera += 1                                                   ' Cambia estado de bande para no volver a entrar


        If bandera = 1 Then                                                     ' Si se ha pulsado el boton de encendido


            PictureBox8.Image = My.Resources.boton2
            PictureBox1.Image = My.Resources.routerEncendido
            PictureBox10.Image = My.Resources.ethernet


            'This expects the the mac address as a string of hex numbers separated by dashes
            'e.g. 4d-3f-2-F-3c-40
            'The "magic packet" consists of six FF's followed by the six bytes of the mac address repeated 16 times
            Dim dataSend((17 * 6) - 1) As Byte
            Dim macAddr(5) As Byte
            Dim s() As String = Split("00-E0-4C-AE-28-3A", "-")

            For i As Integer = 0 To 5
                dataSend(i) = &HFF                      'Six FF's at the front of the data
                macAddr(i) = Convert.ToByte(s(i), 16)   'Cache the six bytes of the mac address
            Next
            For i = 1 To 16                 'follow the six FF's with 16 copies of the mac address
                Array.Copy(macAddr, 0, dataSend, i * 6, 6)
            Next

            Dim myUdpClient As UdpClient

            Try
                myUdpClient = New UdpClient("192.168.0.1", 9)                   ' Crea una coneccion 
                myUdpClient.Send(dataSend, dataSend.Length)                     ' Envia el paquete magico al cliente
                myUdpClient.Close()                                             ' Cierra la coneccion

            Catch ex As Exception
                MsgBox("error fallo sistema ")

            End Try



            PictureBox15.Visible = True
            PictureBox15.Image = My.Resources.blinkSteam


        End If



        If bandera = 2 Then                                                     ' Si se vuelve a presionar el boton de encedio
            ' apaga todo 



            PictureBox8.Image = My.Resources.boton4
            PictureBox1.Image = My.Resources.router5
            PictureBox10.Image = Nothing

            bandera = 0

            CheckForIllegalCrossThreadCalls = False
            socketServidor.enviarMensajeTodosClientes("ApagarPC")

        End If

    End Sub
    ' ----------------------------------------------------------------------------------------------------


    ' ----------------------------------------------------------------------------------------------------
    ' ----------------------------- Evento para cargar gif en vista previa -------------------------------
    ' ----------------------------------------------------------------------------------------------------
    Private Sub PictureBox13_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox13.MouseEnter
        PictureBox13.Image = My.Resources.Anuncio
        sonido.Play()

    End Sub



    Private Sub PictureBox13_MouseLeave_1(sender As Object, e As EventArgs) Handles PictureBox13.MouseLeave
        PictureBox13.Image = My.Resources.vr_746x419
        sonido.Stop()
    End Sub
    ' ----------------------------------------------------------------------------------------------------


    ' --------------------------------------------------------------------------------------------------
    ' ------------------- Funciones para ejecutar comandos de servidor ---------------------------------
    ' --------------------------------------------------------------------------------------------------


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: esta línea de código carga datos en la tabla 'ListaJuegosDataSet.Juegos' Puede moverla o quitarla según sea necesario.
        Me.JuegosTableAdapter.Fill(Me.ListaJuegosDataSet.Juegos)
        Me.WindowState = FormWindowState.Maximized

        Me.Opacity = 1

        Try
            socketServidor.Puerto = puerto                      ' Asigna el puerto de escucha 
            socketServidor.IniciarEscucha()                     ' Iinicia comunicacion 
        Catch err As Exception
            MsgBox("Se ha generado un error al iniciar la comunicacion" & err.Message,      ' Ventana de mensaje en caso de la coneccio n opuedo realizarse 
                    MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)

        End Try


        TableLayoutPanel6.Visible = False
        Panel2.Visible = False
    End Sub


    ' --------------------------------------------------------------------------------------------------
    ' --------------- Evento que se ejecutará cuando un nuevo cliente se conecte -----------------------
    ' --------------------------------------------------------------------------------------------------

    Private Sub socketServidor_NuevaConexion(ByVal IDTerminal As _
                 System.Net.IPEndPoint) Handles socketServidor.NuevaConexion


        CheckForIllegalCrossThreadCalls = False                         ' Habilita la coneccion a varios dispositivos eleiminando el cruce de datos



        TextBox1.Text = TextBox1.Text & "Cliente Conectado: " &         ' Espicifia en el msgbox Servidor la ip del cliente conectado
                        IDTerminal.Address.ToString & vbCrLf

        TextBox1.Select(TextBox1.Text.Length, 0)                        ' Mantiene el focus en la ultima linea del msgbox 
        TextBox1.ScrollToCaret()
    End Sub


    ' --------------------------------------------------------------------------------------------------
    ' ----------------- Evento que se ejecutará cuando finalice la conexión de un cliente --------------
    ' --------------------------------------------------------------------------------------------------
    Private Sub socketServidor_ConexionTerminada(ByVal IDTerminal As _
                 System.Net.IPEndPoint) Handles socketServidor.ConexionTerminada

        CheckForIllegalCrossThreadCalls = False

        TextBox1.Text = TextBox1.Text & "Cliente Desconectado: " &      ' Espicifia en el msgbox Servidor la ip del cliente desconectado 
                        IDTerminal.Address.ToString & vbCrLf

        TextBox1.Select(TextBox1.Text.Length, 0)                         ' Mantiene el focus en la ultima linea del msgbox 
        TextBox1.ScrollToCaret()
    End Sub


    ' --------------------------------------------------------------------------------------------------
    ' ----------- Evento que se ejecutará cuando se reciban datos de los clientes conectados -----------
    ' --------------------------------------------------------------------------------------------------
    Private Sub socketServidor_DatosRecibidos(ByVal IDTerminal As _
               System.Net.IPEndPoint) Handles socketServidor.DatosRecibidos
        'Datos del cliente que se conecta



        CheckForIllegalCrossThreadCalls = False                         ' Habilita la coneccion a varios dispositivos eleiminando el cruce de datos

        TextBox2.Text = TextBox2.Text & "Cliente: (" &                   ' Especifica la direcion del cliente 
                        IDTerminal.Address.ToString & ") " &
                        socketServidor.ObtenerDatos(IDTerminal)


        TextBox2.Text = TextBox2.Text & vbCrLf                          ' Envia un enter al msgBox Cliente y lo ubica en la ultima linea
        TextBox2.Select(TextBox2.Text.Length, 0)

        TextBox2.ScrollToCaret()


    End Sub


    ' --------------------------------------------------------------------------------------------------
    ' ------------------- Enviar mensaje a todos los clientes conectados -------------------------------
    ' --------------------------------------------------------------------------------------------------
    Private Sub BTInicio_Click(sender As Object, e As EventArgs) Handles BTInicio.Click

        Dim mensaje As String = ""                                     ' Crea la variable para enviar el mensaje 

        mensaje = direccionRaiz                                             ' Envia la direccion raiz a todos los clientes 


        CheckForIllegalCrossThreadCalls = False
        socketServidor.enviarMensajeTodosClientes(mensaje)                  ' Liena de comando para enviar mensajes a los cclientes


    End Sub



    Private Sub BTParar_Click(sender As Object, e As EventArgs) Handles BTParar.Click

        Dim mensaje As String = ""

        mensaje = "cerrar"


        CheckForIllegalCrossThreadCalls = False
        socketServidor.enviarMensajeTodosClientes(mensaje)
    End Sub

    ' --------------------------------------------------------------------------------------------------
    ' -------------------- Evento que se ejecutara al cerrar formulario -------------------------------- 
    ' --------------------------------------------------------------------------------------------------
    Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        Try
            ' socketServidor.DetenerEscucha()
            socketServidor.CerrarTodosClientes()                            ' Cierra el puerto 
        Catch err As Exception
            MsgBox("Error al detener la escucha:" + vbCrLf +                ' Mensaje en caso de que no se cierre el puerto 
                   vbCrLf + err.Message,
                   MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        End Try

        End


    End Sub

    ' --------------------------------------------------------------------------------------------------
    ' ---------------------- Evento que se ejecutara al habilitar CMD ---------------------------------- 
    ' --------------------------------------------------------------------------------------------------

    Private Sub HabilitarCMDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HabilitarCMDToolStripMenuItem.Click

        TxtCmd.Enabled = True                                   ' Hailita el txtBox para escribir codigos cmd

        TxtCmd.Text = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" & vbCrLf +
                      "-           Comandos del sistema Habilitados         -" & vbCrLf +
                      "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" & vbCrLf +
                      "- cmd: "

    End Sub


    ' --------------------------------------------------------------------------------------------------
    ' ----------------- Evento que se ejecutara al presionar Enter en TxtCmd --------------------------- 
    ' --------------------------------------------------------------------------------------------------
    Private Sub TxtCmd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtCmd.KeyPress
        If Asc(e.KeyChar) = 13 Then                                 ' Si se presiono enter 
            If TxtCmd.Text = "- cmd: habilitar" Then                ' Si el codigo cmd es correcto 

                BTAgregar.Enabled = True                            ' Habilitar boton para editar la base de datos
                TxtCmd.Enabled = False
            End If
        End If
    End Sub


    ' --------------------------------------------------------------------------------------------------
    ' ------------ Evento que se ejecutara al selecionar un elemnto del DataGrid ----------------------- 
    ' --------------------------------------------------------------------------------------------------
    Private Sub DataGridView1_CellClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick


        ' Obtencion de valores de las celdas de la base de datos
        id = DataGridView1.CurrentRow.Cells(0).Value                    ' Toma el valor id de la base de datos 
        nombreJuego = DataGridView1.CurrentRow.Cells(1).Value           ' Toma el nombre del juego de la base de datos
        direccionRaiz = DataGridView1.CurrentRow.Cells(2).Value         ' Toma la direccion raiz de la base de datos 


        Label1.Text = id                                                ' Borrar cunando terminen pruebas 
        Label2.Text = nombreJuego
        Label3.Text = direccionRaiz


        ' Identificacion de la fila para mostrar Gif 
        Select Case id
            Case 1
                PictureBox13.Image = My.Resources.ultimate_booster

            Case 2
                PictureBox13.Image = My.Resources.dreadeye2

        End Select



    End Sub

    ' --------------------------------------------------------------------------------------------------
    ' ---------------- Evento que se ejecutara al pulsar el boton Editar ------------------------------- 
    ' --------------------------------------------------------------------------------------------------

    Private Sub BTAgregar_Click(sender As Object, e As EventArgs) Handles BTAgregar.Click

        Base_de_Datos.Show()

    End Sub


    ' --------------------------------------------------------------------------------------------------
    ' ---------------- Evento que se ejecutara al pulsar logo Steam ------------------------------------ 
    ' --------------------------------------------------------------------------------------------------
    Private Sub PictureBox15_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox15.DoubleClick
        CheckForIllegalCrossThreadCalls = False
        socketServidor.enviarMensajeTodosClientes("AbrirSteam")



        TableLayoutPanel6.Enabled = True
        TableLayoutPanel6.BackColor = Color.Black
        TableLayoutPanel6.Visible = True

        PictureBox13.Enabled = False

        Panel2.Visible = True

        GroupBox2.ForeColor = Color.White




        PictureBox13.Image = My.Resources.steamFondo
        PictureBox15.Image = My.Resources.LogoSteam
        PictureBox16.Image = My.Resources.loading_7

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CheckForIllegalCrossThreadCalls = False
        socketServidor.enviarMensajeTodosClientes("IniciarJuego")

        PictureBox16.Image = My.Resources.loading_48



    End Sub


End Class
