using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project;
using System.Collections.Generic;

namespace PHR_Management_App
{
    // HealthInputForm.Designer.cs 파일에 UI 컨트롤이 선언되어 있음을 가정합니다.
    public partial class HealthInputForm : Form
    {
        private DBClass dbc;
        // mockMemberId를 VARCHAR2 타입으로 'M001' 사용 (DB의 MEMBER_ID 필드가 VARCHAR2 타입이라고 가정)
        // 실제 구현에서는 로그인 사용자 ID를 사용해야 합니다.
        private string mockMemberId = "M001"; // 사용자 ID (로그인 세션에서 가져와야 함)
        private Dictionary<TextBox, string> healthItemMap;
        Dictionary<string, string> itemDict = new Dictionary<string, string>()
        {
            { "혈당 수치", "IT001" },
            { "수축기 혈압", "IT002" },
            { "체온", "IT003" }
            // 필요한 경우 더 많은 항목 추가
        };
        // ====================================================================
        // 생성자 및 초기화
        // ====================================================================
        public HealthInputForm(DBClass dbClass)
        {
            // Designer.cs에 정의된 UI 요소를 초기화합니다.
            InitializeComponent();
            InitializeDataInputs();
            InitializeHealthItemMap();
            dbc = dbClass;
            this.Load += HealthInputForm_Load;

            // HospitalAddbtn 이벤트 연결
            if (HospitalAddbtn != null)
            {
                HospitalAddbtn.Click += HospitalAddbtn_Click;
            }
            // dataGridHospitalLog 초기화 설정
            if (dataGridHospitalLog != null)
            {
                dataGridHospitalLog.AutoGenerateColumns = true;
                dataGridHospitalLog.ReadOnly = true;
                dataGridHospitalLog.RowHeadersVisible = false; // 깔끔한 표시를 위해 숨김
                dataGridHospitalLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }

        private void HealthInputForm_Load(object sender, EventArgs e)
        {
            // 폼 로드 시 필요한 초기화 작업 (예: 사용자 정보 로딩 등)
        }
        private void InitializeHealthItemMap()
        {
            // 텍스트 박스 객체들이 Designer.cs에서 생성되었음을 가정하고 매핑합니다.
            healthItemMap = new Dictionary<TextBox, string>()
            {
                // 이 변수 이름들이 Designer.cs에 선언되어 있어야 합니다.
                { txtSystolic, "BP01" },
                { txtBloodSugar, "GL01" },
                { txtWeight, "WT01" },
                { txtHeartRate, "HR01" },
            };

            // ⚠️ DataAddbtn에 이벤트 연결
            if (DataAddbtn != null)
            {
                DataAddbtn.Click += DataAddbtn_Click;
            }
        }
        private void InitializeDataInputs()
        {
                      // --- 2. 임계치 항목 콤보박스 초기화 (cmbThresholdItem) ---
            if (cmbThresholdItem != null)
            {
                cmbThresholdItem.Items.Clear();
                foreach (string itemName in itemDict.Keys)
                {
                    cmbThresholdItem.Items.Add(itemName);
                }
                if (cmbThresholdItem.Items.Count > 0)
                {
                    cmbThresholdItem.SelectedIndex = 0;
                }
            }

        }
        private void Thresholdbtn_Click(object sender, EventArgs e)
        {
            // 1. 선택된 항목의 ITEM_CODE 가져오기
            string selectedItemName = cmbThresholdItem.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedItemName) || !itemDict.ContainsKey(selectedItemName))
            {
                MessageBox.Show("임계치 유형을 선택해주세요.", "선택 오류");
                return;
            }
            string itemCode = itemDict[selectedItemName];

            // 초기화
            txtUpperBound.Clear();
            txtLowerBound.Clear();

            string sql = @"
                SELECT 
                    UPPER_BOUND, 
                    LOWER_BOUND 
                FROM THRESHOLD_SETTING
                WHERE MEMBER_ID = :memberId AND ITEM_CODE = :itemCode";

