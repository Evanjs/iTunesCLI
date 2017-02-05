using System;
using System.Collections.Generic;
using Fclp;
using iTunesLibFramework;


namespace iTunesCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new FluentCommandLineParser();
            if (args.Length == 0)
                args = Console.ReadLine()?.Split(' ');

            p.Setup<string>('s', "playfirst").Callback(songName => Commands.PlayFirstSongMatchingName(songName).Write());
            p.Setup<string>('p', "play").Callback(message => Commands.Play().Write());
            p.Setup<string>('u', "pause").Callback(message => Commands.Pause().Write());
            p.Setup<string>('l', "playpause").Callback(songName => Commands.PlayPause().Write());
            p.Setup<List<int>>('i', "playsongbyid").Callback(ids => Commands.PlaySongById(ids[0], ids[1], ids[2], ids[3]).Write());
            p.Setup<string>('f', "searchsongs").Callback(x => Commands.Search(x).ForEach(result => result.Write()));
            p.Parse(args);
            Commands.iTunes = null;
        }
    }

    public static class Extensions
    {
        public static void Write(this string output)
        {
            Console.WriteLine(output);
        }
    }
}