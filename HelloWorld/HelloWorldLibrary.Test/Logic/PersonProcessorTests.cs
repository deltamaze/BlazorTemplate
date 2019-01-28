using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using HelloWorldLibrary;
using HelloWorldLibrary.Models;
using HelloWorldLibrary.Logic;
using HelloWorldLibrary.Utilities;
using Moq;
using Autofac.Extras.Moq;


namespace HelloWorldLibrary.Test.Logic
{
    public class PersonProcessorTests
    {
        [Theory]
        [InlineData("6'8\"", true, 80)]
        [InlineData("6\"8'", false, 0)]
        [InlineData("six'eight\"", false, 0)]
        public void ConvertHeightTextToInches_VariousOptions(
            string heightText,
            bool expectedIsValid,
            double expectedHeightInInches)
        {
            //He passes null because ConvertHeightTextToInches() and CreatePerson_Successful() methods do not access 
            //the database isqlight even though it is a requirement for PersonProcessor
            PersonProcessor processor = new PersonProcessor(null);

            var actual = processor.ConvertHeightTextToInches(heightText);

            Assert.Equal(expectedIsValid, actual.isValid);
            Assert.Equal(expectedHeightInInches, actual.heightInInches);
        }

        [Theory]
        [InlineData("Tim", "Corey", "6'8\"", 80)]
        [InlineData("Charitry", "Corey", "5'4\"", 64)]
        public void CreatePerson_Successful(string firstName, string lastName, string heightText, double expectedHeight)
        {
            PersonProcessor processor = new PersonProcessor(null);

            PersonModel expected = new PersonModel
            {
                FirstName = firstName,
                LastName = lastName,
                HeightInInches = expectedHeight,
                Id = 0
            };

            var actual = processor.CreatePerson(firstName, lastName, heightText);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
            Assert.Equal(expected.HeightInInches, actual.HeightInInches);

        }

        [Theory]
        [InlineData("Tim#", "Corey", "6'8\"", "firstName")]
        [InlineData("Charitry", "C88ey", "5'4\"", "lastName")]
        [InlineData("Jon", "Corey", "SixTwo", "heightText")]
        [InlineData("", "Corey", "5'11\"", "firstName")]
        public void CreatePerson_ThrowsException(string firstName, string lastName, string heightText, string expectedInvalidParameter)
        {
            PersonProcessor processor = new PersonProcessor(null);

            var ex = Record.Exception(() => processor.CreatePerson(firstName, lastName, heightText));

            Assert.NotNull(ex);
            Assert.IsType<ArgumentException>(ex);
            if (ex is ArgumentException argEx)
            {
                Assert.Equal(expectedInvalidParameter, argEx.ParamName);
            }

        }

        [Fact]
        public void LoadPeople_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISqliteDataAccess>()
                    .Setup(x => x.LoadData<PersonModel>("select * from Persons"))
                    .Returns(GetSamplePeople());
                //mock.Provide<string>("test"); was testing if the constuctor had an additional parameter
                var cls = mock.Create<PersonProcessor>();

