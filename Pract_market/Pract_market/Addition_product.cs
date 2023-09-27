using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pract_market
{
    public partial class Addition_product : Form
    {
        private bool add;
        public int id; // id изменяемой строки
        private static string connectionString = "Data Source = LAPTOP-B3GKVM66;Initial Catalog = prakt_market;Integrated Security = true;";
        public string[] row; // строка, которая будет изменятся
        public Addition_product(bool flag)
        {
            InitializeComponent();
            this.add = flag;
            if (flag)
            {
                button1.Text = "ADD";
                this.Text = "Addition product";
            }
            else if (!flag)
            {
                button1.Text = "EDIT";
                this.Text = "Editing product";
            }
        }

        private void AddRow()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"INSERT INTO PRODUCT values ('{textBox1.Text}', {comboBox1.SelectedIndex}, {int.Parse(textBox2.Text)})");          
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        private void ChangeRow()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"UPDATE PRODUCT SET Name_product = '{textBox1.Text}', Unit = {comboBox1.SelectedIndex}, [Price ($)] = {int.Parse(textBox2.Text)} WHERE Id_product = {id}");
                command.Connection = conn;
                command.ExecuteNonQuery();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0 || textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Not all fields are filled in", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (add)
                {
                    AddRow();
                }
                else if (!add)
                {
                    ChangeRow();
                }
            }           
        }

        public int setId
        {
            set { id = value; }
        }
        public string[] Row
        {
            set { row = value; }
        }

        private void Addition_product_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select Name_unit from UNIT";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                conn.Close();
                comboBox1.Items.Add("<Select unit>");
                for (int i = 0; i < data.Tables[0].Columns[0].Table.Rows.Count; i++)
                {
                    comboBox1.Items.Add(data.Tables[0].Columns[0].Table.Rows[i].ItemArray[0].ToString());
                }
            }
            if (add)
            {
                button1.Text = "ADD";
                this.Text = "Addition product";
            }
            else if (!add)
            {
                textBox1.Text = Convert.ToString(row[1]);
                comboBox1.Text = Convert.ToString(row[2]);
                textBox2.Text = Convert.ToString(row[3]);                
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender == textBox1)
            {
                if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || e.KeyChar == '_' || e.KeyChar == (char)Keys.Back)
                {
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (sender == textBox2)
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '_' || e.KeyChar == (char)Keys.Back)
                {
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}
