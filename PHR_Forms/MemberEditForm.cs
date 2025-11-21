using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project;

namespace PHR_Forms
{
    public partial class MemberEditForm : Form
    {
        DBClass dbc = new DBClass();

        public MemberEditForm()
        {
            InitializeComponent();
            dbc.DB_ObjCreate(); 
        }

        private void MemberEditForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtId.ReadOnly = true;

                if (dbc.DS == null)
                    dbc.DS = new DataSet();

                string query = "SELECT * FROM MEMBER_INFO WHERE MEMBER_ID = :id";

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                dbc.DBAdapter = new OracleDataAdapter();
                dbc.DBAdapter.SelectCommand = new OracleCommand(query, dbc.Connection);
                dbc.DBAdapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = UserSession.MemberId;

                if (dbc.DS.Tables.Contains("MY_INFO"))
                    dbc.DS.Tables["MY_INFO"].Clear();

                dbc.DBAdapter.Fill(dbc.DS, "MY_INFO");

                if (dbc.DS.Tables["MY_INFO"].Rows.Count > 0)
                {
                    DataRow row = dbc.DS.Tables["MY_INFO"].Rows[0];

                    txtId.Text = row["MEMBER_ID"]?.ToString() ?? "";
                    txtPw.Text = row["PASSWORD"]?.ToString() ?? "";
                    txtName.Text = row["NAME"]?.ToString() ?? "";
                    txtPhone.Text = row["CONTACT"]?.ToString() ?? "";
                    txtEmail.Text = row["EMAIL"]?.ToString() ?? "";
                }
                else
                {
                    MessageBox.Show("회원 정보를 찾을 수 없습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("정보 불러오기 실패: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("정말 수정하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string query = "UPDATE MEMBER_INFO SET PASSWORD = :pw, NAME = :name, CONTACT = :phone, EMAIL = :email WHERE MEMBER_ID = :id";

                    if (dbc.Connection.State != ConnectionState.Open)
                        dbc.Connection.Open();

                    OracleCommand cmd = new OracleCommand(query, dbc.Connection);

                    cmd.Parameters.Add("pw", OracleDbType.Varchar2).Value = txtPw.Text;
                    cmd.Parameters.Add("name", OracleDbType.Varchar2).Value = txtName.Text;
                    cmd.Parameters.Add("phone", OracleDbType.Varchar2).Value = txtPhone.Text;
                    cmd.Parameters.Add("email", OracleDbType.Varchar2).Value = txtEmail.Text;
                    cmd.Parameters.Add("id", OracleDbType.Int32).Value = int.Parse(txtId.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("정보가 수정되었습니다.", "성공");
                        UserSession.UserName = txtName.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("수정 오류: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("정말 탈퇴하시겠습니까?\n탈퇴 후에는 복구할 수 없습니다.", "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string query = "DELETE FROM MEMBER_INFO WHERE MEMBER_ID = :id";

                    if (dbc.Connection.State != ConnectionState.Open)
                        dbc.Connection.Open();

                    OracleCommand cmd = new OracleCommand(query, dbc.Connection);
                    cmd.Parameters.Add("id", OracleDbType.Int32).Value = int.Parse(txtId.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("탈퇴되었습니다. 프로그램을 종료합니다.", "알림");
                        UserSession.ClearSession();
                        Application.Restart();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("탈퇴 오류: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}