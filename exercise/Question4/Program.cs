using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Question4
{
    class Program
    {


        //Method 1: 使用類型定義，比較不好
        // private class Operation
        // {
        //     public string Command { get; }
        //     public Func<Task> Action { get; }

        //     public Operation(string command, Func<Task> action)
        //     {
        //         Command = command;
        //         Action = action;
        //     }
        // }

        // private static readonly Dictionary<int, Operation> Operations = new Dictionary<int, Operation>
        // {
        //     [0] = new Operation("top", RunSearchBatchAsync),
        //     [1] = new Operation("country", RunSearchBatchAsync)
        // };



        // static void Main(string[] args)
        // {
        //     Console.WriteLine("Hello World!");




        // }

        //  private static async Task RunSearchBatchAsync()
        // {


        //     // SearchInstancesRequest searchRequest = new SearchInstancesRequest(
        //     //     "GearboxSensor",
        //     //     new List<string>() { "Contoso WindFarm Hierarchy" },
        //     //     new SearchInstancesParameters(true, new InstancesSortParameter(InstancesSortBy.Rank), true, 10),
        //     //     new SearchInstancesHierarchiesParameters(
        //     //         new HierarchiesExpandParameter(HierarchiesExpandKind.UntilChildren),
        //     //         new HierarchiesSortParameter(HierarchiesSortBy.CumulativeInstanceCount),
        //     //         10));
        //     // SearchInstancesResponsePage searchResponse = await _client.TimeSeriesInstances.SearchAsync(searchRequest);
        //     // PrintResponse(searchResponse);
        // }





        private delegate string CommandDelegate(string[] args);



        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Wrong Command. command format like: <action> <arg1> [<arg2>...]");
                return;
            }

            CommandDelegate d;

            switch (args[0].ToLower())
            {
                case "top":
                    d = GetTopProcess;

                    break;

                case "country":
                    d = GetCountryProcess;

                    break;

                default:
                    Console.WriteLine($"Unknown Command '{args[0].ToLower()}'. command format like: <action> <arg1> [<arg2>...]");
                    return;
            }

            Console.WriteLine(d(args.Skip(1).ToArray()));


        }



        private static string GetTopProcess(string[] args)
        {
            var c = new Clawer();
            try
            {
                return c.GetTopUri(int.Parse(args[0])).Result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }


        private static string GetCountryProcess(string[] args)
        {
            var c = new Clawer();
            try
            {
                return c.GetCountryTopUri(args[0], 20).Result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

    }
}
