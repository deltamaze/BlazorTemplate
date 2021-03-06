﻿using System;
using System.Collections.Generic;
using System.Text;
using HelloWorldLibrary;
using HelloWorldLibrary.Models;
using Xunit;

namespace HelloWorldLibrary.Test
{
    public class DataAccessTest
    {
        [Fact]
        public void AddPersonToPeopleList_ShouldWord()
        {
            PersonModel newPerson = new PersonModel { Name = "Tim" };
            List<PersonModel> people = new List<PersonModel>();

            DataAccess.AddPersonToPeopleList(people, newPerson);

            Assert.True(people.Count == 1);

            Assert.Contains<PersonModel>(newPerson, people);
        }

        [Theory]
        [InlineData("", "Name")]
        public void AddPersonToPeopleList_ShouldFail(string name, string param) //test commit from new device
        {
            PersonModel newPerson = new PersonModel { Name = name, };
            List<PersonModel> people = new List<PersonModel>();


            Assert.Throws<ArgumentException>(param, () => DataAccess.AddPersonToPeopleList(people, newPerson));
        }
    }
}
