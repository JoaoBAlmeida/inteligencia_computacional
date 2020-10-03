using DeepSearch_Labyrinth.Services;
using System;

namespace DeepSearch_Labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            SearchService service;
            while (true)
            {
                int opt = MapChoice();
                switch (opt)
                {
                    case 1:
                        service = new SearchService("map_1");
                        break;
                    case 2:
                        service = new SearchService("map_2");
                        break;
                    default:
                        Console.WriteLine("Please, choose a valid map");
                        continue;
                }

                /*
                //service.Widesolver();
                service.Deepsolver();
                service.printMatrix();
                service.printSearch();
                service.printPath();

                Console.WriteLine("Custo de Busca: " + service.getSearchCost());
                Console.WriteLine("Custo do Caminho: " + service.getPathCost());
                */

                service.printMap();
                service.Deepsolver();
                service.printMapPath();
                Console.WriteLine("Busca pesou: " + service.getSearchCost());
                Console.WriteLine("Caminho Pesou: " + service.getPathCost());
                Console.WriteLine("Total de Busca: " + service.getTotalCost());

                Console.WriteLine("================================");
                Console.WriteLine("Want to try again?\nY | Any other key, finishes app");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "Y":
                        continue;
                    case "y":
                        continue;
                    default:
                        return;
                }
            }
        }

        public static int MapChoice()
        {
            Console.WriteLine("Please, Choose your map: ");
            Console.WriteLine("1 - Map 1");
            Console.WriteLine("2 - Map 2");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
