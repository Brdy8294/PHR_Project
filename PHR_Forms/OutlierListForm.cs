using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project;

namespace PHR_Forms
{
    public partial class OutlierListForm : Form
    {
        DBClass dbc = new DBClass();

        public OutlierListForm()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dbc.DS == null) dbc.DS = new DataSet();

                string query = @"
                    SELECT 
                        H.MEASURE_DATE AS ""측정일"",
                        H.ITEM_CODE AS ""항목"",
                        H.MEASURE_VALUE AS ""나의수치"",
                        T.UPPER_BOUND AS ""위험상한"",
                        T.LOWER_BOUND AS ""위험하한""
                    FROM HEALTH_DATA H
                    JOIN THRESHOLD_SETTING T 
                      ON H.MEMBER_ID = T.MEMBER_ID AND H.ITEM_CODE = T.ITEM_CODE
                    WHERE H.MEMBER_ID = :id
                      AND (H.MEASURE_VALUE > T.UPPER_BOUND OR H.MEASURE_VALUE < T.LOWER_BOUND)
                    ORDER BY H.MEASURE_DATE DESC";

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                dbc.DBAdapter = new OracleDataAdapter();
                dbc.DBAdapter.SelectCommand = new OracleCommand(query, dbc.Connection);
                dbc.DBAdapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = UserSession.MemberId;

                if (dbc.DS.Tables.Contains("OUTLIER_LIST"))
                    dbc.DS.Tables["OUTLIER_LIST"].Clear();

                dbc.DBAdapter.Fill(dbc.DS, "OUTLIER_LIST");

                dgvOutliers.DataSource = dbc.DS.Tables["OUTLIER_LIST"];
                dgvOutliers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dbc.DS.Tables["OUTLIER_LIST"].Rows.Count == 0)
                {
                    MessageBox.Show("다행히 위험한 이상치가 발견되지 않았습니다!", "정상");
                }
                else
                {
                    MessageBox.Show($"총 {dbc.DS.Tables["OUTLIER_LIST"].Rows.Count}건의 이상치가 발견되었습니다.\n주의가 필요합니다!", "경고");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("분석 실패: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}