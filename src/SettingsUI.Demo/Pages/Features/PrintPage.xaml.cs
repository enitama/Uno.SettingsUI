﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Graphics.Printing;
using SettingsUI.Helpers;
using SettingsUI.Extensions;
using System.Collections.Generic;
using CommunityToolkit.WinUI.Helpers;
using System;

namespace SettingsUI.Demo.Pages;
public sealed partial class PrintPage : Page
{
    private PrintHelper _printHelper;
    public PrintPage()
    {
        this.InitializeComponent();
        Loaded += PrintPage_Loaded;
    }

    private void PrintPage_Loaded(object sender, RoutedEventArgs e)
    {
        ShowOrientationSwitch.IsOn = true;

        DefaultOrientationComboBox.ItemsSource = new List<PrintOrientation>()
            {
                PrintOrientation.Default,
                PrintOrientation.Portrait,
                PrintOrientation.Landscape
            };
        DefaultOrientationComboBox.SelectedIndex = 0;
    }

    private async void PrintingIsNotSupported()
    {
        // Printing is not supported on this device
        ContentDialog noPrintingDialog = new ContentDialog()
        {
            XamlRoot = MainWindow.Instance.Content.XamlRoot,
            Title = "Printing is not supported",
            Content = "\nSorry, printing is not supported on this device.",
            PrimaryButtonText = "OK"
        };
        await noPrintingDialog.ShowAsyncQueue();
    }
   
    private async void btnDirectPrint_Click(object sender, RoutedEventArgs e)
    {
        if (PrintManager.IsSupported())
        {
            _printHelper = new PrintHelper(DirectPrintContainer);

            _printHelper.OnPrintCanceled += PrintHelper_OnPrintCanceled;
            _printHelper.OnPrintFailed += PrintHelper_OnPrintFailed;
            _printHelper.OnPrintSucceeded += PrintHelper_OnPrintSucceeded;

            var printHelperOptions = new PrintHelperOptions(false);
            printHelperOptions.Orientation = (PrintOrientation) DefaultOrientationComboBox.SelectedItem;

            if (ShowOrientationSwitch.IsOn)
            {
                printHelperOptions.AddDisplayOption(StandardPrintTaskOptions.Orientation);
            }

            await _printHelper.ShowPrintUIAsync(WinRT.Interop.WindowNative.GetWindowHandle(MainWindow.Instance), "Windows Community Toolkit Sample App", printHelperOptions, true);
        }
        else
        {
            PrintingIsNotSupported();
        }
    }
    private void ReleasePrintHelper()
    {
        _printHelper.Dispose();

        if (!DirectPrintContainer.Children.Contains(PrintableContent))
        {
            DirectPrintContainer.Children.Add(PrintableContent);
        }
    }

    private async void PrintHelper_OnPrintSucceeded()
    {
        ReleasePrintHelper();
        ContentDialog noPrintingDialog = new ContentDialog()
        {
            XamlRoot = this.Content.XamlRoot,
            Title = "Printing Done",
            Content = "\nDone, element printed.",
            PrimaryButtonText = "OK"
        };
        await noPrintingDialog.ShowAsyncQueue();
    }

    private async void PrintHelper_OnPrintFailed()
    {
        ReleasePrintHelper();
        ContentDialog noPrintingDialog = new ContentDialog()
        {
            XamlRoot = this.Content.XamlRoot,
            Title = "Printing error",
            Content = "\nSorry, failed to print.",
            PrimaryButtonText = "OK"
        };
        await noPrintingDialog.ShowAsyncQueue();
    }
    private void PrintHelper_OnPrintCanceled()
    {
        ReleasePrintHelper();
    }
}
