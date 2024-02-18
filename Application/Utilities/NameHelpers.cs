using System.Text.RegularExpressions;

namespace Application.Utilities;

public static class NameHelpers
{
    public static string CleanName(string name)
    {
        return Regex.Replace(name, @"\s+", "").ToUpper();
    }
}
