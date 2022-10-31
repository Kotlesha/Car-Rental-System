using System;
using System.Collections.Generic;

namespace Car_Rental_System
{
    static class Menu
    {
        private static CurrentAccount CreateCurrentAccount()
        {
            while (true)
            {
                Console.Write("Введите, пожалуйста, тип оплаты: картой или наличными: ");
                string choice = Console.ReadLine().Trim();
                if (choice.Equals("картой"))
                {
                    Console.Write("Введите, пожалуйста, номер карты: ");
                    string numberOfCard = Console.ReadLine().Trim();
                    Validation.CheckString(ref numberOfCard);
                    Validation.CheckCardNumber(ref numberOfCard);
                    Console.Write("Введите, пожалуйста, срок действия карты: ");
                    string periodValidity = Console.ReadLine().Trim();
                    Validation.CheckString(ref periodValidity);
                    DateTime periodOfValidity = Validation.CheckDate(periodValidity, "dd.MM.yyyy");
                    Console.Write("Введите, пожалуйста, количество денег: ");
                    string money = Console.ReadLine().Trim();
                    int AmountOfmoney = Validation.CheckInt(money); 
                    return new(TypeOfPayment.Card, AmountOfmoney, numberOfCard, periodOfValidity);
                }
                else if (choice.Equals("наличными"))
                {
                    Console.Write("Введите, пожалуйста, количество денег: ");
                    string money = Console.ReadLine().Trim();
                    int AmountOfmoney = Validation.CheckInt(money);
                    return new(TypeOfPayment.Money, AmountOfmoney);
                }
                else
                {
                    Console.WriteLine("Неправильный ввод! Повторите, пожалуйста, попытку!");
                }
            }
        }

        private static Person AddPerson() 
        {
            Console.Write("Введите, пожалуйста, имя: ");
            string name = Console.ReadLine().Trim();
            Validation.CheckString(ref name);
            Console.Write("Введите, пожалуйста, фамилию: ");
            string surname = Console.ReadLine().Trim();
            Validation.CheckString(ref surname);
            Console.Write("Введите, пожалуйста, отчество: ");
            string patronymic = Console.ReadLine().Trim();
            Validation.CheckString(ref patronymic);
            Console.Write("Введите, пожалуйста, дату рождения: ");
            string Birth = Console.ReadLine().Trim();
            DateTime dateOfBirth = Validation.CheckDate(Birth, "dd.MM.yyyy");
            Console.Write("Введите, пожалуйста, телефонный номер: ");
            string telephoneNumber = Console.ReadLine().Trim();
            Validation.CheckString(ref telephoneNumber);
            Validation.CheckTelephoneNumber(ref telephoneNumber);
            
            return new(new(name, surname, patronymic), dateOfBirth, telephoneNumber);
        }

        private static Customer AddCustomer() => new(AddPerson(), CreateCurrentAccount());

        private static Car AddCar()
        {
            Console.Write("Введите, пожалуйста, марку автомобиля: ");
            string brand = Console.ReadLine().Trim();
            Validation.CheckString(ref brand);
            Console.Write("Введите, пожалуйста, модель автомобиля: ");
            string model = Console.ReadLine().Trim();
            Validation.CheckString(ref model);
            Console.Write("Введите, пожалуйста, регистрационный номер: ");
            string registrationNumber = Console.ReadLine().Trim();
            Validation.CheckString(ref registrationNumber);
            Validation.CheckRegistrationNumber(ref registrationNumber);
            Console.Write("Введите, пожалуйста, цену аренды автомобиля за сутки: ");
            string money = Console.ReadLine().Trim();
            int price = Validation.CheckInt(money);
            return new(brand, model, registrationNumber, price);
        }

