using UnityEngine;
using UnityEngine.Events;
public class GotoGround : MonoBehaviour
{
    [SerializeField] private float projectDistance = 1.0f;
    [SerializeField] private float groundOffset = 0f;
    [SerializeField] private Transform projectFrom;
    [SerializeField] private LayerMask groundLayer;

    public float distanceFromGround;

    public UnityEvent<float> changeDistanceFromGround;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit newHit;

        if (Physics.Raycast(projectFrom.position, Vector3.down, out newHit, projectDistance, groundLayer, QueryTriggerInteraction.Ignore))
        {
            transform.rotation = Quaternion.identity;
            transform.position = newHit.point + new Vector3(0,groundOffset, 0);
            distanceFromGround = newHit.distance;
            changeDistanceFromGround.Invoke(distanceFromGround);
        }
    }
}
