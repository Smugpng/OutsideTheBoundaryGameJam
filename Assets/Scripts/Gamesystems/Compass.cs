using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform playerTransform;
    Vector3 playerDir;

    private void Update()
    {
        playerDir.z = playerTransform.eulerAngles.y;
        transform.localEulerAngles = playerDir;
    }
}