        private static RentalPoint AddRentalPoint()
        {
            Console.Write("Введите, пожалуйста, название пункта проката: ");
            string name = Console.ReadLine().Trim();
            Validation.CheckString(ref name);
            Console.Write("Введите, пожалуйста, адрес пункта проката: ");
            string adress = Console.ReadLine().Trim();
            Validation.CheckString(ref adress);
            SortedSet<Car> cars = new();
            Console.WriteLine("В пункт проката необходимо добавить хотя бы одну машину!");
            Car car = AddCar();
            cars.Add(car);

            while (true)
            {
                Console.Write("Хотите добавить ещё одну машину? Введите Да для добавления ещё одной машины, Нет - в противном случае: ");
                string choice = Console.ReadLine().Trim();
                if (choice.Equals("Да", StringComparison.OrdinalIgnoreCase))
                {
                    Car car1 = AddCar();
                    cars.Add(car1);
                }
                else if (choice.Equals("Нет", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }  
                else
                {
                    Console.WriteLine("Неправильный ввод! Повторите, пожалуйста, попытку!");
                }
            }

            return new(name, adress, cars, new List<Order>());
        }

        private static CarPark AddCarPark()
        {
            Console.WriteLine("Добро пожаловать в программу Прокат автомобилей!");
            Console.WriteLine("В самом начале необходимо добавить всю информацию.");
            int number = 0, count = 0;
            Console.WriteLine($"Создание {++count} пункта проката");
            RentalPoint rentalPoint1 = AddRentalPoint();
            Console.WriteLine($"Создание администратора {++number} пункта проката");
            RentalPointAdministrator administrator_1 = new(AddPerson(), rentalPoint1);
            Console.WriteLine($"Создание {++count} пункта проката");
            RentalPoint rentalPoint2 = AddRentalPoint();
            Console.WriteLine($"Создание администратора {++number} пункта проката");
            RentalPointAdministrator administrator_2 = new(AddPerson(), rentalPoint2);
            List<RentalPointAdministrator> rentalPointAdministrators = new() { administrator_1, administrator_2 };

            while (true)
            {
                Console.Write("Хотите добавить ещё один пункт проката? Введите Да для добавления ещё одного пункта проката, Нет - в противном случае: ");
                string choice = Console.ReadLine().Trim();
                if (choice.Equals("Да"))
                {
                    Console.WriteLine($"Создание {++count} пункта проката");
                    RentalPoint rentalPoint = AddRentalPoint();
                    Console.WriteLine($"Создание администратора {++number} пункта проката");
                    RentalPointAdministrator administrator = new(AddPerson(), rentalPoint);
                    rentalPointAdministrators.Add(administrator);
                }
                else if (choice.Equals("Нет"))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод! Повторите, пожалуйста, попытку!");
                }
            }

            CarPark carPark = new(rentalPointAdministrators, ListOfCustomers());
            Serialize.Serialization(rentalPointAdministrators);
            return carPark;
        }

        private static CarPark AddCarPark(List<RentalPointAdministrator> rentalPointAdministrators, List<Customer> customers) => new(rentalPointAdministrators, customers);

