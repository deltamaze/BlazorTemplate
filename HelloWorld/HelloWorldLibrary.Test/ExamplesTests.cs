using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using HelloWorldLibrary;
using System.IO;

namespace HelloWorldLibrary.Test
{
    public class ExamplesTests
    {
        [Fact]
        public void ExampleLoadStaticTestFile_ValidNameShouldWork()
        {
            //Assert
            string actual = Example.ExampleLoadTextFIle("ValidFIleName");

            Assert.True(actual.Length > 0);
        }
        

        [Fact]
        public void ExampleLoadStaticTestFile_InValidNameShouldWork()
        {
            //Assert
            Assert.Throws<ArgumentException>("file", () => Example.ExampleLoadTextFIle(""));//assert catches error and compared exception type
        }
    }
}
