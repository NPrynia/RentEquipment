using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentEquipmentLibrary
{
    public class ClassForRent
    {

        public static decimal costRent(DateTime dateStart, DateTime dateEnd, decimal costProduct)
        {
            try
            {
               
                double qtyDay = (dateEnd - dateStart).TotalDays;
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
