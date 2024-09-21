using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBehaviour : MonoBehaviour
{
    public float xSpan = 2.25f,
    despawnY = -2.25f,
    minXSpeed = 1.5f,
    maxXSpeed = 3,
    gravityAcc = 10,
    minYSpeedLimit = 4,
    maxYSpeedLimit = 6,
    bounceSpeedLoss = 2;

    public GameColor color;
    private int xDirection;
    private float xSpeed, ySpeedLimit, yVelocity = 0;

    private bool isMouseOver = false;
    private int selectedTimer = 0;

    void Start()
    {
        ObjectStore.enemies.Add(this);

        var colors = Enum.GetValues(typeof(GameColor));
        color = (GameColor)colors.GetValue(Random.Range(0, colors.Length));

        transform.position = new Vector2(
            Random.Range(-xSpan, xSpan),
            transform.position.y
        );

        xDirection = Random.Range(0, 1) > 0.5 ? 1 : -1;
        xSpeed = Random.Range(minXSpeed, maxXSpeed);
        ySpeedLimit = Random.Range(minYSpeedLimit, maxYSpeedLimit);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isMouseOver) selectedTimer = 5;
        }
        else
        {
            selectedTimer = Mathf.Max(selectedTimer - 1, 0);
        }

        Color realColor = GameColorUtils.GetRealColor(color);
        GetComponent<Renderer>().material.color = selectedTimer > 0 ? GameColorUtils.DarkenRealColor(realColor) : realColor;

        float xPos = transform.position.x + xDirection * xSpeed * Time.deltaTime;
        if (Mathf.Abs(transform.position.x) > xSpan) {
            xDirection *= -1;
            xPos = Mathf.Sign(transform.position.x) * xSpan;
        }

        float yPos = transform.position.y + yVelocity * Time.deltaTime;
        yVelocity -= gravityAcc * Time.deltaTime;
        if (yVelocity < -ySpeedLimit) yVelocity = ySpeedLimit - bounceSpeedLoss;
        if (transform.position.y < despawnY) Destroy(gameObject);

        transform.position = new Vector2(xPos, yPos);
    }

    public void OnMouseEnter()
    {
        isMouseOver = true;
    }

    public void OnMouseExit()
    {
        isMouseOver = false;
    }

    public void OnDestroy()
    {
        ObjectStore.enemies.Remove(this);
    }

    public bool IsSelected()
    {
        return selectedTimer > 0;
    }
}