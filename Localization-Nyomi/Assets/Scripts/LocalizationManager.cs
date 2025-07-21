using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    private bool active = false;

    public void ChangeLanguage(int localeID)
    {
        if (active) return;

        active = true;
        StartCoroutine(ChangeLanguageCoroutine(localeID));
    }

    IEnumerator ChangeLanguageCoroutine(int _localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
    }
}
