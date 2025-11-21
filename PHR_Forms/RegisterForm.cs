using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project;

namespace PHR_Forms
{
    public partial class RegisterForm : Form
    {
        DBClass dbc = new DBClass();

        public RegisterForm()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
        }
        
        private void btnRegist_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text) || string.IsNullOrEmpty(txtPw.Text) ||
                    string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("ID, 비밀번호, 이름은 필수 입력 항목입니다.", "알림");
                    return;
                }

                string query = "INSERT INTO MEMBER_INFO (MEMBER_ID, PASSWORD, NAME, CONTACT, EMAIL) VALUES (:id, :pw, :name, :phone, :email)";

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                OracleCommand cmd = new OracleCommand(query, dbc.Connection);

                cmd.Parameters.Add("id", OracleDbType.Int32).Value = int.Parse(txtId.Text.Trim());
                cmd.Parameters.Add("pw", OracleDbType.Varchar2).Value = txtPw.Text.Trim();
                cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = txtName.Text.Trim();
                cmd.Parameters.Add("phone", OracleDbType.Varchar2).Value = txtPhone.Text.Trim();
                cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = txtEmail.Text.Trim();

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("회원가입이 완료되었습니다!", "성공");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("회원가입에 실패했습니다.", "실패");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("ID는 숫자만 입력 가능합니다.", "입력 오류");
            }
            catch (OracleException ex)
            {
                if (ex.Number == 1)
                {
                    MessageBox.Show("이미 존재하는 ID입니다. 다른 ID를 사용해주세요.", "중복 오류");
                }
                else
                {
                    MessageBox.Show("DB 오류: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
            finally
            {
                dbc.CloseConnection();
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}