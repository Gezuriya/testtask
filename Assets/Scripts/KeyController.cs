using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    CanvasGroup canvGroup;
    Canvas canv;
    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        canv = GetComponentInParent<Canvas>();
        canvGroup = GetComponent<CanvasGroup>();
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        var slot = rect.parent;
        slot.SetAsLastSibling();
        canvGroup.blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta/canv.scaleFactor;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        canvGroup.blocksRaycasts = true;
    }

}
