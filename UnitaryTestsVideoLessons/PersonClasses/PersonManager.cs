using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitaryTestsVideoLessons.PersonClasses
{
    public class PersonManager
    {
        public Person CreatePerson(string fstName, string lstName, bool isSupervisor)
        {
            Person ret = null;

            if (!string.IsNullOrEmpty(fstName))
            {
                if (isSupervisor) ret = new Supervisor();
                else ret = new Employee();

                ret.FirstName = fstName;
                ret.LastName = lstName;
            }

            return ret;
        }

        public List<Person> GetPeople()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person { FirstName = "Silvano", LastName = "Schröder" });
            people.Add(new Person { FirstName = "Melissa", LastName = "Monteiro" });
            people.Add(new Person { FirstName = "Maria Cristina", LastName = "Monteiro" });

            return people;
        }

        public List<Person> GetSupervisors()
        {
            List<Person> people = new List<Person>();

            people.Add(CreatePerson("Eduarda", "Passig", true));
            people.Add(CreatePerson("Laura", "Ribeiro", true));

            return people;
        }
    }
}
