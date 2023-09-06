namespace WebSite.Helpers;
public static class ReadJsonFromFileHelper
{
    public static string ReadJsonFromTextFile(string filePath)
    {
        try
        {
            string text = System.IO.File.ReadAllText(filePath);
            return text;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed: " + ex.Message);
            return "[]";
        }

    }
}