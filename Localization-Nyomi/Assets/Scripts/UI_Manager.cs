using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] GameObject settingsPanels;

    void Awake()
    {
        settingsPanels.SetActive(false);
    }

    void Start()
    {
        Debug.Log("UI Manager Initialized");
    }

    public void ToggleSettingsPanel()
    {
        settingsPanels.SetActive(!settingsPanels.activeSelf);
    }
}
