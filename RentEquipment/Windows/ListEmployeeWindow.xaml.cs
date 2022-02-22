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

namespace RentEquipment.Windows
{
    /// <summary>
    /// Логика взаимодействия для ListEmployeeWindow.xaml
    /// </summary>
    /// 

    public partial class ListEmployeeWindow : Window
    {

        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По ФИО",
            "По роле",
            "По гендеру"
           ,

        };


        public ListEmployeeWindow()
        {
            InitializeComponent();
            lvEmployee.ItemsSource = ClassHelper.AppData.Context.Employee.ToList();
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();

        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();               
        }

        private void Filter()
        {
            List<EF.Employee> listEmployee = new List<EF.Employee>();
            listEmployee = ClassHelper.AppData.Context.Employee.ToList();
            listEmployee = listEmployee.Where
                (i => i.FIO.ToLower().Contains(tbSearch.Text.ToLower())||
                 i.Login.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 i.Phone.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.ID).ToList();
                    lvEmployee.ItemsSource = listEmployee;
                    break;
                case 1:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.FIO).ToList();
                    lvEmployee.ItemsSource = listEmployee;
                    break;
                case 2:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.IDRole).ToList();
                    lvEmployee.ItemsSource = listEmployee;
                    break;
                case 3:
                    listEmployee = listEmployee.OrderBy(Employee => Employee.IDGender).ToList();
                    lvEmployee.ItemsSource = listEmployee;
                    break;
            }

            lvEmployee.ItemsSource = listEmployee;

        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void DeleteEmployee()
        {
            var resClick = MessageBox.Show("Удалить пользователя ?", "Внимание", MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (resClick == MessageBoxResult.No)
            {
                return;
            }

            if (lvEmployee.SelectedItem is EF.Employee)
            {
                var empl = lvEmployee.SelectedItem as EF.Employee;
                ClassHelper.AppData.Context.Employee.Remove(empl);
                ClassHelper.AppData.Context.SaveChanges();
                MessageBox.Show("Пользователь удален");
                Filter();

            }


        }

        private void lvEmployee_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                DeleteEmployee();
            }

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            DeleteEmployee();
        }

        private void lvEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void lvEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvEmployee.SelectedItem is EF.Employee)
            {
                var empl = lvEmployee.SelectedItem as EF.Employee;
                AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow(empl);
                addEmployeeWindow.ShowDialog();
                Filter();
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployee.SelectedItem is EF.Employee)
            {
                var empl = lvEmployee.SelectedItem as EF.Employee;
                AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow(empl);
                addEmployeeWindow.ShowDialog();
                Filter();
            }
               

        }
    }
}
