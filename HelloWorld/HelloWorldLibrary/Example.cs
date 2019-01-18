using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldLibrary
{
    public static class Example
    {
        public static string ExampleLoadTextFIle(string file)
        {
            if (file.Length < 10)
            {
                throw new System.ArgumentException("invalid file name","file");
            }
            return "The file was correctly loaded.";
        }
    }
}
