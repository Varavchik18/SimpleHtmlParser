using System.Collections.ObjectModel;

namespace DemoConsoleAppWebClient
{
    public class DateHelper
    {

        public void ShowPossibleLocalTimeZones(DateTimeOffset offsetDateTime)
        {
            TimeSpan offset = offsetDateTime.Offset;
            ReadOnlyCollection<TimeZoneInfo> timeZones;

            Console.WriteLine("{0} could belong to the following time zones:",
                              offsetDateTime.ToString());
            timeZones = TimeZoneInfo.GetSystemTimeZones();
            foreach (TimeZoneInfo timeZone in timeZones)
            {
                if (timeZone.GetUtcOffset(offsetDateTime.DateTime).Equals(offset))
                    Console.WriteLine("   {0}", timeZone.DisplayName);
            }
            Console.WriteLine();
        }

        public DateTime GetCurrentDateAndTime()
        {
            DateTime dateTime = DateTime.Now;

            DateTime thisDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            return thisDate;
        }

        public DateOnly GetCurrentDateOnly(DateTime dateTime) => DateOnly.FromDateTime(dateTime);
        public TimeOnly GetCurrentTimeOnly(DateTime dateTime) => TimeOnly.FromDateTime(dateTime);
    }
}
