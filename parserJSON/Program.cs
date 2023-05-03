using Newtonsoft.Json;

namespace parserJSON;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        DataList data = new DataList();

        var listData = readDataFromFile();
        var resultData = listData
            .GroupBy(x => new { x.rec_id, x.timestamp })
            .Select(g => g.Count() > 1 ? g.First() : g.Single())
            .ToList();

        writeDataToFile(resultData);
    }

    public static List<Data> readDataFromFile()
    {
        string json = File.ReadAllText("file.json");
        List<Data> datas = JsonConvert.DeserializeObject<List<Data>>(json);
        return datas;
    }

    public static void writeDataToFile(List<Data> resultData)
    {
        string json = JsonConvert.SerializeObject(resultData, Formatting.Indented);
        File.WriteAllText("fileResult.json", json);
    }
}