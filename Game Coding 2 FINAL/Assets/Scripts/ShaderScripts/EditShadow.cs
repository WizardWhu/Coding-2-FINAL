using UnityEngine;

public class EditShadow : MonoBehaviour
{
    [SerializeField] private float MaxShadowRadius;
    [SerializeField] private float MinShadowRadius;

    [SerializeField] private float MaxHeight;
    [SerializeField] private float MinHeight;

    private Material currentMaterial;
    void Start()
    {
        currentMaterial = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
    }
    public void ChangeShadowInfo(float distanceFromGround)
    {
        currentMaterial.SetFloat("_Radius", Mathf.Lerp(MinShadowRadius, MaxShadowRadius, 1 - ((distanceFromGround - MinHeight) / (MaxHeight - MinHeight))));
    }
}
