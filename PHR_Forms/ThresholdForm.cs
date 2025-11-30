using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project;

namespace PHR_Forms
{
    public partial class ThresholdForm : Form
    {
        DBClass dbc = new DBClass();

        public ThresholdForm()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtType.Text) || string.IsNullOrEmpty(txtCode.Text) ||
                    string.IsNullOrEmpty(txtMax.Text) || string.IsNullOrEmpty(txtMin.Text))
                {
                    MessageBox.Show("모든 값을 입력해주세요.", "알림");
                    return;
                }

                string query = "INSERT INTO THRESHOLD_SETTING (SET_ID, MEMBER_ID, ITEM_CODE, UPPER_BOUND, LOWER_BOUND) " +
                               "VALUES (SEQ_THRESHOLD_ID.NEXTVAL, :memberId, :itemCode, :maxVal, :minVal)";

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                OracleCommand cmd = new OracleCommand(query, dbc.Connection);

                cmd.Parameters.Add("memberId", OracleDbType.Int32).Value = UserSession.MemberId;

                cmd.Parameters.Add("itemCode", OracleDbType.Varchar2).Value = txtCode.Text.Trim();

                cmd.Parameters.Add("maxVal", OracleDbType.Decimal).Value = decimal.Parse(txtMax.Text.Trim());

                cmd.Parameters.Add("minVal", OracleDbType.Decimal).Value = decimal.Parse(txtMin.Text.Trim());

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("임계치 설정이 저장되었습니다!", "성공");
                    txtType.Clear();
                    txtCode.Clear();
                    txtMax.Clear();
                    txtMin.Clear();
                }
                else
                {
                    MessageBox.Show("저장에 실패했습니다.", "실패");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("최대값과 최소값은 '숫자'만 입력해야 합니다.", "입력 오류");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }
    }
}