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
    /// Логика взаимодействия для extendTimeRentWindow.xaml
    /// </summary>
    public partial class extendTimeRentWindow : Window
    {

        EF.rentList rent;

        public extendTimeRentWindow(EF.rentList rentList)
        {
            InitializeComponent();
            dpTimeRentEnd.DisplayDateStart = rentList.TimeRentEnd;
            rent = rentList;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
            }
        }

        private void btnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(dpTimeRentEnd.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату");
                return;
            }
            rent.TimeRentEnd= (DateTime)dpTimeRentEnd.SelectedDate;
            this.Close();
        }
    }
}
