using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float LevelSeconds;
    private float currentSeconds;
    public static event Action<float> timerUpdate;
    public static event Action timerIsUp;

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
            Debug.Log("Time Is Up!");
        }
    }
}
