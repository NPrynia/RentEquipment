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
using RentEquipment.EF;
using RentEquipment.Windows;

namespace RentEquipment.Windows
{
    
    /// <summary>
    /// Логика взаимодействия для OutputEquipmentWindow.xaml
    /// </summary>
    public partial class OutputEquipmentWindow : Window
    {
        public OutputEquipmentWindow()
        {
            InitializeComponent();

            dpDateStartRent.SelectedDate = DateTime.Today;
            dpDateEndRent.DisplayDateStart = DateTime.UtcNow;
            dpDateEndRent.DisplayDateEnd = DateTime.UtcNow.AddMonths(6);

            var employeeCollection = ClassHelper.AppData.Context.Employee.ToList();

            var clientCollection = ClassHelper.AppData.Context.Client.ToList();

            var productCollection = ClassHelper.AppData.Context.Product.ToList();

            var employees = from p in employeeCollection select new {p.FIO }.FIO;

            var clietns = from p in clientCollection select new { p.FIO }.FIO;

            var products = from p in productCollection select new { p.Product1 }.Product1;

            cbFIOEmployee.ItemsSource = employees.ToList();

            cbFIOClient.ItemsSource = clietns.ToList();

            cbProduct.ItemsSource = products.ToList();

        }

        void cbFIOEmployee_TextChanged(object sender, RoutedEventArgs e)
        {
            cbFIOEmployee.IsDropDownOpen = true;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(cbFIOEmployee.ItemsSource);
            cv.Filter = s =>
                ((string)s).IndexOf(cbFIOEmployee.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            
        }

        private void cbProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            cbProduct.IsDropDownOpen = true;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(cbProduct.ItemsSource);
            cv.Filter = s =>
                ((string)s).IndexOf(cbProduct.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            tbCost.Text = Convert.ToString(costRent());
        }

        private void cbFIOClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            cbFIOClient.IsDropDownOpen = true;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(cbFIOClient.ItemsSource);
            cv.Filter = s =>
                ((string)s).IndexOf(cbFIOClient.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            var employeeCollection = ClassHelper.AppData.Context.Employee.ToList();

            var clientCollection = ClassHelper.AppData.Context.Client.ToList();

            var productCollection = ClassHelper.AppData.Context.Product.ToList();



            if (cbFIOEmployee.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите сотрудника  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbFIOClient.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите клиента  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (cbProduct.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите товар  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dpDateEndRent.DisplayDate == null)
            {
                MessageBox.Show("Выберите дату аренды  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            var IDemployees = from p in employeeCollection
                            where p.FIO == cbFIOEmployee.Text
                            select new { p.ID }.ID;
            var IDClient = from p in clientCollection
                           where p.FIO == cbFIOClient.Text
                              select new { p.ID }.ID;
            var IDProduct = from p in productCollection
                            where p.Product1 == cbProduct.Text
                              select new { p.ID }.ID;



            var addRent = ClassHelper.AppData.Context.Rent.ToList().FirstOrDefault();
            addRent.IDEmployee = IDemployees.ToList()[0];
            addRent.IDEmployee = IDClient.ToList()[0];
            addRent.IDEmployee = IDProduct.ToList()[0];
            int id = IDClient.ToList()[0];
            addRent.isDelete = false;
            addRent.TimeRent = (DateTime)dpDateStartRent.SelectedDate;
            addRent.TimeRentEnd = (DateTime)dpDateEndRent.SelectedDate;
            addRent.Cost = costRent();
            ClassHelper.AppData.Context.Rent.Add(addRent);
            MessageBox.Show("Аренда добавлена");
            ClassHelper.AppData.Context.SaveChanges();
            
           

        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

            AddEquipmentWindow addEquipmentWindow = new AddEquipmentWindow();
            addEquipmentWindow.ShowDialog();
        }

        public decimal costRent()
        {
            decimal costRent = 0;
            var productCollection = ClassHelper.AppData.Context.Product.ToList();

            var cost = from p in productCollection
                       where p.Product1 == cbProduct.Text
                       select new { p.Cost }.Cost;

            try
            {
                costRent = cost.ToList()[0];
                DateTime start = DateTime.Parse(Convert.ToString(dpDateStartRent.SelectedDate));
                DateTime end = DateTime.Parse(Convert.ToString(dpDateEndRent.SelectedDate));
                double qtyDay = (end - start).TotalDays;
                if (qtyDay == 0)
                {
                    qtyDay = 1;
                }
                return Math.Round((decimal)(costRent * 5 / 100)) * Convert.ToInt32(qtyDay);
            }
            catch
            {
                return 0;
            }

          
        }

        private void dpDateEndRent_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            tbCost.Text = Convert.ToString(costRent());
        }
    }

}
