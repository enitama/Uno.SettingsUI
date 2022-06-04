﻿<p align="center">
    <img alt="IDE-version" src="https://img.shields.io/badge/IDE-vs2022-blue.svg"></img>
    <a href="https://www.nuget.org/packages/SettingsUI">
        <img alt="nuget-version" src="https://img.shields.io/nuget/v/SettingsUI.svg"></img>
    </a> 
    <a href="https://www.nuget.org/packages/SettingsUI">
        <img alt="Nuget" src="https://img.shields.io/nuget/dt/SettingsUI"></img>
    </a> 
    
</p>

<br>
<p align="center">
	<b>🙌 Donate Bitcoin with <a href="https://link.trustwallet.com/send?coin=0&address=bc1qzs4kt4aeqym6gsde669g5rksv4swjhzjqqp23a">Trust</a>🙌</b><br>
	<b>🙌 Donate ETH with <a href="https://link.trustwallet.com/send?coin=60&address=0x40Db4476c1D498b167f76A2c7ED9D45b65eb5d0C">Trust</a>🙌</b><br><br>
	<b>🙌 Bitcoin: bc1qzs4kt4aeqym6gsde669g5rksv4swjhzjqqp23a<br></b>
	<b>🙌 ETH: 0x40Db4476c1D498b167f76A2c7ED9D45b65eb5d0C</b>
</p>
<br>

# SettingsUI
 
SettingsUI Helps you create your own application settings page (like Windows 11 Settings) With many useful helper classes

> **_NOTE:_** SettingsUI is based on WindowsAppSDK 1.1.0 stable

## Install
```
Install-Package SettingsUI
```

After installing, add the following resource to app.xaml

```
<ResourceDictionary Source="ms-appx:///SettingsUI/Themes/SettingsUI.xaml"/>
```

See the Demo app to see how to use it

