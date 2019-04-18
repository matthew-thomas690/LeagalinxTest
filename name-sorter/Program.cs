using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace name_sorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole())
            .AddTransient<INameDataParser, LegalinXNameDataParser>()
            .AddSingleton<IComparer<FullName>, LegalinXNameComparer>()
            .BuildServiceProvider();         

            try
            {
                var nameData = File.ReadAllLines(args.First());
                var nameParser = serviceProvider.GetService<INameDataParser>();
                var names = nameParser.Parse(nameData).ToList();
                names.Sort(serviceProvider.GetService<IComparer<FullName>>());


                INameWriter fileWriter = new FileNameWriter("./sorted-names-list.txt");
                fileWriter.Execute(names);
                INameWriter consoleWriter = new ConsoleNameWriter(Console.Out);

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("------- NAME LIST -------");
                Console.WriteLine(Environment.NewLine);

                consoleWriter.Execute(names);

            }
            catch(Exception ex)
            {
                var logger = serviceProvider.GetService<ILogger<Program>>();
                logger.LogError(ex, ex.Message);
            }


            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
    }
}
