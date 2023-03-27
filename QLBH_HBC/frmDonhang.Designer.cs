
namespace QLBH_HBC
{
    partial class frmDonhang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.TRANGTHAI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MADH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NGAYTAO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.NGUOITAO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TENDL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.GHICHU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TONGTIEN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(685, 685);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(661, 661);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Load += new System.EventHandler(this.gridControl1_Load);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.TRANGTHAI,
            this.MADH,
            this.NGAYTAO,
            this.NGUOITAO,
            this.TENDL,
            this.GHICHU,
            this.TONGTIEN});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // TRANGTHAI
            // 
            this.TRANGTHAI.Caption = "Trạng thái";
            this.TRANGTHAI.FieldName = "TRANGTHAI";
            this.TRANGTHAI.MinWidth = 25;
            this.TRANGTHAI.Name = "TRANGTHAI";
            this.TRANGTHAI.Visible = true;
            this.TRANGTHAI.VisibleIndex = 0;
            this.TRANGTHAI.Width = 94;
            // 
            // MADH
            // 
            this.MADH.Caption = "Số đơn hàng";
            this.MADH.FieldName = "MADH";
            this.MADH.MinWidth = 25;
            this.MADH.Name = "MADH";
            this.MADH.Visible = true;
            this.MADH.VisibleIndex = 1;
            this.MADH.Width = 94;
            // 
            // NGAYTAO
            // 
            this.NGAYTAO.Caption = "Ngày tạo";
            this.NGAYTAO.FieldName = "NGAYTAO";
            this.NGAYTAO.MinWidth = 25;
            this.NGAYTAO.Name = "NGAYTAO";
            this.NGAYTAO.Visible = true;
            this.NGAYTAO.VisibleIndex = 2;
            this.NGAYTAO.Width = 94;
            // 
            // NGUOITAO
            // 
            this.NGUOITAO.Caption = "Người tạo";
            this.NGUOITAO.FieldName = "NGUOITAO";
            this.NGUOITAO.MinWidth = 25;
            this.NGUOITAO.Name = "NGUOITAO";
            this.NGUOITAO.Visible = true;
            this.NGUOITAO.VisibleIndex = 3;
            this.NGUOITAO.Width = 94;
            // 
            // TENDL
            // 
            this.TENDL.Caption = "Tên đại lý";
            this.TENDL.FieldName = "TENDL";
            this.TENDL.MinWidth = 25;
            this.TENDL.Name = "TENDL";
            this.TENDL.Visible = true;
            this.TENDL.VisibleIndex = 4;
            this.TENDL.Width = 94;
            // 
            // GHICHU
            // 
            this.GHICHU.Caption = "gridColumn5";
            this.GHICHU.CustomizationCaption = "Ghi chú";
            this.GHICHU.FieldName = "GHICHU";
            this.GHICHU.MinWidth = 25;
            this.GHICHU.Name = "GHICHU";
            this.GHICHU.Visible = true;
            this.GHICHU.VisibleIndex = 5;
            this.GHICHU.Width = 94;
            // 
            // TONGTIEN
            // 
            this.TONGTIEN.Caption = "Tổng tiền";
            this.TONGTIEN.FieldName = "TONGTIEN";
            this.TONGTIEN.MinWidth = 25;
            this.TONGTIEN.Name = "TONGTIEN";
            this.TONGTIEN.Visible = true;
            this.TONGTIEN.VisibleIndex = 6;
            this.TONGTIEN.Width = 94;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(685, 685);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(665, 665);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmDonhang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 685);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmDonhang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDonhang";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn TRANGTHAI;
        private DevExpress.XtraGrid.Columns.GridColumn MADH;
        private DevExpress.XtraGrid.Columns.GridColumn NGAYTAO;
        private DevExpress.XtraGrid.Columns.GridColumn NGUOITAO;
        private DevExpress.XtraGrid.Columns.GridColumn TENDL;
        private DevExpress.XtraGrid.Columns.GridColumn GHICHU;
        private DevExpress.XtraGrid.Columns.GridColumn TONGTIEN;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}