using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace wpfloginscreen
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public Uzivatel Uzi;
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\sqle2012; Initial Catalog=LoginDB; Integrated Security=True;");
            //SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLSAMAN; User Id=sa;Password=Akolade1; Integrated Security=True;");
            //SqlConnection sqlCon = new SqlConnection();
//            sqlCon.ConnectionString = DBAccess.GetConnStr();

            try
            { 
  //              sqlCon.Open();
                /*   if (sqlCon.State == ConnectionState.Closed)
                   {
                       sqlCon.ConnectionString = "Data Source=localhost\\SQLSAMAN; User Id=sa;Password=Akolade1; Integrated Security=True;";
                       sqlCon.Open();
                   }
                  */ 
                 //   int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                  
                //int count = 1;
               
                DBAccess.createConn();
                //String query = "SELECT COUNT(1) FROM [Zakazky].[dbo].[ZADATEL] WHERE ALIAS=@Username";
                //SqlCommand sqlCmd = new SqlCommand(query, null);
                //sqlCmd.CommandType = CommandType.Text;
                //sqlCmd.Parameters.AddWithValue("@Username",txtUsername.Text);
                //sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);


                Uzi = DBAccess.VratUzivatele(txtUsername.Text);
              //  int count = Convert.ToInt32(DBAccess.executeQuery(sqlCmd));
              //if (count == 1)
              //  {
              //      MainWindow dashboard = new MainWindow();
              //      dashboard.Show();
              //      this.Close();
              //  }

                if (Uzi != null)
                {

                    if ((Uzi.Heslo != null && Uzi.Heslo == txtPassword.Password) || (Uzi.Heslo == null && txtPassword.Password==""))
                    {
                        MainWindow dashboard = new MainWindow();
                        dashboard.Persona = Uzi;
                        dashboard.Show();
                        this.Close();
                    }

                    else throw new Exception("Uživatelské jméné nebo heslo nesouhlasí.");/* MessageBox.Show("Username or password is incorrect.");*/
                }
                else
                {
                   throw new Exception("Uživatelské jméné nebo heslo nesouhlasí.");
                    //MessageBox.Show("Username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Chyba",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            finally
            {
                DBAccess.closeConn();
                //sqlCon.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = DBAccess.GetUserAlias();
        }
    }
}
