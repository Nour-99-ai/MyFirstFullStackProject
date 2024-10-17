public class OrderNumberGenerator
{
    public static string GenerateUniqueOrderNumber()
    {
        // Generate a new GUID
        var guid = Guid.NewGuid();

        // Get the string representation of the GUID without dashes
        string guidString = guid.ToString("N"); // "N" format removes dashes

        // Take the first 7 characters of the GUID string
        string orderNumber = guidString.Substring(0, 7).ToUpper();

        return orderNumber;
    }


}