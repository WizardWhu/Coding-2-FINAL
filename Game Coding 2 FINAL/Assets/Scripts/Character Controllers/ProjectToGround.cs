using UnityEngine;

public class ProjectToGround : MonoBehaviour
{
    [SerializeField] private Transform projectFrom;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float projectDistance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit newHit;
        Physics.Raycast(projectFrom.position, Vector3.down, out newHit, projectDistance, groundLayer, QueryTriggerInteraction.Ignore);
        transform.position = newHit.point;
    }
}
