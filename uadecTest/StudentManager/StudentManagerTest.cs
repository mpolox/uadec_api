using System;
using System.Collections.Generic;
using System.Text;
using uadec.BusinessLogic;
using Xunit;

namespace uadecTest.StudentManager
{
    public class StudentManagerTest
    {
        [Fact]
        public void EqualNamesTest()
        {
            string compareString = "galván";
            string expectedString_01 = "Galvan";
            string expectedString_02 = "galvan";
            string expectedString_03 = "gálvan";
            string expectedString_04 = "GalvÁn";
            string expectedString_05 = "galván";
            string expectedString_06 = "galván ";
            Assert.True(compareString.IsEqualTo(expectedString_01));
            Assert.True(compareString.IsEqualTo(expectedString_02));
            Assert.True(compareString.IsEqualTo(expectedString_03));
            Assert.True(compareString.IsEqualTo(expectedString_04));
            Assert.True(compareString.IsEqualTo(expectedString_05));
            Assert.True(compareString.IsEqualTo(expectedString_06));
        }

        [Fact]
        public void DifferentNamesTest()
        {
            string compareString = "Marcopolo";
            string expectedString_01 = "Nacopolo";
            string expectedString_02 = "polo";
            string expectedString_03 = "Marcopoloo";
            string expectedString_04 = "Marco";
            string expectedString_05 = "Marc0polo";
            Assert.False(compareString.IsEqualTo(expectedString_01));
            Assert.False(compareString.IsEqualTo(expectedString_02));
            Assert.False(compareString.IsEqualTo(expectedString_03));
            Assert.False(compareString.IsEqualTo(expectedString_04));
            Assert.False(compareString.IsEqualTo(expectedString_05));
        }
    }
}
