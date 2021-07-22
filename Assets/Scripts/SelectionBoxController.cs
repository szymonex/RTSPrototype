using UnityEngine;
using UnityEngine.UI;

public class SelectionBoxController : MonoBehaviour
{
    [SerializeField]private RectTransform selectionBox;
    Rect selectionRect; //rzeczywista wartoœæ na ekranie, jak ten box jest zaznaczony
    Rect boxRect; // wartoœæ któr¹ bêdziemy przekazywali do RectTransform selectionBox

    private void Awake()
    {
        selectionBox = selectionBox.GetComponent<Image>().transform as RectTransform;
        selectionBox.gameObject.SetActive(false);
    }

    public void ShowSelectionBox(Vector2 mousePos)
    {
        selectionBox.gameObject.SetActive(true);
        selectionRect.position = mousePos;
    }

    public void TransformSelectionBox(Vector2 mousePos)
    {
        selectionRect.size = mousePos - selectionRect.position;
        boxRect = AbsRect(selectionRect);
        selectionBox.anchoredPosition = boxRect.position;
        selectionBox.sizeDelta = boxRect.size;
    }

    public void HideSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
    }

    private Rect AbsRect(Rect rect)
    {
        if(rect.width < 0)
        {
            rect.x += rect.width;
            rect.width *= -1;
        }
        if (rect.height < 0)
        {
            rect.y += rect.height;
            rect.height *= -1;
        }
        return rect;
    }
}
