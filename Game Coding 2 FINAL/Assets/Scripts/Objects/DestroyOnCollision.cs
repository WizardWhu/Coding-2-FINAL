using System.Linq;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [Header("Destroy Physics")]
    [SerializeField] private GameObject DestroyedPieces;
    [SerializeField] private float DestructionVelocity;
    [SerializeField] private float ExplosionModifier = 1;
    [SerializeField] private float ExplosionUpwardsModifier = 2;

    [Header("Points")]
    [SerializeField] private int pointValue = 0;
    [SerializeField] private bool isHighValue = false;

    private bool destroyed = false;

    private ScoreCounter scoreCounter;
    private FadeoutCast fadeOutCast;
    private Material currentMat;
    private void Start()
    {
        fadeOutCast = GameObject.FindAnyObjectByType<FadeoutCast>();
        scoreCounter = GameObject.FindAnyObjectByType<ScoreCounter>();
        currentMat = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!destroyed && collision.gameObject.CompareTag("Player") && collision.relativeVelocity.magnitude > DestructionVelocity)
        {
            fadeOutCast.RemoveObstacle(gameObject);
            destroyed = true;
            GameObject createdPieces = Instantiate(DestroyedPieces, transform.position, transform.rotation);

            Rigidbody[] newRBs = createdPieces.GetComponentsInChildren<Rigidbody>();
            if (createdPieces.GetComponent<Rigidbody>() != null) newRBs.Append(createdPieces.GetComponent<Rigidbody>());

            for(int i = 0; i < newRBs.Length; i++)
            {
                newRBs[i].transform.GetComponent<Renderer>().material = currentMat;
                newRBs[i].AddForce(collision.rigidbody.linearVelocity * ExplosionModifier, ForceMode.Impulse);
                newRBs[i].AddExplosionForce(collision.rigidbody.linearVelocity.magnitude*ExplosionModifier, collision.collider.ClosestPoint(transform.position), collision.rigidbody.linearVelocity.magnitude*ExplosionModifier, ExplosionUpwardsModifier, ForceMode.Impulse);
            }

            scoreCounter.IncrementScore(pointValue);
            SpawnPointsPopups.instance.PointsGained(pointValue, transform.position, isHighValue);
            Destroy(gameObject);
        }
    }



}
