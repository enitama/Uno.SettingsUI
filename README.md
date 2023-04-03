# Uno Platform port of SettingsUI

This is a fork of [WinUICommunity.SettingsUI](https://github.com/WinUICommunity/SettingsUI) for usage with Uno Platform.

This package has been tested with WinUI 3 and Wasm targets and the following controls only:

- `SettingsPageControl`
- `SettingsGroup`
- `SettingExpander`
- `Setting`

This fork is maintained on a hobby basis only, as it is used as a dependency for a hobby project.

This repo is therefore likely to fall behind upstream. Help is appreciated!

## Install

```
dotnet add package Unofficial.WinUICommunity.SettingsUI.Uno --version 3.0.0-pre
```

After installing, add the following resource to app.xaml

```xml
<ResourceDictionary Source="ms-appx:///SettingsUI/Themes/Generic.xaml"/>
```

See the [upstream repo](https://github.com/WinUICommunity/SettingsUI) for general usage information.
