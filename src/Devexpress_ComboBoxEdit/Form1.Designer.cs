using DevExpress.XtraWaitForm;

namespace Devexpress_ComboBoxEdit
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbox = new DevExpress.XtraEditors.ComboBoxEdit();
            this.listMessages = new DevExpress.XtraEditors.ListBoxControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbFilterMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnClear = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
            ((System.ComponentModel.ISupportInitialize)(this.cmbox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilterMode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbox
            // 
            this.cmbox.Location = new System.Drawing.Point(121, 61);
            this.cmbox.Name = "cmbox";
            this.cmbox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbox.Size = new System.Drawing.Size(437, 20);
            this.cmbox.TabIndex = 0;
            this.cmbox.SelectedIndexChanged += new System.EventHandler(this.cmbox_SelectedIndexChanged);
            this.cmbox.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cmbox_ButtonClick);
            this.cmbox.EditValueChanged += new System.EventHandler(this.cmbox_EditValueChanged);
            // 
            // listMessages
            // 
            this.listMessages.Location = new System.Drawing.Point(31, 121);
            this.listMessages.Name = "listMessages";
            this.listMessages.Size = new System.Drawing.Size(527, 285);
            this.listMessages.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(31, 64);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "下拉选择过滤";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(31, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "过滤模式设置";
            // 
            // cmbFilterMode
            // 
            this.cmbFilterMode.Location = new System.Drawing.Point(121, 12);
            this.cmbFilterMode.Name = "cmbFilterMode";
            this.cmbFilterMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFilterMode.Size = new System.Drawing.Size(437, 20);
            this.cmbFilterMode.TabIndex = 3;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(272, 425);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "清空日志信息";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // progressPanel1
            // 
            this.progressPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel1.Appearance.Options.UseBackColor = true;
            this.progressPanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.progressPanel1.Caption = "数据初始化中，请稍后 ...";
            this.progressPanel1.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel1.Description = "";
            this.progressPanel1.Location = new System.Drawing.Point(145, 99);
            this.progressPanel1.Name = "progressPanel1";
            this.progressPanel1.Size = new System.Drawing.Size(297, 140);
            this.progressPanel1.TabIndex = 6;
            this.progressPanel1.Text = "数据初始化中";
            this.progressPanel1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 460);
            this.Controls.Add(this.progressPanel1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cmbFilterMode);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.listMessages);
            this.Controls.Add(this.cmbox);
            this.Name = "Form1";
            this.Text = "DevComboBoxEditor过滤测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFilterMode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cmbox;
        private DevExpress.XtraEditors.ListBoxControl listMessages;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFilterMode;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Timer timer1;
        private ProgressPanel progressPanel1;
    }
}

