using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int currentScore;
    [SerializeField] private int MaxScore;

    public static event Action<float> scoreUpdate;
    public static event Action maxScoreHit;

    void Start()
    {
        currentScore = 0;
    }

    public void IncrementScore(int addedScore)
    {
        currentScore += addedScore;
        scoreUpdate?.Invoke(currentScore);
        if (currentScore >= MaxScore)
        {
            maxScoreHit?.Invoke();
            Debug.Log("YOU HIT THE MAX SCORE");
        }
    }
}
