using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    private int currentScore;
    [SerializeField] private int MaxScore;

    public static event Action<float> scoreUpdate;
    public UnityEvent maxScoreHit;
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
            maxScoreHit.Invoke();
        }
    }
}
