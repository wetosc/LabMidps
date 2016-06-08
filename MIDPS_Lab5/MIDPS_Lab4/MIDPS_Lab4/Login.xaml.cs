using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;
using DLLSpecial;
namespace MIDPS_Lab5
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            DataSet rghtx = Singleton.Instance.rightsForUser(username.Text, password.Password);
            if (rghtx.Tables[0].Rows.Count >0)
            {
                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                this.Close();
                DataRow row = rghtx.Tables[0].Rows[0];
                User temp = new User(); temp.isAdmin = (bool)row[3]; temp.canViewExtra = (bool) row[4]; temp.canEdit = (bool) row[5];
                main.myController.model.currentUser = temp;
                main.modifyButtons();
                main.Show();
            }
        }
    }
}
