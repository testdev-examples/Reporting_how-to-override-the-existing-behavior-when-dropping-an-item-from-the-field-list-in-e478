using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.UI;

namespace FieldListDragDrop {
    public class MyFieldListDragDropService : IFieldListDragDropService {
        IDesignerHost host;
        public MyFieldListDragDropService(IDesignerHost host) { this.host = host; }
        public IDragHandler GetDragHandler() {
            return new MyFieldDragHandler(host);
        }
    }
    public class MyFieldDragHandler : FieldDragHandler {
        public MyFieldDragHandler(IDesignerHost host)
            : base(host) {
        }
        public override void HandleDragDrop(object sender, DragEventArgs e) {
            AdornerService.ResetSnapping();
            RulerService.HideShadows();

            XRControl parent = BandViewSvc.GetControlByScreenPoint(new Point(e.X, e.Y));
            if (parent == null)
                return;

            XRRichText demoRichText = new XRRichText();
            PointF location = GetDragDropLocation(e, demoRichText, parent);

            DesignTool.AddToContainer(Host, demoRichText);

            demoRichText.LocationF = location;
            demoRichText.Size = new Size(100, 25);
            demoRichText.DataBindings.Add("Rtf", null, "test");
        }

        private PointF GetDragDropLocation(DragEventArgs e, XRControl control, XRControl parent) {
            PointF bandPoint = EvalBandPoint(e, parent.Band);
            bandPoint = BandViewSvc.SnapBandPoint(bandPoint, parent.Band, control, new XRControl[] { control });
            PointF screenPoint = BandViewSvc.ControlViewToScreen(bandPoint, parent.Band);
            return BandViewSvc.ScreenToControl(new RectangleF(screenPoint, SizeF.Empty), parent).Location;
        }

        static DragDataObject GetDragData(IDataObject dataObject) {
            return (DragDataObject)dataObject.GetData(typeof(DragDataObject));
        }
    }
}
