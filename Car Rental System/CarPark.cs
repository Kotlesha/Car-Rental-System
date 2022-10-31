using System;
using System.Collections.Generic;

namespace Car_Rental_System
{
    [Serializable]
    class CarPark
    {
        private List<RentalPointAdministrator> administrators;
        private List<Customer> customers;

        public CarPark(List<RentalPointAdministrator> administrators, List<Customer> customers)
        {
            this.administrators = administrators;
            this.customers = customers;
        }

        public List<RentalPointAdministrator> Administrators => administrators;

        public List<Customer> Customers => customers;

        public RentalPointAdministrator FindAdministrator(string nameOfRentalPoint)
        {
            foreach (var administrator in administrators)
            {
                if (administrator.RentalPoint.Name.Equals(nameOfRentalPoint, StringComparison.OrdinalIgnoreCase))
                {
                    return administrator;
                }
            }

            return null;
        }

        public void DisplayCars(string nameOfRentalPoint)
        {
            RentalPointAdministrator administrator = FindAdministrator(nameOfRentalPoint);

            foreach (var car in administrator.RentalPoint.Cars)
            {
                if (!car.IsInTheRental)
                {
                    Console.WriteLine(car);
                }
            }
        }

        public RentalPoint FindRentalPoint(string nameOfRentalPoint)
        {
            foreach (var administrator in administrators)
            {
                if (administrator.RentalPoint.Name.Equals(nameOfRentalPoint, StringComparison.OrdinalIgnoreCase))
                {
                    return administrator.RentalPoint;
                }
            }

            return null;
        }

        private List<Customer> GetCorrectCustomers()
        {
            List<Customer> correct_customers = new();

            foreach (Customer customer in customers)
            {
                if (!customer.CarInRental)
                {
                    correct_customers.Add(customer);
                }
            }

            return correct_customers;
        }

        public Customer FindCustomer(string nameOfCustomer)
        {
            foreach (Customer customer in customers)
            {
                if (customer.Person.FullName.Name.Equals(nameOfCustomer, StringComparison.OrdinalIgnoreCase))
                {
                    return customer;
                }
            }

            return null;
        }

        public void DisplayCustomers()
        {
            List<Customer> customers = GetCorrectCustomers();

            foreach (Customer customer in customers)
            {
                Console.Write(customer);
            }

            Console.WriteLine();
        }

        public void DisplayAllCustomers()
        {
            foreach (Customer customer in customers)
            {
                Console.Write(customer);
            }

            Console.WriteLine();
        }

        public void DisplayCustomers(string nameOfCustomer) => Console.WriteLine("\n" + FindCustomer(nameOfCustomer));

        public void DisplayRentalPoint()
        {
            foreach (var administrator in administrators)
            {
                Console.Write("\n" + administrator.RentalPoint);
            }

            Console.WriteLine();
        }

        public void DisplayRentalPoint(string nameOfRentalPoint) => Console.WriteLine("\n" + FindRentalPoint(nameOfRentalPoint));

        public void AddCustomerToList(Customer customer) => customers.Add(customer);

        public void AddRentalPointToList(RentalPointAdministrator administrator) => administrators.Add(administrator);

        public void AddCarToRentalPoint(Car car, string nameOfRentalPoint)
        {
            RentalPoint rentalPoint = FindRentalPoint(nameOfRentalPoint);
            rentalPoint.Cars.Add(car);
        }
    }
}
