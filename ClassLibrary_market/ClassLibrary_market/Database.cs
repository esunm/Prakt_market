using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary_market
{
    public class Database
    {
        private static string connectionString = "Data Source = LAPTOP-B3GKVM66;Initial Catalog = prakt_market;Integrated Security = true;";

        /// <summary>
        /// Получение прибыли компании.
        /// </summary>
        public double Profit()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select sum([Price ($)]*Quantity) from PRODUCT, SALE where Id_product = Product";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return Convert.ToDouble(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }

        /// <summary>
        /// Получение отдела с минимальным объем реализации в день.
        /// </summary>
        public string DepartmentMin()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select Name_departmant from DEPARTMENT where Volume = (select min(Volume) from DEPARTMENT)";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0].ToString();
            }
        }

        /// <summary>
        /// Получение должности с самым высоким окладом.
        /// </summary>
        public string SalaryMax()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select Name_position from POSITION where [Salary ($)] = (select max([Salary ($)]) from POSITION)";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0].ToString();
            }
        }

        /// <summary>
        /// Получение суммы выплат всем сотрудникам.
        /// </summary>
        public double SalaryAll()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select Sum([Salary ($)]*Position) from POSITION, STAFF where Id_position = Position";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return Convert.ToDouble(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }

        /// <summary>
        /// Получение количества сотрудников, работающих на должности Salesman.
        /// </summary>
        public int Pos()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select COUNT(Position) from STAFF where Position = 1";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return Convert.ToInt32(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }

        /// <summary>
        /// Получение количества сотрудников, работающих в отделе Finance department.
        /// </summary>
        public int Dep()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select COUNT(Department) from STAFF where Department = 1";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return Convert.ToInt32(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }

        /// <summary>
        /// Получение самого дорогого товара.
        /// </summary>
        public string Product()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select Name_product from PRODUCT where [Price ($)] = (select max([Price ($)]) from PRODUCT)";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0].ToString();
            }
        }

        /// <summary>
        /// Получение наименования отдела, который сделал больше всего продаж.
        /// </summary>
        public string DepSale()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select Name_departmant from SALE, DEPARTMENT where Id_department = Department and Id_sale = (select count([Id_sale]) from SALE)";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0].ToString();
            }
        }

        /// <summary>
        /// Получение количества сотрудников, живущих во Флориде.
        /// </summary>
        public int StaffFlorda()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select COUNT(Id_staff) from STAFF where Address_h like '%Florida%'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return Convert.ToInt32(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }

        /// <summary>
        /// Получение количества денег, за самую прибыльную продажу.
        /// </summary>
        public double MaxProfitSale()
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlCommand cmd = sqlcon.CreateCommand();
                cmd.CommandText = "select max(Quantity*[Price ($)]) from PRODUCT, SALE where Id_product = Product";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataSet data = new DataSet();
                dataAdapter.Fill(data);
                sqlcon.Close();
                return Convert.ToDouble(data.Tables[0].Columns[0].Table.Rows[0].ItemArray[0]);
            }
        }
    }
}
