# FAQ

**Why is the row yellow?** The app is moving towards the stored position. It becomes green when the camera driver reports the stored coordinates. Manual movement clears the colour. Green does not verify the actual video picture.

**Why does movement still look stepped?** The camera driver accepts a limited set of positions. Longer transition time cannot create finer hardware steps.

**Where are my positions?** Permanent positions belong to the selected camera and profile. Temporary positions last until the app closes. Permanent positions are managed under Settings → Presets.

**Why did the camera not connect?** It may be disconnected, busy, unsupported, or waiting for your streaming confirmation. Click the central camera button to select a source. This app cannot detect whether your stream has begun.

**Can it show video or share a screen?** No. Use your capture or streaming application for those tasks.

**Where are profiles stored?** `%LOCALAPPDATA%\CamPressets\profiles.json`. A `.bak` copy is written when the file is replaced. Close the app before copying or restoring these files. Keep the original backup when upgrading from 0.6.x: removed startup-program options are discarded when the updated profile is saved.

**Does language follow a profile?** No. Language is an application preference. Camera, movement, image controls and window layout are profile settings.

**Will this work with another camera?** It may work if its Windows driver exposes the standard controls used by the app. Please report the model, driver version and controls that work. Do not assume that every USB camera supports pan or zoom.

**Why does Windows warn about the download?** The supplied build is unsigned. Read the source, download from the project's release page and compare published hashes. A checksum checks file integrity; it does not establish publisher identity. Do not turn off Windows protection to install it.

**Where can I request a feature?** Open an issue and describe the problem, expected behaviour and one example. Use Discussions when you want to explore an idea first.
