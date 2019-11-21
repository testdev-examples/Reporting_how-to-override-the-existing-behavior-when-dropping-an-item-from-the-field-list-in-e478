using System;
using System.Windows.Forms;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.UserDesigner;

namespace FieldListDragDrop {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void ShowReportDesigner() { 
            XRDesignFormEx designForm = new XRDesignFormEx();

            designForm.DesignPanel.DesignerHostLoaded +=
                new DesignerLoadedEventHandler(DesignPanel_DesignerHostLoaded);

            designForm.OpenReport(new XtraReport1());

            designForm.ShowDialog();
        }

        void DesignPanel_DesignerHostLoaded(object sender, DesignerLoadedEventArgs e) {
            e.DesignerHost.RemoveService(typeof(IFieldListDragDropService));
            e.DesignerHost.AddService(typeof(IFieldListDragDropService), 
                new MyFieldListDragDropService(e.DesignerHost));
        }

        private void Form1_Load(object sender, EventArgs e) {
            ShowReportDesigner();
        }
    }

}
