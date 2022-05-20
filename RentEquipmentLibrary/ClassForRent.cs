using System;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentEquipmentLibrary
{
    public  class ClassForRent
    {

        public decimal costRent(DatePicker dateStart, DatePicker dateEnd , decimal costProduct)
        {
            try
            {
                DateTime start = DateTime.Parse(Convert.ToString(dateStart.SelectedDate));
                DateTime end = DateTime.Parse(Convert.ToString(dateEnd.SelectedDate));
                double qtyDay = (end - start).TotalDays;
                if (qtyDay == 0)
                {
                    qtyDay = 1;
                }
                return Math.Round((decimal)(costProduct * 5 / 100)) * Convert.ToInt32(qtyDay);
            }
            catch
            {
                return 0;
            }


        }
    }
}
