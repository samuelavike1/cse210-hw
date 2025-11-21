public class Address
{
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        // Simple check – you can expand this if you want
        string countryUpper = _country.ToUpper();
        return countryUpper == "USA" || countryUpper == "UNITED STATES" || countryUpper == "UNITED STATES OF AMERICA";
    }

    public string GetFullAddress()
    {
        // Multiline string for shipping label
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}