namespace PHR_Forms
{
    partial class ThresholdForm
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
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.btnInsert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(33, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "임계치 유형 설정";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(306, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(221, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "임계치 데이터 입력";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(197, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 27);
            this.label3.TabIndex = 2;
            this.label3.Text = "임계치 유형";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(215, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 27);
            this.label4.TabIndex = 2;
            this.label4.Text = "항목코드";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(229, 245);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 27);
            this.label5.TabIndex = 2;
            this.label5.Text = "최대값";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(229, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 27);
            this.label6.TabIndex = 2;
            this.label6.Text = "최소값";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(314, 151);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(213, 28);
            this.txtType.TabIndex = 3;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(314, 197);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(213, 28);
            this.txtCode.TabIndex = 3;
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(314, 242);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(213, 28);
            this.txtMax.TabIndex = 3;
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(314, 285);
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(213, 28);
            this.txtMin.TabIndex = 3;
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(334, 358);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(144, 53);
            this.btnInsert.TabIndex = 4;
            this.btnInsert.Text = "데이터 입력";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // ThresholdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.txtMax);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ThresholdForm";
            this.Text = "ThresholdForm";
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
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.Button btnInsert;
    }
}