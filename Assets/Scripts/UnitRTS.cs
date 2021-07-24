using UnityEngine;

public class UnitRTS : MonoBehaviour
{
    public Vector3 destinationPoint;
    public bool hasNewDestinationPoint = false;
    public bool canMove = false;
    public Pathfinding pathfinding;
    public float speed = 0.01f;
    public Vector3 offsetFroSelectionCenter;
    private Vector3 nextPoint;
    private bool isPointAchieved = true;
    private int pathCount = -1;
    [SerializeField] private GameObject selectedGameObject;

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
        nextPoint = pathfinding.pathWorldPoints[pathCount];
        nextPoint.y = 1f;
        isPointAchieved = false;
    }

    private void MoveUnitRTS()
    {
        Vector3 targetPosition = nextPoint - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed);

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
