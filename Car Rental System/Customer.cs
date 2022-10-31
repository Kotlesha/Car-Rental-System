using System;

namespace Car_Rental_System
{
    [Serializable]
    class Customer
    {
        private CurrentAccount currentAccount;
        private Person person;
        private bool carInRental;

        public Person Person => person;

        public CurrentAccount CurrentAccount => currentAccount;

        public bool CarInRental
        {
            get => carInRental;
            set => carInRental = value;
        }

        public Customer(Person person, CurrentAccount currentAccount) 
        {
            this.person = person;
            this.currentAccount = currentAccount;
        }

        public override bool Equals(object obj)
        {
            if (obj is Customer customer)
            {
                return customer.Person.Equals(person) && customer.currentAccount.Equals(currentAccount); 
            }

            return false;
        }

        public override string ToString() => string.Concat(person, " ", currentAccount, "\n");
    }
}
