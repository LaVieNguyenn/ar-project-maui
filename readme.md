# Login Page - ARProject (.NET MAUI)

This is the `LoginPage.xaml` UI layout for the **ARProject**, built using **.NET MAUI (.NET 9)**. It provides a modern, responsive login interface designed for mobile-first AR applications.

## 🔧 Technologies

* .NET MAUI (.NET 9)
* XAML
* MVVM Architecture
* MAUI Borders (instead of Frame)

## 🖼 UI Features

* Rounded logo at the top center (e.g., `logo.jpg` or `.webp`)
* Clean input field for email entry
* Separator with "- OR Continue with -"
* Social login buttons (Google, Apple, Facebook)
* Navigation to Sign Up page via `TapGestureRecognizer`
* "Next" and "Cancel" buttons styled with modern colors and rounded edges

## 📂 Image Requirements

Ensure the following images are added to the `Resources/Images/` folder and marked as `MauiImage`:

* `logo.jpg` (or `logo.webp`)
* `icon_google.png`
* `icon_apple.png`
* `icon_facebook.png`

## 🚀 Usage

In `App.xaml.cs`, to display this page on startup:

```csharp
protected override Window CreateWindow(IActivationState? activationState)
{
    return new Window(new LoginPage());
}
```

## 📌 Notes

* Supports `ScrollView` to avoid UI cutoff on small screens or when keyboard is shown
* Built with `Border` controls for modern look and better performance with .NET 9
* Uses `Command` binding with MVVM for navigation actions

## ✅ To-Do

* Add `Password` field with secure entry
* Integrate authentication logic and validation
* Implement persistent login state and navigation to `AppShell` after login

---

**Author:** *Your Name Here*

Feel free to update this README as the project evolves.
