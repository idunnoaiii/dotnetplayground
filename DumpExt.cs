
using Newtonsoft.Json;

namespace DotnetPlayGround;

public static class Helper
{
    public static void Dump<T>(this T data)
    {
        string json = JsonConvert.SerializeObject(
            data,
            Formatting.Indented);

        Console.WriteLine(json);
    }
}