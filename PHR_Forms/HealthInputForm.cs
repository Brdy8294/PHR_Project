using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project; 

namespace PHR_Forms
{
    public partial class HealthInputForm : Form
    {
        DBClass dbc = new DBClass();

        public HealthInputForm()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
            LoadDataList(); // 켜지자마자 목록 조회
        }

        private void LoadDataList()
        {
            try
            {
                if (dbc.DS == null) dbc.DS = new DataSet();

                string query = "SELECT ITEM_CODE, MEASURE_VALUE, MEASURE_DATE FROM HEALTH_DATA WHERE MEMBER_ID = :id ORDER BY MEASURE_DATE DESC";

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                dbc.DBAdapter = new OracleDataAdapter();
                dbc.DBAdapter.SelectCommand = new OracleCommand(query, dbc.Connection);
                dbc.DBAdapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = UserSession.MemberId;

                if (dbc.DS.Tables.Contains("HEALTH_LIST"))
                    dbc.DS.Tables["HEALTH_LIST"].Clear();

                dbc.DBAdapter.Fill(dbc.DS, "HEALTH_LIST");

                dgvList.DataSource = dbc.DS.Tables["HEALTH_LIST"];

                
                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("목록 조회 실패: " + ex.Message);
            }
        }

        private void SaveMeasurement(string itemCode, string valueStr)
        {
            if (string.IsNullOrEmpty(valueStr)) return;

            try
            {
                string query = "INSERT INTO HEALTH_DATA (DATA_ID, MEMBER_ID, ITEM_CODE, MEASURE_VALUE, MEASURE_DATE) " +
                               "VALUES (SEQ_HEALTH_DATA_ID.NEXTVAL, :memberId, :code, :val, SYSDATE)";

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                OracleCommand cmd = new OracleCommand(query, dbc.Connection);
                cmd.Parameters.Add("memberId", OracleDbType.Int32).Value = UserSession.MemberId;
                cmd.Parameters.Add("code", OracleDbType.Varchar2).Value = itemCode;
                cmd.Parameters.Add("val", OracleDbType.Decimal).Value = decimal.Parse(valueStr);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBP.Text) && string.IsNullOrEmpty(txtSugar.Text) &&
                    string.IsNullOrEmpty(txtWeight.Text) && string.IsNullOrEmpty(txtHeight.Text) &&
                    string.IsNullOrEmpty(txtHeartRate.Text))
                {
                    MessageBox.Show("적어도 하나의 수치는 입력해야 합니다.", "알림");
                    return;
                }

                SaveMeasurement("BP01", txtBP.Text);       // 혈압
                SaveMeasurement("GL01", txtSugar.Text);    // 혈당
                SaveMeasurement("WT01", txtWeight.Text);   // 체중
                SaveMeasurement("HT01", txtHeight.Text);   // 키 (Height)
                SaveMeasurement("HR01", txtHeartRate.Text);// 심박수

                MessageBox.Show("건강 데이터가 저장되었습니다!", "성공");

                txtBP.Clear();
                txtSugar.Clear();
                txtWeight.Clear();
                txtHeight.Clear();
                txtHeartRate.Clear();

                LoadDataList();
            }
            catch (FormatException)
            {
                MessageBox.Show("수치에는 '숫자'만 입력해주세요.", "입력 오류");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }
    }
}