using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project;

namespace PHR_Forms
{
    public partial class HospitalForm : Form
    {
        DBClass dbc = new DBClass();

        public HospitalForm()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
            LoadHospitalHistory();
        }

        private void LoadHospitalHistory()
        {
            try
            {
                if (dbc.DS == null) dbc.DS = new DataSet();

                string query = @"
                    SELECT S.SYNC_ID, H.HOSP_NAME, S.DIAGNOSIS, S.PRESCRIPTION, S.EXAM_RESULT, S.VISIT_DATE 
                    FROM HOSP_SYNC_DATA S, HOSPITAL_INFO H 
                    WHERE S.HOSP_CODE = H.HOSP_CODE 
                      AND S.MEMBER_ID = :id 
                    ORDER BY S.VISIT_DATE DESC";

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                dbc.DBAdapter = new OracleDataAdapter();
                dbc.DBAdapter.SelectCommand = new OracleCommand(query, dbc.Connection);
                dbc.DBAdapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = UserSession.MemberId;

                if (dbc.DS.Tables.Contains("HOSP_HISTORY"))
                    dbc.DS.Tables["HOSP_HISTORY"].Clear();

                dbc.DBAdapter.Fill(dbc.DS, "HOSP_HISTORY");

                dgvList.DataSource = dbc.DS.Tables["HOSP_HISTORY"];
                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("목록 조회 실패: " + ex.Message);
            }
        }

        private string GetOrCreateHospitalCode(string hospName)
        {
            string checkSql = "SELECT HOSP_CODE FROM HOSPITAL_INFO WHERE HOSP_NAME = :name";
            OracleCommand cmd = new OracleCommand(checkSql, dbc.Connection);
            cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = hospName;

            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                string newCode = "H" + DateTime.Now.ToString("mmssfff");

                string insertSql = "INSERT INTO HOSPITAL_INFO (HOSP_CODE, HOSP_NAME) VALUES (:code, :name)";
                OracleCommand insertCmd = new OracleCommand(insertSql, dbc.Connection);
                insertCmd.Parameters.Add("code", OracleDbType.Varchar2).Value = newCode;
                insertCmd.Parameters.Add("name", OracleDbType.Varchar2).Value = hospName;
                insertCmd.ExecuteNonQuery();

                return newCode;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHospName.Text) || string.IsNullOrEmpty(txtDiagnosis.Text))
                {
                    MessageBox.Show("병원명과 진단명은 필수입니다.", "알림");
                    return;
                }

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                string hospCode = GetOrCreateHospitalCode(txtHospName.Text.Trim());

                string query = @"
                    INSERT INTO HOSP_SYNC_DATA (SYNC_ID, MEMBER_ID, HOSP_CODE, DIAGNOSIS, PRESCRIPTION, EXAM_RESULT, VISIT_DATE) 
                    VALUES (SEQ_HOSP_SYNC_ID.NEXTVAL, :memberId, :hospCode, :diag, :meds, :exam, SYSDATE)";

                OracleCommand cmd = new OracleCommand(query, dbc.Connection);
                cmd.Parameters.Add("memberId", OracleDbType.Int32).Value = UserSession.MemberId;
                cmd.Parameters.Add("hospCode", OracleDbType.Varchar2).Value = hospCode;
                cmd.Parameters.Add("diag", OracleDbType.Varchar2).Value = txtDiagnosis.Text.Trim();
                cmd.Parameters.Add("meds", OracleDbType.Varchar2).Value = txtMeds.Text.Trim();
                cmd.Parameters.Add("exam", OracleDbType.Varchar2).Value = txtExam.Text.Trim();

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("진료 기록이 추가되었습니다.", "성공");

                    txtHospName.Clear();
                    txtDiagnosis.Clear();
                    txtMeds.Clear();
                    txtExam.Clear();

                    LoadHospitalHistory();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("저장 오류: " + ex.Message);
            }
        }
    }
}