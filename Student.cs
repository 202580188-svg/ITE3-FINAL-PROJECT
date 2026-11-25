namespace AseanPhonebook;

/// <summary>One phonebook entry. Knows how to describe itself; owns no storage logic.</summary>
public class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public char Gender { get; set; }          // 'M' or 'F'
    public PhoneNumber Phone { get; }

    public Student(string studentNumber, string surname, string firstName,
                   string occupation, char gender, PhoneNumber phone)
    {
        StudentNumber = studentNumber;
        Surname = surname;
        FirstName = firstName;
        Occupation = occupation;
        Gender = char.ToUpper(gender);
        Phone = phone;
    }

    private string Pronoun => Gender == 'F' ? "Her" : "His";

    /// <summary>Format used by the edit menu.</summary>
    public string ToEditString() =>
        $"{FirstName} {Surname} is a {Occupation}. {Pronoun} number is {Phone}";

    /// <summary>Format used by the search results.</summary>
    public string ToSearchString() =>
        $"{Surname}, {FirstName}, with student number {StudentNumber}, is a {Occupation}. " +
        $"{Pronoun} phone number is {Phone}";
}