        private static List<Customer> ListOfCustomers()
        {
            int number = 0;
            Console.WriteLine($"Создание {++number} клиента");
            Customer customer = new(AddPerson(), CreateCurrentAccount());
            List<Customer> customers = new() { customer };

            while (true)
            {
                Console.Write("Хотите добавить ещё один клиента? Введите Да для добавления ещё одного клиента, Нет - в противном случае: ");
                string choice = Console.ReadLine().Trim();
                if (choice.Equals("Да", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Создание {++number} клиента");
                    Customer customer1 = AddCustomer();
                    customers.Add(customer1);
                }
                else if (choice.Equals("Нет", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод! Повторите, пожалуйста, попытку!");
                }
            }

            Serialize.Serialization(customers);
            return customers;
        }

        private static string GetCustomer(CarPark carPark, string nameOfCustomer)
        {
            Customer customer = carPark.FindCustomer(nameOfCustomer);
            while (customer is null)
            {
                Console.WriteLine("Клиента с таким именем не существует или у него уже арендована машина!");
                Console.WriteLine("Пожалуйста, повторите попытку!");
                Console.Write("Введите, пожалуйста, имя клиента: ");
                nameOfCustomer = Console.ReadLine().Trim();
                customer = carPark.FindCustomer(nameOfCustomer);
            }

            return nameOfCustomer;
        }

        private static string GetRentalPoint(CarPark carPark, string nameOfRentalPoint)
        {
            RentalPointAdministrator administrator = carPark.FindAdministrator(nameOfRentalPoint);
            while (administrator is null)
            {
                Console.WriteLine("Пункта проката с таким именем не существует!");
                Console.WriteLine("Пожалуйста, повторите попытку!");
                Console.Write("Введите, пожалуйста, название пункта проката: ");
                nameOfRentalPoint = Console.ReadLine().Trim();
                administrator = carPark.FindAdministrator(nameOfRentalPoint);
            }

            return nameOfRentalPoint;
        }

        private static void CreateOrder(CarPark carPark)
        {
            Console.WriteLine();
            carPark.DisplayCustomers();
            Console.Write("Введите, пожалуйста, имя клиента для создания заказа: ");
            string name = Console.ReadLine().Trim();
            Validation.CheckString(ref name);
            name = GetCustomer(carPark, name);
            Customer customer = carPark.FindCustomer(name);
            Console.Write("Введите, пожалуйста, название пункта проката: ");
            string nameOfRentalPoint = Console.ReadLine().Trim();
            Validation.CheckString(ref nameOfRentalPoint);
            nameOfRentalPoint = GetRentalPoint(carPark, nameOfRentalPoint);
            RentalPointAdministrator administrator = carPark.FindAdministrator(nameOfRentalPoint);
            Console.Write("Введите, пожалуйста, время начала: ");
            string start = Console.ReadLine().Trim();
            Validation.CheckString(ref start);
            DateTime startTime = Validation.CheckDate(start, "dd.MM.yyyy HH:mm");
            Console.Write("Введите, пожалуйста, время окончания: ");
            string end = Console.ReadLine().Trim();
            DateTime endTime = Validation.CheckDate(end, "dd.MM.yyyy HH:mm");
            administrator.CreateOrder(customer, startTime, endTime);
        }

        private static bool GetSignRental(CarPark carPark)
        {
            int count = 0;

            foreach (var customer in carPark.Customers)
            {
                if (!customer.CarInRental)
                {
                    count++;
                }
            }

            return count == carPark.Customers.Count;
        }

        private static void EndOrder(CarPark carPark)
        {
            bool sign = GetSignRental(carPark);

            if (sign)
            {
                Console.WriteLine("\nНе существует ни одного действующего заказа!");
                Console.WriteLine();
                return;
            }

            Console.Write("Введите, пожалуйста, название пункта проката: ");
            string nameOfRentalPoint = Console.ReadLine().Trim();
            Validation.CheckString(ref nameOfRentalPoint);
            nameOfRentalPoint = GetRentalPoint(carPark, nameOfRentalPoint);
            RentalPointAdministrator administrator = carPark.FindAdministrator(nameOfRentalPoint);
            carPark.DisplayRentalPoint(nameOfRentalPoint);
            Console.Write("Введите, пожалуйста, имя клиента для окончания заказа: ");
            string name = Console.ReadLine().Trim();
            Validation.CheckString(ref name);
            name = GetCustomer(carPark, name);
            administrator.EndOrder(carPark.FindCustomer(name), carPark);
        }

        private static CarPark GetCarPark()
        {
            while (true)
            {
                Console.WriteLine("Вы хотите самостоятельно ввести данные или взять их из файлов?");
                Console.Write("Введите, пожалуйста, Да - для ввода данных, Нет - для взятия данных их файлов: ");
                string choice = Console.ReadLine().Trim();
                Console.Clear();
                if (choice.Equals("Да", StringComparison.OrdinalIgnoreCase))
                {
                    return AddCarPark();
                }
                else if (choice.Equals("Нет", StringComparison.OrdinalIgnoreCase))
                {
                    List<RentalPointAdministrator> administrators = Serialize.Deserialization<RentalPointAdministrator>();
                    List<Customer> customers = Serialize.Deserialization<Customer>();
                    return AddCarPark(administrators, customers);
                }
                else
                {
                    Console.WriteLine("Неправильный ввод! Пожалуйста, повторите, попытку!");
                }
            }
        }

        public static void Run()
        {
            CarPark carPark = GetCarPark();

            while (true)
            {
                Console.WriteLine("Что вы хотите сделать?");
                Console.WriteLine("1.Посмотреть информацию об определённом пункте проката(название, адрес, заказы, машины, находящиеся в данном пункте проката);");
                Console.WriteLine("2.Вывести информацию о всех пунктах проката;");
                Console.WriteLine("3.Посмотреть информацию об определённом клиенте;");
                Console.WriteLine("4.Вывести информацию о всех клиентах;");
                Console.WriteLine("5.Вывести информацию о всех клиентах, котороые не брали машину в прокат;");
                Console.WriteLine("6.Создать заказ;");
                Console.WriteLine("7.Закончить заказ;");
                Console.WriteLine("8.Добавить машину в пункт проката;");
                Console.WriteLine("9.Добавить пункт проката;");
                Console.WriteLine("10.Добавить клиента в базу данных;");
                Console.WriteLine("11.Выход.");
                Console.Write("Введите, пожалуйста, номер команды: ");

                string choice = Console.ReadLine().Trim();
                int number = Validation.CheckInt(choice);

                if (number == 1)
                {
                    Console.Write("\nВведите, пожалуйста, название пункта проката: ");
                    string nameOfRentalPoint = Console.ReadLine().Trim();
                    nameOfRentalPoint = GetRentalPoint(carPark, nameOfRentalPoint);
                    carPark.DisplayRentalPoint(nameOfRentalPoint);
                }
                else if (number == 2)
                {
                    Console.WriteLine("\nИнформация о всех пунктах проката: ");
                    carPark.DisplayRentalPoint();
                }
                else if (number == 3)
                {
                    Console.Write("\nВведите, пожалуйста, имя клиента: ");
                    string nameOfCustomer = Console.ReadLine().Trim();
                    nameOfCustomer = GetCustomer(carPark, nameOfCustomer);
                    carPark.DisplayCustomers(nameOfCustomer);
                }
                else if (number == 4)
                {
                    Console.WriteLine();
                    Console.WriteLine("Информация о всех клиентах: ");
                    carPark.DisplayAllCustomers();
                }
                else if (number == 5)
                {
                    carPark.DisplayCustomers();
                }
                else if (number == 6)
                {
                    CreateOrder(carPark);
                }
                else if (number == 7)
                {
                    EndOrder(carPark);
                }
                else if (number == 8)
                {
                    Console.WriteLine($"\nДобавление машины: ");
                    Car car = AddCar();
                    Console.Write("\nВведите, пожалуйста, название пункта проката: ");
                    string nameOfRentalPoint = Console.ReadLine().Trim();
                    nameOfRentalPoint = GetRentalPoint(carPark, nameOfRentalPoint);
                    carPark.AddCarToRentalPoint(car, nameOfRentalPoint);
                }
                else if (number == 9)
                {
                    Console.WriteLine($"\nСоздание пункта проката");
                    RentalPoint rentalPoint = AddRentalPoint();
                    Console.WriteLine($"\nСоздание администратора пункта проката");
                    RentalPointAdministrator administrator = new(AddPerson(), rentalPoint);
                    carPark.AddRentalPointToList(administrator);
                }
                else if (number == 10)
                {
                    Console.WriteLine("\n" + "Добавление клиента:");
                    carPark.AddCustomerToList(AddCustomer());
                }
                else if (number == 11)
                {
                    Serialize.Serialization(carPark.Customers);
                    Serialize.Serialization(carPark.Administrators);
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод! Повторите, пожалуйста попытку!");
                }
            }
        }
    }
}
