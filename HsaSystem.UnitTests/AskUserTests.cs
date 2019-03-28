using FakeItEasy;
using HsaSystem.Input;
using HsaSystem.Models;
using HsaSystem.Output;
using Moq;
using NUnit.Framework;
using Times = Moq.Times;

namespace HsaSystem.Tests
{
  public class AskUserTests
  {
    [SetUp]
    public void Setup()
    {
    }

    #region ForNumber

    [Test]
    public void ForNumber_InvokesTheWriteMessage_WithTheCorrectArgument_WithFakes()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      const string ExpectedPrompt = "hello world";
      A.CallTo(() => fakeReader.ReadLine()).Returns("5");
      var askUser = new AskUser(fakeWriter, fakeReader);

      // Act
      askUser.ForNumber(ExpectedPrompt);

      // Assert
      A.CallTo(() => fakeWriter.WriteMessage(A<string>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ForNumber_InvokesTheWriteMessage_WithTheCorrectArgument_WithMocks()
    {
      // Arrange
      var writerMock = new Mock<IWriter>();
      var readerMock = new Mock<IReader>();
      
      readerMock.Setup(x => x.ReadLine()).Returns("5");

      var askUser = new AskUser(writerMock.Object, readerMock.Object);
      var expectedPrompt = "hello world";

      // Act
      askUser.ForNumber(expectedPrompt);

      // Assert
      writerMock.Verify(x => x.WriteMessage(expectedPrompt), Times.Once);
    }

    [Test]
    public void ForNumber_InvokesTheReadLine_OnlyOnce()
    {
      // Arrange
      var writerMock = new Mock<IWriter>();
      var readerMock = new Mock<IReader>();
      readerMock.Setup(x => x.ReadLine()).Returns("5");
      var askUser = new AskUser(writerMock.Object, readerMock.Object);
      var expectedPrompt = "hello world";

      // Act
      askUser.ForNumber(expectedPrompt);

      // Assert
      readerMock.Verify(x => x.ReadLine(), Times.Once);
    }

    [TestCase(5)]
    [TestCase(10)]
    [TestCase(-10)]
    [TestCase(0)]
    public void AskUserForNumber_WhenGivenANumber_WillReturnThatNumber(int expectedNumber)
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      const string Prompt = "hello world";
      A.CallTo(() => fakeReader.ReadLine()).Returns(expectedNumber.ToString());
      var askUser = new AskUser(fakeWriter, fakeReader);

      // Act
      var actualNumber = askUser.ForNumber(Prompt);

      // Assert
      Assert.That(actualNumber, Is.EqualTo(expectedNumber));
    }

    [Test]
    public void ForNumber_WhenGivenInvalidNumber_WillInvokeWriteMessage()
    {
      // TODO: Re-write with Fakes
      // Arrange
      var writerMock = new Mock<IWriter>();
      var readerMock = new Mock<IReader>();
      var askUser = new AskUser(writerMock.Object, readerMock.Object);
      var invalidInput = "hello";
      var validInput = "5";

      readerMock.SetupSequence(x => x.ReadLine()).Returns(invalidInput).Returns(validInput);
      var prompt = "hello world";

      // Act
      askUser.ForNumber(prompt);

      // Assert
      writerMock.Verify(x => x.WriteMessage(It.IsAny<string>()), Times.Exactly(3));
    }

    //[Test]
    //public void Build_WhenCalled_InvokesAskUserForNumberOnce()
    //{
    //  var user = new UserTester();

    //  Assert.That(user.CounterOfAskUserForNumber, Is.EqualTo(0));
    //  user.Build();
    //  Assert.That(user.CounterOfAskUserForNumber, Is.EqualTo(1));
      
    //}

    [Test]
    public void UsersAge_WhenGivenInvalidInputFirst_WillAskUserForValidInput()
    {
      // Arrange
      var fakeWriter = A.Fake<IWriter>();
      var fakeReader = A.Fake<IReader>();
      var invalidAge = "abc";
      var validAge = 1;

      // Act
      //var sut = new User(fakeWriter, fakeReader);
      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(invalidAge, validAge.ToString());
      //sut.SetUsersAge();

      // Assert
      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();
      A.CallTo(() => fakeWriter.WriteMessage("Please enter a valid number.")).MustHaveHappened();
    }

    

    #endregion

    //    #region ActivityLevelTests

    //    [Test]
    //    public void ActivityLevel_WhenGivenNumberWithinMinAndMax_ActivityLevelWillBeSet()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const int ExpectedMin = 1;
    //      const int ExpectedMax = 5;
    //      const int ExpectedActivityLevel = 4;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).Returns(ExpectedActivityLevel.ToString());
    //      sut.AskUserForRatingBetween(ExpectedMin, ExpectedMax, LevelType.Activity);
    //      var actualActivityLevel = sut.ActivityLevel;

    //      // Assert
    //      Assert.That(actualActivityLevel, Is.EqualTo(ExpectedActivityLevel));
    //    }

    //    [Test]
    //    public void ActivityLevel_WhenGivenNumberOutsideOfMinAndMax_WillAskUserForValidInput()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const int GivenMin = 1;
    //      const int GivenMax = 5;
    //      const int InvalidActivityLevel = 30;
    //      const int ExpectedActivityLevel = 4;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);

    //      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidActivityLevel.ToString(), ExpectedActivityLevel.ToString());
    //      sut.AskUserForRatingBetween(GivenMin, GivenMax, LevelType.Activity);

    //      // Assert
    //      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();

    //      // This will not get called if all input are valid numbers.
    //      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.\n")).MustNotHaveHappened();

    //      // Message displayed when level is outside the range.
    //      A.CallTo(() => fakeWriter.WriteMessage($"\nPlease enter a number between {GivenMin} and {GivenMax}.")).MustHaveHappened();
    //    }

    //    [Test]
    //    public void ActivityLevel_WhenGivenInvalidNumberFirst_WillAskUserForValidInput()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const int GivenMin = 1;
    //      const int GivenMax = 5;
    //      const string InvalidActivityLevel = "abc";
    //      const int ExpectedActivityLevel = 4;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);

    //      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidActivityLevel, ExpectedActivityLevel.ToString());
    //      sut.AskUserForRatingBetween(GivenMin, GivenMax, LevelType.Activity);

    //      // Assert
    //      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();
    //      A.CallTo(() => fakeWriter.WriteMessage($"\nPlease Rate your Activity Level from {GivenMin} to {GivenMax}.")).MustHaveHappened();
    //      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.\n")).MustHaveHappened();
    //    }

    //    #endregion

    //    #region NutritionLevelTests

    //    [Test]
    //    public void NutritionLevel_WhenGivenNumberWithinMinAndMax_NutritionLevelWillBeSet()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const int ExpectedMin = 1;
    //      const int ExpectedMax = 5;
    //      const int ExpectedNutritionLevel = 4;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).Returns(ExpectedNutritionLevel.ToString());
    //      //sut.AskUserForRatingBetween(ExpectedMin, ExpectedMax, LevelType.Nutrition);
    //      var actualNutritionLevel = sut.NutritionLevel;

    //      // Assert
    //      Assert.That(actualNutritionLevel, Is.EqualTo(ExpectedNutritionLevel));
    //    }

    //    [Test]
    //    public void NutritionLevel_WhenGivenNumberOutsideOfMinAndMax_WillAskUserForValidInput()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const int GivenMin = 1;
    //      const int GivenMax = 5;
    //      const int InvalidNutritionLevel = 30;
    //      const int ExpectedNutritionLevel = 4;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);

    //      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidNutritionLevel.ToString(), ExpectedNutritionLevel.ToString());
    //      sut.AskUserForRatingBetween(GivenMin, GivenMax, LevelType.Nutrition);

    //      // Assert
    //      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();

    //      // This will not get called if all input are valid numbers.
    //      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.\n")).MustNotHaveHappened();

    //      // Message displayed when level is outside the range.
    //      A.CallTo(() => fakeWriter.WriteMessage($"\nPlease enter a number between {GivenMin} and {GivenMax}.")).MustHaveHappened();
    //    }

    //    [Test]
    //    public void NutritionLevel_WhenGivenInvalidNumberFirst_WillAskUserForValidInput()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const int GivenMin = 1;
    //      const int GivenMax = 5;
    //      const string InvalidNutritionLevel = "abc";
    //      const int ExpectedNutritionLevel = 4;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);

    //      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidNutritionLevel, ExpectedNutritionLevel.ToString());
    //      sut.AskUserForRatingBetween(GivenMin, GivenMax, LevelType.Nutrition);

    //      // Assert
    //      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();
    //      A.CallTo(() => fakeWriter.WriteMessage($"\nPlease Rate your Nutrition Level from {GivenMin} to {GivenMax}.")).MustHaveHappened();
    //      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.\n")).MustHaveHappened();
    //    }

    //    #endregion

    //    #region IsMarriedTests

    //    [Test]
    //    public void IsMarried_WhenGivenLowerCaseYes_WillReturnTrue()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const string GivenMarried = "yes";
    //      const bool ExpectedMarried = true;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).Returns(GivenMarried);
    //      sut.IsUserMarried();
    //      var actualMarried = sut.IsMarried;

    //      // Assert
    //      Assert.That(actualMarried, Is.EqualTo(ExpectedMarried));
    //    }

    //    [Test]
    //    public void IsMarried_WhenGivenUpperCaseYes_WillReturnTrue()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const string GivenMarried = "YES";
    //      const bool ExpectedMarried = true;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).Returns(GivenMarried);
    //      sut.IsUserMarried();
    //      var actualMarried = sut.IsMarried;

    //      // Assert
    //      Assert.That(actualMarried, Is.EqualTo(ExpectedMarried));
    //    }

    //    [Test]
    //    public void IsMarried_WhenGivenMixedCaseYes_WillReturnTrue()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const string GivenMarried = "YeS";
    //      const bool ExpectedMarried = true;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).Returns(GivenMarried);
    //      sut.IsUserMarried();
    //      var actualMarried = sut.IsMarried;

    //      // Assert
    //      Assert.That(actualMarried, Is.EqualTo(ExpectedMarried));
    //    }

    //    [Test]
    //    public void IsMarried_WhenGivenLowerCaseNo_WillReturnFalse()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const string GivenMarried = "no";
    //      const bool ExpectedMarried = false;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).Returns(GivenMarried);
    //      sut.IsUserMarried();
    //      var actualMarried = sut.IsMarried;

    //      // Assert
    //      Assert.That(actualMarried, Is.EqualTo(ExpectedMarried));
    //    }

    //    [Test]
    //    public void IsMarried_WhenGivenUpperCaseNo_WillReturnFalse()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const string GivenMarried = "NO";
    //      const bool ExpectedMarried = false;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).Returns(GivenMarried);
    //      sut.IsUserMarried();
    //      var actualMarried = sut.IsMarried;

    //      // Assert
    //      Assert.That(actualMarried, Is.EqualTo(ExpectedMarried));
    //    }

    //    [Test]
    //    public void IsMarried_WhenGivenMixedCaseNo_WillReturnFalse()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const string GivenMarried = "nO";
    //      const bool ExpectedMarried = false;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).Returns(GivenMarried);
    //      sut.IsUserMarried();
    //      var actualMarried = sut.IsMarried;

    //      // Assert
    //      Assert.That(actualMarried, Is.EqualTo(ExpectedMarried));
    //    }

    //    [Test]
    //    public void IsMarried_WhenGivenInvalidString_WillAskUserForValidInput()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const string InvalidMarried = "Invalid Answer";
    //      const string ValidMarried = "Yes";
    //      const bool ExpectedMarried = true;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidMarried, ValidMarried);
    //      sut.IsUserMarried();
    //      var actualMarried = sut.IsMarried;

    //      // Assert
    //      Assert.That(actualMarried, Is.EqualTo(ExpectedMarried));
    //    }

    //    [Test]
    //    public void IsMarried_WhenGivenInvalidInputType_WillAskUserForValidInput()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const int InvalidMarried = 123;
    //      const string ValidMarried = "Yes";
    //      const bool ExpectedMarried = true;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidMarried.ToString(), ValidMarried);
    //      sut.IsUserMarried();
    //      var actualMarried = sut.IsMarried;

    //      // Assert
    //      Assert.That(actualMarried, Is.EqualTo(ExpectedMarried));
    //    }

    //    #endregion

    //    #region NumberOfDependentsTests

    //    [Test]
    //    public void NumberOFCoveredDependents_GivenNumberFive_WillReturnFive()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const int ExpectedNumberOfDependents = 5;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).Returns(ExpectedNumberOfDependents.ToString());
    //      sut.AskForNumberOfCoveredDependents();

    //      // Assert
    //      var actualNumberOfDependents = sut.NumberOfCoveredDependents;
    //      Assert.That(actualNumberOfDependents, Is.EqualTo(ExpectedNumberOfDependents));
    //    }

    //    [Test]
    //    public void NumberOFCoveredDependents_GivenInvalidInput_WillAskUserForValidInput()
    //    {
    //      // Arrange
    //      var fakeWriter = A.Fake<IWriter>();
    //      var fakeReader = A.Fake<IReader>();
    //      const string InvalidDependents = "one hundred";
    //      const int ExpectedNumberOfDependents = 5;

    //      // Act
    //      var sut = new User(fakeWriter, fakeReader);
    //      A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(InvalidDependents, ExpectedNumberOfDependents.ToString());
    //      sut.AskForNumberOfCoveredDependents();

    //      // Assert
    //      A.CallTo(fakeWriter).Where(x => x.Method.Name == "WriteMessage").MustHaveHappened();
    //      A.CallTo(() => fakeWriter.WriteMessage("\nPlease enter a valid number.")).MustHaveHappened();
    //    }

    //    #endregion

    
  }

  //public class UserTester : User
  //{
  //  public int CounterOfAskUserForNumber { get; set; } = 0;

  //  public override int AskUserForNumber(string prompt)
  //  {
  //    CounterOfAskUserForNumber++;
  //    return 0;
  //  }
  //}
}