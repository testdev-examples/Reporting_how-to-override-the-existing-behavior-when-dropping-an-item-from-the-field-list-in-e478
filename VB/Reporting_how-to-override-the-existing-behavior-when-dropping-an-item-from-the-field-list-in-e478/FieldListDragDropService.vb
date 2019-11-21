Imports System
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraReports.Design
Imports DevExpress.XtraReports.UI

Namespace FieldListDragDrop
	Public Class MyFieldListDragDropService
		Implements IFieldListDragDropService

		Private host As IDesignerHost
		Public Sub New(ByVal host As IDesignerHost)
			Me.host = host
		End Sub
		Public Function GetDragHandler() As IDragHandler Implements IFieldListDragDropService.GetDragHandler
			Return New MyFieldDragHandler(host)
		End Function
	End Class
	Public Class MyFieldDragHandler
		Inherits FieldDragHandler

		Public Sub New(ByVal host As IDesignerHost)
			MyBase.New(host)
		End Sub

		Public Overrides Sub HandleDragDrop(ByVal sender As Object, ByVal e As DragEventArgs)
			AdornerService.ResetSnapping()
			RulerService.HideShadows()

            Dim parent As XRControl = BandViewSvc.GetControlByScreenPoint(New System.Drawing.Point(e.X, e.Y))
			If parent Is Nothing Then
				Return
			End If

			Dim xRLabel As New XRRichText()
			Dim location As PointF = GetDragDropLocation(e, xRLabel, parent)

			DesignTool.AddToContainer(Host, xRLabel)

			xRLabel.LocationF = location
			xRLabel.Size = New Size(100, 25)
			xRLabel.DataBindings.Add("Rtf", Nothing, "test")
		End Sub

		Private Function GetDragDropLocation(ByVal e As DragEventArgs, ByVal control As XRControl, ByVal parent As XRControl) As PointF
			Dim bandPoint As PointF = EvalBandPoint(e, parent.Band)
			bandPoint = BandViewSvc.SnapBandPoint(bandPoint, parent.Band, control, New XRControl() { control })
			Dim screenPoint As PointF = BandViewSvc.ControlViewToScreen(bandPoint, parent.Band)
			Return BandViewSvc.ScreenToControl(New RectangleF(screenPoint, SizeF.Empty), parent).Location
		End Function

		Private Shared Function GetDragData(ByVal dataObject As IDataObject) As DragDataObject
			Return DirectCast(dataObject.GetData(GetType(DragDataObject)), DragDataObject)
		End Function
	End Class
End Namespace
