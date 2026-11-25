namespace AseanPhonebook;

/// <summary>Holds all students (in memory only) and provides add / find / search.</summary>
public class Phonebook
{
    private readonly List<Student> _students = new();   // no size limit

    public bool Exists(string studentNumber) => Find(studentNumber) != null;

    public Student? Find(string studentNumber) =>
        _students.FirstOrDefault(s =>
            s.StudentNumber.Equals(studentNumber, StringComparison.OrdinalIgnoreCase));

    /// <summary>Adds a student; returns false if the student number is already used.</summary>
    public bool Add(Student student)
    {
        if (Exists(student.StudentNumber)) return false;
        _students.Add(student);
        return true;
    }

    /// <summary>Changes a student number; returns false if the new one is taken.</summary>
    public bool ChangeStudentNumber(Student student, string newNumber)
    {
        if (!student.StudentNumber.Equals(newNumber, StringComparison.OrdinalIgnoreCase)
            && Exists(newNumber))
            return false;
        student.StudentNumber = newNumber;
        return true;
    }

    /// <summary>Students whose CURRENT country code is in the list, sorted by surname.</summary>
    public List<Student> SearchByCountries(IEnumerable<AseanCountry> countries)
    {
        var codes = countries.Select(c => c.Code).ToHashSet();
        return _students
            .Where(s => codes.Contains(s.Phone.CountryCode))
            .OrderBy(s => s.Surname, StringComparer.OrdinalIgnoreCase)
            .ThenBy(s => s.FirstName, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
}
