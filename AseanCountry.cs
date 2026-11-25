namespace AseanPhonebook;

/// <summary>Represents one supported ASEAN country and its dialing code.</summary>
public class AseanCountry
{
    public string Name { get; }
    public string Code { get; }

    public AseanCountry(string name, string code)
    {
        Name = name;
        Code = code;
    }

    // Order matches the search menu: [1] Philippines ... [5] Malaysia
    public static readonly IReadOnlyList<AseanCountry> All = new List<AseanCountry>
    {
        new("Philippines", "63"),
        new("Thailand",    "66"),
        new("Singapore",   "65"),
        new("Indonesia",   "62"),
        new("Malaysia",    "60"),
    };

    public static AseanCountry? FromCode(string code) =>
        All.FirstOrDefault(c => c.Code == code);

    public static bool IsValidCode(string code) => FromCode(code) != null;
}
