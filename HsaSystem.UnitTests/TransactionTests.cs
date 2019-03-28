using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FakeItEasy;
using HsaSystem.Input;
using HsaSystem.Models;
using HsaSystem.Output;
using NUnit.Framework;

namespace HsaSystem.Tests
{
  public class TransactionTests
  {
    [Test]
    public void TestTransaction()
    {
      //var fakeWriter = A.Fake<IWriter>();
      //var fakeReader = A.Fake<IReader>();
      //var fakeTransReader = A.Fake<ITransactionReader>();

      //var sut = new Hsa(fakeWriter, fakeReader);
      //List<string> fakeFile = new List<string>
      //{
      //  "2019,1,50,-100,Kids bring home a cold from school so go see the doctor,",
      //  "2020,2,100,1000,Your employer surprises you with an HSA incentive!,"
      //};

      //A.CallTo(() => fakeTransReader.Read()).Returns(fakeFile);

      //sut.CreateTransactions(fakeTransReader);
    }

    //private static User CreateUser()
    //{
    //  var fakeWriter = A.Fake<IWriter>();
    //  var fakeReader = A.Fake<IReader>();

    //  var age = "30";
    //  var activityLevel = "5";
    //  var nutritionLevel = "5";
    //  var isMarried = "yes";
    //  var covDeps = "4";
    //  var savingsDedication = "5";
    //  var employerBens = "5";

    //  A.CallTo(() => fakeReader.ReadLine()).ReturnsNextFromSequence(
    //    age,
    //    activityLevel,
    //    nutritionLevel,
    //    isMarried,
    //    covDeps,
    //    savingsDedication,
    //    employerBens);
    //  var user = new User(fakeWriter, fakeReader);
    //  user.Build();

    //  return user;
    //}
  }
}
