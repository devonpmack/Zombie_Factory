using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour {
    public int mode = 0; // 1 rotate, 2 move
    public GameObject rb;
    public GameObject mb;
    public GameObject xb;
    public bool paused = false;
    public bool MenuOpen = false;
    // Use this for initialization
    public void SetMenuStatus(bool open)
    {
        MenuOpen = open;
    }
    public void toggle(int n)
    {
        if (mode != n)
        {
            mode = n;
        } else
        {
            mode = 0;
        }
        //switch(mode)
        //{
        //    case 0:
        //        rb.GetComponentInChildren<Text>().text = "R";
        //        mb.GetComponentInChildren<Text>().text = "M";
        //        break;
        //    case 1:
        //        rb.GetComponentInChildren<Text>().text = "O";
        //        break;
        //    case 2:
        //        mb.GetComponentInChildren<Text>().text = "O";
        //        break;
        //}
    }
    private void Update()
    {
        switch (mode)
        {
            case 0:
                EventSystem.current.SetSelectedGameObject(null);
                break;
            case 1:
                rb.GetComponent<Button>().Select();
                break;
            case 2:
                mb.GetComponent<Button>().Select();
                break;
            case 3:
                xb.GetComponent<Button>().Select();
                break;
        }
    }

    private static readonly float[] BoundsX = new float[] { 0.5f, 11.5f };
    private static readonly float[] BoundsY = new float[] { 0.5f, 11.5f };

    public bool onMap(Vector2 position)
    {
        if (position.x > BoundsX[0] && position.x < BoundsX[1] && position.y > BoundsY[0] && position.y < BoundsY[1])
        {
            return true;
        }

        return false;
    }

}
