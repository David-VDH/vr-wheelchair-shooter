using UnityEngine;
using TMPro;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject gameStartContainer;
    [SerializeField] Timer timer;

    public TextMeshProUGUI startGameText;
    private float timeLeft = 20f;
    private bool timerRunning = true;
    private float lastRealTime;

    void Awake()
    {
        Time.timeScale = 0;
        lastRealTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if (timerRunning)
        {
            float deltaTime = Time.realtimeSinceStartup - lastRealTime;
            timeLeft -= deltaTime;
            lastRealTime = Time.realtimeSinceStartup;

            startGameText.text = "Game starts in " + Mathf.RoundToInt(timeLeft).ToString() + " Seconds.";

            if (timeLeft <= 0)
            {
                timeLeft = 0;
                timerRunning = false;
                gameStartContainer.SetActive(false);
                Time.timeScale = 1;
                timer.SetCanStartTimerToTrue();
            }
        }
    }
}
