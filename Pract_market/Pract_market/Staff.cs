using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract_market
{
    public partial class Staff : Form
    {
        private static string connectionString = "Data Source = LAPTOP-B3GKVM66;Initial Catalog = prakt_market;Integrated Security = true;";
        private SqlConnection myConnection; // подключение к базе

        public Staff()
        {
            InitializeComponent();
        }

        // Метод для обновления dataGridView1
        private void Update1(string sql)
        {
            dataGridView1.Columns.Clear();
            myConnection = new SqlConnection(connectionString);
            myConnection.Open();
            SqlCommand cmd = myConnection.CreateCommand(); // создается команда
            cmd.CommandText = sql; // добавление текста запроса 
            SqlDataAdapter adapter = new SqlDataAdapter(cmd); // мост между DataSet и SQL Server
            DataSet m_set = new DataSet(); // хранилище таблицы
            adapter.Fill(m_set); // заполнение DataSet
            dataGridView1.DataSource = m_set.Tables[0]; // заполнение dataGridView1 из таблицы
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender == button1) // добавить сотрудника 
            {
                this.Hide();
                Actions add = new Actions(true);
                add.ShowDialog();
                this.Show();
            }
            else if (sender == button2) // изменить данные сотрудника 
            {
                this.Hide();
                Id edit = new Id(true,"STAFF");
                edit.ShowDialog();
                this.Show();
                Update1("select Id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant, Name_position from STAFF, DEPARTMENT, POSITION where Id_department=Department and Id_position=Position order by [Id_staff]");

                this.Hide();
                Actions edit1 = new Actions(false);
                edit1.setId = edit.getId;
                int index = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++) // поиск нужной строки с id
                {
                    if (int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()) == edit.getId)
                    {
                        index = i;
                        break;
                    }
                }
                string[] row = new string[dataGridView1.Rows[index].Cells.Count]; // массив строк, в котором хранится строка для изменений 
                for (int i = 0; i < dataGridView1.Rows[index].Cells.Count; i++) // заполняется значениями массив строк
                {
                    row[i] = dataGridView1.Rows[index].Cells[i].Value.ToString();
                }
                edit1.Row = row;
                edit1.ShowDialog();
                this.Show();
                Update1("select Id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant, Name_position from STAFF, DEPARTMENT, POSITION where Id_department=Department and Id_position=Position order by [Id_staff]");
            }
            else if (sender == button3) // удалить сотрудника
            {
                this.Hide();
                Id del = new Id(false, "STAFF");
                del.ShowDialog();
                this.Show();
                Update1("select id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant, Name_position from STAFF, DEPARTMENT, POSITION where Id_department = Department and Id_position = Position");
            }
            else if (sender == button4) // в главное меню
            {
                Close();
            }
            else if (sender == button5) // фильтр
            {
                if (comboBox3.SelectedIndex > 0 && comboBox4.SelectedIndex > 0) // оба фильтра 
                {
                    using (SqlConnection sqlcon = new SqlConnection(connectionString))
                    {
                        sqlcon.Open();
                        SqlCommand cmd1 = sqlcon.CreateCommand();
                        cmd1.CommandText = $"select id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant as Department, Name_position as Position from STAFF, DEPARTMENT, POSITION where Id_department = Department and Id_position = Position and Name_position = '{comboBox3.Text}' and Name_department = '{comboBox4.Text}'";
                        SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd1);
                        DataSet data1 = new DataSet();
                        dataAdapter1.Fill(data1);
                        sqlcon.Close();
                        dataGridView1.DataSource = data1.Tables[0];
                    }
                }
                else if (comboBox3.SelectedIndex > 0) // фильтр для должностей
                {
                    using (SqlConnection sqlcon = new SqlConnection(connectionString))
                    {
                        sqlcon.Open();
                        SqlCommand cmd1 = sqlcon.CreateCommand();
                        cmd1.CommandText = $"select id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant as Department, Name_position as Position from STAFF, DEPARTMENT, POSITION where Id_department = Department and Id_position = Position and Name_position = '{comboBox3.Text}'";
                        SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd1);
                        DataSet data1 = new DataSet();
                        dataAdapter1.Fill(data1);
                        sqlcon.Close();
                        dataGridView1.DataSource = data1.Tables[0];
                    }
                }
                else if (comboBox4.SelectedIndex > 0) // фильтр для отделов
                {
                    using (SqlConnection sqlcon = new SqlConnection(connectionString))
                    {
                        sqlcon.Open();
                        SqlCommand cmd1 = sqlcon.CreateCommand();
                        cmd1.CommandText = $"select id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant as Department, Name_position as Position from STAFF, DEPARTMENT, POSITION where Id_department = Department and Id_position = Position and Name_departmant = '{comboBox4.Text}'";
                        SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd1);
                        DataSet data1 = new DataSet();
                        dataAdapter1.Fill(data1);
                        sqlcon.Close();
                        dataGridView1.DataSource = data1.Tables[0];
                    }
                }
                else if (comboBox3.SelectedIndex == 0 || comboBox4.SelectedIndex == 0)
                {
                    using (SqlConnection sqlcon = new SqlConnection(connectionString))
                    {
                        sqlcon.Open();
                        SqlCommand cmd1 = sqlcon.CreateCommand();
                        cmd1.CommandText = $"select id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant as Department, Name_position as Position from STAFF, DEPARTMENT, POSITION where Id_department = Department and Id_position = Position";
                        SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd1);
                        DataSet data1 = new DataSet();
                        dataAdapter1.Fill(data1);
                        sqlcon.Close();
                        dataGridView1.DataSource = data1.Tables[0];
                    }
                }
                label5.Visible = true;
                label5.Text = $"Records: {dataGridView1.RowCount - 1}";
            }
            else if (sender == button6) // сортировка
            {
                if (comboBox2.SelectedIndex == 1) // по возрастанию 
                {
                    using (SqlConnection sqlcon = new SqlConnection(connectionString))
                    {
                        sqlcon.Open();
                        SqlCommand cmd1 = sqlcon.CreateCommand();
                        cmd1.CommandText = $"select id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant as Department, Name_position as Position from STAFF, DEPARTMENT, POSITION where Id_department = Department and Id_position = Position order by {comboBox1.Text} asc";
                        SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd1);
                        DataSet data1 = new DataSet();
                        dataAdapter1.Fill(data1);
                        sqlcon.Close();
                        dataGridView1.DataSource = data1.Tables[0];
                    }
                }
                else if (comboBox2.SelectedIndex == 2) // по убыванию 
                {
                    using (SqlConnection sqlcon = new SqlConnection(connectionString))
                    {
                        sqlcon.Open();
                        SqlCommand cmd1 = sqlcon.CreateCommand();
                        cmd1.CommandText = $"select id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant as Department, Name_position as Position from STAFF, DEPARTMENT, POSITION where Id_department = Department and Id_position = Position order by {comboBox1.Text} desc";
                        SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd1);
                        DataSet data1 = new DataSet();
                        dataAdapter1.Fill(data1);
                        sqlcon.Close();
                        dataGridView1.DataSource = data1.Tables[0];
                    }
                }
            }
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant, Name_position from STAFF, DEPARTMENT, POSITION where Id_department = Department and Id_position = Position";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                dataGridView1.DataSource = data.Tables[0];

                sqlcon.Open();
                SqlCommand cmd1 = sqlcon.CreateCommand();
                cmd1.CommandText = "select Id_staff, Surname_staff, Name_staff, [Login], [Password], Address_h, STAFF.Phone, Name_departmant as [Department], Name_position as [Position] from STAFF, DEPARTMENT, POSITION where Id_department = Department and Id_position = Position";
                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(cmd1);
                DataSet data1 = new DataSet();
                dataAdapter1.Fill(data1);
                sqlcon.Close();
                dataGridView1.DataSource = data1.Tables[0];
                comboBox1.Items.Add("<Select fild>");
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    comboBox1.Items.Add(dataGridView1.Columns[i].HeaderText);
                }

                sqlcon.Open();
                SqlCommand cmd2 = sqlcon.CreateCommand();
                cmd2.CommandText = "select Name_position from POSITION";
                SqlDataAdapter dataAdapter2 = new SqlDataAdapter(cmd2);
                DataSet data2 = new DataSet();
                dataAdapter2.Fill(data2);
                sqlcon.Close();
                comboBox3.Items.Add("<Select position>");
                for (int i = 0; i < data2.Tables[0].Columns[0].Table.Rows.Count; i++)
                {
                    comboBox3.Items.Add(data2.Tables[0].Columns[0].Table.Rows[i].ItemArray[0].ToString().Trim(' '));
                }

                sqlcon.Open();
                SqlCommand cmd3 = sqlcon.CreateCommand();
                cmd3.CommandText = "select Name_departmant from DEPARTMENT";
                SqlDataAdapter dataAdapter3 = new SqlDataAdapter(cmd3);
                DataSet data3 = new DataSet();
                dataAdapter3.Fill(data3);
                sqlcon.Close();
                comboBox4.Items.Add("<Select department>");
                for (int i = 0; i < data3.Tables[0].Columns[0].Table.Rows.Count; i++)
                {
                    comboBox4.Items.Add(data3.Tables[0].Columns[0].Table.Rows[i].ItemArray[0].ToString().Trim(' '));
                }
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                button6.Enabled = false;
            }
            else
            {
                button6.Enabled = true;
            }
            if (comboBox3.SelectedIndex == 0 && comboBox4.SelectedIndex == 0)
            {
                button5.Enabled = false;
            }
            else
            {
                button5.Enabled = true;
            }
        }
    }
}
