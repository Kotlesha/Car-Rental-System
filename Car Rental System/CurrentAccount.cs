using System;

namespace Car_Rental_System
{
    enum TypeOfPayment
    {
        Card,
        Money
    }

    [Serializable]
    class CurrentAccount
    {
        private TypeOfPayment typeOfPayment;
        private int amountOfMoney;
        private Card card;

        public CurrentAccount(TypeOfPayment typeOfPayment, int amountOfMoney)
        {
            this.typeOfPayment = typeOfPayment;
            this.amountOfMoney = amountOfMoney;
        }

        public CurrentAccount(TypeOfPayment typeOfPayment, int amountOfMoney, string numberOfCard, DateTime periodOfValidity) : this(typeOfPayment, amountOfMoney)
        {
            this.card = new(numberOfCard, periodOfValidity);
        }
   
        public void Calculation(int money) => amountOfMoney -= money;

        public bool CheckMoney(int money)
        {
            if (amountOfMoney < 0 || amountOfMoney - money < 0)
            {
                return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is CurrentAccount currentAccount)
            {
                if (currentAccount.card == null) 
                {
                    return currentAccount.typeOfPayment == typeOfPayment && currentAccount.amountOfMoney == amountOfMoney;
                }
                else
                {
                    return currentAccount.typeOfPayment == typeOfPayment && currentAccount.amountOfMoney == amountOfMoney && currentAccount.card.Equals(card);
                }
            }

            return false;
        }

        public override string ToString()
        {
            string choice = typeOfPayment == TypeOfPayment.Card ? "карта" : "наличные";
            return $"тип оплаты: {choice}, остаток: {amountOfMoney}";
        }
    }
}