![SettingsUI](https://raw.githubusercontent.com/ghost1372/Resources/main/SettingsUI/0.png)

![SettingsUI](https://raw.githubusercontent.com/ghost1372/Resources/main/SettingsUI/1.png)

![SettingsUI](https://raw.githubusercontent.com/ghost1372/Resources/main/SettingsUI/5.png)

![SettingsUI](https://raw.githubusercontent.com/ghost1372/Resources/main/SettingsUI/2.png)

![SettingsUI](https://raw.githubusercontent.com/ghost1372/Resources/main/SettingsUI/3.png)

![SettingsUI](https://raw.githubusercontent.com/ghost1372/Resources/main/SettingsUI/4.png)


## Documentation

### InlineAutoCompleteTextBox

```xml
<controls:InlineAutoCompleteTextBox IsSuggestionCaseSensitive="False" SuggestionsSource="{x:Bind DemoSuggestions, Mode=OneWay}"/>
```

```cs
private List<string> DemoSuggestions { get; } = new();

public MainWindow()
{
    this.InitializeComponent();
    DemoSuggestions.Add("Ted Mosby");
    DemoSuggestions.Add("Marshal Eriksen");
    DemoSuggestions.Add("Barney Stinson");
}
```

### SystemBackdropsHelper
you can use `SystemBackdropsHelper` for accessing Mica and Acrylic Effect for your Window.

#### ThemeHelper

if you want to Mica/Acrylic effect change with Windows/Application theme (Dark/Light Mode) you can use ThemeHelper with a new Initialize Method:

```cs
ThemeHelper.Initialize(m_window, true);
// Default Material is Mica if you want to Change to Acrylic:
// ThemeHelper.Initialize(m_window, true, BackdropType.DesktopAcrylic);
```

now if you want to change Material in application runtime, you should use GetCurrent Method, because GetCurrent Returns an instance of `SystemBackdropsHelper` previously created by the ThemeHelper.

```cs
public SystemBackdropsHelper backdropsHelper = SystemBackdropsHelper.GetCurrent();
backdropsHelper.SetBackdrop(BackdropType.Mica);
```

#### Using Without ThemeHelper
you can use `SystemBackdropsHelper` without ThemeHelper but keep in mind that Materials will not change by changing the theme And you have to handle the theme changes yourself.

1. Initialize `SystemBackdropsHelper`

```cs
SystemBackdropsHelper backdropsHelper = SystemBackdropsHelper.CreateInstance();
backdropsHelper.Initialize(window, BackdropType.Mica);
```
2. for changing Material in runtime:

```cs
backdropsHelper.SetBackdrop(BackdropType.DesktopAcrylic);
```

### ApplicationDataContainerHelper

you can use ApplicationDataContainerHelper for save and restore application settings 

```cs
ApplicationDataContainerHelper settings = ApplicationDataContainerHelper.GetCurrent();
```

Save:
```cs
settings.Save<string>("stringKey", txtString.Text);
settings.Save<double>("doubleKey", txtNumber.Value);
settings.Save<bool>("boolKey", chkBool.IsChecked.Value);
```

Load:
```cs
txtString2.Text = settings.Read<string>("stringKey");
txtNumber2.Value = settings.Read<double>("doubleKey");
chkBool2.IsChecked = settings.Read<bool>("boolKey");
```

Clear:
```cs
settings.Clear();
```

### Title bar customization
We've made it easy to use the  [customized Windows title bar](https://docs.microsoft.com/en-us/windows/apps/develop/title-bar?tabs=wasdk)

![SettingsUI](https://raw.githubusercontent.com/ghost1372/Resources/main/SettingsUI/4.png)

first add following xaml in your MainWindow:

```xml
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition />
    </Grid.RowDefinitions>
    <Grid x:Name="AppTitleBar" Height="48">
        <Grid.ColumnDefinitions>
            <ColumnDefinition x:Name="LeftPaddingColumn" Width="0"/>
            <ColumnDefinition x:Name="IconColumn" Width="Auto"/>
            <ColumnDefinition x:Name="TitleColumn" Width="Auto"/>
            <ColumnDefinition x:Name="LeftDragColumn" Width="*"/>
            <ColumnDefinition x:Name="SearchColumn" Width="Auto"/>
            <ColumnDefinition x:Name="RightDragColumn" Width="*"/>
            <ColumnDefinition x:Name="RightPaddingColumn" Width="0"/>
        </Grid.ColumnDefinitions>
        <Image x:Name="TitleBarIcon" Source="/Assets/logo.png"
        Grid.Column="1"
        Width="16" Height="16"
        Margin="8,0,0,0"/>
        <TextBlock x:Name="TitleTextBlock" 
            Text="SettingsUI Demo" 
            Style="{StaticResource CaptionTextBlockStyle}"
            Grid.Column="2"
            VerticalAlignment="Center"
            Margin="4,0,0,0"/>
        <AutoSuggestBox Grid.Column="4" QueryIcon="Find"
                PlaceholderText="Search"
                VerticalAlignment="Center"
                Width="260" Margin="4,0"/>
    </Grid>

    <local:ShellPage Grid.Row="1"/>

</Grid>
```

now you can Initialize title bar:
```cs
public MainWindow()
{
    this.InitializeComponent();
    TitleBarHelper.Initialize(this, TitleTextBlock, AppTitleBar, LeftPaddingColumn, IconColumn, TitleColumn, LeftDragColumn, SearchColumn, RightDragColumn, RightPaddingColumn);
}
```

### ThemeHelper
You can simplify the operation of saving, retrieving and selecting the Application theme, All operations are performed automatically.

first of all, Initialize ThemeHelper (Theme retrieving is done automatically by the Initialize method):

```cs
protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
{
    m_window = new MainWindow();
    ThemeHelper.Initialize(m_window);
    m_window.Activate();
}
```

copy this xaml in your page:
```xml
 <StackPanel x:Name="ThemePanel" Margin="10">
    <RadioButton Tag="Light" Checked="OnThemeRadioButtonChecked" Content="Light"/>
    <RadioButton Tag="Dark" Checked="OnThemeRadioButtonChecked" Content="Dark" />
    <RadioButton Tag="Default" Checked="OnThemeRadioButtonChecked" Content="Use system setting" />
</StackPanel>
```
now call `OnThemeRadioButtonChecked` method for change and save application theme:
```cs
private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
{
    ThemeHelper.OnThemeRadioButtonChecked(sender);
}
```
now call `SetThemeRadioButtonChecked` method for selecting currect radiobutton item when page is loading:
```cs
ThemeHelper.SetThemeRadioButtonChecked(ThemePanel);
```

### WindowHelper

#### SetWindowSize
you can set your MainWindow Size:
```cs
IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
WindowHelper.SetWindowSize(, 200, 300);
```

#### Window Height/Width Min/Max Size
you can set Window Height/Width Min/Max Size, first you need to register `RegisterWindowMinMax` method in your window class:

```cs
public MainWindow()
{
    this.InitializeComponent();
    WindowHelper.RegisterWindowMinMax(this);
    //OR you can use extension method
    //RegisterWindowMinMax();
}
```
now you can change window Min/Max size:
```cs
WindowHelper.MinWindowWidth = 500;
WindowHelper.MinWindowHeight = 300;

WindowHelper.MaxWindowWidth = 800;
WindowHelper.MaxWindowHeight = 600;
```

#### GetAppWindowForCurrentWindow
you can get AppWindow for Current Window:
```cs
var appWindow = WindowHelper.GetAppWindowForCurrentWindow(this);
```

### VisualHelper

### TaskbarHelper
you can handle Taskbar for your application:

```cs
TaskbarHelper.SetProgressState(windowHandle, TaskbarStates.Error);

TaskbarHelper.SetProgressValue(windowHandle, 56, 100);
```

### ContentDialogExtension

By default, only 1 ContentDialog can be opened, with the help of this extension you can open multiple ContentDialogs.

```cs
ContentDialog dialog = new ContentDialog()
{
    Title = "Title",
    Content = "Content",
    PrimaryButtonText = "OK",
    CloseButtonText = "Cancel",
    DefaultButton = ContentDialogButton.Primary,
    XamlRoot = xamlRoot
};
var result = await dialog.ShowAsyncQueue();
```

### Observable

inherited from `INotifyPropertyChanged`

```cs
public class ShellViewModel : Observable
{
    private bool isBackEnabled;
    public bool IsBackEnabled
    {
        get { return isBackEnabled; }
        set { Set(ref isBackEnabled, value); }
    }
}
```

### RelayCommand

inherited from `ICommand`

```cs
private ICommand itemInvokedCommand;

public ICommand ItemInvokedCommand => itemInvokedCommand ?? (itemInvokedCommand = new RelayCommand<string>(OnItemInvoked));

public void OnItemInvoked(string arg)
{

}
```

### AutoSuggestBoxHelper

Simple helper for Loading suggestions list in `AutoSuggestBox`

```cs
private void AutoSuggest_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
{
    AutoSuggestBoxHelper.LoadSuggestions(sender, args, mylist);
}
```

### NavHelper and ShellViewModel

Easily implement a `NavigationView` with Back button and AutoSuggestBox

```xml
<NavigationView
    x:Name="navigationView"
    IsBackEnabled="{x:Bind ViewModel.IsBackEnabled, Mode=OneWay}"
    ItemInvoked="navigationView_ItemInvoked"
    SelectedItem="{x:Bind ViewModel.Selected, Mode=OneWay}">
    <NavigationView.AutoSuggestBox>
        <AutoSuggestBox Name="autoSuggestBox" QueryIcon="Find" PlaceholderText="Search" TextChanged="OnControlsSearchBoxTextChanged" QuerySubmitted="OnControlsSearchBoxQuerySubmitted"/>
    </NavigationView.AutoSuggestBox>
    <NavigationView.MenuItems>
        <NavigationViewItem
            Margin="0,0,12,0"
            helpers:NavHelper.NavigateTo="views:GeneralPage"
            Content="General"/>

        <NavigationViewItem
            Margin="0,0,12,0"
            helpers:NavHelper.NavigateTo="views:AwakePage"
            Content="General"/>

        <NavigationViewItem
            Margin="0,0,12,0"
            helpers:NavHelper.NavigateTo="views:FancyZonesPage"
            Content="General"/>
    </NavigationView.MenuItems>
    <Frame x:Name="shellFrame"/>
</NavigationView>
```
now create a new `ShellViewModel` and initialize it

```cs
public ShellViewModel ViewModel { get; } = new ShellViewModel();

ViewModel.InitializeNavigation(shellFrame, navigationView)
                    .WithAutoSuggestBox(autoSuggestBox)
                    .WithKeyboardAccelerator(KeyboardAccelerators)
                    .WithDefaultPage(typeof(myPage))
                    .WithSettingsPage(typeof(mySettingsPage));
```

add UserControl or Page `Loaded` event

```cs
private void UserControl_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
{
    ViewModel.OnLoaded();
}

private void navigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
{
    ViewModel.OnItemInvoked(args);
}

private void OnControlsSearchBoxQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
{
    ViewModel.OnAutoSuggestBoxQuerySubmitted(args);
}

private void OnControlsSearchBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
{
    ViewModel.OnAutoSuggestBoxTextChanged(args);
}
```

if you have `Microsoft.Xaml.Behaviors.WinUI.Managed` nuget package, you can bind `Loaded` and `ItemInvoked` events to command

```xml
<UserControl>
    <i:Interaction.Behaviors>
        <ic:EventTriggerBehavior EventName="Loaded">
            <ic:InvokeCommandAction Command="{x:Bind ViewModel.LoadedCommand}"/>
        </ic:EventTriggerBehavior>
    </i:Interaction.Behaviors>

<NavigationView>
    <i:Interaction.Behaviors>
        <ic:EventTriggerBehavior EventName="ItemInvoked">
            <ic:InvokeCommandAction Command="{x:Bind ViewModel.ItemInvokedCommand}"/>
        </ic:EventTriggerBehavior>
    </i:Interaction.Behaviors>
</NavigationView>

<AutoSuggestBox>
    <i:Interaction.Behaviors>
        <ic:EventTriggerBehavior EventName="TextChanged">
            <ic:InvokeCommandAction Command="{x:Bind ViewModel.AutoSuggestBoxTextChangedCommand}"/>
        </ic:EventTriggerBehavior>
        <ic:EventTriggerBehavior EventName="QuerySubmitted">
            <ic:InvokeCommandAction Command="{x:Bind ViewModel.AutoSuggestBoxQuerySubmittedCommand}"/>
        </ic:EventTriggerBehavior>
    </i:Interaction.Behaviors>
</NavigationView>
</UserControl>
```

### UpdateHelper

you can use UpdateHelper for checking application updates from github release page

first you must create a new release tag in github repository, tag version must be in this format : `1.0.0.0`
now we can check for update with github username and github repository

```cs
var ver = await UpdateHelper.CheckUpdateAsync("ghost1372", "SettingsUI");
if(ver.IsExistNewVersion)
{
    Debug.WriteLine(ver.ReleaseUrl);
    Debug.WriteLine(ver.CreatedAt.ToString());
    Debug.WriteLine(ver.PublishedAt.ToString());
    
    //Asset is List so maybe there is more than one file you can use forech or increase index
    Debug.WriteLine(ver.Assets[0].Url);
    Debug.WriteLine(ver.IsPreRelease.ToString());
    Debug.WriteLine(ver.Assets[0].Size.ToString());
    Debug.WriteLine(ver.Version);
    Debug.WriteLine(ver.Changelog);
}
```

### GeneralHelper

#### EnableSound
enable sound for controls

```cs
GeneralHelper.EnableSound();
```

#### GetEnum
get enum from string

```cs
var myenum = GeneralHelper.GetEnum<ElementTheme>("Dark");
```

#### IsNetworkAvailable

Check if the internet is connected or not

```cs
GeneralHelper.IsNetworkAvailable();
```

#### GetGeometry

```xml
<x:String x:Key="FavoriteGeometry">M16.4996 5.2021C16.4996 2.76017 15.3595 1.00342 13.4932 1.00342C12.467 1.00342 12.1149 1.60478 11.747 3.00299C11.6719 3.29184 11.635 3.43248 11.596 3.57109C11.495 3.92982 11.3192 4.54058 11.069 5.4021C11.0623 5.42518 11.0524 5.44692 11.0396 5.467L8.17281 9.95266C7.49476 11.0136 6.49429 11.8291 5.31841 12.2793L4.84513 12.4605C3.5984 12.9379 2.87457 14.2416 3.1287 15.5522L3.53319 17.6383C3.77462 18.8834 4.71828 19.8743 5.9501 20.1762L13.5778 22.0457C16.109 22.6661 18.6674 21.1312 19.3113 18.6059L20.7262 13.0567C21.1697 11.3174 20.1192 9.54796 18.3799 9.10449C18.1175 9.03758 17.8478 9.00373 17.5769 9.00373H15.7536C16.2497 7.37084 16.4996 6.11106 16.4996 5.2021ZM4.60127 15.2667C4.48576 14.671 4.81477 14.0783 5.38147 13.8614L5.85475 13.6802C7.33036 13.1152 8.58585 12.0918 9.43674 10.7604L12.3035 6.27477C12.3935 6.13388 12.4629 5.98082 12.5095 5.82026C12.7608 4.95525 12.9375 4.34126 13.0399 3.97737C13.083 3.82412 13.1239 3.66867 13.1976 3.3847C13.3875 2.663 13.4809 2.50342 13.4932 2.50342C14.3609 2.50342 14.9996 3.48749 14.9996 5.2021C14.9996 6.08659 14.6738 7.53754 14.0158 9.51717C13.8544 10.0027 14.2158 10.5037 14.7275 10.5037H17.5769C17.7228 10.5037 17.868 10.522 18.0093 10.558C18.9459 10.7968 19.5115 11.7496 19.2727 12.6861L17.8578 18.2353C17.4172 19.9631 15.6668 21.0133 13.9349 20.5889L6.30718 18.7193C5.64389 18.5568 5.13577 18.0232 5.00577 17.3528L4.60127 15.2667Z</x:String>

var myGeometry = GeneralHelper.GetGeometry("FavoriteGeometry");

Icon = new PathIcon { Data = myGeometry },
```

#### GetColorFromHex

```cs
var color = Application.Current.Resources["SystemAccentColor"];
var accent = Helper.GetColorFromHex(color.ToString());
```

#### StorageFileHelper

```cs
// NOTE This must be used from an async function
string myText = "Great information that the users wants to keep";
StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

// Save some text to a file named appFilename.txt (in the local cache folder)
var storageFile = await StorageFileHelper.WriteTextToLocalCacheFileAsync(myText, "appFilename.txt");

// Load some text from a file named appFilename.txt in the local cache folder 
string loadedText = await StorageFileHelper.ReadTextFromLocalCacheFileAsync("appFilename.txt");

// Save some text to a file named appFilename.txt (in the local folder)
storageFile = await StorageFileHelper.WriteTextToLocalFileAsync(myText, "appFilename.txt");

// Load some text from a file named appFilename.txt in the local folder 
loadedText = await StorageFileHelper.ReadTextFromLocalFileAsync("appFilename.txt");

// Check if a file exists in a specific folder
bool exists = await localFolder.FileExistsAsync("appFilename.txt");

// Check if a file exists in a specific folder or in one of its subfolders
bool exists = await localFolder.FileExistsAsync("appFilename.txt", true);

// Check if a file name is valid or not
bool isFileNameValid = StorageFileHelper.IsFileNameValid("appFilename.txt");

// Check if a file path is valid or not
bool isFilePathValid = StorageFileHelper.IsFilePathValid("folder/appFilename.txt");
```
