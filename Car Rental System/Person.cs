using System;
using System.Linq;

namespace Car_Rental_System
{
    [Serializable]
    class Person
    {
        private FullName fullName;
        private DateTime dateOfBirth;
        private string telephoneNumber;

        public Person(FullName fullName, DateTime dateOfBirth, string telephoneNumber)
        {
            this.fullName = fullName;
            this.dateOfBirth = dateOfBirth;
            this.telephoneNumber = telephoneNumber;
        }

        public FullName FullName => fullName;

        public override bool Equals(object obj)
        {
            if (obj is Person person)
            {
                return person.fullName.Equals(fullName) && person.dateOfBirth == dateOfBirth && person.telephoneNumber == telephoneNumber;
            }

            return false;
        }

        public override string ToString() => $"{fullName}, дата рождения: {dateOfBirth.ToShortDateString()}, мобильный номер: {telephoneNumber},";
    }
}
