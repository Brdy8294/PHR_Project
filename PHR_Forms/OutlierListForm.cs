using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PHR_Forms
{
    public partial class OutlierListForm : Form
    {
        public OutlierListForm()
        {
            InitializeComponent();
        }

        private void MinMaxSearchBtn_Click(object sender, EventArgs e)
        {
            // 입력값 검증
            if (!double.TryParse(txtMinWarning.Text, out double minWarning) ||
                !double.TryParse(txtMaxWarning.Text, out double maxWarning))
            {
                MessageBox.Show("임계치를 올바르게 입력하세요.");
                return;
            }

            string connStr =
                "User Id=YOUR_ID; Password=YOUR_PASSWORD; Data Source=YOUR_DB";

            string sql = @"

            SELECT 
            VALUE_CODE AS 수치코드,
            VALUE_NAME AS 수치이름,
            CURRENT_VALUE AS 현재수치,
            MIN_WARNING AS 최소임계치,
            MAX_WARNING AS 최대임계치,
            MEASURE_DATE AS 측정날짜
            FROM HEALTH_VALUE
            WHERE CURRENT_VALUE < :MinWarning
            OR CURRENT_VALUE > :MaxWarning
            ORDER BY CURRENT_VALUE ASC";

            try
            {
                using (OracleConnection conn = new OracleConnection(connStr))
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                {
                    cmd.Parameters.Add(":MinWarning", OracleDbType.Double).Value = minWarning;
                    cmd.Parameters.Add(":MaxWarning", OracleDbType.Double).Value = maxWarning;

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생: " + ex.Message);
            }
        }
    }
}
