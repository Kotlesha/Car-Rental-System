using System;
using System.Collections.Generic;

namespace Car_Rental_System
{
    [Serializable]
    class RentalPoint
    {
        private string name;
        private string adress;
        private SortedSet<Car> cars;
        private List<Order> orders;

        public string Name => name;
        public SortedSet<Car> Cars => cars;

        public RentalPoint(string name, string adress, SortedSet<Car> cars, List<Order> orders)
        {
            this.name = name;
            this.adress = adress;
            this.cars = SetValues(cars);
            this.orders = orders;
        }
 
        public void RemoveCar(Car car) => cars.Remove(car);
        public void AddCar(Car car) => cars.Add(car);

        public Order FindOrder(Customer customer)
        {
            foreach (Order order in orders)
            {
                if (order.Customer.Equals(customer))
                {
                    return order;
                }
            }

            return null;
        }

        public void RemoveOrder(Order order) => orders.Remove(order);

        public void AddOrder(Order order) => orders.Add(order);

        private SortedSet<Car> SetValues(SortedSet<Car> cars)
        {
            foreach (Car car in cars)
            {
                car.StartRentalPoint = name;
            }

            return cars;
        }

        public override string ToString()
        {
            string listOfCars = string.Empty, listOfOrders = string.Empty;

            if (cars.Count != 0)
            {
                foreach (Car car in cars)
                {
                    listOfCars = string.Concat(listOfCars, car, "\n");
                }
            }
            else
            {
                listOfCars = "Свободных машин нет!\n";
            }

            if (orders.Count != 0)
            {
                foreach (Order order in orders)
                {
                    listOfOrders = string.Concat(listOfOrders, order, "\n");
                }
            }
            else
            {
                listOfOrders = "Заказов нет!\n";
            }

            return $"Название пункта проката: {name}, адрес пункта проката: {adress} \nСписок машин:\n{listOfCars}Список заказов:\n{listOfOrders}";
        }
    }
}
