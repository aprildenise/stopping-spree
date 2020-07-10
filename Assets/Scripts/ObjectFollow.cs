using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;



    private void LateUpdate()
    {
        Vector3 newPosition = target.position;
        transform.position = newPosition + offset;
    }
}