Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraReports.UserDesigner

Namespace FieldListDragDrop
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

        Private Sub ShowReportDesigner()
            Dim designForm As New XRDesignFormEx()

            AddHandler designForm.DesignPanel.DesignerHostLoaded, AddressOf DesignPanel_DesignerHostLoaded

            designForm.OpenReport(New XtraReport1())

            designForm.ShowDialog()
        End Sub

        Private Sub DesignPanel_DesignerHostLoaded(ByVal sender As Object, ByVal e As DesignerLoadedEventArgs)
			e.DesignerHost.RemoveService(GetType(IFieldListDragDropService))
			e.DesignerHost.AddService(GetType(IFieldListDragDropService), New MyFieldListDragDropService(e.DesignerHost))
		End Sub

        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ShowReportDesigner()
        End Sub
    End Class

End Namespace
