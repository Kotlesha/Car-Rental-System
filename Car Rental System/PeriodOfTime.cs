using System;

namespace Car_Rental_System
{
    [Serializable]
    class PeriodOfTime
    {
        private DateTime startTime;
        private DateTime endTime;

        public PeriodOfTime(DateTime startTime, DateTime endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;

            if (startTime > endTime)
            {
                this.startTime = endTime;
                this.endTime = startTime;
                Console.WriteLine("Предупреждение! Время окончания должно быть меньше, чем время начала!");
            }
        }

        public int Period() => (int)Math.Ceiling((endTime - startTime).TotalDays);

        public override string ToString() => $"Начало: {startTime}, конец: {endTime}, период в сутках: {Period()}";
    }
}
