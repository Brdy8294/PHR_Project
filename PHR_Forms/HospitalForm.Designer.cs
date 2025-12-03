namespace PHR_Forms
{
    partial class HospitalForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtHospName = new System.Windows.Forms.TextBox();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.txtMeds = new System.Windows.Forms.TextBox();
            this.txtExam = new System.Windows.Forms.TextBox();
            this.chkFrequent = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(43, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "병원 정보 연동 페이지";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(217, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 39);
            this.label2.TabIndex = 0;
            this.label2.Text = "병원 데이터 입력";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(85, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 29);
            this.label3.TabIndex = 1;
            this.label3.Text = "병원명";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(85, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 29);
            this.label4.TabIndex = 1;
            this.label4.Text = "진단명";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(68, 339);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 29);
            this.label5.TabIndex = 1;
            this.label5.Text = "투약정보";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(66, 483);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 29);
            this.label6.TabIndex = 1;
            this.label6.Text = "검사결과";
            // 
            // txtHospName
            // 
            this.txtHospName.Location = new System.Drawing.Point(168, 160);
            this.txtHospName.Name = "txtHospName";
            this.txtHospName.Size = new System.Drawing.Size(113, 28);
            this.txtHospName.TabIndex = 2;
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.Location = new System.Drawing.Point(168, 207);
            this.txtDiagnosis.Multiline = true;
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(323, 113);
            this.txtDiagnosis.TabIndex = 2;
            // 
            // txtMeds
            // 
            this.txtMeds.Location = new System.Drawing.Point(168, 336);
            this.txtMeds.Multiline = true;
            this.txtMeds.Name = "txtMeds";
            this.txtMeds.Size = new System.Drawing.Size(323, 113);
            this.txtMeds.TabIndex = 2;
            // 
            // txtExam
            // 
            this.txtExam.Location = new System.Drawing.Point(168, 480);
            this.txtExam.Multiline = true;
            this.txtExam.Name = "txtExam";
            this.txtExam.Size = new System.Drawing.Size(323, 113);
            this.txtExam.TabIndex = 2;
            // 
            // chkFrequent
            // 
            this.chkFrequent.AutoSize = true;
            this.chkFrequent.Location = new System.Drawing.Point(252, 620);
            this.chkFrequent.Name = "chkFrequent";
            this.chkFrequent.Size = new System.Drawing.Size(148, 22);
            this.chkFrequent.TabIndex = 3;
            this.chkFrequent.Text = "자주가는 병원";
            this.chkFrequent.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(241, 670);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(159, 44);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "데이터 추가";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(558, 98);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersWidth = 62;
            this.dgvList.RowTemplate.Height = 30;
            this.dgvList.Size = new System.Drawing.Size(571, 544);
            this.dgvList.TabIndex = 5;
            // 
            // HospitalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 741);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkFrequent);
            this.Controls.Add(this.txtExam);
            this.Controls.Add(this.txtMeds);
            this.Controls.Add(this.txtDiagnosis);
            this.Controls.Add(this.txtHospName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "HospitalForm";
            this.Text = "HospitalForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHospName;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.TextBox txtMeds;
        private System.Windows.Forms.TextBox txtExam;
        private System.Windows.Forms.CheckBox chkFrequent;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvList;
    }
}