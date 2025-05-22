using System.Globalization;

namespace EmployeeReports.Server.Utils
{
    public class DateParser
    {
        private static readonly string[] SupportedFormats = new[]
        {
            "yyyy-MM-dd",
            "MM/dd/yyyy",
            "dd-MM-yyyy",
            "dd/MM/yyyy",
            "yyyy/MM/dd",
            "yyyyMMdd",
            "dd MMM yyyy",
            "MMM dd, yyyy",
            "yyyy-MM-ddTHH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "M/d/yyyy", "M-d-yyyy", 
            "d/M/yyyy",
            "d-M-yyyy"
        };

        public static bool TryParse(string input, out DateTime date)
        {
            return DateTime.TryParseExact(
                input.Trim(),
                SupportedFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date
            );
        }
    }
}
