using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float amount, duration;

    private Vector3 startPos;

    private float timer = 0;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (timer > 0)
        {
            float angle = Random.Range(-Mathf.PI, Mathf.PI);
            transform.position = startPos + new Vector3(
                Mathf.Cos(angle) * amount,
                Mathf.Sin(angle) * amount,
                0
            );

            timer -= Time.deltaTime;
        }
        else
        {
            transform.position = startPos;
        }
    }

    public void Trigger()
    {
        timer = duration;
    }
}