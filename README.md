This is a Library for Android's TextInputLayout control ported to .NET MAUI
## Setup

Register the TextInputLayout during MAUI app startup.

### MauiProgram.cs

```csharp
using Nwesp.Maui.Android.Hosting;

public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();

    builder
        .UseMauiApp<App>()
        .AddTextInputLayout();

    return builder.Build();
}
```

## Theme Configuration

Configure TextInputLayout themes in `App.xaml.cs`.

```csharp
using Nwesp.Maui.Android.Hosting;

public App(AppShell shell)
{
    InitializeComponent();

    this.ConfigureTextInputLayoutThemes();
}
```

## Basic Demo

```xml
<input:TextInputLayout BoxBackgroundMode="Filled" Hint="Label Text">
    <input:MaterialEntry Text="Input Text" />
</input:TextInputLayout>

<input:TextInputLayout BoxBackgroundMode="Outline" Hint="Label Text">
    <input:MaterialEntry Text="Input Text" />
</input:TextInputLayout>
```

<img width="393" height="880" alt="image" src="https://github.com/user-attachments/assets/b4a65e50-b3df-46e3-a429-6994d2545962" />


## Design Notes

The default `TextInputLayout` implementation is primarily based on Material Design 3 (M3) specifications.

Official Material 3 Text Field guidelines:  
https://m3.material.io/components/text-fields/specs
