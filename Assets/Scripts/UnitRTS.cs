using UnityEngine;

public class UnitRTS : MonoBehaviour
{
    [SerializeField] private GameObject selectedGameObject;
    public Vector3 destinationPoint;
    public bool hasNewDestinationPoint = false;
    public bool canMove = false;
    private Vector3 nextPoint;
    private bool isPointAchieved = true;
    public Pathfinding pathfinding;
    [SerializeField] private float speed = 5f;
    public Vector3 offsetFroSelectionCenter;
    private int pathCount = -1;

    private void Awake()
    {
        SetSelectedVisible(false);
        pathfinding = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        if (hasNewDestinationPoint)
        {
            pathfinding.SetTargetPoint(destinationPoint + offsetFroSelectionCenter);
            hasNewDestinationPoint = false;
        }
        if (canMove)
        {
            if (isPointAchieved && pathCount < pathfinding.pathWorldPoints.Count - 1)
            {
                NextPoint();
            }
            MoveUnitRTS();
        }
    }

    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    private void NextPoint()
    {
        pathCount++;
        //Debug.Log("pathWorldPoints.Count " + pathfinding.pathWorldPoints.Count);
        nextPoint = pathfinding.pathWorldPoints[pathCount];
        nextPoint.y = 1f;
        isPointAchieved = false;
    }

    private void MoveUnitRTS()
    {
        Vector3 targetPosition = nextPoint - transform.position;

        //transform.position += new Vector3(targetPosition.x * speed * Time.deltaTime, 0f, targetPosition.z * speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, 0.1f);

        if(Vector3.Distance(gameObject.transform.position, nextPoint) < 0.1f || hasNewDestinationPoint)
        {
            isPointAchieved = true;
        }
        if(Vector3.Distance(gameObject.transform.position, destinationPoint) < 0.1f)
        {
            canMove = false;
        }
    }
}
