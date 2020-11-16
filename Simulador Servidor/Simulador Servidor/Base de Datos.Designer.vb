<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Base_de_Datos
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim IdLabel As System.Windows.Forms.Label
        Dim Campo1Label As System.Windows.Forms.Label
        Dim Campo2Label As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Base_de_Datos))
        Me.ListaJuegosDataSet = New Simulador_Servidor.ListaJuegosDataSet()
        Me.JuegosBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.JuegosTableAdapter = New Simulador_Servidor.ListaJuegosDataSetTableAdapters.JuegosTableAdapter()
        Me.TableAdapterManager = New Simulador_Servidor.ListaJuegosDataSetTableAdapters.TableAdapterManager()
        Me.JuegosBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.IdTextBox = New System.Windows.Forms.TextBox()
        Me.Campo1TextBox = New System.Windows.Forms.TextBox()
        Me.Campo2TextBox = New System.Windows.Forms.TextBox()
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.JuegosBindingNavigatorSaveItem = New System.Windows.Forms.ToolStripButton()
        Me.JuegosDataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        IdLabel = New System.Windows.Forms.Label()
        Campo1Label = New System.Windows.Forms.Label()
        Campo2Label = New System.Windows.Forms.Label()
        CType(Me.ListaJuegosDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JuegosBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JuegosBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.JuegosBindingNavigator.SuspendLayout()
        CType(Me.JuegosDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ListaJuegosDataSet
        '
        Me.ListaJuegosDataSet.DataSetName = "ListaJuegosDataSet"
        Me.ListaJuegosDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'JuegosBindingSource
        '
        Me.JuegosBindingSource.DataMember = "Juegos"
        Me.JuegosBindingSource.DataSource = Me.ListaJuegosDataSet
        '
        'JuegosTableAdapter
        '
        Me.JuegosTableAdapter.ClearBeforeFill = True
        '
        'TableAdapterManager
        '
        Me.TableAdapterManager.BackupDataSetBeforeUpdate = False
        Me.TableAdapterManager.JuegosTableAdapter = Me.JuegosTableAdapter
        Me.TableAdapterManager.UpdateOrder = Simulador_Servidor.ListaJuegosDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete
        '
        'JuegosBindingNavigator
        '
        Me.JuegosBindingNavigator.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.JuegosBindingNavigator.BindingSource = Me.JuegosBindingSource
        Me.JuegosBindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.JuegosBindingNavigator.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.JuegosBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem, Me.JuegosBindingNavigatorSaveItem})
        Me.JuegosBindingNavigator.Location = New System.Drawing.Point(0, 0)
        Me.JuegosBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.JuegosBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.JuegosBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.JuegosBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.JuegosBindingNavigator.Name = "JuegosBindingNavigator"
        Me.JuegosBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.JuegosBindingNavigator.Size = New System.Drawing.Size(672, 25)
        Me.JuegosBindingNavigator.TabIndex = 0
        Me.JuegosBindingNavigator.Text = "BindingNavigator1"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Posición"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Posición actual"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(37, 22)
        Me.BindingNavigatorCountItem.Text = "de {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Número total de elementos"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'IdLabel
        '
        IdLabel.AutoSize = True
        IdLabel.Location = New System.Drawing.Point(12, 244)
        IdLabel.Name = "IdLabel"
        IdLabel.Size = New System.Drawing.Size(19, 13)
        IdLabel.TabIndex = 1
        IdLabel.Text = "Id:"
        '
        'IdTextBox
        '
        Me.IdTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.JuegosBindingSource, "Id", True))
        Me.IdTextBox.Location = New System.Drawing.Point(57, 241)
        Me.IdTextBox.Name = "IdTextBox"
        Me.IdTextBox.Size = New System.Drawing.Size(100, 20)
        Me.IdTextBox.TabIndex = 2
        '
        'Campo1Label
        '
        Campo1Label.AutoSize = True
        Campo1Label.Location = New System.Drawing.Point(12, 285)
        Campo1Label.Name = "Campo1Label"
        Campo1Label.Size = New System.Drawing.Size(39, 13)
        Campo1Label.TabIndex = 3
        Campo1Label.Text = "Juego:"
        '
        'Campo1TextBox
        '
        Me.Campo1TextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.JuegosBindingSource, "Campo1", True))
        Me.Campo1TextBox.Location = New System.Drawing.Point(57, 282)
        Me.Campo1TextBox.Name = "Campo1TextBox"
        Me.Campo1TextBox.Size = New System.Drawing.Size(100, 20)
        Me.Campo1TextBox.TabIndex = 4
        '
        'Campo2Label
        '
        Campo2Label.AutoSize = True
        Campo2Label.Location = New System.Drawing.Point(12, 322)
        Campo2Label.Name = "Campo2Label"
        Campo2Label.Size = New System.Drawing.Size(32, 13)
        Campo2Label.TabIndex = 5
        Campo2Label.Text = "URL:"
        '
        'Campo2TextBox
        '
        Me.Campo2TextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.JuegosBindingSource, "Campo2", True))
        Me.Campo2TextBox.Location = New System.Drawing.Point(57, 319)
        Me.Campo2TextBox.Name = "Campo2TextBox"
        Me.Campo2TextBox.Size = New System.Drawing.Size(582, 20)
        Me.Campo2TextBox.TabIndex = 6
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem.Text = "Agregar nuevo"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorDeleteItem.Text = "Eliminar"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Mover primero"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Mover anterior"
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Mover siguiente"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Mover último"
        '
        'JuegosBindingNavigatorSaveItem
        '
        Me.JuegosBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.JuegosBindingNavigatorSaveItem.Image = CType(resources.GetObject("JuegosBindingNavigatorSaveItem.Image"), System.Drawing.Image)
        Me.JuegosBindingNavigatorSaveItem.Name = "JuegosBindingNavigatorSaveItem"
        Me.JuegosBindingNavigatorSaveItem.Size = New System.Drawing.Size(23, 22)
        Me.JuegosBindingNavigatorSaveItem.Text = "Guardar datos"
        '
        'JuegosDataGridView
        '
        Me.JuegosDataGridView.AutoGenerateColumns = False
        Me.JuegosDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.JuegosDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.JuegosDataGridView.DataSource = Me.JuegosBindingSource
        Me.JuegosDataGridView.Location = New System.Drawing.Point(210, 50)
        Me.JuegosDataGridView.Name = "JuegosDataGridView"
        Me.JuegosDataGridView.Size = New System.Drawing.Size(428, 245)
        Me.JuegosDataGridView.TabIndex = 7
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Id"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Id"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Campo1"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Campo1"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Campo2"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Campo2"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 200
        '
        'Base_de_Datos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 372)
        Me.Controls.Add(Me.JuegosDataGridView)
        Me.Controls.Add(IdLabel)
        Me.Controls.Add(Me.IdTextBox)
        Me.Controls.Add(Campo1Label)
        Me.Controls.Add(Me.Campo1TextBox)
        Me.Controls.Add(Campo2Label)
        Me.Controls.Add(Me.Campo2TextBox)
        Me.Controls.Add(Me.JuegosBindingNavigator)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Base_de_Datos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Base_de_Datos"
        CType(Me.ListaJuegosDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JuegosBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JuegosBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.JuegosBindingNavigator.ResumeLayout(False)
        Me.JuegosBindingNavigator.PerformLayout()
        CType(Me.JuegosDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListaJuegosDataSet As ListaJuegosDataSet
    Friend WithEvents JuegosBindingSource As BindingSource
    Friend WithEvents JuegosTableAdapter As ListaJuegosDataSetTableAdapters.JuegosTableAdapter
    Friend WithEvents TableAdapterManager As ListaJuegosDataSetTableAdapters.TableAdapterManager
    Friend WithEvents JuegosBindingNavigator As BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents JuegosBindingNavigatorSaveItem As ToolStripButton
    Friend WithEvents IdTextBox As TextBox
    Friend WithEvents Campo1TextBox As TextBox
    Friend WithEvents Campo2TextBox As TextBox
    Friend WithEvents JuegosDataGridView As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
End Class
