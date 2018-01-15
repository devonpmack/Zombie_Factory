using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    public int mode = 0; // 1 rotate, 2 move
    public GameObject rb;
    public GameObject mb;
    public bool MenuOpen = true;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
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
        switch(mode)
        {
            case 0:
                //rb.GetComponentsInChildren<Text>.guiText = 
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

}
