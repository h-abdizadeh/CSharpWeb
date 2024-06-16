using System.Globalization;

namespace SheypoorChi.Core.Classes;

public class DateTimeGeneration
{
    public string GetPersianDate()
    {
        var date = DateTime.Now;
        var persian = new PersianCalendar();

        var pdate =
            $"{persian.GetYear(date)}/{persian.GetMonth(date)}/{persian.GetDayOfMonth(date)}";

        return pdate;
    }

}
