using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HelloWorldLibrary.Models;

namespace HelloWorldLibrary
{
    public class DataAccess
    {
        private static string personTextFIle = "PersonText.txt";

        public static void AddNewPerson(PersonModel person)
        {
            List<PersonModel> people = GetAllPeople();

            AddPersonToPeopleList(people, person);

            List<string> lines = ConvertModelsToCsv(people);
            File.WriteAllLines(personTextFIle, lines);

            File.WriteAllLines(personTextFIle, lines);

        }

        public static void AddPersonToPeopleList(List<PersonModel> people, PersonModel person)
        {
            people.Add(person);
        }

        public static List<string> ConvertModelsToCsv(List<PersonModel> people)
        {
            List<string> output = new List<string>();
            foreach (PersonModel user in people)
            {
                output.Add($"{user.Name}");
            }
            return output;
            
        }

        public static List<PersonModel> GetAllPeople()
        {

            List<PersonModel> output = new List<PersonModel>();
            string[] content = File.ReadAllLines(personTextFIle);

            foreach(string line in content)
            {
                output.Add(new PersonModel { Name = line });
            }
            return output;
        }
    }
}
