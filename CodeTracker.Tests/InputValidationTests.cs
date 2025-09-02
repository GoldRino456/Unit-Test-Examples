using CodeTracker.GoldRino456;

namespace CodeTracker.Tests;

[TestClass]
public class InputValidationTests
{
    [TestMethod]
    public void CheckDateTimeRange_StartTimeBeforeEndTime_ReturnTrue()
    {
        var startingTime = DateTime.UnixEpoch;
        var endingTime = DateTime.UnixEpoch.AddDays(2);

        var result = InputValidationUtilities.CheckDateTimeRange(startingTime, endingTime);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CheckDateTimeRange_StartTimeAfterEndTime_ReturnFalse()
    {
        var startingTime = DateTime.UnixEpoch.AddDays(2);
        var endingTime = DateTime.UnixEpoch;

        var result = InputValidationUtilities.CheckDateTimeRange(startingTime, endingTime);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CheckDateTimeRange_StartTimeEqualsEndTime_ReturnFalse()
    {
        var startingTime = DateTime.UnixEpoch;
        var endingTime = DateTime.UnixEpoch;

        var result = InputValidationUtilities.CheckDateTimeRange(startingTime, endingTime);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CheckInputAsDateTime_ValidFormatting_ReturnDateTime()
    {
        string dateString = "01/23/2023 01:25:23";
        DateTime dateTime = new DateTime(2023, 1, 23, 1, 25, 23);

        var result = InputValidationUtilities.CheckInputAsDateTime(dateString);

        Assert.AreEqual(result, dateTime);
    }

    [TestMethod]
    public void CheckInputAsDateTime_MissingLeadingZeroes_ReturnNull()
    {
        string dateString = "2/17/2023 5:31:19";

        var result = InputValidationUtilities.CheckInputAsDateTime(dateString);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void CheckInputAsDateTime_DateOnlyNoPunctuation_ReturnNull()
    {
        string dateString = "6092015";

        var result = InputValidationUtilities.CheckInputAsDateTime(dateString);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void CheckInputAsInt_ValidInput_ReturnInt()
    {
        string intString = "15";
        int validInt = 15;

        var result = InputValidationUtilities.CheckInputAsInt(intString);

        Assert.AreEqual(result, validInt);
    }

    [TestMethod]
    public void CheckInputAsInt_TypoWithNumber_ReturnNull()
    {
        string intString = "3 1";

        var result = InputValidationUtilities.CheckInputAsInt(intString);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void CheckInputAsInt_GibberishStringWithNumbers_ReturnNull()
    {
        string intString = "#78G@34wasd";

        var result = InputValidationUtilities.CheckInputAsInt(intString);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void CheckInputAsIntInList_ValidIntInList_ReturnInt()
    {
        List<int> ints = [1, 2, 3, 4, 5];
        string intString = "5";

        var result = InputValidationUtilities.CheckInputAsIntInList(ints, intString);

        Assert.IsInstanceOfType<Int32>(result);
    }

    [TestMethod]
    public void CheckInputAsIntInList_ValidIntNotInList_ReturnNull()
    {
        List<int> ints = [1, 2, 3, 4, 5];
        string intString = "12";

        var result = InputValidationUtilities.CheckInputAsIntInList(ints, intString);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void CheckInputAsIntInList_InvalidInt_ReturnNull()
    {
        List<int> ints = [1, 2, 3, 4, 5];
        string intString = "Q";

        var result = InputValidationUtilities.CheckInputAsIntInList(ints, intString);

        Assert.IsNull(result);
    }
}

