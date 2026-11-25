namespace AseanPhonebook;

/// <summary>Menus and workflow. Delegates storage/searching to Phonebook.</summary>
public class PhonebookApp
{
    private readonly Phonebook _book = new();

    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("[1] Store to ASEAN phonebook");
            Console.WriteLine("[2] Edit entry in ASEAN phonebook");
            Console.WriteLine("[3] Search ASEAN phonebook by country");
            Console.WriteLine("[4] Exit");
            int choice = ConsoleHelper.ReadInt("Enter choice: ");

            switch (choice)
            {
                case 1: StoreEntries(); break;
                case 2: EditEntry(); break;
                case 3: SearchByCountry(); break;
                case 4: running = false; break;
                default: Console.WriteLine("Invalid option."); break;
            }
        }
    }

    private void StoreEntries()
    {
        do
        {
            Console.WriteLine();
            string number;
            while (true)
            {
                number = ConsoleHelper.ReadNonEmpty("Enter student number: ");
                if (!_book.Exists(number)) break;
                Console.WriteLine("That student number already exists.");
            }
            string surname = ConsoleHelper.ReadNonEmpty("Enter surname: ");
            string first = ConsoleHelper.ReadNonEmpty("Enter first name: ");
            string job = ConsoleHelper.ReadNonEmpty("Enter occupation: ");
            char gender = ConsoleHelper.ReadGender("Enter gender (M for male, F for female): ");
            string cc = ConsoleHelper.ReadCountryCode("Enter country code: ");
            string area = ConsoleHelper.ReadDigits("Enter area code: ");
            string num = ConsoleHelper.ReadDigits("Enter number: ");

            var student = new Student(number, surname, first, job, gender,
                                      new PhoneNumber(cc, area, num));
            _book.Add(student);
            Console.WriteLine("Entry stored.");
        }
        while (ConsoleHelper.ReadYesNo("Do you want to enter another entry [Y/N]? "));
    }

    private void EditEntry()
    {
        Console.WriteLine();
        string number = ConsoleHelper.ReadNonEmpty("Enter student number: ");
        Student? s = _book.Find(number);
        if (s == null)
        {
            Console.WriteLine("Error: student number not found.");
            return;
        }

        while (true)
        {
            Console.WriteLine($"Here is the existing information about {s.StudentNumber}:");
            Console.WriteLine(s.ToEditString());
            Console.WriteLine("Which of the following information do you wish to change?");
            Console.WriteLine("[1] Student number [2] Surname [3] Gender [4] Occupation");
            Console.WriteLine("[5] Country code [6] Area code [7] Phone number");
            Console.WriteLine("[8] None - Go back to main menu");
            int choice = ConsoleHelper.ReadInt("Enter choice: ");

            switch (choice)
            {
                case 1:
                    string newNum = ConsoleHelper.ReadNonEmpty("Enter new student number: ");
                    if (!_book.ChangeStudentNumber(s, newNum))
                        Console.WriteLine("That student number already exists. No change made.");
                    break;
                case 2: s.Surname = ConsoleHelper.ReadNonEmpty("Enter new surname: "); break;
                case 3: s.Gender = ConsoleHelper.ReadGender("Enter new gender (M/F): "); break;
                case 4: s.Occupation = ConsoleHelper.ReadNonEmpty("Enter new occupation: "); break;
                case 5: s.Phone.CountryCode = ConsoleHelper.ReadCountryCode("Enter new country code: "); break;
                case 6: s.Phone.AreaCode = ConsoleHelper.ReadDigits("Enter new area code: "); break;
                case 7: s.Phone.Number = ConsoleHelper.ReadDigits("Enter new phone number: "); break;
                case 8: return;
                default: Console.WriteLine("Invalid option."); break;
            }
            Console.WriteLine();
        }
    }

    private void SearchByCountry()
    {
        Console.WriteLine();
        Console.WriteLine("From which country:");
        Console.WriteLine("[1] Philippines [2] Thailand [3] Singapore [4] Indonesia");
        Console.WriteLine("[5] Malaysia [6] ALL [0] No More");

        var chosen = new List<AseanCountry>();
        bool all = false;
        int n = 1;
        while (true)
        {
            int c = ConsoleHelper.ReadInt($"Enter choice {n}: ");
            if (c == 0) break;
            if (c == 6) { all = true; break; }
            if (c >= 1 && c <= 5)
            {
                var country = AseanCountry.All[c - 1];
                if (!chosen.Contains(country)) chosen.Add(country);
                n++;
            }
            else Console.WriteLine("Invalid option.");
        }

        if (all) chosen = AseanCountry.All.ToList();
        if (chosen.Count == 0)
        {
            Console.WriteLine("No country selected.");
            return;
        }

        string names = all ? "all countries" : string.Join(" and ", chosen.Select(c => c.Name));
        var results = _book.SearchByCountries(chosen);
        Console.WriteLine($"Here are the students from {names}:");
        if (results.Count == 0) Console.WriteLine("(no entries found)");
        foreach (var s in results) Console.WriteLine(s.ToSearchString());
    }
}
