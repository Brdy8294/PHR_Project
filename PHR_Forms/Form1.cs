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

            CheckAndCreateDatabase();
        }

        private void CheckAndCreateDatabase()
        {
            try
            {
                try
                {
                    dbc.ExecuteNonQuery("DROP TABLE MEMBER_INFO");
                }
                catch
                {
                    // 테이블이 없어서 삭제 실패한 경우는 그냥 무시하고 넘어감
                }

                string createSql = @"
                    CREATE TABLE MEMBER_INFO (
                        MEMBER_ID NUMBER(10) PRIMARY KEY,
                        NAME VARCHAR2(50) NOT NULL,
                        BIRTH_DATE DATE,
                        GENDER VARCHAR2(10),
                        EMAIL VARCHAR2(100) NOT NULL UNIQUE,
                        CONTACT VARCHAR2(20),
                        JOIN_DATE DATE DEFAULT SYSDATE,
                        PASSWORD VARCHAR2(50) NOT NULL
                    )";
                dbc.ExecuteNonQuery(createSql);

                string insertSql = "INSERT INTO MEMBER_INFO (MEMBER_ID, NAME, EMAIL, CONTACT, PASSWORD) VALUES (1001, '홍길동', 'hong@test.com', '010-1234-5678', '1234')";
                dbc.ExecuteNonQuery(insertSql);

            }
            catch (Exception ex)
            {
                MessageBox.Show("DB 초기화 중 오류: " + ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = txtUserId.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("ID와 비밀번호를 입력하세요.", "알림");
                    return;
                }

                
                string query = "select * from MEMBER_INFO where MEMBER_ID = :id and PASSWORD = :pw";

                dbc.DBAdapter = new OracleDataAdapter();
                dbc.DBAdapter.SelectCommand = new OracleCommand(query, dbc.Connection);

                dbc.DBAdapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = int.Parse(userId);
                dbc.DBAdapter.SelectCommand.Parameters.Add("pw", OracleDbType.Varchar2).Value = password;

                if (dbc.DS.Tables.Contains("MEMBER_INFO"))
                    dbc.DS.Tables["MEMBER_INFO"].Clear();

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
                    MessageBox.Show("ID 또는 비밀번호가 일치하지 않습니다.", "로그인 실패");
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
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}