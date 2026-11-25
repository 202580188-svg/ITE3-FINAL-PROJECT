namespace AseanPhonebook;

/// <summary>Reusable, validated console input.</summary>
public static class ConsoleHelper
{
    public static string ReadNonEmpty(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string v = (Console.ReadLine() ?? "").Trim();
            if (v.Length > 0) return v;
            Console.WriteLine("Input cannot be empty.");
        }
    }

    public static string ReadDigits(string prompt)
    {
        while (true)
        {
            string v = ReadNonEmpty(prompt);
            if (v.All(char.IsDigit)) return v;
            Console.WriteLine("Digits only, please.");
        }
    }

    public static char ReadGender(string prompt)
    {
        while (true)
        {
            string v = ReadNonEmpty(prompt).ToUpper();
            if (v == "M" || v == "F") return v[0];
            Console.WriteLine("Enter M or F.");
        }
    }

    public static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            string v = ReadNonEmpty(prompt).ToUpper();
            if (v == "Y") return true;
            if (v == "N") return false;
            Console.WriteLine("Enter Y or N.");
        }
    }

    /// <summary>Re-prompts until the code is one of the five supported ASEAN codes (e.g. 99 is rejected).</summary>
    public static string ReadCountryCode(string prompt)
    {
        while (true)
        {
            string v = ReadDigits(prompt);
            if (AseanCountry.IsValidCode(v)) return v;
            Console.WriteLine("Invalid country code. Valid codes: " +
                string.Join(", ", AseanCountry.All.Select(c => $"{c.Code} ({c.Name})")));
        }
    }

    public static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int n)) return n;
            Console.WriteLine("Enter a number.");
        }
    }
}
