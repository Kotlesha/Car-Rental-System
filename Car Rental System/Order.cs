using System;

namespace Car_Rental_System
{
    [Serializable]
    class Order
    {
        private const double percent = 0.2;
        private Customer customer;
        private Car car;
        private PeriodOfTime periodOfTime;

        public Car Car
        {
            get => car;
            set => car = value;
        }

        public Customer Customer => customer;

        public Order(Customer customer, Car car, PeriodOfTime periodOfTime)
        {
            this.customer = customer;
            this.car = car;
            this.periodOfTime = periodOfTime;

            if (!customer.CurrentAccount.CheckMoney(GetDeposit()) || !customer.CurrentAccount.CheckMoney(GetPrice()))
            {
                this.car = null;
            }
        }

        public int GetDeposit() => (int)(periodOfTime.Period() * car.Price * percent);

        public int GetPrice() => periodOfTime.Period() * car.Price - GetDeposit();

        public override string ToString() => string.Concat(customer, car, "\n", periodOfTime);
    }
}
