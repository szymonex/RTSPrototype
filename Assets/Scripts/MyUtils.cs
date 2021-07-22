using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyUtils
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithY(Input.mousePosition, Camera.main);
        vec.y = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithY(Vector3 screenPosition, Camera worldCamera)
    {
        Ray ray = worldCamera.ScreenPointToRay(screenPosition);
        Vector3 worldPosition = new Vector3();
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 1000f))
        {
            worldPosition = raycastHit.point;
        }

        return worldPosition;
    }
}
