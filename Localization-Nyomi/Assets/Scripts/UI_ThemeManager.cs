using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[Serializable]
public class Theme
{
    public string textColor;
    public string backgroundColor;
    public string buttonColor;
    public string buttonTextColor;
}

[Serializable]
public class ThemeData
{
    public Theme light;
    public Theme dark;
}

public class UI_ThemeManager : MonoBehaviour
{
    [Header("Current Theme")]
    public Theme currentTheme;

    [Header("Theme Data")]
    public ThemeData themes;

    [Header("Settings")]
    public bool isDarkMode = true;
    public int themeValue = 0; // 0 = dark, 1 = light

    [Header("UI Elements")]
    public TextMeshProUGUI[] textElements;
    public Image[] backgroundImages;
    public Button[] buttons;

    void Start()
    {
        LoadThemes();
        SwitchTheme(isDarkMode);
        ApplyThemeManually();
    }

    void LoadThemes()
    {
        TextAsset themeFile = Resources.Load<TextAsset>("Theme/theme-settings");

        if (themeFile != null)
        {
            themes = JsonUtility.FromJson<ThemeData>(themeFile.text);
            Debug.Log("Themes loaded successfully");
        }
        else
        {
            Debug.LogWarning("Theme file not found. Using default themes.");
            CreateDefaultThemes();
        }
    }

    void CreateDefaultThemes()
    {
        themes = new ThemeData();

        themes.light = new Theme
        {
            textColor = "#000000",
            backgroundColor = "#FFFFFF",
            buttonColor = "#E0E0E0",
            buttonTextColor = "#000000"
        };

        themes.dark = new Theme
        {
            textColor = "#FFFFFF",
            backgroundColor = "#2D2D2D",
            buttonColor = "#404040",
            buttonTextColor = "#FFFFFF"
        };
    }

    public void SwitchTheme(bool useDarkMode)
    {
        isDarkMode = useDarkMode;
        themeValue = useDarkMode ? 0 : 1;
        currentTheme = isDarkMode ? themes.dark : themes.light;
        Debug.Log($"Switched to {(isDarkMode ? "Dark" : "Light")} theme");
    }

    void ApplyThemeManually()
    {
        foreach (TextMeshProUGUI text in textElements)
            ApplyToText(text);

        foreach (Image image in backgroundImages)
            ApplyToImage(image);

        foreach (Button button in buttons)
            ApplyToButton(button);
    }

    public Color GetColor(string colorHex)
    {
        if (ColorUtility.TryParseHtmlString(colorHex, out Color color))
            return color;

        Debug.LogWarning($"Invalid color: {colorHex}");
        return Color.white;
    }

    public void ApplyToText(TextMeshProUGUI text)
    {
        if (text != null && currentTheme != null)
            text.color = GetColor(currentTheme.textColor);
    }

    public void ApplyToImage(Image image)
    {
        if (image != null && currentTheme != null)
            image.color = GetColor(currentTheme.backgroundColor);
    }

    public void ApplyToButton(Button button)
    {
        if (button != null && currentTheme != null)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = GetColor(currentTheme.buttonColor);
            button.colors = colors;

            Text buttonText = button.GetComponentInChildren<Text>();
            if (buttonText != null)
                buttonText.color = GetColor(currentTheme.buttonTextColor);
        }
    }
    
    public void SwitchThemeByValue(int value)
    {
        bool useDarkMode = (value == 0);
        isDarkMode = useDarkMode;
        currentTheme = isDarkMode ? themes.dark : themes.light;
        ApplyThemeManually();
        Debug.Log($"Switched to {(isDarkMode ? "Dark" : "Light")} theme (value: {value})");
    }
}
