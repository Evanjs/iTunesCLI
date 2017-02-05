using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using iTunesLib;
using iTunesAdminLib;

namespace iTunesLibFramework
{
    public static class Commands
    {
        public static IiTunes iTunes { get; set; } = new iTunesAppClass();
        public static IiTunesAdmin iTunesAdmin { get; set; }

        public static void Play() => iTunes.Play();
        public static void Pause() => iTunes.Pause();
        public static void PlayPause() => iTunes.PlayPause();
        public static List<string> SearchSongsByName(string query) =>
            (from IITTrack track in iTunes.LibraryPlaylist.Tracks
             where track.Name.ToLower().Contains(query)
             select GetTrackInfo(track)).ToList();

        public static string GetTrackInfo(IITTrack track) => $@"
Name: {track.Name}
Artist: {track.Artist}
Album: {track.Album}
Playlist: {track.Playlist.Name}
Source ID: {track.sourceID}
Playlist ID: {track.playlistID}
Track ID: {track.trackID}
Database ID: {track.TrackDatabaseID}";

        public static void GetPIDsFromOtherIds(int sourceID, int playlistID, int trackID, int databaseID, out int high,
            out int low)
        {
            var refo = iTunes.GetITObjectByID(sourceID, playlistID, trackID, databaseID);
            iTunes.GetITObjectPersistentIDs(refo, out high, out low);
        }

        public static string PlaySongById(int sourceID, int playlistID, int trackID, int databaseID)
        {
            int high, low;
            //var track = iTunes.LibrarySource.GetITObjectIDs()
            GetPIDsFromOtherIds(sourceID, playlistID, trackID, databaseID, out high, out low);
            //iTunes.get;
            //iTunes.LibraryPlaylist.Tracks.ItemByPersistentID[high, low].Play();
            var t = iTunes.LibraryPlaylist.Tracks.ItemByPersistentID[high, low];
            t.Play();
            return $"Now playing: {t.Name} by {t.Artist} from album {t.Album}";
        }
        public static List<string> Search(string query) => iTunes.LibraryPlaylist.Search(query, ITPlaylistSearchField.ITPlaylistSearchFieldSongNames).Cast<IITTrack>().Select(GetTrackInfo).ToList();
    }
}
