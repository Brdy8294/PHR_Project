using System;
using System.Windows.Forms;
using PHR_Project;

namespace PHR_Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Text = $"PHR 시스템 - {UserSession.UserName}님 환영합니다";
        }

        private void btnEditInfo_Click(object sender, EventArgs e)
        {
            MemberEditForm form = new MemberEditForm();
            form.ShowDialog();
        }

        private void btnThreshold_Click(object sender, EventArgs e)
        {
            ThresholdForm form = new ThresholdForm();
            form.ShowDialog();
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            DashboardForm form = new DashboardForm();
            form.ShowDialog();
        }

        private void btnHospital_Click(object sender, EventArgs e)
        {
            HospitalForm form = new HospitalForm();
            form.ShowDialog();
        }

        private void btnHealthInput_Click(object sender, EventArgs e)
        {
            HealthInputForm form = new HealthInputForm();
            form.ShowDialog();
        }

        private void btnOutlier_Click(object sender, EventArgs e)
        {
            OutlierListForm form = new OutlierListForm();
            form.ShowDialog();
        }
    }
}