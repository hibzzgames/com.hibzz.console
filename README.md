# hibzz.console

![LICENSE](https://img.shields.io/badge/LICENSE-CC--BY--4.0-ee5b32?style=for-the-badge) [![Twitter Follow](https://img.shields.io/badge/follow-%40hibzzgames-1DA1f2?logo=twitter&style=for-the-badge)](https://twitter.com/hibzzgames) [![Discord](https://img.shields.io/discord/695898694083412048?color=788bd9&label=DIscord&style=for-the-badge)](https://discord.gg/tZdZFK7) ![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

![demo](https://i.imgur.com/66znH1Z.png)

The hibzz.console package is a tool and framework used to add an in-game console for games made in Unity. The package is designed around ease-of-use and flexibility. This package can be installed in the Unity Package Manager using the following git URL.

```
https://github.com/Hibzz-Games/com.hibzz.console.git
```

Or, a better way to add it is using a scoped registry since the package is published to NPM as well. To do so, from the package manager, go to advanced project settings where you can add new scoped registeries. Here use `https://registry.npmjs.org` as the URL and add `com.hibzz.console` as a scope. Now under the package manager, you'll be able to view and install this package under "My Registeries". By adding it as a scoped registry, you'll be able to choose the version you want to install as well as get updates directly from the package manager.

Once the package is installed, go to the hierarchy window and right-click to add a new gameobject under Custom > Hibzz.Console. Note that only one instance of the console can exist at a time and any added instances will be removed automatically when entering play mode. Just to be safe, press the ***"Scan for Commands"*** button from the unity inspector so that the console is up to date on all commands.

By default, the Slash button (`/`) is used as the prefix and to activate the console from the keyboard. This can be modified easily in the prefab. You may also change the default log color from here.

## Features

- Simple console with support for colored texts
- Easy to create custom commands
- Lightweight
- Automatically scan for new console commands
- Seamless support for both standard and new input systems
- Easy to use API
- Admin access with the ability to execute marked commands only in console-admin mode
- Use arrow keys to cycle through previous commands
- Resizable console window
- Reporting system used to send one-time information to the users
- Easy onboarding: Add the console to your scene in less than 2 clicks
- Built-in object dictionary used to store intermediary value
- Ctrl + click URLs to open in default browser
- Command autocomplete
- Secure input
- Optional [Hibzz.DefineManager](https://github.com/Hibzz-Games/Hibzz.DefineManager/) integration 

<br>

[READ THE DOCUMENTATION](https://github.com/Hibzz-Games/unity.console/wiki/Documentation)

[VIEW CHANGELOG](https://github.com/Hibzz-Games/unity.console/wiki/Changelogs)

## Have a question or want to contribute?
If you have any questions or want to contribute, feel free to join the [Discord server](https://discord.gg/tZdZFK7) or [Twitter](https://twitter.com/hibzzgames). I'm always looking for feedback and ways to improve this tool. Thanks!

Additionally, you can support the development of these opensource projects via [GitHub Sponsors](https://github.com/sponsors/sliptrixx) and gain early access to the projects.


