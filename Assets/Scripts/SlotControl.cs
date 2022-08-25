using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotControl : MonoBehaviour, IDropHandler
{
    Inventory inv;
    public Color current;

      void Start()
       {
        Upd();
       }

    public void Upd()
    {
        if (gameObject.name == "ZamokPng")
        {
            inv = GetComponentInParent<Inventory>();
            StartCoroutine(GetColor());
        }
    }
    IEnumerator GetColor()
    {
        yield return new WaitForSeconds(0.1f);
        current = inv.currentColor;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(gameObject.name != "ZamokPng")
        {
            if (gameObject.transform.childCount == 0)
            {
                var otherTransform = eventData.pointerDrag.transform;
                otherTransform.SetParent(transform);
                otherTransform.localPosition = Vector3.zero;
            }
        }
        else
        {
            var otherTransform = eventData.pointerDrag.transform;
            if(otherTransform.GetComponent<Image>().color == current)
            {
                Destroy(otherTransform.gameObject);
                inv.UpdText();
            }
        }

    }
}
