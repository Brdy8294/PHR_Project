using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; 
using Oracle.DataAccess.Client;
using PHR_Project;

namespace PHR_Forms
{
    public partial class DashboardForm : Form
    {
        DBClass dbc = new DBClass();

        public DashboardForm()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();

            LoadDataAndChart("MEASURE_DATE ASC");
        }

        private void LoadDataAndChart(string orderBy)
        {
            try
            {
                if (dbc.DS == null) dbc.DS = new DataSet();

                string query = $@"
                    SELECT ITEM_CODE, MEASURE_VALUE, MEASURE_DATE 
                    FROM HEALTH_DATA 
                    WHERE MEMBER_ID = :id 
                    ORDER BY {orderBy}";

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                dbc.DBAdapter = new OracleDataAdapter();
                dbc.DBAdapter.SelectCommand = new OracleCommand(query, dbc.Connection);
                dbc.DBAdapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = UserSession.MemberId;

                if (dbc.DS.Tables.Contains("CHART_DATA"))
                    dbc.DS.Tables["CHART_DATA"].Clear();

                dbc.DBAdapter.Fill(dbc.DS, "CHART_DATA");
                DataTable dt = dbc.DS.Tables["CHART_DATA"];

                dgvList.DataSource = dt;
                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                DrawChart(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 로드 실패: " + ex.Message);
            }
        }

        private void DrawChart(DataTable dt)
        {
            chart1.Series.Clear();

            Series series = new Series("건강수치");
            series.ChartType = SeriesChartType.Column; 
            series.IsValueShownAsLabel = true; 

            foreach (DataRow row in dt.Rows)
            {
                string xValue = $"{row["ITEM_CODE"]}\n({Convert.ToDateTime(row["MEASURE_DATE"]).ToString("MM-dd")})"; // X축: 코드+날짜
                double yValue = Convert.ToDouble(row["MEASURE_VALUE"]); 

                series.Points.AddXY(xValue, yValue);
            }

            chart1.Series.Add(series);
        }

        private void btnSortDate_Click(object sender, EventArgs e)
        {
            LoadDataAndChart("MEASURE_DATE ASC");
        }

        private void btnSortValue_Click(object sender, EventArgs e)
        {
            LoadDataAndChart("MEASURE_VALUE DESC");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}