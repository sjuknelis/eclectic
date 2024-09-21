using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBehaviour : MonoBehaviour
{
    public bool isColorNull;
    public GameColor fixedColor;
    public bool isMultiShot;

    public GameColor? color = null;

    private bool isMouseOver = false;
    private int selectedTimer = 0;

    void Start()
    {
        ObjectStore.powers.Add(this);

        if (!isColorNull) color = fixedColor;
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
    }

    public void OnMouseEnter()
    {
        isMouseOver = true;
    }

    public void OnMouseExit()
    {
        isMouseOver = false;
    }

    public bool IsSelected()
    {
        return selectedTimer > 0;
    }
}