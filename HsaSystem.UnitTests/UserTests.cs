using FakeItEasy;
using HsaSystem.Input;
using HsaSystem.Models;
using HsaSystem.Output;
using NUnit.Framework;

namespace HsaSystem.Tests
{
  public class Tests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void UsersAge_WhenGivenTheNumberFive_WillBeFive()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      var expectedAge = 5;
      
      // Act
      var sut = new User(fakeWriter, fakeReader);
      A.CallTo(() => fakeReader.ReadLine()).Returns(expectedAge.ToString());
      sut.SetUsersAge();
      var age = sut.Age;

      // Assert
      Assert.That(age, Is.EqualTo(expectedAge));
    }

    [Test]
    public void UsersAge_WhenGivenInvalidInputFirst_WillAskUserForValidInput()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      var invalidAge = "abc";
      var validAge = 1;
      
      // Act
      var sut = new User(fakeWriter, fakeReader);
      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(invalidAge, validAge.ToString());
      sut.SetUsersAge();
      
      // Assert
      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();
      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.\n")).MustHaveHappened();
    }

    [Test]
    public void ActivityLevel_WhenGivenNumberWithinMinAndMax_ActivityLevelWillBeSet()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      const int ExpectedMin = 1;
      const int ExpectedMax = 5;
      const int ExpectedActivityLevel = 4;

      // Act
      var sut = new User(fakeWriter, fakeReader);
      A.CallTo(() => fakeReader.ReadLine()).Returns(ExpectedActivityLevel.ToString());
      sut.SetLevelAndType(ExpectedMin, ExpectedMax, LevelType.Activity);
      var actualActivityLevel = sut.ActivityLevel;

      // Assert
      Assert.That(actualActivityLevel, Is.EqualTo(ExpectedActivityLevel));
    }

    [Test]
    public void ActivityLevel_WhenGivenNumberOutsideOfMinAndMax_WillAskUserForValidInput()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      const int GivenMin = 1;
      const int GivenMax = 5;
      const int InvalidActivityLevel = 30;
      const int ExpectedActivityLevel = 4;

      // Act
      var sut = new User(fakeWriter, fakeReader);

      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidActivityLevel.ToString(), ExpectedActivityLevel.ToString());
      sut.SetLevelAndType(GivenMin, GivenMax, LevelType.Activity);

      // Assert
      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();

      // This will not get called if all input are valid numbers.
      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.\n")).MustNotHaveHappened();
      
      // Message displayed when level is outside the range.
      A.CallTo(() => fakeWriter.WriteMessage($"\nPlease enter a number between {GivenMin} and {GivenMax}.")).MustHaveHappened();
    }

    [Test]
    public void ActivityLevel_WhenGivenInvalidNumberFirst_WillAskUserForValidInput()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      const int GivenMin = 1;
      const int GivenMax = 5;
      const string InvalidActivityLevel = "abc";
      const int ExpectedActivityLevel = 4;

      // Act
      var sut = new User(fakeWriter, fakeReader);

      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidActivityLevel, ExpectedActivityLevel.ToString());
      sut.SetLevelAndType(GivenMin, GivenMax, LevelType.Activity);

      // Assert
      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();
      A.CallTo(() => fakeWriter.WriteMessage($"\nPlease Rate your Activity Level from {GivenMin} to {GivenMax}.")).MustHaveHappened();
      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.\n")).MustHaveHappened();
    }

     [Test]
    public void NutritionLevel_WhenGivenNumberWithinMinAndMax_NutritionLevelWillBeSet()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      const int ExpectedMin = 1;
      const int ExpectedMax = 5;
      const int ExpectedNutritionLevel = 4;

      // Act
      var sut = new User(fakeWriter, fakeReader);
      A.CallTo(() => fakeReader.ReadLine()).Returns(ExpectedNutritionLevel.ToString());
      sut.SetLevelAndType(ExpectedMin, ExpectedMax, LevelType.Nutrition);
      var actualNutritionLevel = sut.NutritionLevel;

      // Assert
      Assert.That(actualNutritionLevel, Is.EqualTo(ExpectedNutritionLevel));
    }

    [Test]
    public void NutritionLevel_WhenGivenNumberOutsideOfMinAndMax_WillAskUserForValidInput()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      const int GivenMin = 1;
      const int GivenMax = 5;
      const int InvalidNutritionLevel = 30;
      const int ExpectedNutritionLevel = 4;

      // Act
      var sut = new User(fakeWriter, fakeReader);

      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidNutritionLevel.ToString(), ExpectedNutritionLevel.ToString());
      sut.SetLevelAndType(GivenMin, GivenMax, LevelType.Nutrition);

      // Assert
      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();

      // This will not get called if all input are valid numbers.
      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.\n")).MustNotHaveHappened();
      
      // Message displayed when level is outside the range.
      A.CallTo(() => fakeWriter.WriteMessage($"\nPlease enter a number between {GivenMin} and {GivenMax}.")).MustHaveHappened();
    }

    [Test]
    public void NutritionLevel_WhenGivenInvalidNumberFirst_WillAskUserForValidInput()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      const int GivenMin = 1;
      const int GivenMax = 5;
      const string InvalidNutritionLevel = "abc";
      const int ExpectedNutritionLevel = 4;

      // Act
      var sut = new User(fakeWriter, fakeReader);

      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidNutritionLevel, ExpectedNutritionLevel.ToString());
      sut.SetLevelAndType(GivenMin, GivenMax, LevelType.Nutrition);

      // Assert
      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();
      A.CallTo(() => fakeWriter.WriteMessage($"\nPlease Rate your Nutrition Level from {GivenMin} to {GivenMax}.")).MustHaveHappened();
      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.\n")).MustHaveHappened();
    }
  }
}