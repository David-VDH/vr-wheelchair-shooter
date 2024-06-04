using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] Score score;

    public TextMeshProUGUI timerText;

    private float timer = 0f;
    private bool isGameRunning = true;
    private bool canStartTimer = false;

    private void Start()
    {
        UpdateTimerText();
    }

    private void Update()
    {
        if (isGameRunning && canStartTimer)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + timer.ToString("F2");
    }

    public void SetIsGameRunningToFalse()
    {
        if (isGameRunning)
        {
            isGameRunning = false;
        }
    }

    public void SetCanStartTimerToTrue()
    {
        if (!canStartTimer)
        {
            canStartTimer = true;
        }
    }
}
