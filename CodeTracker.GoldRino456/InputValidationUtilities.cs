using System.Globalization;

namespace CodeTracker.GoldRino456;

public static class InputValidationUtilities
{
    public static DateTime? CheckInputAsDateTime(string input)
    {
        var isValid = DateTime.TryParseExact(input.Trim(),
            "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, 
            out var dateTime);

        if(isValid)
        {
            return dateTime;
        }

        return null;
    }

    public static bool CheckDateTimeRange(DateTime startingDateTime, DateTime endingDateTime)
    {
        return endingDateTime > startingDateTime;
    }

    public static int? CheckInputAsInt(string input)
    {
        var isValid = Int32.TryParse(input.Trim(), out var num);

        if (isValid)
        {
            return num;
        }

        return null;
    }

    public static int? CheckInputAsIntInList(List<int> integerList, string input)
    {
        var validInt = CheckInputAsInt(input);

        if(validInt != null && integerList.Contains(validInt.Value))
        {
            return validInt;
        }

        return null;
    }
}
