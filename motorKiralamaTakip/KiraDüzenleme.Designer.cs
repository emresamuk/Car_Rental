namespace motorKiralamaTakip
{
    partial class KiraDüzenleme
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
            this.dtpTeslimTarihi = new System.Windows.Forms.DateTimePicker();
            this.dtpAlmaTarihi = new System.Windows.Forms.DateTimePicker();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.btnKiraSil = new DevExpress.XtraEditors.SimpleButton();
            this.txtAracPlaka = new DevExpress.XtraEditors.TextEdit();
            this.txtMusteriTC = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtKiraBedeli = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAracPlaka.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriTC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKiraBedeli.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTeslimTarihi
            // 
            this.dtpTeslimTarihi.Location = new System.Drawing.Point(116, 155);
            this.dtpTeslimTarihi.Name = "dtpTeslimTarihi";
            this.dtpTeslimTarihi.Size = new System.Drawing.Size(147, 20);
            this.dtpTeslimTarihi.TabIndex = 50;
            // 
            // dtpAlmaTarihi
            // 
            this.dtpAlmaTarihi.Location = new System.Drawing.Point(148, 127);
            this.dtpAlmaTarihi.Name = "dtpAlmaTarihi";
            this.dtpAlmaTarihi.Size = new System.Drawing.Size(147, 20);
            this.dtpAlmaTarihi.TabIndex = 49;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(26, 42);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 17);
            this.labelControl1.TabIndex = 48;
            this.labelControl1.Text = "Araç Plakası:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(26, 155);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(82, 17);
            this.labelControl5.TabIndex = 46;
            this.labelControl5.Text = "Kiralama Bitiş:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(26, 72);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 17);
            this.labelControl3.TabIndex = 42;
            this.labelControl3.Text = "Müşteri TC:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(26, 127);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(116, 17);
            this.labelControl2.TabIndex = 41;
            this.labelControl2.Text = "Kiralama Başlangıcı:";
            // 
            // btnKaydet
            // 
            this.btnKaydet.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.Appearance.Options.UseFont = true;
            this.btnKaydet.Location = new System.Drawing.Point(37, 211);
            this.btnKaydet.Margin = new System.Windows.Forms.Padding(2);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(98, 31);
            this.btnKaydet.TabIndex = 51;
            this.btnKaydet.Text = "Kirala";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnKiraSil
            // 
            this.btnKiraSil.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKiraSil.Appearance.Options.UseFont = true;
            this.btnKiraSil.Location = new System.Drawing.Point(139, 211);
            this.btnKiraSil.Margin = new System.Windows.Forms.Padding(2);
            this.btnKiraSil.Name = "btnKiraSil";
            this.btnKiraSil.Size = new System.Drawing.Size(98, 31);
            this.btnKiraSil.TabIndex = 56;
            this.btnKiraSil.Text = "Sil";
            this.btnKiraSil.Click += new System.EventHandler(this.btnKiraSil_Click);
            // 
            // txtAracPlaka
            // 
            this.txtAracPlaka.Location = new System.Drawing.Point(111, 40);
            this.txtAracPlaka.Name = "txtAracPlaka";
            this.txtAracPlaka.Size = new System.Drawing.Size(138, 20);
            this.txtAracPlaka.TabIndex = 57;
            // 
            // txtMusteriTC
            // 
            this.txtMusteriTC.Location = new System.Drawing.Point(111, 72);
            this.txtMusteriTC.Name = "txtMusteriTC";
            this.txtMusteriTC.Size = new System.Drawing.Size(138, 20);
            this.txtMusteriTC.TabIndex = 58;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(26, 102);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(65, 17);
            this.labelControl4.TabIndex = 59;
            this.labelControl4.Text = "Kira Bedeli:";
            // 
            // txtKiraBedeli
            // 
            this.txtKiraBedeli.Location = new System.Drawing.Point(111, 99);
            this.txtKiraBedeli.Name = "txtKiraBedeli";
            this.txtKiraBedeli.Size = new System.Drawing.Size(138, 20);
            this.txtKiraBedeli.TabIndex = 60;
            // 
            // KiraDüzenleme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 368);
            this.Controls.Add(this.txtKiraBedeli);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtMusteriTC);
            this.Controls.Add(this.txtAracPlaka);
            this.Controls.Add(this.btnKiraSil);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.dtpTeslimTarihi);
            this.Controls.Add(this.dtpAlmaTarihi);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "KiraDüzenleme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KiraDüzenleme";
            ((System.ComponentModel.ISupportInitialize)(this.txtAracPlaka.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMusteriTC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKiraBedeli.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTeslimTarihi;
        private System.Windows.Forms.DateTimePicker dtpAlmaTarihi;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.SimpleButton btnKiraSil;
        private DevExpress.XtraEditors.TextEdit txtAracPlaka;
        private DevExpress.XtraEditors.TextEdit txtMusteriTC;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtKiraBedeli;
    }
}