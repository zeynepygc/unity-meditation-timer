using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text timerText;
    public TMP_Text instructionText;
    public Image practiceImage;

    [Header("Practice Sprites")]
    public Sprite mantraSprite;
    public Sprite candleSprite;
    public Sprite yantraSprite;
    public Sprite nadiShuddhiSprite;
    public Sprite kapalbhatiSprite;
    public Sprite bhramariSprite;

    private float remainingTime;
    private bool isRunning = false;

    void OnEnable()
    {
        SetupTimer();
    }

    void SetupTimer()
    {
        remainingTime = GameManager.Instance.selectedDuration * 60;
        isRunning = true;

        string practice = GameManager.Instance.selectedPractice;
        string category = GameManager.Instance.selectedCategory;

        switch (practice.ToLower())
        {
            case "mantra":
                instructionText.text = "Chanting Om...";
                practiceImage.sprite = mantraSprite;
                break;
            case "candle":
                instructionText.text = "Let your eyes relax... Focus on the flame...";
                practiceImage.sprite = candleSprite;
                break;
            case "yantra":
                instructionText.text = "Let your gaze be steady... Just let your attention move...";
                practiceImage.sprite = yantraSprite;
                break;
            case "nadi shuddi":
                instructionText.text = "Don't forget to switch nostrils...";
                practiceImage.sprite = nadiShuddhiSprite;
                break;
            case "kapalbhati":
                instructionText.text = "Focus on forceful exhalations...";
                practiceImage.sprite = kapalbhatiSprite;
                break;
            case "bhramari":
                instructionText.text = "Feel the buzzing...";
                practiceImage.sprite = bhramariSprite;
                break;
            default:
                instructionText.text = "Get ready for your practice...";
                practiceImage.sprite = null;
                break;
        }
    }

    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0)
        {
            remainingTime = 0;
            isRunning = false;
            timerText.text = "00:00";
            // Show result panel
            FindFirstObjectByType<PanelManager>().ShowPanel("Result");

        }
        else
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
