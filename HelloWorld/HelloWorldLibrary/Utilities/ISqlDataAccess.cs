using System.Collections.Generic;
using HelloWorldLibrary.Models;

namespace HelloWorldLibrary.Utilities
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T>(string sql);
        void SaveData(PersonModel person, string sql);
        void UpdateData<T>(T person, string sql);
    }
}