using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace PHR_Project
{
    public class DBClass
    {
        private int selectedRowIndex;
        private int selectedKeyValue;
        private OracleDataAdapter dBAdapter;
        private DataSet dS;
        private OracleCommandBuilder myCommandBuilder;
        private DataTable resultTable;
        private OracleConnection connection;

        private string connectionString = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = xe)));";

        public int SelectedRowIndex
        {
            get { return selectedRowIndex; }
            set { selectedRowIndex = value; }
        }

        public int SelectedKeyValue
        {
            get { return selectedKeyValue; }
            set { selectedKeyValue = value; }
        }

        public OracleDataAdapter DBAdapter
        {
            get { return dBAdapter; }
            set { dBAdapter = value; }
        }

        public DataSet DS
        {
            get { return dS; }
            set { dS = value; }
        }

        public OracleCommandBuilder MyCommandBuilder
        {
            get { return myCommandBuilder; }
            set { myCommandBuilder = value; }
        }

        public DataTable ResultTable
        {
            get { return resultTable; }
            set { resultTable = value; }
        }

        public OracleConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public DBClass()
        {
            connection = new OracleConnection(connectionString);
        }

        public void DB_Open(string query, string tableName)
        {
            try
            {
                if (DS == null) DS = new DataSet();

                DBAdapter = new OracleDataAdapter(query, connection);
                MyCommandBuilder = new OracleCommandBuilder(DBAdapter);

                if (DS.Tables.Contains(tableName))
                {
                    DS.Tables[tableName].Clear();
                }

                DBAdapter.Fill(DS, tableName);
                ResultTable = DS.Tables[tableName];
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DB_ObjCreate()
        {
            ResultTable = new DataTable();
            DS = new DataSet();
        }

        public OracleDataReader ExecuteReader(string query)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                OracleCommand cmd = new OracleCommand(query, connection);
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}