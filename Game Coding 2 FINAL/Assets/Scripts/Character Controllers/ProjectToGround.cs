using UnityEngine;

public class ProjectToGround : MonoBehaviour
{
    [SerializeField] private Transform projectFrom;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float projectDistance;

    [SerializeField] private float MaxForceDistance;
    [SerializeField] private ConstantForce cForce;
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit newHit;
        
        if(Physics.Raycast(projectFrom.position, Vector3.down, out newHit, projectDistance, groundLayer, QueryTriggerInteraction.Ignore))
        {
            cForce.force = new Vector3(0, Mathf.Lerp(maxForce, minForce, Mathf.Clamp(newHit.distance/ MaxForceDistance, 0, 1)), 0);
        }
    }
}
