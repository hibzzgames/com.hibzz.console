# hibzz.console

The hibzz.console package is a tool and framework used to add an in-game console for games made in Unity. This package can be installed in the Unity Package Manager using the following git URL.

```
https://github.com/Hibzz-Games/com.hibzz.console.git
```

Or, a better way to add it is using a scoped registry since the package is published to NPM as well. To do so, from the package manager, go to advanced project settings where you can add new scoped registeries. Here use `https://registry.npmjs.org` as the URL and add `com.hibzz.console` as a scope. Now under the package manager, you'll be able to view and install this package under "My Registeries". By adding it as a scoped registry, you'll be able to choose the version you want to install as well as get updates directly from the package manager.

Once the package is installed, go to the hierarchy window and right-click to add a new gameobject under Custom > Hibzz.Console. Note that only one instance of the console can exist at a time and any added instances will be removed automatically when entering play mode. Just to be safe, press the ***"Scan for Commands"*** button from the unity inspector so that the console is up to date on all commands.

By default, the Slash button (`/`) is used as the prefix and to activate the console from the keyboard. This can be modified easily in the prefab. You may also change the default log color from here.

[READ THE DOCUMENTATION](https://github.com/Hibzz-Games/unity.console/wiki/Documentation)

[VIEW CHANGELOG](https://github.com/Hibzz-Games/unity.console/wiki/Changelogs)

