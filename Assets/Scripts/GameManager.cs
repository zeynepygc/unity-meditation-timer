using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;   // Singleton pattern

    [Header("Player Choices")]
    public string selectedCategory;   // e.g. "pranayama", "mantra"
    public string selectedPractice;   // e.g. "nadi shuddi", "kapalbhati"
    public int selectedDuration;      // <-- add this for minutes

    private void Awake()
    {
        // Make sure there’s only one GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persists between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


