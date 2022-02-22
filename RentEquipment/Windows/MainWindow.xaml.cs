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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RentEquipment.Windows;

namespace RentEquipment
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            ListEmployeeWindow employeeWindow = new ListEmployeeWindow();
            this.Hide();
            employeeWindow.ShowDialog();
            this.Show();

        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            ModernWpf.Controls.Flyout.ShowAttachedFlyout(this);
            MessageBox.Show("This is a test text!", "Some title", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            ListClientWindow listClientWindow = new ListClientWindow();
            this.Hide();
            listClientWindow.ShowDialog();
            this.Show();


        }

        private void btnEquipment_Click(object sender, RoutedEventArgs e)
        {
            ListEquimentWindow listEquimentWindow = new ListEquimentWindow();
            this.Hide();
            listEquimentWindow.ShowDialog();
            this.Show();
        }

        private void btnRent_Click(object sender, RoutedEventArgs e)
        {


        }
    }
}
