using System.Collections.Generic;
using HelloWorldLibrary.Models;

namespace HelloWorldLibrary.Logic
{
    public interface IPersonProcessor
    {
        (bool isValid, double heightInInches) ConvertHeightTextToInches(string heightText);
        PersonModel CreatePerson(string firstName, string lastName, string heightText);
        List<PersonModel> LoadPeople();
        void SavePerson(PersonModel person);
        void UpdatePerson(PersonModel person);
    }
}