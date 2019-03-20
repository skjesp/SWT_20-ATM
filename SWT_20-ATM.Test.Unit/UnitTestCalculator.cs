using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SWT_20_ATM;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SWT_20_ATM.Test.Unit
{
    [TestFixture]
    public class UnitTestCalculator
    {
        [TestCase(  0,  0,  0,   10,   0.0)]        // 0  degrees (North)
        [TestCase(  0,  0,  10,  10,  45.0)]        // 45 degrees (North East)
        [TestCase(  0,  0,  10,   0,  90.0)]        // 90 degrees (East)
        [TestCase(  0,  0,  10, -10, 135.0)]        // 135 degrees(South East)
        [TestCase(  0,  0,   0, -10, 180.0)]        // 180 degrees(South)
        [TestCase(  0,  0, -10, -10, 225.0)]        // 225 degrees(South West)
        [TestCase(  0,  0, -10,   0, 270.0)]        // 270 degrees(West)
        [TestCase(  0,  0, -10,  10, 315.0)]        // 315 degrees(North West)
        public void Calculate_GetDirection_returns_CorrectDirection(int x1, int y1, int x2, int y2, double result)
        {
            double degrees = Calculator.GetDirection2D(x1, y1, x2, y2);
            Assert.AreEqual( result, degrees);
        }

        [TestCase(  0,   0,   0,   0,    0.0)]         
        [TestCase(  0,   0,   0,  10,   10.0)]         
        [TestCase(  0,   0,  10,   0,   10.0)]         
        [TestCase(  0,  10,   0,   0,   10.0)]         
        [TestCase( 10,   0,   0,   0,   10.0)]        
        [TestCase(  0,   0,   0, -10,   10.0)]         
        [TestCase(  0,   0, -10,   0,   10.0)]         
        [TestCase(  0, -10,   0,   0,   10.0)]        
        [TestCase(-10,   0,   0,   0,   10.0)]        
        public void Calculate_GetDistance_returns_CorrectDistance(int x1, int y1, int x2, int y2, double result)
        {
            double distance = Calculator.GetDistance(x1, y1, x2,y2);
            Assert.AreEqual(result, distance);
        }


        // Format : (Hour:Min:Sec)
        [TestCase(0, 0, "00:04:00", 0, 10, "00:04:01", 10.0)]
        [TestCase(0, 0, "00:04:00", 0, 10, "00:04:10",  1.0)]
        [TestCase(0, 0, "00:04:01", 0, 10, "00:04:00", 10.0)]
        [TestCase(0, 0, "00:04:01", 0, 10, "00:04:00", 10.0)]
        [TestCase(0, 10,"00:04:01", 0,  0, "00:04:00", 10.0)]
        public void Calculate_GetSpeed_returns_CorrectSpeed(int x1, int y1, string dateStr1, int x2, int y2, string dateStr2, double result)
        {
            DateTime date1 = DateTime.Parse(dateStr1);
            DateTime date2 = DateTime.Parse(dateStr2);
            
            double speed = Calculator.GetSpeed(x1, y1, date1, x2, y2, date2);
            Assert.AreEqual(result, speed);
        }


        /// <summary>
        /// Make sure that the calculator.GetSpeed() throws a exception if both timestamps are equal
        /// </summary>
        [TestCase]
        public void Calculate_GetSpeed_infSpeed_throwException()
        {
            int x1 = 0;
            int y1 = 0;

            int x2 = 0;
            int y2 = 5;

            DateTime date1 = DateTime.Parse("00:04:00");
            DateTime date2 = DateTime.Parse("00:04:00");

            Assert.ThrowsException<ArgumentException>(() => Calculator.GetSpeed(x1, y1, date1, x2, y2, date2));
        }

    }

}
