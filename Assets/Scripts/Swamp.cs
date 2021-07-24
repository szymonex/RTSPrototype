using UnityEngine;

public class Swamp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            UnitRTS unitRTS = other.GetComponent<UnitRTS>();
            unitRTS.speed -= 0.05f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Unit")
        {
            UnitRTS unitRTS = other.GetComponent<UnitRTS>();
            unitRTS.speed += 0.05f;
        }
    }
}
