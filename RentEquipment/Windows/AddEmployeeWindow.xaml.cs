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
using RentEquipment.ClassHelper;

namespace RentEquipment.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        public bool isEdit = false;
        EF.Employee editEmployee = new EF.Employee();
        public AddEmployeeWindow()
        {
            InitializeComponent();
            cbRole.ItemsSource = ClassHelper.AppData.Context.Role.ToList();
            cbRole.DisplayMemberPath = "Role1";
            cbGender.ItemsSource = ClassHelper.AppData.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "Gender1";
            cbRole.SelectedIndex = 1;
            isEdit = false;
        }

        public AddEmployeeWindow(EF.Employee employee)
        {
            InitializeComponent();
            cbRole.ItemsSource = ClassHelper.AppData.Context.Role.ToList();
            cbRole.DisplayMemberPath = "Role1";
            cbGender.ItemsSource = ClassHelper.AppData.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "Gender1";
            cbRole.SelectedIndex = 1;
            isEdit = true;
            tbFirstName.Text = employee.FirstName;
            tbLastName.Text = employee.SecondName;
            tbPatronymicName.Text = employee.Patronymic;
            tbLoginName.Text = employee.Login;
            passPasswordName.Password = employee.Password;
            passRepitPasswordName.Password = employee.Password;
            cbRole.SelectedIndex = employee.IDRole - 1;
            cbGender.SelectedIndex = employee.IDGender - 1;
            tbPhone.Text = employee.Phone;
            tbTitle.Text = "Изменение сотрудника";
            btnChangeAdd.Content = "Изменить";
            editEmployee = employee;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //Validation
            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                MessageBox.Show("Введите  имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassHelper.Validation.validationFIO(tbFirstName.Text) == false)
            {
                MessageBox.Show("Некорректное имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                MessageBox.Show("Введите  фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassHelper.Validation.validationFIO(tbLastName.Text) == false)
            {
                MessageBox.Show("Некорректная фамилия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbPatronymicName.Text != "")
            {
                if (ClassHelper.Validation.validationFIO(tbPatronymicName.Text) == false)
                {
                    MessageBox.Show("Некорректное отчество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(tbLoginName.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(passPasswordName.Password))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (passPasswordName.Password != passRepitPasswordName.Password)
            {
                MessageBox.Show("Пароли не совпадают ");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                MessageBox.Show("Введите телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassHelper.Validation.validationNum(tbPhone.Text) == false)
            {
                MessageBox.Show("Некорректный телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbPhone.Text.Length < 12)
            {
                MessageBox.Show("Введите телефон полностью", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (cbGender.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите гендер  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           



            //Add
            if (isEdit == false)
            {
                var user = AppData.Context.Employee.ToList().
                   Where(i => i.Login == tbLoginName.Text).FirstOrDefault();
                if (user != null)
                {
                    MessageBox.Show("Логин занят");
                    return;
                }
                var addUser = AppData.Context.Employee.ToList().FirstOrDefault();
                addUser.FirstName = tbFirstName.Text;
                addUser.SecondName = tbLastName.Text;
                addUser.Patronymic = tbPatronymicName.Text;
                addUser.Login = tbLoginName.Text;
                addUser.Password = passPasswordName.Password;
                addUser.IDRole = cbRole.SelectedIndex + 1;
                addUser.Phone = tbPhone.Text;
                addUser.IDGender = cbGender.SelectedIndex + 1;
                AppData.Context.Employee.Add(addUser);
                MessageBox.Show("Пользователь добавлен");
                AppData.Context.SaveChanges();
            }

            //Change
            else
            {
                if (tbLoginName.Text == editEmployee.Login)
                {

                }
                else
                {
                    var user = AppData.Context.Employee.ToList().
                      Where(i => i.Login == tbLoginName.Text).FirstOrDefault();
                    if (user != null)
                    {
                        MessageBox.Show("Логин занят");
                        return;
                    }
                }


                editEmployee.FirstName = tbFirstName.Text;
                editEmployee.SecondName = tbLastName.Text;
                editEmployee.Patronymic = tbPatronymicName.Text;
                editEmployee.Login = tbLoginName.Text;
                editEmployee.Password = passPasswordName.Password;
                editEmployee.IDRole = cbRole.SelectedIndex + 1;
                editEmployee.Phone = tbPhone.Text;
                editEmployee.IDGender = cbGender.SelectedIndex + 1;
                MessageBox.Show("Пользователь изменен");
                AppData.Context.SaveChanges();
            }
        }

        private void tbRepitPasswordName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tbPhone_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), @"[0,1,2,3,4,5,6,7,8,9,\b]");

        }

        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tbPhone.Text, "[^0-9,-]"))
            {
                MessageBox.Show("Вводите цифры");
                tbPhone.Text = tbPhone.Text.Remove(tbPhone.Text.Length - 1);
            }
        }
    }
}
