using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
public class SpawnPointsPopups : MonoBehaviour
{
    public static SpawnPointsPopups instance { get; private set; }

    private ObjectPool<PointsLabel> pointsLabelPopupPool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Points Label Popup")]
    [SerializeField] private PointsLabel pointsLabelPrefab;

    [Header("Display Setup")]
    [Range(0.8f, 1.5f), SerializeField] public float displayLength = 1f;

    private Camera mainCamera;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        pointsLabelPopupPool = new ObjectPool<PointsLabel>(
            () =>
            {
                PointsLabel pointsLabel = Instantiate(pointsLabelPrefab, transform);
                pointsLabel.Initialize(displayLength, this);
                return pointsLabel;
            },
            pointsLabel => pointsLabel.gameObject.SetActive(true),
            pointsLabel => pointsLabel.gameObject.SetActive(false)
            );

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

  
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main;
    }

    public void PointsGained(int points, Vector3 position, bool isHigh)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(position);
        screenPosition.z = 0;

        bool direction = false;
        SpawnPointsPopup(points, screenPosition, direction, isHigh);
    }

    private void SpawnPointsPopup(int points, Vector3 position, bool direction, bool isHigh)
    {
        PointsLabel pointsLabel = pointsLabelPopupPool.Get();
        pointsLabel.Display(points, position, direction, isHigh, new Vector3(0,0,0));
    }

    public void ReturnPointsLabelToPool(PointsLabel pointLabel3D)
    {
        pointsLabelPopupPool.Release(pointLabel3D);
    }
}
