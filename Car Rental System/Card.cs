using System;

namespace Car_Rental_System
{
    [Serializable]
    class Card
    {
        private string numberOfCard;
        private DateTime periodOfValidity;

        public Card(string numberOfCard, DateTime periodOfValidity)
        {
            this.numberOfCard = numberOfCard;
            this.periodOfValidity = periodOfValidity;
        }

        public override bool Equals(object obj)
        {
            if (obj is Card card)
            {
                return card.numberOfCard == numberOfCard && card.periodOfValidity == periodOfValidity;
            }

            return false;
        }

        public override string ToString() => $"Номер кредитной карты: {numberOfCard}, срок действия: {periodOfValidity.ToShortDateString()}";
    }
}
