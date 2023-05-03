using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;

namespace parserJSON;

public class JsonManager
{
    public JsonManager(ILoggerFactory loggerFactory)
    {
        loggerFactory.CreateLogger<JsonManager>();
    }

    public List<Data> readDataFromFile()
    {
        string json = File.ReadAllText("file.json");
        List<Data> data = JsonConvert.DeserializeObject<List<Data>>(json);
        return data;
    }
    
    public void writeDataToFile(List<Data> resultData)
    {
        string json = JsonConvert.SerializeObject(resultData, Formatting.Indented);
        File.WriteAllText("fileResult.json", json);
    }
}