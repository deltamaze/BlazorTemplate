using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HelloWorldLibrary.Test.Logic
{
    public class PersonProcessorTests
    {
        [Theory]
        [InlineData("6'8\"",true,80)]
        public void ConvertHeightTextToInches_VariousOptions(
            string heightText,
            bool expectedIsValid,
            double expectedHeightInInches)
        {

        }
    }
}
