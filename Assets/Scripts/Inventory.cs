using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject[] Slots;
    [SerializeField] GameObject[] Keys;
    [SerializeField] GameObject Zamok, YouWin;
    public Color red, yellow, blue, currentColor;
    public TextMeshProUGUI txt;
    public int Count;
    public void UpdEvery()
    {
        Count = 0;
        txt.text = Count.ToString() + "/3";
        int i = Random.Range(0, 3);
        if (i == 0)
        {
            Zamok.GetComponent<Image>().color = red;
            currentColor = red;
        }
        else if (i == 1)
        {
            Zamok.GetComponent<Image>().color = yellow;
            currentColor = yellow;
        }
        else
        {
            Zamok.GetComponent<Image>().color = blue;
            currentColor = blue;
        }
        foreach (GameObject slot in Slots)
        {
            if(slot.transform.childCount != 0)
                Destroy(slot.transform.GetChild(0).gameObject);
        }

        foreach (GameObject slot in Slots)
        {
            var obj = Instantiate(Keys[Random.Range(0, Keys.Length)]);
            obj.transform.SetParent(slot.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        }
    }
    public void UpdText()
    {
        Count++;
        txt.text = Count.ToString() + "/3";
        if(Count == 3)
        {
            YouWin.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
