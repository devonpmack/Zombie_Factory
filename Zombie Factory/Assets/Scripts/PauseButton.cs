using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GetComponentInChildren(typeof(Text)).GetComponent<Text>().text = "Pa";
        }

        else
        {
            GetComponentInChildren(typeof(Text)).GetComponent<Text>().text = "Pl";
            Time.timeScale = 0;
        }
        Camera.main.gameObject.GetComponent<Controller>().paused = !running;
    }
}
