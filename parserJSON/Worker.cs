using Microsoft.Extensions.Logging;

namespace parserJSON;

/// <summary>
/// Класс для проверки корректности
/// </summary>
public class Worker
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Метод получения дубликатов, использовать для проверки
    /// </summary>
    /// <param name="listData">Список данных, полученных с JSON файла</param>
    /// <returns>Список дубликатов, в единственном экземпляре</returns>
    public List<Data> getDublicate(List<Data> listData)
    {
        _logger.LogInformation("Начало поиска дубликатов");
        var deletedList = listData.GroupBy(x => new { x.rec_id, x.timestamp })
            .Where(g => g.Count() > 1)
            .Select(g => g.First())
            .ToList();
        _logger.LogInformation($"Удалено {deletedList.Count}");
        return deletedList;
    }

    /// <summary>
    /// Получение очищенного от дубликатов списка
    /// </summary>
    /// <param name="listData">Список данных, полученных с JSON файла</param>
    /// <returns>Очищенный список</returns>
    public List<Data> RemoveDuplicate(List<Data> listData)
    {
        _logger.LogInformation("Начало очистки дубликатов");
        var list = listData
            .GroupBy(x => new { x.rec_id, x.timestamp })
            .Select(g => g.Count() > 1 ? g.First() : g.Single())
            .ToList();
        _logger.LogInformation($"Найдено дубликатов {listData.Count - list.Count}");
        return list;
    }
}