using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;


namespace Car_Rental_System
{
    static class Validation
    {
        public static void CheckString(ref string data)
        {
            while (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
            {
                Console.WriteLine("Строка не может быть пустой!");
                Console.Write("Повторите, пожалуйста, попытку: ");
                data = Console.ReadLine().Trim();
            }
        }

        public static void CheckRegistrationNumber(ref string registrationNumber)
        {
            while (!Regex.IsMatch(registrationNumber, @"^[0-9]{4} [A-Z]{2}-[0-9]"))
            {
                Console.WriteLine("Поле регистрационный номер автомобиля имеет неверный формат(0000 XX-0)!");
                Console.WriteLine("Повторите, пожалуйста, попытку: ");
                registrationNumber = Console.ReadLine().Trim();
            }
        }

        public static void CheckCardNumber(ref string numberOfCard)
        {
            while (!Regex.IsMatch(numberOfCard, @"^[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}"))
            {
                Console.WriteLine("Неправильный формат(XXXX-XXXX-XXXX-XXXX)!");
                Console.WriteLine("Повторите, пожалуйста, попытку: ");
                numberOfCard = Console.ReadLine().Trim();
            }
        }

        public static void CheckTelephoneNumber(ref string telephoneNumber)
        {
            string[] operators = { "29", "33", "44", "25" };

            while (telephoneNumber.Split('-').Length < 4)
            {
                Console.WriteLine("Поле номер телефона должно включать в себя 5 символов '-'");
                Console.Write("Повторите, пожалуйста, попытку: ");
                telephoneNumber = Console.ReadLine().Trim();
            }

            while (telephoneNumber.Split('-')[0] != "+375")
            {
                Console.WriteLine("Поле номер телефона должно включать в себя код +375!");
                Console.WriteLine("Повторите, пожалуйста, попытку: ");
                telephoneNumber = Console.ReadLine().Trim();
            }

            while (!operators.Contains(telephoneNumber.Split('-')[1]))
            {
                Console.WriteLine("Поле номер телефона включает в себя код несуществующего оператора!");
                Console.WriteLine("Повторите, пожалуйста, попытку: ");
                telephoneNumber = Console.ReadLine().Trim();
            }

            while (!Regex.IsMatch(telephoneNumber[6..], @"[0-9]{2}-[0-9]{2}-[0-9]{3}"))
            {
                Console.WriteLine("Поле номер телефона включает в себя недопустимые символы!");
                Console.WriteLine("Повторите, пожалуйста, попытку: ");
                telephoneNumber = Console.ReadLine().Trim();
            }
        }

        public static int CheckInt(string number)
        {
            int result;

            while (!int.TryParse(number, out result))
            {
                Console.WriteLine("Строка не является числом!");
                Console.Write("Повторите, пожалуйста, попытку: ");
                number = Console.ReadLine().Trim();
            }

            while (result <= 0)
            {
                while (!int.TryParse(number, out result))
                {
                    Console.WriteLine("Строка не является числом!");
                    Console.Write("Повторите, пожалуйста, попытку: ");
                    number = Console.ReadLine().Trim();
                }
            }
                
            return result;
        }

        public static DateTime CheckDate(string date, string format)
        {
            DateTime result;
            while (!DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                Console.WriteLine("Введённая строка имеет неправильный формат!");
                Console.WriteLine($"Правильный формат: {format}");
                Console.Write("Повторите, пожалуйста, попытку: ");
                date = Console.ReadLine().Trim();
            }

            return result;
        }
    }
}
