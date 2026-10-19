using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerialzieField] private LargeDinosaurController playerController;
    [SerializeField] private Timer gameTimer;

    public void Complete()
    {
        playerController.PlayerCanMove(true);
        gameTimer.startTimer();
    }
}
