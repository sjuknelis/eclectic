using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public float speed = 1f;

    private Vector3 pointA, pointB;
    private GameObject destroyObject;

    private float scale = 0;

    void Update()
    {
        scale += speed * Time.deltaTime;
        
        var delta = pointB - pointA;
        if (scale >= delta.magnitude)
        {
            Destroy(destroyObject);
            Destroy(gameObject);
        }

        transform.rotation = Quaternion.Euler(
            0,
            0,
            Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * Mathf.Rad2Deg
        );
        transform.localScale = new Vector3(
            scale,
            transform.localScale.y,
            transform.localScale.z
        );
        transform.position = pointA + delta.normalized * scale / 2;
    }

    public void SetPath(Vector3 pointA, Vector3 pointB, GameObject destroyObject)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.destroyObject = destroyObject;
    }
}