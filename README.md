# iTunesCLI
Command Line Interface for iTunes

Utilizes the iTunes COM Interop DLLs -- Windows only

Currently implemented (functional) commands
* -s | --playfirst "track name query" - Searches for a track containing the given string and plays the first matching track
* -i | --playsongbyid "sourceID" "playlistID" "trackID" "databaseID" - Plays the track with matching IDs
* -f | --searchsongs "track name query" - Searches the library for songs containing the given string
* -t | --playlist "playlist name query" - Searches the library for playlists containing the given string - considers shuffle switch
* -z | --shuffle - can be used with various queries to denote that shuffle should be toggled during their execution
