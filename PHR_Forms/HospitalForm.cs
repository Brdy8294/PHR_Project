using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project; // DBClass가 있는 네임스페이스

namespace PHR_Management_App
{
    public partial class HospitalForm : Form
    {
        private DBClass dbc;
        private string mockMemberId = "M001"; // 테스트용 로그인 ID

        // 폼 디자이너에 이 이름으로 컨트롤을 만들어주세요!
        // 1. ComboBox: cmbHospitalSelection
        // 2. DataGridView: dataGridDiagnosis (진단명)
        // 3. DataGridView: dataGridMedication (투약정보)
        // 4. DataGridView: dataGridTestResult (검사결과)
        // 5. Button: btnSearch (조회 버튼)

        public HospitalForm(DBClass dbClass)
        {
            InitializeComponent();
            dbc = dbClass;
            this.Text = "병원 데이터 연동 페이지";
            SetupUI();
        }

        private void SetupUI()
        {
            // DataGridView 기본 설정
            if (dataGridDiagnosis != null) SetupGrid(dataGridDiagnosis);
            if (dataGridMedication != null) SetupGrid(dataGridMedication);
            if (dataGridTestResult != null) SetupGrid(dataGridTestResult);

            // 이벤트 연결
            this.Load += HospitalForm_Load;
            if (btnSearch != null) btnSearch.Click += SearchBtn_Click;
        }

        private void SetupGrid(DataGridView grid)
        {
            grid.AutoGenerateColumns = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
        }

        private void HospitalForm_Load(object sender, EventArgs e)
        {
            LoadHospitalDropdown();
        }

        // 1. 병원 목록(ComboBox) 로드
        private void LoadHospitalDropdown()
        {
            if (cmbHospitalSelection == null) return;

            try
            {
                // HOSPITAL_INFO 테이블 조회
                string sql = "SELECT HOSP_CODE, HOSP_NAME FROM HOSPITAL_INFO ORDER BY HOSP_NAME";

                using (OracleDataAdapter adapter = new OracleDataAdapter(sql, dbc.Connection))
                {
                    DataTable dt = new DataTable();

                    if (dbc.Connection.State != ConnectionState.Open) dbc.Connection.Open();
                    adapter.Fill(dt);
                    if (dbc.Connection.State == ConnectionState.Open) dbc.Connection.Close();

                    cmbHospitalSelection.DataSource = dt;
                    cmbHospitalSelection.DisplayMember = "HOSP_NAME";
                    cmbHospitalSelection.ValueMember = "HOSP_CODE";

                    if (cmbHospitalSelection.Items.Count > 0)
                        cmbHospitalSelection.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("병원 목록 로드 실패 (테이블 확인 필요): " + ex.Message);
            }
        }

        // 2. 조회 버튼 클릭
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (cmbHospitalSelection == null || cmbHospitalSelection.SelectedValue == null)
            {
                MessageBox.Show("병원을 선택해주세요.");
                return;
            }

            string selectedHospCode = cmbHospitalSelection.SelectedValue.ToString();
            LoadAllHealthData(mockMemberId, selectedHospCode);
        }

        // 3. 전체 데이터 로드 (진단, 투약, 검사)
        public void LoadAllHealthData(string memberId, string hospCode)
        {
            try
            {
                if (dbc.Connection.State != ConnectionState.Open) dbc.Connection.Open();

                // 3-1. 진단명 로드
                if (dataGridDiagnosis != null)
                {
                    string sql = @"
                        SELECT D.DIAGNOSIS_NAME AS 진단명, D.DIAGNOSIS_CODE AS 코드, HSD.SYNC_DATE AS 진료일
                        FROM DIAGNOSIS D
                        JOIN HOSP_SYNC_DATA HSD ON D.SYNC_ID = HSD.SYNC_ID
                        WHERE HSD.MEMBER_ID = :memberId AND HSD.HOSP_CODE = :hospCode
                        ORDER BY HSD.SYNC_DATE DESC";

                    FillGrid(dataGridDiagnosis, sql, memberId, hospCode);
                }

                // 3-2. 투약정보 로드
                if (dataGridMedication != null)
                {
                    string sql = @"
                        SELECT M.MED_NAME AS 약품명, M.DOSAGE AS 용량, M.START_DATE AS 시작일, M.END_DATE AS 종료일
                        FROM MEDICATION M
                        JOIN HOSP_SYNC_DATA HSD ON M.SYNC_ID = HSD.SYNC_ID
                        WHERE HSD.MEMBER_ID = :memberId AND HSD.HOSP_CODE = :hospCode
                        ORDER BY M.START_DATE DESC";

                    FillGrid(dataGridMedication, sql, memberId, hospCode);
                }

                // 3-3. 검사결과 로드
                if (dataGridTestResult != null)
                {
                    string sql = @"
                        SELECT I.ITEM_NAME AS 검사명, H.MEASURE_VALUE AS 결과, I.UNIT AS 단위, H.MEASURE_DATE AS 검사일
                        FROM HEALTH_DATA H
                        JOIN MEASURE_ITEM I ON H.ITEM_CODE = I.ITEM_CODE
                        JOIN HOSP_SYNC_DATA HSD ON H.SYNC_ID = HSD.SYNC_ID
                        WHERE HSD.MEMBER_ID = :memberId AND HSD.HOSP_CODE = :hospCode
                        ORDER BY H.MEASURE_DATE DESC";

                    FillGrid(dataGridTestResult, sql, memberId, hospCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 조회 오류: " + ex.Message);
            }
            finally
            {
                if (dbc.Connection.State == ConnectionState.Open) dbc.Connection.Close();
            }
        }

        // 그리드 채우기 도우미 함수
        private void FillGrid(DataGridView grid, string sql, string memberId, string hospCode)
        {
            using (OracleDataAdapter adapter = new OracleDataAdapter(sql, dbc.Connection))
            {
                adapter.SelectCommand.Parameters.Add("memberId", OracleDbType.Varchar2).Value = memberId;
                adapter.SelectCommand.Parameters.Add("hospCode", OracleDbType.Varchar2).Value = hospCode;

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                grid.DataSource = dt;
            }
        }

        private void dataGridMedication_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}