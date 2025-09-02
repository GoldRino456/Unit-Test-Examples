namespace CodeTracker.GoldRino456
{
    public static class CodeTracker
    {
        private static DatabaseManager? _databaseManager;
        private static bool _isAppRunning;

        static void Main()
        {
            InitializeCodeTrackerComponents();
            
            while(_isAppRunning)
            {
                DisplayEngine.ClearConsole();
                var selection = DisplayEngine.PromptForMenuSelection();
                ProcessMenuSelection(selection);
            }
        }

        private static void InitializeCodeTrackerComponents()
        {
            _databaseManager = new();
            _isAppRunning = true;
        }

        private static void ProcessMenuSelection(string selection)
        {
            switch(selection)
            {
                case "Add A New Coding Session":
                    AddCodingSession();
                    break;

                case "View Coding Sessions":
                    ViewCodingSessions();
                    break;

                case "Edit A Session":
                    EditCodingSession();
                    break;

                case "Delete A Session":
                    DeleteCodingSession();
                    break;

                case "Quit":
                    _isAppRunning = false;
                    break;
            }
        }

        private static void AddCodingSession()
        {
            DisplayEngine.ClearConsole();
            var newSession = DisplayEngine.PromptForCodingSessionDetails();
            _databaseManager.CreateNewCodingSession(newSession);
        }

        private static void ViewCodingSessions()
        {
            DisplayEngine.ClearConsole();
            DisplayEngine.DisplayCodingSessions(_databaseManager.GetAllCodingSessions());
            DisplayEngine.PromptForAnyKeyPress();
        }

        private static void EditCodingSession()
        {
            DisplayEngine.ClearConsole();
            DisplayEngine.DisplayCodingSessions(_databaseManager.GetAllCodingSessions());
            var choice = DisplayEngine.PromptForEditChoice(_databaseManager.GetAllRowIDs());

            if(choice == 0) { return; }

            var newSession = DisplayEngine.PromptForCodingSessionDetails();
            _databaseManager.UpdateExistingCodingSession(choice, newSession);
        }

        private static void DeleteCodingSession()
        {
            DisplayEngine.ClearConsole();
            DisplayEngine.DisplayCodingSessions(_databaseManager.GetAllCodingSessions());
            var choice = DisplayEngine.PromptForDeletion(_databaseManager.GetAllRowIDs());

            if (choice == 0) { return; }

            _databaseManager.DeleteExistingCodingSession(choice);
        }
    }
}