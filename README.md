# iTunesCLI
Command Line Interface for iTunes

Utilizes the iTunes COM Interop DLLs -- Windows only

Currently implemented (functional) commands
* -s | --playfirst "track name query" - Searches for a track containing the given string and plays the first matching track
* -i | --playsongbyid "sourceID" "playlistID" "trackID" "databaseID" - Plays the track with the matching IDs
* -f | --searchsongs "track name query" - Searches the library for songs containing the given query
