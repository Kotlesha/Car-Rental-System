using System;

namespace Car_Rental_System
{
    [Serializable]
    class Car : IComparable
    {
        private string brand;
        private string model;
        private string registrationNumber;
        private int price;
        private bool isInTheRental = false;
        private string startRentalPoint;

        public int Price => price;

        public string StartRentalPoint
        {
            get => startRentalPoint;
            set => startRentalPoint = value;
        }

        public bool IsInTheRental
        {
            get => isInTheRental;
            set => isInTheRental = value;
        }

        public Car(string brand, string model, string registrationNumber, int price)
        {
            this.brand = brand;
            this.registrationNumber = registrationNumber;
            this.model = model;
            this.price = price;
        }

        public int CompareTo(object obj)
        {
            if (obj is Car car)
            { 
                return registrationNumber.CompareTo(car.registrationNumber);
            }
            else
            {
                throw new ArgumentException("Некорекктное значение параметра!", nameof(obj));
            }
        }

        public override string ToString()
        {
            string choice = IsInTheRental ? "Да" : "Нет";
            return $"Информация о машине: марка: {brand}, модель: {model}, регистрационный номер: {registrationNumber}, цена аренды за сутки: {price}, находится ли в аренде: {choice}";
        }
    }
}
