namespace AseanPhonebook;

/// <summary>Value holder for an international phone number: country code, area code, number.
/// </summary>
public class PhoneNumber
{
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string Number { get; set; }

    public PhoneNumber(string countryCode, string areaCode, string number)
    {
        CountryCode = countryCode;
        AreaCode = areaCode;
        Number = number;
    }

    public override string ToString() => $"{CountryCode}-{AreaCode}-{Number}";
}
