using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using dotDash;

namespace UnitTests
{
    [TestFixture]
    public class WithDal
    {
        [Test]
        public void WithGet()
        {
            Dal dal = new Dal();
            dal.Get();
        }
    }
}
