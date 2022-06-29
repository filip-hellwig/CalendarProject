using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Tests
{
    [TestClass()]
    public class GlobalsTests
    {
        [TestMethod()]
        public void addWeekTest()
        {
            Globals.numWeek = 5;
            Globals.addWeek();
            Assert.IsTrue(Globals.numWeek == 5);
        }
    }
}