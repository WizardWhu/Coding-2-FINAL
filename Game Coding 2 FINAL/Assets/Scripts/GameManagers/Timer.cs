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
        TimerUp = true;
        currentSeconds = LevelSeconds;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (TimerUp) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentSeconds -= Time.deltaTime;
        timerUpdate?.Invoke(currentSeconds);
        if (currentSeconds <= 0)
        {
            Debug.Log("Active");

            TimerUp = true;
            timerIsUp.Invoke();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