                var expected = GetSamplePeople();
                var actual = cls.LoadPeople();

                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.Equal(expected[i].FirstName, actual[i].FirstName);
                    Assert.Equal(expected[i].LastName, actual[i].LastName);
                }

            }
        }

        private List<PersonModel> GetSamplePeople()
        {
            List<PersonModel> output = new List<PersonModel>
            {
                new PersonModel
                {
                    FirstName = "Tim",
                    LastName = "Corey"
                },
                new PersonModel
                {
                    FirstName = "Charity",
                    LastName = "Corey"
                },
                new PersonModel
                {
                    FirstName = "Jon",
                    LastName = "Corey"
                },
                new PersonModel
                {
                    FirstName = "Will",
                    LastName = "Corey"
                }
            };
            return output;

        }

        [Fact]
        public void SavePeople_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var testPerson = GetSamplePeople()[0];
                string testSql = "insert into Person (FirstName, LastName, HeightInInches) " +
              "values (@FirstName, @LastName, @HeightInInches)";
                mock.Mock<ISqliteDataAccess>()
                    .Setup(x => x.SaveData(testPerson, testSql));

                var cls = mock.Create<PersonProcessor>();
                
                cls.SavePerson(testPerson);

                mock.Mock<ISqliteDataAccess>()
                    .Verify(x => x.SaveData(testPerson, testSql), Times.Exactly(1));

            }
        }

        [Fact]
        public void UpdatePeople_ValidCall()
        {
            using (var mock = AutoMock.GetLoose())
            {
                PersonModel testPerson = GetSamplePeople()[0];
                string testSql = "update Person set FirstName = @FirstName, LastName = @LastName" +
                ", HeightInInches = @HeightInInches where Id = @Id";
                //mock ISqliteDataAccess 
                mock.Mock<ISqliteDataAccess>()
                    .Setup(x => x.UpdateData<PersonModel>(testPerson, testSql));

                var cls = mock.Create<PersonProcessor>();
                cls.UpdatePerson(testPerson);

                mock.Mock<ISqliteDataAccess>()
                    .Verify(x => x.UpdateData<PersonModel>(testPerson, testSql), Times.Exactly(1));

            }
        }

        //[Fact]
        //public void LoadPeople_ValidCall()
        //{
        //    // We installed Moq when we created this test becuase LoadPeople() actually uses an instance of ISqliteDataAccess
        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        mock.Mock<ISqliteDataAccess>()
        //            .Setup(x => x.SaveData<PersonModel>("select * from Person"))
        //            .Returns(GetSamplePeople());
        //        var cls = mock.Create<PersonProcessor>();
        //        var expected = GetSamplePeople();
        //        var actual = cls.LoadPeople();
        //        Assert.True(actual != null);
        //        Assert.Equal(expected.Count, actual.Count);
        //        for (int i = 0; i < expected.Count; i++)
        //        {
        //            Assert.Equal(expected[i].FirstName, actual[i].FirstName);
        //            Assert.Equal(expected[i].LastName, actual[i].LastName);
        //        }
        //    }

        //}
        //[Fact]
        //public void SavePeople_ValidCall()
        //{
        //    //Here is how you test a void method and how many times you called it 
        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        //var person = GetSamplePeople()[0];
        //        var person = new PersonModel
        //        {
        //            Id = 1,
        //            FirstName = "Shahin",
        //            LastName = "Ansari",
        //            HeightInInches = 80
        //        };
        //        string sql = "insert into Person (FirstName, LastName, HeightInInches) " +
        //        "values ('Shahin', 'Ansari', 80)";
        //        mock.Mock<ISqliteDataAccess>()
        //            .Setup(x => x.SaveData(person, sql));
        //        var cls = mock.Create<PersonProcessor>();
        //        cls.SavePerson(person);
        //        // There are other options besides Exactly(), such as AtLeastOnce, AtMostOnce, Between,...
        //        //The line below verifies that the SaveData() method was called exactly once. We do this because
        //        //SaveData() does not return anything for use to compare with the expected, so we have to do it this way
        //        mock.Mock<ISqliteDataAccess>().Verify(x => x.SaveData(person, sql), Times.Exactly(1));
        //    }

        //}
        //private List<PersonModel> GetSamplePeople()
        //{
        //    List<PersonModel> output = new List<PersonModel>
        //    {
        //        new PersonModel
        //        {
        //            FirstName = "Shahin",
        //            LastName = "Ansari"
        //        },
        //            new PersonModel
        //        {
        //            FirstName = "Maryam",
        //            LastName = "Ata"
        //        },
        //            new PersonModel
        //        {
        //            FirstName = "Joe",
        //            LastName = "Shmo"
        //        }
        //    };
        //    return output;
        //}
    }
}
