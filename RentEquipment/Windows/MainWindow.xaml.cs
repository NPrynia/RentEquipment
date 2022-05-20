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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModernWpf.Controls.Primitives;
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
            contentFrame.Navigate(new Pages.OutputEquipmentPage());

        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //svPanel.IsPaneOpen = !svPanel.IsPaneOpen;
        }

        private void nv_ItemInvoked(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewItemInvokedEventArgs args)
        {

            if (args.IsSettingsInvoked == true)
            {
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.Tag.ToString();
                switch (navItemTag)
                {
                    case "Список сотрудников":
                        contentFrame.Navigate(new Pages.ListEmployeePage());
                        break;
                    case "Список клиентов":
                        contentFrame.Navigate(new Pages.ListClientPage() );
                        break;
                    case "Список оборудования":
                        contentFrame.Navigate(new Pages.ListEquipmentPage(), null);
                        break;
                    case "Список аренды":
                        contentFrame.Navigate(new Pages.ListRentPage(), null);
                        break;
                    case "Оформить аренду":
                        contentFrame.Navigate(new Pages.OutputEquipmentPage(), null);
                        break;
                    case "Выход":
                        this.Close();
                        break;


                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            { 
            }
        }

       
    }
}
