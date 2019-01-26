using System.Collections.Generic;

namespace HelloWorldLibrary.Utilities
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T>(string sql);
        void SaveData<T>(T person, string sql);
        void UpdateData<T>(T person, string sql);
    }
}