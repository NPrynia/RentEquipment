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
using RentEquipmentLibrary;

namespace RentEquipment.Pages
{
    /// <summary>
    /// Логика взаимодействия для OutputEquipmentPage.xaml
    /// </summary>
    public partial class OutputEquipmentPage : Page
    {
        

        public OutputEquipmentPage()
        {
            InitializeComponent();
            dpDateStartRent.SelectedDate = DateTime.Today;
            dpDateEndRent.DisplayDateStart = DateTime.UtcNow;
            dpDateEndRent.DisplayDateEnd = DateTime.UtcNow.AddMonths(6);
            var employeeCollection = ClassHelper.AppData.Context.Employee.ToList();
            var clientCollection = ClassHelper.AppData.Context.Client.ToList();
            var productCollection = ClassHelper.AppData.Context.Product.Where(i => i.IsRent == false).ToList();


            var employees = from p in employeeCollection select new { p.FIO }.FIO;

            var clietns = from p in clientCollection select new { p.FIO }.FIO;

            var products = from p in productCollection select new { p.Product1 }.Product1;

            cbFIOEmployee.ItemsSource = employees.ToList();

            cbFIOClient.ItemsSource = clietns.ToList();

            cbProduct.ItemsSource = products.ToList();

           

           
        }


        /// <summary>
        /// Event for changing CoomboBoxText
        /// Search Employee
        /// </summary>
        void cbFIOEmployee_TextChanged(object sender, RoutedEventArgs e)
        {
            cbFIOEmployee.IsDropDownOpen = true;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(cbFIOEmployee.ItemsSource);
            cv.Filter = s =>
                ((string)s).IndexOf(cbFIOEmployee.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;

        }

        /// <summary>
        /// Event for changing CoomboBoxText
        /// Search product
        /// rentCost depand prooduct and Date rent
        /// </summary>
        private void cbProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            var productCollection = ClassHelper.AppData.Context.Product.ToList();

            var cost = from p in productCollection
                       where p.Product1 == cbProduct.Text
                       select new { p.Cost }.Cost;

            cbProduct.IsDropDownOpen = true;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(cbProduct.ItemsSource);
            cv.Filter = s =>
                ((string)s).IndexOf(cbProduct.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;

            try
            {
                DateTime start = DateTime.Parse(Convert.ToString(dpDateStartRent.SelectedDate));
                DateTime end = DateTime.Parse(Convert.ToString(dpDateEndRent.SelectedDate));
                tbCost.Text = Convert.ToString(Convert.ToString(RentEquipmentLibrary.ClassForRent.costRent(start, end, cost.ToList()[0])));
            }
            catch
            { 

            }
        
        }

        /// <summary>
        /// Event for changing CoomboBoxText
        /// Search Client
        /// </summary>
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

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

            AddEquipmentWindow addEquipmentWindow = new AddEquipmentWindow();
            addEquipmentWindow.ShowDialog();
        }

        /// <summary>
        /// Add rent
        /// </summary>

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            var employeeCollection = ClassHelper.AppData.Context.Employee.ToList();
            var clientCollection = ClassHelper.AppData.Context.Client.ToList();
            var productCollection = ClassHelper.AppData.Context.Product.ToList();
            var rentCollection = ClassHelper.AppData.Context.Rent.ToList();

            if (cbProduct.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите товар  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var IDProduct = from p in productCollection
                            where p.Product1 == cbProduct.Text
                            select new { p.ID }.ID;

            var product = ClassHelper.AppData.Context.Rent.ToList().
                 Where(i => i.isDelete == false && i.IDProduct == IDProduct.ToList()[0]).
                 FirstOrDefault();

            if (product != null)
            {
                MessageBox.Show("Товар находится в аренде", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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

            

            if (dpDateEndRent.SelectedDate == null)
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


            var cost = from p in productCollection
                       where p.Product1 == cbProduct.Text
                       select new { p.Cost }.Cost;

            DateTime start = DateTime.Parse(Convert.ToString(dpDateStartRent.SelectedDate));
            DateTime end = DateTime.Parse(Convert.ToString(dpDateEndRent.SelectedDate));



            var addRent = ClassHelper.AppData.Context.Rent.ToList().FirstOrDefault();
            addRent.IDEmployee = IDemployees.ToList()[0];
            addRent.IDClient = IDClient.ToList()[0];
            addRent.IDProduct = IDProduct.ToList()[0];
            int id = IDClient.ToList()[0];
            addRent.isDelete = false;
            addRent.TimeRent = (DateTime)dpDateStartRent.SelectedDate;
            addRent.TimeRentEnd = (DateTime)dpDateEndRent.SelectedDate;
            addRent.Cost = RentEquipmentLibrary.ClassForRent.costRent(start, end, cost.ToList()[0]);
            ClassHelper.AppData.Context.Rent.Add(addRent);

            var editProduct = ClassHelper.AppData.Context.Product.ToList().
                 Where(i => i.ID == IDProduct.ToList()[0]).FirstOrDefault();

            editProduct.IsRent = true;
            ClassHelper.AppData.Context.SaveChanges();

            MessageBox.Show("Аренда добавлена");
            var productCollectionForCb = ClassHelper.AppData.Context.Product.Where(i => i.IsRent == false).ToList();
            var products = from p in productCollectionForCb select new { p.Product1 }.Product1;
            cbProduct.ItemsSource = products.ToList();
        }


      


        //public decimal costRent()
        //{
        //    decimal costRent = 0;
         

        //    try
        //    {
        //        costRent = cost.ToList()[0];
        //        DateTime start = DateTime.Parse(Convert.ToString(dpDateStartRent.SelectedDate));
        //        DateTime end = DateTime.Parse(Convert.ToString(dpDateEndRent.SelectedDate));
        //        double qtyDay = (end - start).TotalDays;
        //        if (qtyDay == 0)
        //        {
        //            qtyDay = 1;
        //        }
        //        return Math.Round((decimal)(costRent * 5 / 100)) * Convert.ToInt32(qtyDay);
        //    }
        //    catch
        //    {
        //        return 0;
        //    }


        //}


        private void dpDateEndRent_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var productCollection = ClassHelper.AppData.Context.Product.ToList();

            var cost = from p in productCollection
                       where p.Product1 == cbProduct.Text
                       select new { p.Cost }.Cost;

            try
            {
                DateTime start = DateTime.Parse(Convert.ToString(dpDateStartRent.SelectedDate));
                DateTime end = DateTime.Parse(Convert.ToString(dpDateEndRent.SelectedDate));

                tbCost.Text = Convert.ToString(RentEquipmentLibrary.ClassForRent.costRent(start, end, cost.ToList()[0]));
            }
            catch
            {

            }
          
        }
    }
}
