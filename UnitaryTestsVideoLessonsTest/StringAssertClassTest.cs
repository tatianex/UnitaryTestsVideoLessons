using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitaryTestsVideoLessons.PersonClasses;

namespace UnitaryTestsVideoLessonsTest
{
    [TestClass]
    public class StringAssertClassTest
    {
        [TestMethod]
        [Owner("tatianex")]
        public void AreCollectionEqualFailsBecauseNoComparerTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleExpected.Add(new Person { FirstName = "Silvano", LastName = "Schröder" });
            peopleExpected.Add(new Person { FirstName = "Melissa", LastName = "Monteiro" });
            peopleExpected.Add(new Person { FirstName = "Maria Cristina", LastName = "Monteiro" });

            // You shall not pass! - Because they are DIFFERENT objects.
            // peopleActual = PerMgr.GetPeople();
            peopleActual = peopleExpected;

            CollectionAssert.AreEqual(peopleExpected, peopleActual);
        }

        [TestMethod]
        [Owner("tatianex")]
        public void AreCollectionEqualWithComparerTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleExpected.Add(new Person { FirstName = "Silvano", LastName = "Schröder" });
            peopleExpected.Add(new Person { FirstName = "Melissa", LastName = "Monteiro" });
            peopleExpected.Add(new Person { FirstName = "Maria Cristina", LastName = "Monteiro" });

            peopleActual = PerMgr.GetPeople();

            CollectionAssert.AreEqual(peopleExpected, peopleActual,
                Comparer<Person>.Create((x, y) => 
                    x.FirstName == y.FirstName && x.LastName == y.LastName ? 0 : 1));
        }

        [TestMethod]
        [Owner("tatianex")]
        public void AreCollectionEquivalentWithComparerTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();           

            peopleActual = PerMgr.GetPeople();

            peopleExpected.Add(peopleActual[1]);
            peopleExpected.Add(peopleActual[2]);
            peopleExpected.Add(peopleActual[0]);

            CollectionAssert.AreEquivalent(peopleExpected, peopleActual);
        }

        [TestMethod]
        [Owner("tatianex")]
        public void IsCollectionTypeTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleActual = new List<Person>();

            peopleActual = PerMgr.GetSupervisors();            

            CollectionAssert.AllItemsAreInstancesOfType(peopleActual, typeof(Supervisor));
        }
    }
}
