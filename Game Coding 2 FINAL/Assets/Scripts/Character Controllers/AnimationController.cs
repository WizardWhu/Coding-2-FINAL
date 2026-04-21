using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private bool isMoving = false;

    [SerializeField] private float RotationSpeed;
    [SerializeField] private Transform hipsAnimatedTransform;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Awake()
    {
        LargeDinosaurController.StartedMoving += StartWalking;
        LargeDinosaurController.MoveDirection += UpdateRotation;
    }

    void StartWalking(bool moving)
    {
        if (moving)
        {
            isMoving = true;
            animator.SetBool("Moving", true);
        }
        else
        {
            isMoving = false;
            animator.SetBool("Moving", false);
        }
    }
    void UpdateRotation(Vector3 direction)
    {
        hipsAnimatedTransform.localRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, -direction.z));
    }
}
