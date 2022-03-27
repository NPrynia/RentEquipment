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

namespace RentEquipment.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListEquimentWindow.xaml
    /// </summary>
    public partial class ListEquimentWindow : Window
    {
        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По названию",
            "По типу оборудования",
            "По статусу аренды",
            "По стоимости",
            "По гарантии"
           ,
        };
        public ListEquimentWindow()
        {
            InitializeComponent();
            List<EF.Product> listProduct = new List<EF.Product>();
            listProduct = listProduct.Where(i => i.isDelete == false).ToList();
            lvEquipment.ItemsSource = listProduct;
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }
        private void Filter()
        {
            List<EF.Product> listEmployee = new List<EF.Product>();
            listEmployee = ClassHelper.AppData.Context.Product.Where(i => i.isDelete == false).ToList();
            listEmployee = listEmployee.Where
                (i => i.Product1.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 i.Description.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 i.TypeProduct.TypeProduct1.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.ID).ToList();
                    lvEquipment.ItemsSource = listEmployee;
                    break;
                case 1:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.Product1).ToList();
                    lvEquipment.ItemsSource = listEmployee;
                    break;
                case 2:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.IDTypeProduct).ToList();
                    lvEquipment.ItemsSource = listEmployee;
                    break;
                case 3:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.IsRent).ToList();
                    lvEquipment.ItemsSource = listEmployee;
                    break;
                case 4:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.Warranty).ToList();
                    lvEquipment.ItemsSource = listEmployee;
                    break;
                case 5:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.Cost).ToList();
                    lvEquipment.ItemsSource = listEmployee;
                    break;
            }
            lvEquipment.ItemsSource = listEmployee;
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void DeleteEquipment()
        {


            if (lvEquipment.SelectedItem is EF.Product)
            {
                var resClick = MessageBox.Show("Удалить пользователя ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                var empl = lvEquipment.SelectedItem as EF.Product;
                empl.isDelete = true;
                ClassHelper.AppData.Context.SaveChanges();
                MessageBox.Show("Оборудование удалено");
                Filter();

            }


        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            DeleteEquipment();
        }

        private void lvEquipment_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                DeleteEquipment();
            }

        }


        private void lvEquipment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvEquipment.SelectedItem is EF.Product)
            {
                var equipment = lvEquipment.SelectedItem as EF.Product;
                AddEquipmentWindow addEquipmentWindow = new AddEquipmentWindow(equipment);
                addEquipmentWindow.ShowDialog();
                Filter();
            }
        }


        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (lvEquipment.SelectedItem is EF.Product)
            {
                var equipment = lvEquipment.SelectedItem as EF.Product;
                AddEquipmentWindow addEquipmentWindow = new AddEquipmentWindow(equipment);
                addEquipmentWindow.ShowDialog();
                Filter();
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentWindow addEquipmentWindow = new AddEquipmentWindow();
            addEquipmentWindow.ShowDialog();
            Filter();

        }
    }
}
