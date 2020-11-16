Public Class Base_de_Datos
    Private Sub JuegosBindingNavigatorSaveItem_Click(sender As Object, e As EventArgs) Handles JuegosBindingNavigatorSaveItem.Click
        Me.Validate()
        Me.JuegosBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.ListaJuegosDataSet)

    End Sub

    Private Sub Base_de_Datos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: esta línea de código carga datos en la tabla 'ListaJuegosDataSet.Juegos' Puede moverla o quitarla según sea necesario.
        Me.JuegosTableAdapter.Fill(Me.ListaJuegosDataSet.Juegos)

    End Sub

    Private Sub Base_de_Datos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Form1.JuegosTableAdapter.Fill(Form1.ListaJuegosDataSet.Juegos)
        Form1.BTAgregar.Enabled = False
    End Sub
End Class