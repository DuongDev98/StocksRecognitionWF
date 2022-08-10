namespace StocksRecognitionWF
{
    partial class Tester
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
            this.btnLoad = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grMain = new System.Windows.Forms.DataGridView();
            this.colFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFlag = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(408, 11);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(77, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(326, 20);
            this.txtPath.TabIndex = 1;
            this.txtPath.Text = "C:\\Users\\ND\\AppData\\Roaming\\MetaQuotes\\Terminal\\2191F4A3D14D7B4B1EBB84F924777883\\" +
    "MQL4\\Files\\Pattern";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Path";
            // 
            // grMain
            // 
            this.grMain.AllowUserToAddRows = false;
            this.grMain.AllowUserToDeleteRows = false;
            this.grMain.BackgroundColor = System.Drawing.Color.White;
            this.grMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFile,
            this.colNum});
            this.grMain.Location = new System.Drawing.Point(77, 49);
            this.grMain.Name = "grMain";
            this.grMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grMain.Size = new System.Drawing.Size(326, 322);
            this.grMain.TabIndex = 3;
            this.grMain.SelectionChanged += new System.EventHandler(this.grMain_SelectionChanged);
            // 
            // colFile
            // 
            this.colFile.DataPropertyName = "NAME";
            this.colFile.HeaderText = "File";
            this.colFile.Name = "colFile";
            this.colFile.ReadOnly = true;
            // 
            // colNum
            // 
            this.colNum.DataPropertyName = "VER";
            this.colNum.HeaderText = "Ver";
            this.colNum.Name = "colNum";
            // 
            // btnFlag
            // 
            this.btnFlag.Enabled = false;
            this.btnFlag.Location = new System.Drawing.Point(408, 49);
            this.btnFlag.Name = "btnFlag";
            this.btnFlag.Size = new System.Drawing.Size(75, 23);
            this.btnFlag.TabIndex = 4;
            this.btnFlag.Text = "Check Flag";
            this.btnFlag.UseVisualStyleBackColor = true;
            this.btnFlag.Click += new System.EventHandler(this.btnFlag_Click);
            // 
            // Tester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 395);
            this.Controls.Add(this.btnFlag);
            this.Controls.Add(this.grMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnLoad);
            this.Name = "Tester";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tester";
            ((System.ComponentModel.ISupportInitialize)(this.grMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grMain;
        private System.Windows.Forms.Button btnFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum;
    }
}