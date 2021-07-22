using UnityEngine;

public class UnitRTS : MonoBehaviour
{
    [SerializeField] private GameObject selectedGameObject;
    public Vector3 destinationPoint;
    public bool hasNewDestinationPoint = false;
    Vector3 nextPoint;
    public bool canMove = false;
    Pathfinding pathfinding;
    [SerializeField] private float speed = 3f;

    private void Awake()
    {
        SetSelectedVisible(false);
        pathfinding = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        if (hasNewDestinationPoint)
        {
            pathfinding.SetTargetPoint(destinationPoint);
            hasNewDestinationPoint = false;
            canMove = true;
        }
    }

    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    private void MoveToNextPoint()
    {
        Vector3 targetPosition = nextPoint - transform.position;
        transform.position += targetPosition * speed * Time.deltaTime;
    }
}
