using System.Collections;
using TMPro;
using UnityEngine;

public class PointsLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private float normalFontSize = 40;
    [SerializeField] private float largeFontSize = 60;
    [SerializeField] private Color normalFontColor = Color.white;

    [SerializeField] private float startColorFadeAtPercent = 0.8f;

    [Header("Animation Easing")]
    [SerializeField] private AnimationCurve easeCurve;
    private float displayDuration;

    [Header("Bezier curve settings")]
    [SerializeField] private Vector2 highestPointOffset = new Vector2(-350, 300);
    [SerializeField] private Vector2 lowestPointOffset = new Vector2(-100, -500);
    [SerializeField] private float heightVariationMax = 150;
    [SerializeField] private float heightVariationMin = 50;

    private Vector3 hightPointOffsetBasedOnDirection = Vector3.zero;
    private Vector3 dropPointOffsetBasedOnDirection = Vector3.zero;
    private bool direction = true;

    [Header("Visualize")]
    [SerializeField] private bool displayGizmos;
    [SerializeField, Range(1, 30)] private int gizmoResolution = 20;
    private Vector3 startingPositionforVisualization = Vector3.zero;


    private int points = 0;
    private SpawnPointsPopups poolManager;

    private Coroutine moveCoroutine;

    private ScoreCounter scoreCounter;
    private void Start()
    {
        scoreCounter = GameObject.FindAnyObjectByType<ScoreCounter>();
    }
    private void OnDrawGizmos()
    {
        if (!displayGizmos) return;

        OrientCurvesBasedOnDirection();

        Vector3 start = transform.position;

        if (Application.isPlaying)
            start = startingPositionforVisualization;

        float heightVariation = heightVariationMax - heightVariationMin;

        Vector3 highPoint = start + hightPointOffsetBasedOnDirection + new Vector3(0, heightVariation, 0);
        Vector3 dropPoint = highPoint + dropPointOffsetBasedOnDirection;

        int colorChangeIndex = (int)(startColorFadeAtPercent + gizmoResolution);


        Gizmos.color = Color.red;

        Vector3 prevPoint = start;

        for(int i = 1; i <= gizmoResolution; i++)
        {
            float time = 1 / (float)gizmoResolution;
            Vector3 nextPoint = CalculateBezierPoint(time, start, highPoint, dropPoint);

            if (i >= colorChangeIndex) Gizmos.color = Color.yellow;

            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;

        }
    }

    private void OrientCurvesBasedOnDirection()
    {
        //reset to original direction

        hightPointOffsetBasedOnDirection = highestPointOffset;
        dropPointOffsetBasedOnDirection = lowestPointOffset;

        if (direction)
            return;

        hightPointOffsetBasedOnDirection.x = -hightPointOffsetBasedOnDirection.x;
        dropPointOffsetBasedOnDirection.x = -dropPointOffsetBasedOnDirection.x;
    }

    private Vector3 CalculateBezierPoint(float progresss, Vector3 start, Vector3 control, Vector3 end)
    {
        float remainingPath = 1 - progresss;
        Vector3 currentLocation = remainingPath * remainingPath * start;

        currentLocation += 2 * remainingPath * progresss * control;
        currentLocation += progresss * progresss * end;

        return currentLocation;
    }

    public void Initialize(float displayDuration, SpawnPointsPopups poolManager)
    {
        this.poolManager = poolManager;
        this.displayDuration = displayDuration;

        OrientCurvesBasedOnDirection();
    }

    public void Display(int points, Vector3 objPosition, bool direction, bool isHighValue, Vector3 targetPos)
    {
        transform.position = objPosition;
        startingPositionforVisualization = objPosition;
        this.direction = direction;

        lowestPointOffset = objPosition - targetPos;

        this.points = points;
        pointsText.SetText(points.ToString());

        pointsText.color = normalFontColor;
        pointsText.enableVertexGradient = isHighValue;
        pointsText.fontSize = isHighValue ? largeFontSize : normalFontSize;

        if(moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(Move());
        StartCoroutine(ReturnPointsLabelToPool(displayDuration));

    }

    private IEnumerator Move()
    {
        float time = 0;
        float fadeStartTime = startColorFadeAtPercent * displayDuration;

        OrientCurvesBasedOnDirection();

        Vector3 start = transform.position;

        float heightVariation = Random.Range(heightVariationMin, heightVariationMax);

        Vector3 variation = new Vector3(0, heightVariation, 0);

        Vector3 highPoint = (start + hightPointOffsetBasedOnDirection + variation);
        Vector3 dropPoint = highPoint + dropPointOffsetBasedOnDirection;


        while(time < displayDuration)
        {
            time +=Time.deltaTime;

            float progress = time/displayDuration;
            float easedTime = easeCurve.Evaluate(progress);

            if(time > fadeStartTime)
            {
                Color color = pointsText.color;
                float newAlpha = Mathf.Lerp(1, 0, (time - fadeStartTime) / (displayDuration - fadeStartTime));
                color.a = newAlpha;
                pointsText.color = color;
            }
            transform.position = CalculateBezierPoint(easedTime, start, highPoint, dropPoint);
            yield return null;
        }

    }

    private IEnumerator ReturnPointsLabelToPool(float displayLength)
    {
        yield return new WaitForSeconds(displayLength);
        scoreCounter.IncrementScore(points);
        poolManager.ReturnPointsLabelToPool(this);
    }
}