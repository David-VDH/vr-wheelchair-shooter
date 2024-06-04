using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance { get; private set; }

    [SerializeField] Timer timer;

    public TextMeshProUGUI scoreText;

    private GameObject[] targets;

    private int score = 0;
    private int maxScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Target");

        maxScore = targets.Length;

        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void TargetDestroyed()
    {
        AddScore(1);
        CheckAllTargetsDestroyed();
    }

    private void CheckAllTargetsDestroyed()
    {
        if (score >= targets.Length)
        {
            timer.SetIsGameRunningToFalse();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score + " / " + maxScore;
    }
}
