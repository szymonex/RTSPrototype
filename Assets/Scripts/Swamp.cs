using UnityEngine;

public class Swamp : MonoBehaviour
{
    [SerializeField] private float slowingParameter = 0.03f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            UnitRTS unitRTS = other.GetComponent<UnitRTS>();
            unitRTS.speed -= slowingParameter;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Unit")
        {
            UnitRTS unitRTS = other.GetComponent<UnitRTS>();
            unitRTS.speed += slowingParameter;
        }
    }
}
