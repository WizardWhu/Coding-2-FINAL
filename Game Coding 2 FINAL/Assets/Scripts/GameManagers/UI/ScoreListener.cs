using TMPro;
using UnityEngine;

public class ScoreListener : UIListner
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string additionalText;

    private void Awake()
    {
        ScoreCounter.scoreUpdate += UpdateUI;
    }
    private void OnDisable()
    {
        ScoreCounter.scoreUpdate -= UpdateUI;

    }
    public override void UpdateUI(float newNumber)
    {
        newNumber = Mathf.Floor(newNumber);
        string nextNumber = newNumber.ToString();
        for(int i = 0; i < 4 - newNumber.ToString().Length; i++)
        {
            nextNumber = "0" + nextNumber;
        }
        text.text = additionalText + nextNumber;
    }
}

