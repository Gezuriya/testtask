using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotControl : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (gameObject.transform.childCount == 0)
        {
            var otherTransform = eventData.pointerDrag.transform;
            otherTransform.SetParent(transform);
            otherTransform.localPosition = Vector3.zero;
        }
    }
}
