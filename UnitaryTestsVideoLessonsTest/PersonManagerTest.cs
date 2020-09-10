using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitaryTestsVideoLessons.PersonClasses;

namespace UnitaryTestsVideoLessonsTest
{
    public class PersonManagerTest
    {
        [TestMethod]
        [Owner("tatianex")]
        public void CreatePerson_OfTypeEmployeeTest()
        {
            PersonManager PerMgr = new PersonManager();
            Person per;

            per = PerMgr.CreatePerson("Yasmin", "Fernandes", false);

            Assert.IsInstanceOfType(per, typeof(Employee));
        }

        [TestMethod]
        [Owner("tatianex")]
        public void DoEmployeeExistsTest()
        {
            Supervisor super = new Supervisor();

            super.Employees = new List<Employee>();
            super.Employees.Add(new Employee()
            { 
                FirstName = "Ana Luisa",
                LastName = "Machado"
            });

            Assert.IsTrue(super.Employees.Count > 0);
        }
    }
}
