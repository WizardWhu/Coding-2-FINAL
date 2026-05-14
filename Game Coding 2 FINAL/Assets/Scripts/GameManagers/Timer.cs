using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float LevelSeconds;
    private float currentSeconds;
    public static event Action<float> timerUpdate;
    public UnityEvent timerIsUp;

    private bool TimerUp = false;

    private void Start()
    {
        TimerUp = false;
        currentSeconds = LevelSeconds;
    }

    private void Update()
    {
        if (TimerUp) return;

        currentSeconds -= Time.deltaTime;
        timerUpdate?.Invoke(currentSeconds);
        if (currentSeconds <= 0)
        {
            TimerUp = true;
            timerIsUp?.Invoke();
        }
    }


    public void StopTimer()
    {
        TimerUp = true;
    }

    public void StartTimer()
    {
        TimerUp = false;
    }

    public void RestartTimer()
    {
        TimerUp = false;
        currentSeconds = LevelSeconds;
    }
    
}
