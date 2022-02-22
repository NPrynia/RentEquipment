using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentEquipment.EF
{
    using System;
    using System.Collections.Generic;

    public partial class Employee
    {
       
        public string FIO { get => $"{FirstName} {SecondName} {Patronymic}";}

       
    }
}

