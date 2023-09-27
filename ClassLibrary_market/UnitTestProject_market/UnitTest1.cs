using ClassLibrary_market;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject_market
{
    [TestClass]
    public class DataBaseTest
    {
        [TestMethod]
        public void GetProfit()
        {
            Database database = new Database();
            var expected = database.Profit();
            double actual = 33328.00;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinVolume()
        {
            Database database = new Database();
            var expected = database.DepartmentMin();
            string actual = "Marketing department";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MaxSalary()
        {
            Database database = new Database();
            var expected = database.SalaryMax();
            string actual = "Director";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SummSalary()
        {
            Database database = new Database();
            var expected = database.SalaryAll();
            double actual = 577500.00;
            Assert.AreEqual(expected, 577500);
        }

        [TestMethod]
        public void CountSalesman()
        {
            Database database = new Database();
            var expected = database.Pos();
            int actual = 5;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CountFinanceDep()
        {
            Database database = new Database();
            var expected = database.Dep();
            int actual = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExpensiveProduct()
        {
            Database database = new Database();
            var expected = database.Product();
            string actual = "Xbox Series X";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DepSale()
        {
            Database database = new Database();
            var expected = database.DepSale();
            string actual = "Marketing department";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StaffFlorida()
        {
            Database database = new Database();
            var expected = database.StaffFlorda();
            int actual = 4;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MaxProfitSale()
        {
            Database database = new Database();
            var expected = database.MaxProfitSale();
            double actual = 5700.00;
            Assert.AreEqual(expected, actual);
        }
    }
}
