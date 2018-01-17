using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShop : MonoBehaviour {
    public GameObject shopCanvas;
    public Controller main_control;
	public void Toggle()
    {
        shopCanvas.SetActive(!shopCanvas.activeSelf);
        main_control.MenuOpen = shopCanvas.activeSelf;
    }
}
