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


namespace RentEquipment.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListRentPage.xaml
    /// </summary>
    public partial class ListRentPage : Page
    {
        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По цене",
            "По дате начала аренды",
            "По дате конца аренды"
        };
        public ListRentPage()
        {
            InitializeComponent();
            List<EF.rentList> listRent = new List<EF.rentList>();
            listRent = ClassHelper.AppData.Context.rentList.Where(i => i.isDelete == false).ToList();
            lvRent.ItemsSource = listRent;
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;

            


        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (lvRent.SelectedItem is EF.rentList)
            {
                var resClick = MessageBox.Show("Удалить аренду ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }


                var itemRentList = lvRent.SelectedItem as EF.rentList;
                var rent = ClassHelper.AppData.Context.Rent.ToList().
                  Where(i => i.ID == itemRentList.ID).FirstOrDefault();
                rent.isDelete = true;

                var product = ClassHelper.AppData.Context.Product.ToList().
                  Where(i => i.ID == rent.IDProduct).FirstOrDefault();
                product.IsRent = false;

                double qtyDay = (rent.TimeRent - rent.TimeRentEnd).TotalDays;
                product.Cost = RentEquipmentLibrary.ClassForProduct.percentWear(product.Cost, qtyDay);
                ClassHelper.AppData.Context.SaveChanges();
                MessageBox.Show("Аренда удалена");

                List<EF.rentList> listRent = new List<EF.rentList>();
                listRent = ClassHelper.AppData.Context.rentList.Where(i => i.isDelete == false).ToList();
                lvRent.ItemsSource = listRent;
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
        private void Filter()
        {
            List<EF.rentList> listRent = new List<EF.rentList>();
            listRent = ClassHelper.AppData.Context.rentList.Where(i => i.isDelete == false).ToList();
            listRent = listRent.Where
                (i => i.FIOClient.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 i.FIOEmployee.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 i.Cost.ToString().Contains(tbSearch.Text.ToLower()) ||
                 i.Product.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    listRent = listRent.OrderBy(i => i.ID).ToList();
                    lvRent.ItemsSource = listRent;
                    break;
                case 1:
                    listRent = listRent.OrderBy(i => i.Cost).ToList();
                    lvRent.ItemsSource = listRent;
                    break;
                case 2:
                    listRent = listRent.OrderBy(i => i.TimeRent).ToList();
                    lvRent.ItemsSource = listRent;
                    break;
                case 3:
                    listRent = listRent.OrderBy(i => i.TimeRentEnd).ToList();
                    lvRent.ItemsSource = listRent;
                    break;
            }
            lvRent.ItemsSource = listRent;
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }


        private void btnExtend_Click(object sender, RoutedEventArgs e)
        {
            if (lvRent.SelectedItem is EF.rentList)
            {
                var rent = lvRent.SelectedItem as EF.rentList;
                Windows.extendTimeRentWindow extendTimeRentWindow = new Windows.extendTimeRentWindow(rent);
                extendTimeRentWindow.ShowDialog();
                Filter();
            }
            else
            {
                MessageBox.Show("Выберите аренду");
            }
          
        }
    }
}
