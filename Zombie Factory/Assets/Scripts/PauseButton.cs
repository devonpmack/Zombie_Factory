using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {

    private bool running;
    // Use this for initialization
    void Start() {

        running = true;

    }

    public void clicked()
    {

        running = !running;

        if (running)
        {
            Time.timeScale = 1;
           // GetComponentInChildren(Text).
        }

        else
        {
            Time.timeScale = 0;
        }
    }
}
