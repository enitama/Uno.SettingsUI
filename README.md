# Uno Platform port of SettingsUI

This is an unofficial fork of [WinUICommunity.SettingsUI](https://github.com/WinUICommunity/SettingsUI) for usage with Uno Platform.

This package has been tested with WinUI 3 and Wasm targets and the following controls only:

- `SettingsPageControl`
- `SettingsGroup`
- `SettingExpander`
- `Setting`

## Install

```
dotnet add package Unofficial.WinUICommunity.SettingsUI.Uno --version 3.0.0-pre
```

After installing, add the following resource to app.xaml

```xml
<ResourceDictionary Source="ms-appx:///SettingsUI/Themes/Generic.xaml"/>
```

See the [upstream repo](https://github.com/WinUICommunity/SettingsUI) for general usage information.
