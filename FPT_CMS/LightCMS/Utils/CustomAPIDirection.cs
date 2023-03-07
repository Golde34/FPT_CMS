namespace LightCMS.Utils;

public static class CustomAPIDirection
{
    // public const string URL = "http://localhost:5195/api/Subject";
    public static string GetCustomAPIDirection(string direction)
    {
        return "http://localhost:5195/api/" + direction;
    }
}