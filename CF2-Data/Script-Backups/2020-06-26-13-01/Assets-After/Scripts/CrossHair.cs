using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ControlFreak2.CFCursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Escape))
        {
            ControlFreak2.CFCursor.visible = true;
        }

        transform.position = ControlFreak2.CF2Input.mousePosition;
    }
}
