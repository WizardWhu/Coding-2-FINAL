using UnityEngine;

public class StartRound : MonoBehaviour
{
    [SerializeField] private LargeDinosaurController playerController;
    [SerializeField] private Timer gameTimer;

    public void Complete()
    {
        Debug.Log("GaveControl");
        playerController.PlayerCanMove(true);
        gameTimer.StartTimer();
    }
}
