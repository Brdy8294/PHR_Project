using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using PHR_Project;

namespace PHR_Forms
{
    public partial class ThresholdForm : Form
    {
        DBClass dbc = new DBClass();

        Dictionary<string, string> itemDict = new Dictionary<string, string>()
        {
            { "수축기 혈압", "BP01" },
            { "이완기 혈압", "BP02" },
            { "공복 혈당", "GL01" },
            { "식후 혈당", "GL02" },
            { "심박수", "HR01" },
            { "체중", "WT01" },
            { "체온", "BT01" }
        };

        public ThresholdForm()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
            InitializeComboBox();
            LoadThresholdList();
        }

        private void InitializeComboBox()
        {
            cmbItem.Items.Clear();
            foreach (string itemName in itemDict.Keys)
            {
                cmbItem.Items.Add(itemName);
            }

            if (cmbItem.Items.Count > 0)
                cmbItem.SelectedIndex = 0;
        }

        private void LoadThresholdList()
        {
            try
            {
                if (dbc.DS == null) dbc.DS = new DataSet();

                string query = "SELECT SET_ID, ITEM_CODE, UPPER_BOUND, LOWER_BOUND, REG_DATE FROM THRESHOLD_SETTING WHERE MEMBER_ID = :id ORDER BY ITEM_CODE ASC";

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                dbc.DBAdapter = new OracleDataAdapter();
                dbc.DBAdapter.SelectCommand = new OracleCommand(query, dbc.Connection);
                dbc.DBAdapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = UserSession.MemberId;

                if (dbc.DS.Tables.Contains("THRESHOLD_LIST"))
                    dbc.DS.Tables["THRESHOLD_LIST"].Clear();

                dbc.DBAdapter.Fill(dbc.DS, "THRESHOLD_LIST");

                dgvList.DataSource = dbc.DS.Tables["THRESHOLD_LIST"];

                dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvList.Rows.Count > 0)
                {
                    dgvList.Columns["SET_ID"].Visible = false;

                    dgvList.Columns["ITEM_CODE"].HeaderText = "항목코드";
                    dgvList.Columns["UPPER_BOUND"].HeaderText = "최대값";
                    dgvList.Columns["LOWER_BOUND"].HeaderText = "최소값";
                    dgvList.Columns["REG_DATE"].HeaderText = "등록/수정일";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("목록 불러오기 실패: " + ex.Message);
            }
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbItem.SelectedItem != null)
            {
                string selectedName = cmbItem.SelectedItem.ToString();

                if (itemDict.ContainsKey(selectedName))
                {
                    txtCode.Text = itemDict[selectedName];
                }
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbItem.SelectedItem == null || string.IsNullOrEmpty(txtCode.Text) ||
                    string.IsNullOrEmpty(txtMax.Text) || string.IsNullOrEmpty(txtMin.Text))
                {
                    MessageBox.Show("모든 값을 입력해주세요.", "알림");
                    return;
                }

                if (dbc.Connection.State != ConnectionState.Open)
                    dbc.Connection.Open();

                string checkQuery = "SELECT COUNT(*) FROM THRESHOLD_SETTING WHERE MEMBER_ID = :id AND ITEM_CODE = :code";
                OracleCommand checkCmd = new OracleCommand(checkQuery, dbc.Connection);
                checkCmd.Parameters.Add("id", OracleDbType.Int32).Value = UserSession.MemberId;
                checkCmd.Parameters.Add("code", OracleDbType.Varchar2).Value = txtCode.Text.Trim();

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                string query = "";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = dbc.Connection;

                if (count > 0)
                {
                    query = "UPDATE THRESHOLD_SETTING SET UPPER_BOUND = :maxVal, LOWER_BOUND = :minVal, REG_DATE = SYSDATE " +
                            "WHERE MEMBER_ID = :memberId AND ITEM_CODE = :itemCode";

                    cmd.CommandText = query;
                    cmd.Parameters.Add("maxVal", OracleDbType.Decimal).Value = decimal.Parse(txtMax.Text.Trim());
                    cmd.Parameters.Add("minVal", OracleDbType.Decimal).Value = decimal.Parse(txtMin.Text.Trim());
                    cmd.Parameters.Add("memberId", OracleDbType.Int32).Value = UserSession.MemberId;
                    cmd.Parameters.Add("itemCode", OracleDbType.Varchar2).Value = txtCode.Text.Trim();
                }
                else
                {
                    query = "INSERT INTO THRESHOLD_SETTING (SET_ID, MEMBER_ID, ITEM_CODE, UPPER_BOUND, LOWER_BOUND) " +
                            "VALUES (SEQ_THRESHOLD_ID.NEXTVAL, :memberId, :itemCode, :maxVal, :minVal)";

                    cmd.CommandText = query;
                    cmd.Parameters.Add("memberId", OracleDbType.Int32).Value = UserSession.MemberId;
                    cmd.Parameters.Add("itemCode", OracleDbType.Varchar2).Value = txtCode.Text.Trim();
                    cmd.Parameters.Add("maxVal", OracleDbType.Decimal).Value = decimal.Parse(txtMax.Text.Trim());
                    cmd.Parameters.Add("minVal", OracleDbType.Decimal).Value = decimal.Parse(txtMin.Text.Trim());
                }

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    string msg = (count > 0) ? "수정되었습니다!" : "저장되었습니다!";
                    MessageBox.Show($"{cmbItem.SelectedItem} 설정이 {msg}", "성공");

                    txtMax.Clear();
                    txtMin.Clear();

                    LoadThresholdList();
                }
                else
                {
                    MessageBox.Show("작업에 실패했습니다.", "실패");
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