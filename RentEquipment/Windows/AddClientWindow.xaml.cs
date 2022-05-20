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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        bool isEdit=false;
        EF.Client editClient = new EF.Client();

        public AddClientWindow()
        {
            InitializeComponent();
            cbGender.ItemsSource = ClassHelper.AppData.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "Gender1";
            dpBirthday.DisplayDateEnd = DateTime.UtcNow.Subtract(new TimeSpan(5840, 0, 0, 0));
        }

        public AddClientWindow(EF.Client client)
        {
            InitializeComponent();
            cbGender.ItemsSource = ClassHelper.AppData.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "Gender1";

            var passportCollection = ClassHelper.AppData.Context.PassportClient.ToList();
            var IDPassport = from p in passportCollection
                             where p.IDClient == client.ID
                            select new { p.IDPassport }.IDPassport;

            var passport = ClassHelper.AppData.Context.Passport.ToList().
                 Where(i =>  i.ID == IDPassport.ToList()[0]).
                 FirstOrDefault();

            tbNumber.IsReadOnly = true;
            tbSerial.IsReadOnly = true;
            dpBirthday.IsEnabled = false;
            dpBirthday.SelectedDate = client.Birthday;
            dpBirthday.DisplayDateEnd = DateTime.UtcNow.Subtract(new TimeSpan(5840, 0, 0, 0));
            cbGender.ItemsSource = ClassHelper.AppData.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "Gender1";
            isEdit = true;
            tbFirstName.Text = client.FirstName;
            tbLastName.Text = client.SecondName;
            tbPatronymicName.Text = client.Patronymic;
            tbSerial.Text = passport.PassportSeries;
            tbNumber.Text = passport.PassportNumber;
            tbPhone.Text = client.Phone;
            tbEmail.Text = client.Email;
            cbGender.SelectedIndex = client.IDGender - 1;
            tbTitle.Text = "Изменение сотрудника";
            btnChangeAdd.Content = "Изменить";
            editClient = client;
        }

        private void btnChangeAdd_Click(object sender, RoutedEventArgs e)
        {
           
            //Validation
            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                MessageBox.Show("Введите  имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(ClassHelper.Validation.validationFIO(tbFirstName.Text)==false)
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
            
            if (string.IsNullOrWhiteSpace(tbSerial.Text))
            {
                MessageBox.Show("Введите серию пасспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassHelper.Validation.validationNum(tbSerial.Text) == false)
            {
                MessageBox.Show("Некорректная серия паспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbNumber.Text))
            {
                MessageBox.Show("Введите номер пасспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassHelper.Validation.validationNum(tbNumber.Text) == false)
            {
                MessageBox.Show("Некорректный номер паспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                MessageBox.Show("Введите email", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassHelper.Validation.validationEmail(tbEmail.Text) == false)
            {
                MessageBox.Show("Некорректная почта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (cbGender.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите гендер  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpBirthday.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату рождения  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Add
            if (isEdit == false)
            {

                var addUser = ClassHelper.AppData.Context.Client.ToList().FirstOrDefault();
                var passport = ClassHelper.AppData.Context.Passport.ToList().FirstOrDefault();
                var passportClient = ClassHelper.AppData.Context.PassportClient.ToList().FirstOrDefault();
               

                addUser.FirstName = tbFirstName.Text;
                addUser.SecondName = tbLastName.Text;
                addUser.Patronymic = tbPatronymicName.Text;
                passport.PassportSeries = tbSerial.Text;
                passport.PassportNumber = tbNumber.Text;
                passportClient.IDPassport = passport.ID;
                passportClient.IDClient = addUser.ID;
                addUser.Phone = tbPhone.Text;
                addUser.Email = tbEmail.Text;;
                addUser.IDGender = cbGender.SelectedIndex + 1;
                addUser.Birthday = (DateTime)dpBirthday.SelectedDate;
                ClassHelper.AppData.Context.Client.Add(addUser);
                ClassHelper.AppData.Context.SaveChanges();
                ClassHelper.AppData.Context.Passport.Add(passport);
                ClassHelper.AppData.Context.SaveChanges();
                ClassHelper.AppData.Context.PassportClient.Add(passportClient);
                MessageBox.Show("Клиент добавлен");
                ClassHelper.AppData.Context.SaveChanges();
            }

            //Change
            else
            {
                
                editClient.FirstName = tbFirstName.Text;
                editClient.SecondName = tbLastName.Text;
                editClient.Patronymic = tbPatronymicName.Text;
                editClient.Phone = tbPhone.Text;
                editClient.Email = tbEmail.Text;
                editClient.IDGender = cbGender.SelectedIndex + 1;
                MessageBox.Show("Пользователь изменен");
                ClassHelper.AppData.Context.SaveChanges();
            }

        }

        private void tbPhone_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), @"[0,1,2,3,4,5,6,7,8,9,\b]");

        }
      
    }
}
