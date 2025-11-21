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

            // 프로그램 시작 시 DB 테이블 자동 생성 (팀원 편의용)
            CheckAndCreateDatabase();
        }

        private void CheckAndCreateDatabase()
        {
            try
            {
                // DB 연결 테스트
                dbc.DB_Open("select count(*) from MEMBER_INFO", "CHECK_TABLE");
            }
            catch (Exception)
            {
                try
                {
                    // 테이블이 없으면 자동 생성
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

                    // 테스트 데이터(홍길동) 입력
                    string insertSql = "INSERT INTO MEMBER_INFO (MEMBER_ID, NAME, EMAIL, CONTACT, PASSWORD) VALUES (1001, '홍길동', 'hong@test.com', '010-1234-5678', '1234')";
                    dbc.ExecuteNonQuery(insertSql);

                    MessageBox.Show("팀원용 DB 테이블이 자동 생성되었습니다.", "알림");
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("DB 초기화 오류: " + ex2.Message);
                }
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

                // [수정된 부분 시작] --------------------------------------------
                // 기존 DB_Open()은 매개변수 넣기 전에 실행해버리므로, 수동으로 순서를 맞춥니다.

                // 1. 쿼리 준비
                string query = "select * from MEMBER_INFO where MEMBER_ID = :id and PASSWORD = :pw";

                // 2. DBClass의 어댑터와 커맨드를 직접 설정 (Ver 6 방식)
                dbc.DBAdapter = new OracleDataAdapter();
                dbc.DBAdapter.SelectCommand = new OracleCommand(query, dbc.Connection);

                // 3. 매개변수 값 넣기 (이게 실행보다 먼저 와야 함!)
                dbc.DBAdapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = int.Parse(userId);
                dbc.DBAdapter.SelectCommand.Parameters.Add("pw", OracleDbType.Varchar2).Value = password;

                // 4. 이제 실행 (Fill)
                if (dbc.DS.Tables.Contains("MEMBER_INFO"))
                    dbc.DS.Tables["MEMBER_INFO"].Clear();

                dbc.DBAdapter.Fill(dbc.DS, "MEMBER_INFO");
                // [수정된 부분 끝] ----------------------------------------------

                // 5. 결과 확인
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
            MessageBox.Show("회원가입 기능은 다음 단계에서 구현합니다.");
        }
    }
}