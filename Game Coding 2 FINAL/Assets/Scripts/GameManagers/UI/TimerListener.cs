using TMPro;
using UnityEngine;

public class TimerListener : UIListner
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string additionalText;
    private void Awake()
    {
        Timer.timerUpdate += UpdateUI;
    }
    private void OnDisable()
    {
        Timer.timerUpdate -= UpdateUI;
    }
    public override void UpdateUI(float newNumber)
    {

        text.text = additionalText + (Mathf.Floor(newNumber*10f)/10f).ToString();
    }
}

