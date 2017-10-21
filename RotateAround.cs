using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public static float speed = 12f;
    public Transform target;

    private Vector3 zAxis = new Vector3(0, 0, 1);

    void FixedUpdate()
    {
        transform.RotateAround(target.position, zAxis, speed);
    }
}
