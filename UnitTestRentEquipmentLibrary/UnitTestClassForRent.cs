using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestRentEquipmentLibrary
{
    [TestClass]
    public class UnitTestClassForRent
    {
        [TestMethod]
        public void TestMethodForCostRent()
        {
            //Arange
            DateTime dateStart = new DateTime(2022, 04, 29);
            DateTime dateEnd = new DateTime(2022, 05, 2);
            decimal cost = 2500;
            decimal ex = 375 ;
            //act
            decimal result = RentEquipmentLibrary.ClassForRent.costRent(dateStart,dateEnd,cost);
            //Assert
            Assert.AreEqual(ex, result);
        }

        [TestMethod]
        public void TestMethodForPercentWear()
        {
            //Arange
            decimal cost = 2500;
            int qtyDays = 10;
            decimal ex = 2475;
            //act
            decimal result = RentEquipmentLibrary.ClassForProduct.percentWear(cost ,qtyDays);
            //Assert
            Assert.AreEqual(ex, result);
        }

    }
}
