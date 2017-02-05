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

        public static string Play()
        {
            iTunes.Play();
            return PlayerState;
        }
        public static string Pause()
        {
            iTunes.Pause();
            return PlayerState;
        }

        public static string PlayPause()
        {
            iTunes.PlayPause();
            return PlayerState.ToString();
        }

        private static string PlayerState
        {
            get
            {
                switch (iTunes.PlayerState)
                {
                    case ITPlayerState.ITPlayerStatePlaying:
                        return "Now Playing.";
                    case ITPlayerState.ITPlayerStateStopped:
                        return "Paused.";
                    case ITPlayerState.ITPlayerStateFastForward:
                        return "Fast Forwarding...";
                    case ITPlayerState.ITPlayerStateRewind:
                        return "Rewinding...";
                    default:
                        return "Cannot determine player status.";
                }
            }
        }

        public static List<string> SearchSongsByName(string query) =>
            (from IITTrack track in iTunes.LibraryPlaylist.Tracks
             where track.Name.ToLower().Contains(query)
             select GetTrackInfo(track)).ToList();

        public static IITPlaylist FirstPlaylistMatchingString(string query)
            => (from IITPlaylist p in
                    iTunes.LibrarySource.Playlists
                where p.Name.ToLower().Contains(query.ToLower())
                select p).FirstOrDefault();
        //(from IITPlaylist playlist in
        //    iTunes.LibrarySource.Playlists
        //    where playlist.Name.Contains(query)
        //    select playlist).First();

        public static IITTrack FirstSongMatchingName(string songNameQuery)
            => (from IITTrack track in iTunes.LibraryPlaylist.Tracks
                where track.Name.ToLower().Contains(songNameQuery.ToLower())
                select track).First();

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

        public static string PlayFirstSongMatchingName(string songName)
        {
            int high, low;
            var searchedTrack = FirstSongMatchingName(songName);
            GetPIDsFromOtherIds(searchedTrack.sourceID, searchedTrack.playlistID, searchedTrack.trackID, searchedTrack.TrackDatabaseID, out high, out low);
            var t = iTunes.LibraryPlaylist.Tracks.ItemByPersistentID[high, low];
            if (t == null)
            {
                return $"Could not find song containing string {songName}";
            }
            t.Play();
            return $"Now playing: {t.Name} by {t.Artist} from album {t.Album}";
        }

        public static string PlayFirstPlaylistMatchingString(string playlistName, bool shuffle)
        {
            var searchedPlaylist = FirstPlaylistMatchingString(playlistName);

            if (searchedPlaylist != null)
            {
                searchedPlaylist.Shuffle = shuffle;
                searchedPlaylist.PlayFirstTrack();
                return $"Now playing playlist: {searchedPlaylist.Name}";
            }
            else
            {
                return $"Could not find playlist containing string {playlistName}";
            }

        }

        public static string PlaySongById(int sourceID, int playlistID, int trackID, int databaseID)
        {
            int high, low;
            GetPIDsFromOtherIds(sourceID, playlistID, trackID, databaseID, out high, out low);
            var t = iTunes.LibraryPlaylist.Tracks?.ItemByPersistentID[high, low];

            if (t == null)
            {
                return
                    $"Could not find track with sourceID {sourceID}, playlistID {playlistID}, trackID {trackID}, and databaseID {databaseID}.";
            }

            t.Play();
            return $"Now playing: {t.Name} by {t.Artist} from album {t.Album}";
        }
        public static List<string> Search(string query) => iTunes.LibraryPlaylist.Search(query, ITPlaylistSearchField.ITPlaylistSearchFieldSongNames).Cast<IITTrack>().Select(GetTrackInfo).ToList();
    }
}
