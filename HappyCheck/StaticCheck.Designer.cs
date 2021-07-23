using System.Windows.Forms;
namespace HappyCode
{
    partial class StaticCheck
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCheck = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPackages = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgResult = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.progPack = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCheck
            // 
            this.btnCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheck.Location = new System.Drawing.Point(0, 0);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(178, 59);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtPackages);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5, 12, 4, 12);
            this.panel1.Size = new System.Drawing.Size(187, 568);
            this.panel1.TabIndex = 4;
            // 
            // txtPackages
            // 
            this.txtPackages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPackages.Location = new System.Drawing.Point(5, 116);
            this.txtPackages.Multiline = true;
            this.txtPackages.Name = "txtPackages";
            this.txtPackages.Size = new System.Drawing.Size(178, 440);
            this.txtPackages.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(5, 71);
            this.panel5.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panel5.Size = new System.Drawing.Size(178, 45);
            this.panel5.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCheck);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(178, 59);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgResult);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(187, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(626, 568);
            this.panel3.TabIndex = 6;
            // 
            // dgResult
            // 
            this.dgResult.AllowUserToAddRows = false;
            this.dgResult.AllowUserToDeleteRows = false;
            this.dgResult.AllowUserToOrderColumns = true;
            this.dgResult.AllowUserToResizeRows = false;
            this.dgResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgResult.Location = new System.Drawing.Point(0, 131);
            this.dgResult.MultiSelect = false;
            this.dgResult.Name = "dgResult";
            this.dgResult.ReadOnly = true;
            this.dgResult.RowHeadersVisible = false;
            this.dgResult.RowTemplate.Height = 20;
            this.dgResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgResult.Size = new System.Drawing.Size(626, 437);
            this.dgResult.TabIndex = 7;
            this.dgResult.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgResult_CellDoubleClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.btnReport);
            this.panel4.Controls.Add(this.textBox2);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.progPack);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(626, 131);
            this.panel4.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 76);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(516, 35);
            this.textBox1.TabIndex = 0;
            // 
            // btnReport
            // 
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReport.Location = new System.Drawing.Point(516, 76);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(110, 35);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "Report To...";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox2.Location = new System.Drawing.Point(0, 40);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(626, 36);
            this.textBox2.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtFolder);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.btnSelectFolder);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.panel6.Size = new System.Drawing.Size(626, 40);
            this.panel6.TabIndex = 2;
            // 
            // txtFolder
            // 
            this.txtFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFolder.Location = new System.Drawing.Point(80, 8);
            this.txtFolder.Multiline = true;
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(436, 24);
            this.txtFolder.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Check Folder";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectFolder.Location = new System.Drawing.Point(516, 8);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(110, 24);
            this.btnSelectFolder.TabIndex = 1;
            this.btnSelectFolder.Text = "Select Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // progPack
            // 
            this.progPack.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progPack.Location = new System.Drawing.Point(0, 111);
            this.progPack.Name = "progPack";
            this.progPack.Size = new System.Drawing.Size(626, 20);
            this.progPack.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(813, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            this.registerToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.registerToolStripMenuItem.Text = "Register";
            this.registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click);
            // 
            // StaticCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 592);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "StaticCheck";
            this.Text = "Happy Code";
            this.Load += new System.EventHandler(this.Form7_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgResult)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label label1;
        private Button btnReport;
        private TextBox txtPackages;
        private ProgressBar progPack;
        private DataGridView dgResult;
        private Timer timer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem registerToolStripMenuItem;

    }
}