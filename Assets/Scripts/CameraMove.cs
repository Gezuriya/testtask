using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Player;

    private void LateUpdate()
    {
        transform.position = Player.transform.position + new Vector3(0, 3, -1.5f);
    }
}
