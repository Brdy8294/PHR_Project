using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project;

namespace PHR_Forms
{
    public partial class Form1 : Form
    {
        DBClass dbc = new DBClass();

        public Form1()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = txtUserId.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("ID와 비밀번호를 모두 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dbc.DB_Open("select * from MEMBER_INFO where MEMBER_ID = :id and PASSWORD = :pw", "MEMBER_INFO");

                dbc.DBAdapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = int.Parse(userId);
                dbc.DBAdapter.SelectCommand.Parameters.Add("pw", OracleDbType.Varchar2).Value = password;

                dbc.DBAdapter.Fill(dbc.DS, "MEMBER_INFO");

                if (dbc.DS.Tables["MEMBER_INFO"].Rows.Count > 0)
                {
                    DataRow row = dbc.DS.Tables["MEMBER_INFO"].Rows[0];

                    UserSession.MemberId = Convert.ToInt32(row["MEMBER_ID"]);
                    UserSession.UserName = row["NAME"].ToString();

                    MessageBox.Show($"{UserSession.UserName}님 환영합니다!", "로그인 성공");
                }
                else
                {
                    MessageBox.Show("ID 또는 비밀번호가 일치하지 않습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("ID는 숫자만 입력 가능합니다.", "입력 오류");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            MessageBox.Show("회원가입 기능은 다음 단계에서 구현합니다.");
        }
    }
}