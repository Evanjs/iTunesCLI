using System;
using Fclp;
using iTunesLibFramework;


namespace iTunesCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new FluentCommandLineParser();
            p.Setup<string>('p').Callback(songName => Commands.PlayFirstSongMatchingName(songName).Write());
            p.Setup<string>("play").Callback(songName => Commands.Play().Write());
            p.Setup<string>("pause").Callback(songName => Commands.Pause().Write());
            p.Setup<string>("playpause").Callback(songName => Commands.PlayPause().Write());
            p.Setup<string>('s', "psbid").Callback(ids => Commands.PlaySongById(0, 0, 0, 0).Write());
            p.Setup<string>("ssbn").Callback(x => Commands.Search(x).ForEach(result => result.Write()));
            p.Parse(args);
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