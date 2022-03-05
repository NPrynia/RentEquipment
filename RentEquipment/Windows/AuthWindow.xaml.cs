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
using RentEquipment.Windows;
using RentEquipment.EF;
using RentEquipment.ClassHelper;

namespace RentEquipment.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {

        public AuthWindow()
        {
            InitializeComponent();

        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            var authUser = AppData.Context.Employee.ToList().
                Where(i => i.Login == Logintxt.Text && i.Password == Passwordtxt.Password).
                FirstOrDefault();
            if (authUser != null)
            {
                MessageBox.Show("Добро пожаловать");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }

        }
    }
}
