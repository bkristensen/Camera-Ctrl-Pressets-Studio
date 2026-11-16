# Privacy policy

Cam Pressets Studio is a local camera-control application. Its current application
code does not capture video/audio, record meetings, collect analytics, display ads,
create accounts, or send application data to a server.

It reads device names/identifiers and supported camera properties. It reads process
names in the current Windows session to check configured startup programs. Profiles
store camera identifiers, preset names/coordinates, preferences, program paths and
arguments, and an optional salted settings-code hash locally. Do not put secrets in
startup arguments: they are not encrypted.

Applications launched by Studio, such as meeting or recording software, have their
own privacy behaviour. Microsoft Store, GitHub and signing providers also operate
under their own policies. Build tools may contact NuGet and container registries;
that is distinct from running the desktop app.

You can remove local profiles after closing the app, following a backup if needed.
Portable/installer profile retention differs from possible MSIX data redirection;
packaged uninstall behaviour must be verified before a Store release.

For public support, remove private meeting information and sensitive file paths.
Before publishing this policy in a Store listing, maintainers must add an actual
project contact URL and verify it against the exact released behaviour. This draft
must not be represented as a completed Store privacy listing without that review.
