using Spectre.Console;
using System.Globalization;

namespace CodeTracker.GoldRino456
{
    internal static class DisplayEngine
    {
        /// <summary>
        /// Displays a list of <c>CodingSession</c> objects in a table on the console.
        /// </summary>
        /// <param name="sessions"></param>
        public static void DisplayCodingSessions(List<CodingSession> sessions)
        {
            var table = new Table();

            table.AddColumns("[green]ID[/]", "[green]Start Time[/]", "[green]End Time[/]", "[green]Duration[/]").Centered();

            foreach (var session in sessions)
            {
                var sessionProperties = session.GetDisplayFormattedProperties();
                table.AddRow(sessionProperties[0], sessionProperties[1], sessionProperties[2], sessionProperties[3]);
            }

            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Displays the CodeTracker main menu and prompts user to select an option from a limited list.
        /// </summary>
        /// <returns>A string out of: "Add A New Coding Session", "View Coding Sessions", "Edit A Session", "Delete A Session", or "Quit"</returns>
        public static string PromptForMenuSelection()
        {
            var menuChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What Would You Like To Do?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to view more options.)[/]")
                .AddChoices(new[]
                {
                    "Add A New Coding Session", "View Coding Sessions", "Edit A Session", "Delete A Session", "Quit"
                }));

            return menuChoice;
        }

        /// <summary>
        /// Prompts the user to select a row from the existing <c>CodingSession</c> list or 0 to cancel without any row being deleted.
        /// </summary>
        /// <param name="tableIDs">List of all <c>CodingSession</c> table IDs</param>
        /// <returns>int id number of the row to delete or 0.</returns>
        public static int PromptForDeletion(List<int> tableIDs)
        {
            int rowChoice = PromptForRowChoice(tableIDs, "Which session would you like to delete? (or enter 0 to go back.)");
            if (rowChoice == 0) { return rowChoice; }

            string menuChoice = PromptForConfirmation("Are you sure you want to delete this record? (Warning! This cannot be undone!)");
            if (menuChoice.Equals("Yes")) { return rowChoice; }

            return 0;
        }

        /// <summary>
        /// Prompts the user to select a row from the existing <c>CodingSession</c> list to edit.
        /// </summary>
        /// <param name="tableIDs">List of all <c>CodingSession</c> table IDs</param>
        /// <returns>int id number of the row to edit or 0.</returns>
        public static int PromptForEditChoice(List<int> tableIDs)
        {
            int rowChoice = PromptForRowChoice(tableIDs, "Which session would you like to edit? (or enter 0 to go back.)");
            return rowChoice;
        }

        public static void ClearConsole()
        {
            AnsiConsole.Clear();
        }

        /// <summary>
        /// Prompts the user to fill in information for a new coding session's start and end date. Also confirms with the user that their input is correct before saving.
        /// </summary>
        /// <returns>A new <c>CodingSession</c> object.</returns>
        public static CodingSession PromptForCodingSessionDetails()
        {
            while (true)
            {
                var startTime = PromptForDateTime("What time did the coding session begin? (Please enter in the following format: MM/DD/YYYY HH:MM:SS)");
                var endTime = PromptForDateTime("What time did the coding session end? (Please enter in the following format: MM/DD/YYYY HH:MM:SS)");

                Console.WriteLine(startTime + "\t" + endTime);
                Console.WriteLine("Debug: " + InputValidationUtilities.CheckDateTimeRange(startTime, endTime));

                if (!InputValidationUtilities.CheckDateTimeRange(startTime, endTime))
                {
                    AnsiConsole.MarkupLine("[red]End time must take place AFTER the start time![/]");
                    continue;
                }

                CodingSession newSession = new(startTime, endTime);
                DisplaySingleCodingSession(newSession);

                var menuChoice = PromptForConfirmation("Is the coding session information above correct?");
                if (menuChoice.Equals("Yes")) { return newSession; }
            }
        }

        public static void PromptForAnyKeyPress()
        {
            AnsiConsole.Markup("[yellow]Press any key to continue.[/]");
            AnsiConsole.Console.Input.ReadKey(false);
        }

        private static string PromptForConfirmation(string prompt)
        {
            return AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title(prompt)
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to view more options.)[/]")
                                .AddChoices(new[]
                                {
                    "Yes", "No"
                                }));
        }

        private static int PromptForRowChoice(List<int> tableIDs, string prompt)
        {
            var acceptedInputList = tableIDs;
            acceptedInputList.Add(0);

            var input = AnsiConsole.Prompt(
                                new TextPrompt<string>(prompt)
                                .Validate((n) => InputValidationUtilities.CheckInputAsIntInList(acceptedInputList,n) != null
                                ? ValidationResult.Success()
                                : ValidationResult.Error("[red]Invalid input. Please enter a number from the list above (or 0 to go back).[/]")
                                ));

            return Int32.Parse(input);
        }


        private static DateTime PromptForDateTime(string promptMessage)
        {
            var prompt = AnsiConsole.Prompt(
                new TextPrompt<string>(promptMessage)
                .Validate((n) => InputValidationUtilities.CheckInputAsDateTime(n) != null
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]Please ensure your answer is correctly formatted (MM/DD/YYYY HH:MM:SS).[/]")));

            return InputValidationUtilities.CheckInputAsDateTime(prompt).Value;
        }

        private static void DisplaySingleCodingSession(CodingSession session)
        {
            var table = new Table();
            table.AddColumns("[green]ID[/]", "[green]Start Time[/]", "[green]End Time[/]", "[green]Duration[/]").Centered();

            var sessionProperties = session.GetDisplayFormattedProperties();
            table.AddRow(sessionProperties[0], sessionProperties[1], sessionProperties[2], sessionProperties[3]);

            AnsiConsole.Write(table);
        }
    }
}
