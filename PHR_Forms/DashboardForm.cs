using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Windows.Forms.DataVisualization.Charting;

namespace PHR_Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DateArrayBtn_Click(object sender, EventArgs e)
        {
            LoadDashboard("ANALYSIS_DATE ASC");
        }

        private void HighArrayBtn_Click(object sender, EventArgs e)
        {
            LoadDashboard("RISK_LEVEL DESC");
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadDashboard(string orderBySql)
        {
            string connStr =
                "User Id=YOUR_ID; Password=YOUR_PASSWORD; Data Source=YOUR_DB";

            string sql = $@"
            SELECT 
            ITEM_NAME AS 항목이름,
            ITEM_NO AS 항목번호,
            ANALYSIS_DATE AS 분석일자,
            RISK_LEVEL AS 위험등급,
            HEART_RATE AS 심박수,
            BLOOD_PRESSURE AS 혈압
            FROM HEALTH_ANALYSIS
            ORDER BY {orderBySql}";

            try
            {
                using (OracleConnection conn = new OracleConnection(connStr))
                using (OracleDataAdapter adapter = new OracleDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Grid
                    dataGridView1.DataSource = dt;

                    // Chart
                    DrawChart(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류: " + ex.Message);
            }
        }

        private void DrawChart(DataTable dt)
        {
            chart1.Series.Clear();

            Series series = new Series("위험등급");
            series.ChartType = SeriesChartType.Column;

            foreach (DataRow row in dt.Rows)
            {
                series.Points.AddXY(
                    row["항목이름"].ToString(),
                    Convert.ToInt32(row["위험등급"])
                );
            }

            chart1.Series.Add(series);
            chart1.ChartAreas[0].AxisX.Interval = 1;
        }
    }
}
