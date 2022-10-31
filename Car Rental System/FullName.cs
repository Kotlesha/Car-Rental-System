using System;

namespace Car_Rental_System
{
    [Serializable]
    class FullName
    {
        private string name;
        private string surname;
        private string patronymic;

        public FullName(string name, string surname, string patronymic)
        {
            this.name = name;
            this.surname = surname;
            this.patronymic = patronymic;
        }

        public string Name => name;

        public string Surname => surname;

        public override bool Equals(object obj)
        {
            if (obj is FullName fullName)
            {
                return fullName.name == name && fullName.surname == surname && fullName.patronymic == patronymic;
            }

            return false;
        }

        public override string ToString() => $"Имя: {name}, фамилия: {surname}, отчество: {patronymic}";  
    }
}
