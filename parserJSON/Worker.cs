using Microsoft.Extensions.Logging;

namespace parserJSON;

/// <summary>
/// Класс для проверки корректности
/// </summary>
public class Worker
{
    private readonly ILogger<Worker> _logger;
    public Worker(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<Worker>();
    }

    /// <summary>
    /// Метод получения дубликатов, использовать для проверки
    /// </summary>
    /// <param name="listData">Список данных, полученных с JSON файла</param>
    /// <returns>Список дубликатов, в единственном экземпляре</returns>
    public List<Data> getDublicate(List<Data> listData)
    {
        return listData.GroupBy(x => new { x.rec_id, x.timestamp })
            .Where(g => g.Count() > 1)
            .Select(g => g.First())
            .ToList();
    }

    /// <summary>
    /// Получение очищенного от дубликатов списка
    /// </summary>
    /// <param name="listData">Список данных, полученных с JSON файла</param>
    /// <returns>Очищенный список</returns>
    public List<Data> removeDublicate(List<Data> listData)
    {
        return listData
            .GroupBy(x => new { x.rec_id, x.timestamp })
            .Select(g => g.Count() > 1 ? g.First() : g.Single())
            .ToList();
    }
}