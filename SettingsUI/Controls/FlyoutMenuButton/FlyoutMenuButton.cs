﻿using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace WinUICommunity.SettingsUI.Controls;
public partial class FlyoutMenuButton : Button
{
    /// <summary>
    /// The backing <see cref="DependencyProperty"/> for the <see cref="Icon"/> property.
    /// </summary>
    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(object),
        typeof(FlyoutMenuButton),
        new PropertyMetadata(defaultValue: null));

    /// <summary>
    /// Gets or sets the icon.
    /// </summary>
    public object Icon
    {
        get => (object) GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public FlyoutMenuButton()
    {
        this.DefaultStyleKey = typeof(FlyoutMenuButton);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }
}
