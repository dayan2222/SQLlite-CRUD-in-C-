using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace SQLIT_CRUD_CS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // set connection
        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=dbapps.db;Version=3;New=False;Compress=True;");        
        }

        // set executequery
        private void ExecuteQuerry(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        // set localDB
        private void LoadData()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select * from tbapps";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            dataGridView1.DataSource = DT;
            sql_con.Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string txtQuery = "insert into tbapps (ID,Name) values ('" + textBox1.Text + "','"+textBox2.Text+"')";
            ExecuteQuerry(txtQuery);
            LoadData();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            string txtQuery = "update tbapps set Name = '" + textBox2.Text + "' where ID='"+textBox1.Text+"'";
            ExecuteQuerry(txtQuery);
            LoadData();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            string txtQuery = "delete from tbapps where ID = '" + textBox1.Text + "'";
            ExecuteQuerry(txtQuery);
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }
    }
}
