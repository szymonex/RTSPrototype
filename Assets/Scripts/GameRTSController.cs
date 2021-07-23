using System.Collections.Generic;
using UnityEngine;

public class GameRTSController : MonoBehaviour
{
    private SelectionBoxController selectionBoxController;
    private Vector3 startPosition;
    private Vector3 dragPosition;
    [SerializeField] LayerMask layerMask;
    private List<UnitRTS> selectedUnitsRTSList;
    //public Vector3 orderToMovePoint;

    private void Awake()
    {
        selectedUnitsRTSList = new List<UnitRTS>();
        selectionBoxController = GetComponent<SelectionBoxController>();

    }

    private void Update()
    {
        Vector2 mouseStartPos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            selectionBoxController.ShowSelectionBox(mouseStartPos);
            startPosition = MyUtils.GetMouseWorldPosition();
            Box.baseMin = startPosition;
        }

        if (Input.GetMouseButton(0))
        {
            selectionBoxController.TransformSelectionBox(mouseStartPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectionBoxController.HideSelectionBox();
            dragPosition = MyUtils.GetMouseWorldPosition();
            Box.baseMax = dragPosition;
            Collider[] colliderArray = Physics.OverlapBox(Box.Center, Box.Extents, Quaternion.identity, layerMask);

            //Debug.Log("######");

            foreach (UnitRTS unitRTS in selectedUnitsRTSList) // deselect all Units (visualy)
            {
                unitRTS.SetSelectedVisible(false);
            }

            selectedUnitsRTSList.Clear();
            foreach (Collider collider in colliderArray) // select Units within selection area
            {
                //Debug.Log(collider.ToString());
                UnitRTS unitRTS = collider.GetComponent<UnitRTS>();
                if (unitRTS != null)
                {
                    unitRTS.SetSelectedVisible(true);
                    unitRTS.offsetFroSelectionCenter = UnitRTSOffsetFroSelectionCenter(unitRTS.transform.position);
                    selectedUnitsRTSList.Add(unitRTS);
                }
            }
            Debug.Log(selectedUnitsRTSList.Count.ToString());
        }
        if(selectedUnitsRTSList.Count > 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                var orderToMovePoint = MyUtils.GetMouseWorldPosition();
                foreach (UnitRTS unitRTS in selectedUnitsRTSList)
                {
                    unitRTS.hasNewDestinationPoint = true;
                    unitRTS.destinationPoint = orderToMovePoint;
                }
                //Debug.Log(orderToMovePoint);
            }
        }
    }
    public Vector3 UnitRTSOffsetFroSelectionCenter(Vector3 unitPos)
    {
        var offset = Box.Center - unitPos;
        offset.y = 0f;
        return offset;
    }
}
