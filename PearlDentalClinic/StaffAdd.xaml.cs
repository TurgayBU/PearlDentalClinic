using MySql.Data.MySqlClient;
using Mysqlx.Expr;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PearlDentalClinic
{
    /// <summary>
    /// StaffAdd.xaml etkileşim mantığı
    /// </summary>
    public partial class StaffAdd : Window
    {
        public StaffAdd()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string recname = Name.Text;
            string username = UUsername.Text;
            string password = PPassword.Text;
            AddStaffData(recname, username, password);

        }
        private void AddStaffData(string Recname , string Username , string Password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (var connection = new MySqlConnection(connectionString))
                try
                {
                    connection.Open();
                    string query = "INSERT INTO reception(Name,Username,Password)" +
                        "VALUES(@name,@username,@password)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", Recname);
                    cmd.Parameters.AddWithValue("@username", Username);
                    cmd.Parameters.AddWithValue("@password", Password);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Succesfully Added!");
                    Name.Clear();
                    UUsername.Clear();
                    PPassword.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminMain adminMain = new AdminMain();
            adminMain.Show();
            this.Close();
        }
    }
}
