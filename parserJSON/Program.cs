using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace parserJSON;

internal class Program
{
    private readonly ILogger<Program> _logger;
    private readonly Worker _worker;
    private readonly JsonManager _manager;

    public Program(ILogger<Program> logger, Worker worker, JsonManager manager)
    {
        _logger = logger;
        _worker = worker;
        _manager = manager;
    }

    public async Task RunAsynk()
    {
        try
        {
            _logger.LogInformation("Старт программы");

            Console.WriteLine(
                "Введите: \n - Абсолютный путь \n - Имя файла в корневой директории \n - Пусто, если стандартный файл JSON");
            var pathLoad = Console.ReadLine();
            if (!String.IsNullOrEmpty(pathLoad))
            {
                while (!File.Exists(pathLoad))
                {
                    Console.WriteLine($"Файл не найден по пути: {Path.GetFullPath(pathLoad)}");

                    Console.WriteLine("Введите корректный путь к файлу: ");
                    pathLoad = Console.ReadLine();
                    
                    if (String.IsNullOrEmpty(pathLoad))
                    {
                        break;
                    }
                }
            }

            List<Data> listData = await _manager.ReadDataFromFileAsync(pathLoad);

            var resultData = _worker.RemoveDuplicate(listData);

            Console.WriteLine(
                "Введите: \n - Абсолютный путь \n - Имя файла в корневой директории \n - Пусто, если стандартный файл JSON");
            var pathSave = Console.ReadLine();
            await _manager.WriteDataToFileAsync(resultData, pathSave);

            _logger.LogInformation("Программа завершила свою работу");

            Console.WriteLine("Нажмите клавишу, чтобы закрыть программу");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при выполнении программы");
        }
    }

    private static void ConfigureServices(IServiceCollection servise)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("Nlog.json", optional: true, reloadOnChange: true)
            .Build();

        servise.AddLogging(configure => configure.AddNLog(config));

        servise.AddSingleton<Worker>();
        servise.AddSingleton<JsonManager>();
        servise.AddSingleton<Program>();
    }

    public static async Task Main(string[] args)
    {
        var servise = new ServiceCollection();
        ConfigureServices(servise);

        using var serviceProvider = servise.BuildServiceProvider();

        var program = serviceProvider.GetService<Program>();
        await program.RunAsynk();
    }
}