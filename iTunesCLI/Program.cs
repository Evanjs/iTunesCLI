using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Hosting;
using System.Security.Cryptography.X509Certificates;
using iTunesLib;
using iTunesLibFramework;

namespace iTunesCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter a command.");
                var c = Console.ReadLine();

                var arguments = args.Length == 0 ? c.Split(' ') : args;

                if (arguments.Any(a => a == "play") || c == "play")
                    Commands.Play();
                else if (arguments.Any(a => a == "pa") || c == "pa")
                {
                    Commands.Pause();
                }
                else if (arguments.Any(a => a == "pp") || c == "pp")
                {
                    Commands.PlayPause();
                }
                else if (arguments.Any(a => a == "psbid" && arguments.Length == 5))
                {
                    var message = Commands.PlaySongById(arguments[1].ToInt32(), arguments[2].ToInt32(), arguments[3].ToInt32(), arguments[4].ToInt32());
                    Console.WriteLine(message);
                }
                else if (arguments.Any(a => a == "ssbn" && arguments.Length == 2))
                {
                    var query = arguments[1];
                    var searchResults = Commands.Search(query);
                    searchResults.ForEach(Console.WriteLine);
                }
                else if (arguments.Any(a => a == "quit") || c == "quit")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Command not found.");
                }

            }
        }
    }
}
