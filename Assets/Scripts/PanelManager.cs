using UnityEngine;
using System.Collections.Generic;

public class PanelManager : MonoBehaviour
{
    [System.Serializable]
    public class Panel
    {
        public string name;          // must match the actual GameObject name
        public GameObject panelObject;
    }

    [Header("All Panels")]
    public List<Panel> panels = new List<Panel>();

    private Dictionary<string, GameObject> panelDictionary;

    void Awake()
    {
        panelDictionary = new Dictionary<string, GameObject>();
        foreach (var p in panels)
        {
            if (p.panelObject != null)
                panelDictionary[p.name] = p.panelObject;
        }
    }

    public void ShowPanel(string panelName)
    {
        // Hide all panels
        foreach (var p in panelDictionary.Values)
            p.SetActive(false);

        // Show requested one
        if (panelDictionary.ContainsKey(panelName))
        {
            panelDictionary[panelName].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Panel not found: " + panelName);
        }
    }

    // ---------------------------
    // Wrapper methods
    // ---------------------------

    public void ShowStartPanel() => ShowPanel("Start");
    public void ShowDurationPanel() => ShowPanel("Duration");
    public void ShowCategoryPanel() => ShowPanel("Category");
    public void ShowPranayamaPanel() => ShowPanel("Pranayama");
    public void ShowTratakaPanel() => ShowPanel("Trataka");
    public void ShowTimerPanel() => ShowPanel("Timer");

    // ---------------------------
    // Logic for duration + category + practice
    // ---------------------------

    public void SelectDuration(int minutes)
    {
        GameManager.Instance.selectedDuration = minutes;
        Debug.Log("Duration selected: " + minutes);
        ShowPanel("Category");
    }

    public void SelectCategory(string categoryName)
    {
        GameManager.Instance.selectedCategory = categoryName;

        switch (categoryName.ToLower())
        {
            case "pranayama":
                ShowPranayamaPanel();
                break;
            case "trataka":
                ShowTratakaPanel();
                break;
            case "mantra":
                GameManager.Instance.selectedPractice = "mantra";
                ShowTimerPanel();
                break;
            default:
                Debug.LogWarning("Unknown category: " + categoryName);
                break;
        }
    }

    public void SelectPractice(string practiceName)
    {
        GameManager.Instance.selectedPractice = practiceName;
        ShowTimerPanel();
    }
}

