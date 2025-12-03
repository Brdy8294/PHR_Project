namespace PHR_Management_App
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Updatebtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridDiagnosis = new System.Windows.Forms.DataGridView();
            this.dataGridMedication = new System.Windows.Forms.DataGridView();
            this.dataGridTestResult = new System.Windows.Forms.DataGridView();
            this.cmbHospitalSelection = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDiagnosis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMedication)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTestResult)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(249, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "병원 데이터 입력";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(153, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "병원명";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(153, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "진단명";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(138, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "투약정보";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(377, 486);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(100, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "자주가는 병원";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Updatebtn
            // 
            this.Updatebtn.Location = new System.Drawing.Point(354, 508);
            this.Updatebtn.Name = "Updatebtn";
            this.Updatebtn.Size = new System.Drawing.Size(144, 45);
            this.Updatebtn.TabIndex = 5;
            this.Updatebtn.Text = "데이터 추가";
            this.Updatebtn.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(138, 359);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "검사결과";
            // 
            // dataGridDiagnosis
            // 
            this.dataGridDiagnosis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDiagnosis.Location = new System.Drawing.Point(226, 122);
            this.dataGridDiagnosis.Name = "dataGridDiagnosis";
            this.dataGridDiagnosis.RowTemplate.Height = 23;
            this.dataGridDiagnosis.Size = new System.Drawing.Size(421, 107);
            this.dataGridDiagnosis.TabIndex = 8;
            // 
            // dataGridMedication
            // 
            this.dataGridMedication.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMedication.Location = new System.Drawing.Point(226, 235);
            this.dataGridMedication.Name = "dataGridMedication";
            this.dataGridMedication.RowTemplate.Height = 23;
            this.dataGridMedication.Size = new System.Drawing.Size(421, 118);
            this.dataGridMedication.TabIndex = 9;
            this.dataGridMedication.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridMedication_CellContentClick);
            // 
            // dataGridTestResult
            // 
            this.dataGridTestResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTestResult.Location = new System.Drawing.Point(226, 359);
            this.dataGridTestResult.Name = "dataGridTestResult";
            this.dataGridTestResult.RowTemplate.Height = 23;
            this.dataGridTestResult.Size = new System.Drawing.Size(421, 121);
            this.dataGridTestResult.TabIndex = 10;
            // 
            // cmbHospitalSelection
            // 
            this.cmbHospitalSelection.FormattingEnabled = true;
            this.cmbHospitalSelection.Location = new System.Drawing.Point(226, 81);
            this.cmbHospitalSelection.Name = "cmbHospitalSelection";
            this.cmbHospitalSelection.Size = new System.Drawing.Size(307, 20);
            this.cmbHospitalSelection.TabIndex = 11;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(572, 81);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // HospitalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 565);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbHospitalSelection);
            this.Controls.Add(this.dataGridTestResult);
            this.Controls.Add(this.dataGridMedication);
            this.Controls.Add(this.dataGridDiagnosis);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Updatebtn);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "HospitalForm";
            this.Text = "HospitalForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDiagnosis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMedication)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTestResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button Updatebtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridDiagnosis;
        private System.Windows.Forms.DataGridView dataGridMedication;
        private System.Windows.Forms.DataGridView dataGridTestResult;
        private System.Windows.Forms.ComboBox cmbHospitalSelection;
        private System.Windows.Forms.Button btnSearch;
    }
}