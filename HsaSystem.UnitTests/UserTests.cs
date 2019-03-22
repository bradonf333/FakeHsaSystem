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
    public void UserAge_WhenGivenNumberFive_WillBeFive()
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
  }
}