            try
            {
                if (dbc.Connection.State != ConnectionState.Open) dbc.Connection.Open();

                using (OracleCommand cmd = new OracleCommand(sql, dbc.Connection))
                {
                    cmd.Parameters.Add("memberId", OracleDbType.Varchar2).Value = mockMemberId;
                    cmd.Parameters.Add("itemCode", OracleDbType.Varchar2).Value = itemCode;

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // 2. 최대/최소값 가져와서 텍스트박스에 표시
                            // DB의 NUMBER(10, 2)는 C#에서 decimal 또는 double로 읽을 수 있습니다.
                            // 읽어온 값을 문자열로 변환하여 텍스트박스에 넣습니다.
                            if (!reader.IsDBNull(0))
                            {
                                txtUpperBound.Text = reader.GetDecimal(0).ToString();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                txtLowerBound.Text = reader.GetDecimal(1).ToString();
                            }

                            MessageBox.Show($"'{selectedItemName}'의 임계치가 성공적으로 조회되었습니다.", "조회 완료");
                        }
                        else
                        {
                            MessageBox.Show($"선택하신 항목({selectedItemName})에 대한 임계치 설정 정보가 없습니다.", "정보 없음");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"임계치 조회 중 오류 발생: {ex.Message}", "조회 오류");
            }
            finally
            {
                if (dbc.Connection.State == ConnectionState.Open) dbc.Connection.Close();
            }
        }
        // ===================================================================
        // HospitalAddbtn 클릭 이벤트 핸들러: 별도의 폼 없이 병원 기록 통합 조회 (요청 사항)
        // ===================================================================
        private void HospitalAddbtn_Click(object sender, EventArgs e)
        {
            if (dbc == null)
            {
                MessageBox.Show("데이터베이스 연결 객체(dbc)가 초기화되지 않았습니다.", "FATAL 오류");
                return;
            }
            if (dataGridHospitalLog == null)
            {
                MessageBox.Show("데이터그리드 컨트롤(dataGridHospitalLog)이 초기화되지 않았습니다.", "UI 오류");
                return;
            }

            // 🚨 복원된 쿼리: HOSPITAL_INFO 테이블과의 JOIN을 포함
            string sql = @"
                SELECT
                    H.HOSP_NAME AS ""병원명"",
                    -- SYNC_DATE 컬럼의 FormatException을 방지하기 위해 명시적으로 TO_CHAR 사용
                    TO_CHAR(HSD.SYNC_DATE, 'YYYY-MM-DD HH24:MI:SS') AS ""연동일시"",
                    HSD.HOSP_CODE -- 다음 조회를 위해 코드도 함께 가져옵니다.
                FROM HOSP_SYNC_DATA HSD
                -- 🚨 스키마에 따라 HOSPITAL_INFO로 테이블 이름 변경
                JOIN HOSPITAL_INFO H ON HSD.HOSP_CODE = H.HOSP_CODE
                WHERE HSD.MEMBER_ID = :memberId
                ORDER BY HSD.SYNC_DATE DESC";

            try
            {
                using (OracleDataAdapter adapter = new OracleDataAdapter(sql, dbc.Connection))
                {
                    // 🚨 수정: MEMBER_ID는 VARCHAR2 타입입니다.
                    adapter.SelectCommand.Parameters.Add("memberId", OracleDbType.Varchar2).Value = mockMemberId;

                    DataTable dt = new DataTable();

                    if (dbc.Connection.State != ConnectionState.Open) dbc.Connection.Open();
                    adapter.Fill(dt); // 이 시점에서 FormatException이 발생하면 데이터 변환 문제일 가능성 높음

                    dataGridHospitalLog.DataSource = dt;

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show($"총 {dt.Rows.Count}건의 연동 병원 정보가 조회되었습니다.", "조회 성공");
                    }
                    else
                    {
                        dataGridHospitalLog.DataSource = null;
                        MessageBox.Show("조회된 병원 연동 정보가 없습니다.", "조회 정보 없음");
                    }
                }
            }
            catch (FormatException ex)
            {
                // FormatException이 다시 발생하면, DB의 다른 컬럼(예: SYNC_DATE가 아닌 다른 DATE 타입 컬럼)을 확인해야 합니다.
                MessageBox.Show("데이터 변환 오류 (FormatException): 데이터베이스의 날짜/숫자 형식이 C# DataTable로 로드될 때 문제가 발생했습니다. \n\n" +
                                $"상세 오류: {ex.Message}", "FormatException 진단");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"병원 연동 정보 조회 중 오류 발생 ({ex.GetType().Name}): {ex.Message}", "오류");
            }
            finally
            {
                if (dbc.Connection.State == ConnectionState.Open) dbc.Connection.Close();
            }
        }

        private void DataAddbtn_Click(object sender, EventArgs e)
        {
            // dtpMeasureDate 컨트롤을 사용하지 않으므로 측정 날짜는 무조건 '오늘'로 설정합니다.
            DateTime measureDate = DateTime.Today;

            int count = 0;

            // mockMemberId가 "M001"과 같은 문자열 ID라고 가정합니다.
            // 만약 "1001"과 같은 숫자 ID를 문자열로 저장했다면, 아래 바인딩 타입만 Varchar2로 변경해도 작동합니다.
            // 여기서는 VARCHAR2 타입으로 안전하게 바인딩하여 String.Format 오류를 방지합니다.
            string memberIdString = mockMemberId.ToString();

            try
            {
                // 1. Connection Open
                if (dbc.Connection.State != ConnectionState.Open)
                {
                    dbc.Connection.Open();
                }

                // 2. 모든 건강 입력 필드를 순회하며 값이 있는 경우에만 DB에 삽입
                foreach (var item in healthItemMap)
                {
                    TextBox txt = item.Key;
                    string itemCode = item.Value;

                    string rawValue = txt.Text.Trim();

                    if (string.IsNullOrWhiteSpace(rawValue))
                    {
                        continue;
                    }

                    // 3. 숫자로 변환 시도 (OracleException 방지)
                    // 사용자 입력 (11, 111 등)이 정수든 실수든 decimal.TryParse는 잘 처리합니다.
                    if (!decimal.TryParse(rawValue, out decimal measureValue))
                    {
                        // 이 블록은 'abc' 같은 문자가 들어왔을 때만 실행됩니다.
                        MessageBox.Show($"'{rawValue}'는 유효한 숫자가 아닙니다. '{txt.Name}'의 입력값을 확인해주세요. 이 경우 DB 접근을 시도하지 않습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 4. OracleCommand 객체 생성 및 쿼리 설정
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = dbc.Connection;

                        // MEASURE_LOG 테이블에 데이터 삽입 쿼리
                        string insertQuery = @"
                    INSERT INTO MEASURE_LOG 
                        (LOG_ID, MEMBER_ID, ITEM_CODE, MEASURE_DATE, MEASURE_VALUE, INPUT_TYPE)
                    VALUES 
                        (SEQ_MEASURE_LOG.NEXTVAL, :memberId, :itemCode, :measureDate, :measureValue, 'U')
                ";
                        cmd.CommandText = insertQuery;

                        // 5. 파라미터 바인딩 수정 (MEMBER_ID의 타입 불일치 오류 방지)

                        // 만약 MEMBER_ID 컬럼이 DB에서 VARCHAR2(20) 타입이라면:
                        cmd.Parameters.Add("memberId", OracleDbType.Varchar2).Value = 1001;

                        // 만약 MEMBER_ID 컬럼이 DB에서 NUMBER 타입이라면: (이 경우 mockMemberId가 반드시 숫자형 문자열이어야 함)
                        // cmd.Parameters.Add("memberId", OracleDbType.Int32).Value = int.Parse(memberIdString); 

                        cmd.Parameters.Add("itemCode", OracleDbType.Varchar2).Value = itemCode;
                        cmd.Parameters.Add("measureDate", OracleDbType.Date).Value = measureDate;
                        // 측정 값은 소수점도 허용하는 OracleDbType.Decimal로 정확하게 바인딩
                        cmd.Parameters.Add("measureValue", OracleDbType.Decimal).Value = measureValue;

                        // 6. 쿼리 실행
                        cmd.ExecuteNonQuery();
                        count++;
                    }

                    // 성공적으로 삽입했으면 입력 필드 초기화
                    txt.Clear();
                }

                if (count > 0)
                {
                    MessageBox.Show($"{count}개의 건강 데이터가 성공적으로 추가되었습니다.", "데이터 추가 성공");
                    // LoadHealthLog();  // 측정 기록을 다시 로드하는 메서드를 여기에 호출할 수 있습니다.
                }
                else
                {
                    MessageBox.Show("입력된 건강 데이터가 없습니다.", "알림");
                }
            }
            // 7. OracleException에 대한 상세 진단 추가
            catch (OracleException oex)
            {
                string errorMessage = $"Oracle DB 오류 ({oex.Number}): {oex.Message}";

                // 주요 Oracle DB 오류 코드 진단 (기존 진단 유지)
                switch (oex.Number)
                {
                    case 942:
                        errorMessage += "\n\n[진단: ORA-00942] 🚨 테이블 미존재/권한 부족\nMEASURE_LOG 테이블이 DB에 없거나, 사용자에게 접근 권한이 없습니다. (SELECT * FROM MEASURE_LOG; 확인)";
                        break;
                    case 2289:
                        errorMessage += "\n\n[진단: ORA-02289] 🚨 시퀀스 미존재\nSEQ_MEASURE_LOG 시퀀스가 DB에 정의되지 않았습니다. (SELECT SEQ_MEASURE_LOG.NEXTVAL FROM DUAL; 확인)";
                        break;
                    case 1400:
                        errorMessage += "\n\n[진단: ORA-01400] 🚨 NULL 값 삽입\nNULL을 허용하지 않는 컬럼에 값이 빠졌거나 MEMBER_ID가 MEMBER_INFO에 없습니다. (FK 제약조건)";
                        break;
                    case 1031:
                        errorMessage += "\n\n[진단: ORA-01031] 🚨 권한 부족\n현재 DB 사용자에게 INSERT 권한이 없습니다.";
                        break;
                    case 1722:
                        errorMessage += "\n\n[진단: ORA-01722] 🚨 숫자 오류\n컬럼에 들어갈 데이터 타입이 잘못되었습니다. MEMBER_ID 또는 MEASURE_VALUE의 DB 타입을 확인해주세요.";
                        break;
                    default:
                        errorMessage += "\n\n[진단]\nSQL 구문이나 DB 연결 정보를 다시 점검해주세요.";
                        break;
                }

                MessageBox.Show(errorMessage, $"Oracle DB 오류 발생 ({oex.Number})", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // 🚨 일반 오류 처리 (여기서 "입력 문자열의 형식이 잘못되었습니다."가 잡힐 가능성이 높음)
                MessageBox.Show($"일반 오류 발생: {ex.Message}\n\n[추가 진단: 입력 문자열 형식 오류]\n1. 'mockMemberId' 변수가 유효한 문자열인지 확인하세요. (예: M001 또는 1001)\n2. 해당 변수가 DB의 MEMBER_ID 컬럼 타입(VARCHAR2 또는 NUMBER)과 맞게 바인딩되었는지 확인하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 8. Connection Close
                if (dbc.Connection.State == ConnectionState.Open)
                {
                    dbc.Connection.Close();
                }
            }
        }

        // 회원 정보 로드 함수 (미구현)
        private void LoadMemberInfo()
        {
            // ... 기존 로직 유지
        }

        // 건강 기록 로드 함수 (미구현)
        private void LoadHealthLog()
        {
            // ... 기존 로직 유지
        }

        // 단위 조회 도우미 함수 
        private string GetMeasureUnit(string itemCode)
        {
            string unit = "";
            try
            {
                if (dbc.Connection.State != ConnectionState.Open) dbc.Connection.Open();
                string sql = $"SELECT UNIT FROM MEASURE_ITEM WHERE ITEM_CODE = '{itemCode}'";
                using (OracleCommand cmd = new OracleCommand(sql, dbc.Connection))
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        unit = reader["UNIT"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"단위 조회 실패 ({itemCode}): " + ex.Message);
            }
            finally
            {
                if (dbc.Connection.State == ConnectionState.Open) dbc.Connection.Close();
            }
            return unit;
        }

        // ITEM_CODE에 해당하는 단위 조회
        private string GetItemUnit(string itemCode)
        {
            string unit = string.Empty;
            try
            {
                if (dbc.Connection.State != ConnectionState.Open) dbc.Connection.Open();
                string sql = "SELECT UNIT FROM MEASURE_ITEM WHERE ITEM_CODE = :itemCode";

                using (OracleCommand cmd = new OracleCommand(sql, dbc.Connection))
                {
                    cmd.Parameters.Add("itemCode", OracleDbType.Varchar2).Value = itemCode;
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        unit = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"단위 조회 실패 ({itemCode}): " + ex.Message);
            }
            finally
            {
                if (dbc.Connection.State == ConnectionState.Open) dbc.Connection.Close();
            }
            return unit;
        }

        // 데이터그리드 채우기 도우미 함수 
        private void FillGrid(DataGridView grid, string sql)
        {
            try
            {
                using (OracleDataAdapter adapter = new OracleDataAdapter(sql, dbc.Connection))
                {
                    DataTable dt = new DataTable();
                    // FillGrid를 사용하는 곳에서는 연결 상태를 직접 관리해야 합니다.
                    adapter.Fill(dt);
                    grid.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("그리드 채우기 실패: " + ex.Message);
            }
        }

        // DML 실행 도우미 함수 
        private void ExecuteNonQuery(OracleCommand cmd)
        {
            try
            {
                if (dbc.Connection.State != ConnectionState.Open) dbc.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // 실패 시 외부에서 처리
                throw new Exception("DB 작업 실패: " + ex.Message);
            }
            finally
            {
                if (dbc.Connection.State == ConnectionState.Open) dbc.Connection.Close();
            }
        }

        

       



        // 이 메서드는 Designer.cs 파일에 정의되어야 하지만, 
        // 전체 코드 제공을 위해 최소한의 형태로 포함합니다. 
        // 실제 프로젝트에서는 Designer.cs 파일에 존재해야 합니다.

    }
}