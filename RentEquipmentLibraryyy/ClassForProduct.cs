using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentEquipmentLibrary
{
    public class ClassForProduct
    {

        public static decimal percentWear(decimal cost,double qtyDays)
        {
            return Convert.ToDecimal((Convert.ToDouble(cost) - ((Convert.ToDouble(cost) * 0.001 * qtyDays))));
        }
    }
}
