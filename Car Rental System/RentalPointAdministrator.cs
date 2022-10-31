using System;
using System.Linq;
using System.Collections.Generic;

namespace Car_Rental_System
{
    [Serializable]
    class RentalPointAdministrator
    {
        private RentalPoint rentalPoint;
        private Person person;
            
        public RentalPointAdministrator(Person person, RentalPoint rentalPoint) 
        {
            this.person = person;
            this.rentalPoint = rentalPoint;
        }

        public RentalPoint RentalPoint => rentalPoint;

        private bool CheckCars()
        {
            int count = 0;

            foreach (var car in rentalPoint.Cars)
            {
                if (car.IsInTheRental)
                {
                    count++;
                }
            }

            if (count == rentalPoint.Cars.Count)
            {
                return true;
            }

            return false;
        }

        private SortedSet<Car> GetCars()
        {
            SortedSet<Car> cars = new();

            foreach (Car car in rentalPoint.Cars)
            {
                if (!car.IsInTheRental)
                {
                    cars.Add(car);
                }
            }

            return cars;
        }

        private Car ChooseCar()
        {
            Console.WriteLine("Информация о текущем пункте проката: ");
            Console.WriteLine(rentalPoint);

            if (rentalPoint.Cars.Count == 0)
            {
                return null;
            }

            if (CheckCars())
            {
                return null;
            }

            SortedSet<Car> cars = GetCars();

            while (true)
            {
                Console.Write("Для того чтобы выбрать машину, введите её номер по счёту: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice - 1 >= cars.Count || choice - 1 < 0)
                {
                    Console.WriteLine("Такого индекса не существует!");
                }
                else
                {
                    return cars.ElementAt(choice - 1);
                }
            } 
        }

        public void CreateOrder(Customer customer, DateTime startTime, DateTime endTime)
        {
            Car car = ChooseCar();
            Order order = new(customer, car, new(startTime, endTime));

            while (order.Car is null)
            {
                Console.WriteLine("Недостаточно средств!");
                Console.WriteLine("Выберете, пожалуйста, другую машину: ");
                car = ChooseCar();
                order = new(customer, car, new(startTime, endTime));
            }

            order.Car = car;
            customer.CarInRental = true;
            order.Customer.CurrentAccount.Calculation(order.GetDeposit());
            order.Car.IsInTheRental = true;
            rentalPoint.AddOrder(order);
        }

        private Order FindOrder(Customer customer, CarPark carPark)
        {
            foreach (var administrator in carPark.Administrators)
            {
                if (administrator.RentalPoint.FindOrder(customer) != null)
                {
                    return administrator.RentalPoint.FindOrder(customer);
                }
            }

            return null;
        }

        public void EndOrder(Customer customer, CarPark carPark)
        {
            Order order = FindOrder(customer, carPark);

            while (order is null)
            {
                Console.WriteLine("Такого заказа не существует!");
                Console.Write("Введите, пожалуйста, имя клиента: ");
                string nameOfCustomer = Console.ReadLine().Trim();
                customer = carPark.FindCustomer(nameOfCustomer);

                while (customer is null)
                {
                    Console.WriteLine("Клиента с таким именем не существует или у него уже арендована машина!");
                    Console.WriteLine("Пожалуйста, повторите попытку!");
                    Console.Write("Введите, пожалуйста, имя клиента: ");
                    nameOfCustomer = Console.ReadLine().Trim();
                    customer = carPark.FindCustomer(nameOfCustomer);
                }

                order = FindOrder(customer, carPark);
            }

            customer.CarInRental = false;
            RentalPoint oldRentalPoint = carPark.FindRentalPoint(order.Car.StartRentalPoint);
            rentalPoint.AddCar(order.Car);

            if (oldRentalPoint.Name != rentalPoint.Name)
            {
                oldRentalPoint.RemoveCar(order.Car);
            }

            order.Car.StartRentalPoint = rentalPoint.Name;
            order.Customer.CurrentAccount.Calculation(order.GetPrice());
            order.Car.IsInTheRental = false;
            oldRentalPoint.RemoveOrder(order);
        }
    }
}
