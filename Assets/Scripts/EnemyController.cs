using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 StartPos;
    bool Check;
    public float speed;
    private void Start()
    {
        StartPos = transform.position;
        if (StartPos.x < 0)
            Check = true;
        else if (StartPos.x > 0)
            Check = false;
    }

    private void FixedUpdate()
    {
        if (Check)
        {
            transform.Translate(speed * new Vector3(1,0,0) * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            transform.Translate(speed * new Vector3(-1, 0, 0) * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}
