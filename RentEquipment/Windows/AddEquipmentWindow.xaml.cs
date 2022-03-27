using Microsoft.Win32;
using RentEquipment.ClassHelper;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddEquipmentWindow.xaml
    /// </summary>
    public partial class AddEquipmentWindow : Window
    {
        public bool isEdit = false;
        EF.Product editProduct = new EF.Product();
        public string pathPhoto;
        public AddEquipmentWindow()
        {
            InitializeComponent();
            cbTypeEquipment.ItemsSource = ClassHelper.AppData.Context.TypeProduct.ToList();
            cbTypeEquipment.DisplayMemberPath = "TypeProduct1";
            dpWarranty.DisplayDateStart = DateTime.UtcNow;
            isEdit = false;
        }


        public AddEquipmentWindow(EF.Product product)
        {
            InitializeComponent();
            dpWarranty.DisplayDateStart = DateTime.UtcNow;
            isEdit = true;
            tbName.Text = product.Product1;
            tbDescription.Text = product.Description;
            tbCost.Text =Convert.ToString(product.Cost);

            cbTypeEquipment.ItemsSource = ClassHelper.AppData.Context.TypeProduct.ToList();
            cbTypeEquipment.DisplayMemberPath = "TypeProduct1";
            cbTypeEquipment.SelectedIndex = product.IDTypeProduct - 1;
            dpWarranty.SelectedDate = product.Warranty;
            if (product.img != null)
            {
                using(MemoryStream stream = new MemoryStream(product.img))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    photoEquipment.Source = bitmapImage;
                }
            }
            tbTitle.Text = "Изменение оборудования";
            btnChangeAdd.Content = "Изменить";
            editProduct = product;

        }



        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //Validation

            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Введите название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Введите описание", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbCost.Text))
            {
                MessageBox.Show("Введите стоимость", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbTypeEquipment.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите тип продукта  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dpWarranty.DisplayDate == null)
            {
                MessageBox.Show("Выберите дату гарантии  ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            //Add
            if (isEdit == false)
            {
                var addProduct = AppData.Context.Product.ToList().FirstOrDefault();
                addProduct.Product1 = tbName.Text;
                addProduct.Description = tbDescription.Text;
                addProduct.Cost =Convert.ToDecimal( tbCost.Text);
                addProduct.IDTypeProduct = cbTypeEquipment.SelectedIndex + 1;
                addProduct.Warranty = dpWarranty.DisplayDate;
                addProduct.IsRent = false;
                if (pathPhoto != null)
                {

                    addProduct.img = File.ReadAllBytes(pathPhoto);
                }

                AppData.Context.Product.Add(addProduct);
                MessageBox.Show("Оборудование добавлено");
                AppData.Context.SaveChanges();
            }

            //Change
            else
            {
                editProduct.Product1 = tbName.Text;
                editProduct.Description = tbDescription.Text;
                editProduct.Cost = Convert.ToDecimal(tbCost.Text);
                editProduct.IDTypeProduct = cbTypeEquipment.SelectedIndex + 1;
                editProduct.Warranty = dpWarranty.DisplayDate;
                if (pathPhoto != null)
                {
                    editProduct.img = File.ReadAllBytes(pathPhoto);
                }
                MessageBox.Show("Оборудование изменено");
                AppData.Context.SaveChanges();


            }
        }



        private void tbCost_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), @"[0,1,2,3,4,5,6,7,8,9,.,,,\b]");

        }

        private void tbCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tbCost.Text, "[^0-9,,,.,]"))
            {
                MessageBox.Show("Вводите цифры");
                tbCost.Text = tbCost.Text.Remove(tbCost.Text.Length - 1);
            }
        }

        private void btnAddImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if(openFile.ShowDialog() == true)
            {
                photoEquipment.Source = new BitmapImage(new Uri(openFile.FileName));
                pathPhoto = openFile.FileName;
            }

        }
    }
}
