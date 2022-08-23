using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    void Update()
    {
        gameObject.GetComponent<Text>().color = Color.Lerp(new Color(1, 1, 1, 0.4f), new Color(1, 1, 1, 1f), Mathf.PingPong(Time.time, 1));
    }
}
