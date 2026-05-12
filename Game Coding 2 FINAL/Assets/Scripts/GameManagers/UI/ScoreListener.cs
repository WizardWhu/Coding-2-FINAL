using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreListener : UIListner
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string additionalText;
    [SerializeField] private float moveSpeed;

    float currentNumber = 0f;
    bool moving = false;
    [SerializeField] Animator PanelAnimator;

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
        StartCoroutine(moveToScore(newNumber));
    }
    IEnumerator moveToScore(float newNumber)
    {
        string DisplayText = "0000";
        while(currentNumber < newNumber)
        {
            yield return new WaitForSeconds(1f / moveSpeed);
            currentNumber++;
            PanelAnimator.SetBool("ChangingScore", true);
            DisplayText = currentNumber.ToString();
            for (int i = 0; i < 4 - currentNumber.ToString().Length; i++)
            {
                DisplayText = "0" + DisplayText;
            }
            text.text = "<color=yellow>" + additionalText + "</color>" + DisplayText.Substring(0, DisplayText.Length - currentNumber.ToString().Length) + "<color=yellow>" + DisplayText.Substring(DisplayText.Length - currentNumber.ToString().Length, currentNumber.ToString().Length) + "</color>";

        }
        PanelAnimator.SetBool("ChangingScore", false);


    }
}

