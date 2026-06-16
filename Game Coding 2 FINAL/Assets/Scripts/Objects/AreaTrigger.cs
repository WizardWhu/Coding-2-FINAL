using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger : MonoBehaviour
{
    public UnityEvent activateOnTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            activateOnTrigger?.Invoke();
        }
    }
}
