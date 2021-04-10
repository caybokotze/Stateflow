using NUnit.Framework;

namespace Stateflow.Tests
{
    public class Cow
    {
        public Animal Moo()
        {
            return new Animal
            {
                Name = "Georgie"
            };
        }
    }

    public class Animal
    {
        public string Name { get; set; }
        public string Words { get; set; }
    }

    public static class CowHelper
    {
        public static Animal ExtendCow(this Animal animal)
        {
            animal.Words = "asfasdf";
            return animal;
        }
    }
    
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // arrange
            var cowClass = new Cow();
            var animal = cowClass.Moo()
                .ExtendCow();
            // act
            
            // assert
            Assert.That(animal.Name.Equals(""));
        }
    }
}