using UnityEngine;

public class ActiveOnWin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
