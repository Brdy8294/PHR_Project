namespace PHR_Forms
{
    partial class OutlierListForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaxWarning = new System.Windows.Forms.TextBox();
            this.txtMinWarning = new System.Windows.Forms.TextBox();
            this.MinMaxSearchBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "임계 최대치";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "임계 최소치";
            // 
            // txtMaxWarning
            // 
            this.txtMaxWarning.Location = new System.Drawing.Point(158, 70);
            this.txtMaxWarning.Name = "txtMaxWarning";
            this.txtMaxWarning.Size = new System.Drawing.Size(156, 21);
            this.txtMaxWarning.TabIndex = 4;
            // 
            // txtMinWarning
            // 
            this.txtMinWarning.Location = new System.Drawing.Point(158, 139);
            this.txtMinWarning.Name = "txtMinWarning";
            this.txtMinWarning.Size = new System.Drawing.Size(156, 21);
            this.txtMinWarning.TabIndex = 5;
            // 
            // MinMaxSearchBtn
            // 
            this.MinMaxSearchBtn.Location = new System.Drawing.Point(329, 221);
            this.MinMaxSearchBtn.Name = "MinMaxSearchBtn";
            this.MinMaxSearchBtn.Size = new System.Drawing.Size(151, 65);
            this.MinMaxSearchBtn.TabIndex = 6;
            this.MinMaxSearchBtn.Text = "조회";
            this.MinMaxSearchBtn.UseVisualStyleBackColor = true;
            this.MinMaxSearchBtn.Click += new System.EventHandler(this.MinMaxSearchBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(96, 324);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(648, 341);
            this.dataGridView1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(13, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(301, 34);
            this.label3.TabIndex = 8;
            this.label3.Text = "이상치 설정 및 데이터 조회";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(753, 685);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 31);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // OutlierListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 727);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.MinMaxSearchBtn);
            this.Controls.Add(this.txtMinWarning);
            this.Controls.Add(this.txtMaxWarning);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OutlierListForm";
            this.Text = "OutlierListForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxWarning;
        private System.Windows.Forms.TextBox txtMinWarning;
        private System.Windows.Forms.Button MinMaxSearchBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
    }
}