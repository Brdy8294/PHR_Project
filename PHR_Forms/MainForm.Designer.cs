namespace PHR_Forms
{
    partial class MainForm
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
            this.btnEditInfo = new System.Windows.Forms.Button();
            this.btnThreshold = new System.Windows.Forms.Button();
            this.btnHealthInput = new System.Windows.Forms.Button();
            this.btnHospital = new System.Windows.Forms.Button();
            this.btnChart = new System.Windows.Forms.Button();
            this.btnOutlier = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEditInfo
            // 
            this.btnEditInfo.Location = new System.Drawing.Point(161, 214);
            this.btnEditInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEditInfo.Name = "btnEditInfo";
            this.btnEditInfo.Size = new System.Drawing.Size(213, 78);
            this.btnEditInfo.TabIndex = 0;
            this.btnEditInfo.Text = "정보수정";
            this.btnEditInfo.UseVisualStyleBackColor = true;
            this.btnEditInfo.Click += new System.EventHandler(this.btnEditInfo_Click);
            // 
            // btnThreshold
            // 
            this.btnThreshold.Location = new System.Drawing.Point(456, 214);
            this.btnThreshold.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThreshold.Name = "btnThreshold";
            this.btnThreshold.Size = new System.Drawing.Size(213, 78);
            this.btnThreshold.TabIndex = 1;
            this.btnThreshold.Text = "임계치 설정";
            this.btnThreshold.UseVisualStyleBackColor = true;
            this.btnThreshold.Click += new System.EventHandler(this.btnThreshold_Click);
            // 
            // btnHealthInput
            // 
            this.btnHealthInput.Location = new System.Drawing.Point(456, 370);
            this.btnHealthInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHealthInput.Name = "btnHealthInput";
            this.btnHealthInput.Size = new System.Drawing.Size(213, 78);
            this.btnHealthInput.TabIndex = 2;
            this.btnHealthInput.Text = "건강 데이터 입력";
            this.btnHealthInput.UseVisualStyleBackColor = true;
            this.btnHealthInput.Click += new System.EventHandler(this.btnHealthInput_Click);
            // 
            // btnHospital
            // 
            this.btnHospital.Location = new System.Drawing.Point(161, 370);
            this.btnHospital.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHospital.Name = "btnHospital";
            this.btnHospital.Size = new System.Drawing.Size(213, 78);
            this.btnHospital.TabIndex = 3;
            this.btnHospital.Text = "병원 정보 입력";
            this.btnHospital.UseVisualStyleBackColor = true;
            this.btnHospital.Click += new System.EventHandler(this.btnHospital_Click);
            // 
            // btnChart
            // 
            this.btnChart.Location = new System.Drawing.Point(736, 214);
            this.btnChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(213, 78);
            this.btnChart.TabIndex = 4;
            this.btnChart.Text = "종합 분석 차트 조회";
            this.btnChart.UseVisualStyleBackColor = true;
            this.btnChart.Click += new System.EventHandler(this.btnChart_Click);
            // 
            // btnOutlier
            // 
            this.btnOutlier.Location = new System.Drawing.Point(736, 370);
            this.btnOutlier.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOutlier.Name = "btnOutlier";
            this.btnOutlier.Size = new System.Drawing.Size(213, 78);
            this.btnOutlier.TabIndex = 5;
            this.btnOutlier.Text = "이상치 조회";
            this.btnOutlier.UseVisualStyleBackColor = true;
            this.btnOutlier.Click += new System.EventHandler(this.btnOutlier_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(466, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 48);
            this.label1.TabIndex = 6;
            this.label1.Text = "메인화면";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 675);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOutlier);
            this.Controls.Add(this.btnChart);
            this.Controls.Add(this.btnHospital);
            this.Controls.Add(this.btnHealthInput);
            this.Controls.Add(this.btnThreshold);
            this.Controls.Add(this.btnEditInfo);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEditInfo;
        private System.Windows.Forms.Button btnThreshold;
        private System.Windows.Forms.Button btnHealthInput;
        private System.Windows.Forms.Button btnHospital;
        private System.Windows.Forms.Button btnChart;
        private System.Windows.Forms.Button btnOutlier;
        private System.Windows.Forms.Label label1;
    }
}