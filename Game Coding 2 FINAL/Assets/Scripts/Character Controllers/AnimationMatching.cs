using UnityEngine;

public class AnimationMatching : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private Transform animatorTransform;
    [SerializeField] private bool flipped;
    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
    }
    private void Update()
    {
        if (!flipped)
        {
            joint.targetRotation = animatorTransform.localRotation;
        }
        else
        {
            joint.targetRotation = Quaternion.Inverse(animatorTransform.localRotation);

        }
    }
}
