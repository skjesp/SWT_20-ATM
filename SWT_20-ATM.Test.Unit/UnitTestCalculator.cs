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
        [TestCase(0, 0, 5, 5)]
        [TestCase(15, 15, 15, 5)]
        public void Calculate_CorrectDirection(int x1, int y1, int x2, int y2)
        {
            var uut = new  Calculator();
            double result = uut.GetDirection2D(x1, y1, x2, y2);
        }
    }
}
