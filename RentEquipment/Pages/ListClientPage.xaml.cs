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

namespace RentEquipment.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListClientPage.xaml
    /// </summary>
    public partial class ListClientPage : Page
    {
        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По ФИО",
            "По гендеру"
           ,
        };

        public ListClientPage()
        {
            InitializeComponent(); List<EF.Client> listClient = new List<EF.Client>();
            listClient = listClient.Where(i => i.isDelete == false).ToList();
            lvClient.ItemsSource = listClient;
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;

        }


        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            List<EF.Client> listEmployee = new List<EF.Client>();
            listEmployee = ClassHelper.AppData.Context.Client.Where(i => i.isDelete == false).ToList();
            listEmployee = listEmployee.Where
                (i => i.FIO.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 i.Phone.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.ID).ToList();
                    lvClient.ItemsSource = listEmployee;
                    break;
                case 1:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.FIO).ToList();
                    lvClient.ItemsSource = listEmployee;
                    break;
                case 2:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.IDGender).ToList();
                    lvClient.ItemsSource = listEmployee;
                    break;
            }

            lvClient.ItemsSource = listEmployee;

        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void DeleteClient()
        {
           

            if (lvClient.SelectedItem is EF.Client)
            {
                var resClick = MessageBox.Show("Удалить клиента ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                var client = lvClient.SelectedItem as EF.Client;
                client.isDelete = true;
                ClassHelper.AppData.Context.SaveChanges();
                MessageBox.Show("Пользователь удален");
                Filter();

            }


        }

        private void lvClient_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                DeleteClient();
            }

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            DeleteClient();
        }

        private void lvClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvClient.SelectedItem is EF.Client)
            {
                var client = lvClient.SelectedItem as EF.Client;
                AddClientWindow addClientWindow = new AddClientWindow(client);
                addClientWindow.ShowDialog();
                Filter();
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (lvClient.SelectedItem is EF.Client)
            {
                var client = lvClient.SelectedItem as EF.Client;
                AddClientWindow addClientWindow = new AddClientWindow(client);
                addClientWindow.ShowDialog();
                Filter();
            }


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
            Filter();

        }
    }
}

