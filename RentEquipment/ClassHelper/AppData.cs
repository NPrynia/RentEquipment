using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentEquipment.EF;

namespace RentEquipment.ClassHelper
{
   public class AppData
    {
        public static DBRentEquipmentEntities2 Context { get; } = new DBRentEquipmentEntities2();
    }
}
