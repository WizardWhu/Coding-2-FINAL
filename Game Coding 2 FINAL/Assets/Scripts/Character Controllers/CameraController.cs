using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] targetTransforms;
    private Vector3 target;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private float buffering;
    [SerializeField] private float rotationBuffer;

    private Vector3 followTarget;

    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float BaseRotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector2 mousePosition;
    [SerializeField] private float lookSense;

    private void Awake()
    {
        LargeDinosaurController.movedMouse += OnMouseMove;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void OnMouseMove(Vector2 mousePosition)
    {
        if (target != null)
        {
            float amountToMove = mousePosition.x * lookSense * Time.deltaTime;
            transform.RotateAround(new Vector3(target.x, transform.position.y, target.z), Vector3.up, amountToMove);
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        target = Vector3.zero;

        for (int i = 0; i < targetTransforms.Length; i++)
        {
            target += targetTransforms[i].position;
        }
        target /= targetTransforms.Length;


        followTarget = target + ((transform.position - target).normalized * distanceFromPlayer);
        if ((followTarget - transform.position).magnitude > buffering)
        {
            Vector3 targetMove = (followTarget - transform.position).normalized * cameraMoveSpeed * Time.deltaTime;
            transform.position += new Vector3(targetMove.x, 0, targetMove.z);
        }


        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);
        if (1f - Mathf.Clamp(Quaternion.Dot(transform.rotation, targetRotation), 0, 1) > rotationBuffer / BaseRotationSpeed || (followTarget - transform.position).magnitude > buffering)
        {

            float rotationSpeed = BaseRotationSpeed * (1 - Mathf.Clamp(Quaternion.Dot(transform.rotation, targetRotation), 0, 1));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
