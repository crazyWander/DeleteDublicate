using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;

namespace parserJSON;

public class JsonManager
{
    private readonly ILogger<JsonManager> _logger;
    private readonly string _defaultLoadedFile = @"Resourses\ShortSummary.json";
    private readonly string _defaultSaveFile = @"Resourses\resultFile.json";

    public JsonManager(ILogger<JsonManager> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Чтение данных из JSON
    /// </summary>
    /// <param name="loadedFile">Полный путь/Имя(если Json находится в директории программы)/Пусто(если чтение из стандартного файла file.json)</param>
    /// <returns></returns>
    public async Task<List<Data>> ReadDataFromFileAsync(string? loadedFile)
    {
        try
        {
            loadedFile = string.IsNullOrEmpty(loadedFile) ? _defaultLoadedFile : loadedFile;
            loadedFile = Path.GetFullPath(loadedFile);
            
            _logger.LogInformation($"Начало чтения файла - {loadedFile}");
            
            string json = File.ReadAllText(loadedFile);
            List<Data> data = JsonConvert.DeserializeObject<List<Data>>(json);
            
            _logger.LogInformation($"Чтение {data.Count} данных завершено");
            
            return data;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Произошла ошибка при выполнении программы");
            throw;
        }
    }

    public async Task WriteDataToFileAsync(List<Data> resultData, string? saveFile)
    {
        try
        {
            saveFile = string.IsNullOrEmpty(saveFile) ? _defaultSaveFile : saveFile;
            saveFile = Path.GetFullPath(saveFile);
            
            _logger.LogInformation($"Начало записи в файл - {saveFile}");
            
            string json = JsonConvert.SerializeObject(resultData, Formatting.Indented);
            File.WriteAllText(saveFile, json);
            
            _logger.LogInformation($"Всего записано - {resultData.Count} уникальных данных");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Произошла ошибка при выполнении программы");
            throw;
        }
    }
}