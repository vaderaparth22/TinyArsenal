using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCustomAction_Advanced_H3 : CrosshairCustomAction
{
    override public void UpdateCustomAction()
    {
        var centerCircle = GetCrosshairLayer("aimCircle") as CLCenterCircle;
        if (centerCircle == null)
            return;

        if (ControlFreak2.CF2Input.GetMouseButtonDown(0))
        {
            centerCircle.CircleRadius = 70;
        }

        if (ControlFreak2.CF2Input.GetMouseButtonUp(0))
        {
            centerCircle.CircleRadius = 0;
        }

        centerCircle.CircleRadius = Mathf.Max(0, centerCircle.CircleRadius - Time.deltaTime * 200f);
    }
}
