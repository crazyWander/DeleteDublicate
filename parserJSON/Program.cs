using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace parserJSON;

internal class Program
{
    public static void Main(string[] args)
    {
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddNLog());
        Worker worker = new Worker(loggerFactory);
        JsonManager manager = new JsonManager(loggerFactory);
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogInformation("Программа запустилась корректно");
        List<Data> listData = manager.readDataFromFile();
        logger.LogInformation($"Чтение {listData.Count} данных завершено");
        var resultData = worker.removeDublicate(listData);
        logger.LogInformation($"Найдено дубликатов {listData.Count-resultData.Count}");
        manager.writeDataToFile(resultData);
        logger.LogInformation($"Всего записано - {resultData.Count} уникальных данных");
    }
}