using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemID = "default";
	public int quality = 1;
	[System.NonSerialized]
    public GameObject mover;
    // Use this for initialization
    void Start()
    {
        Invoke("despawn", 30);
    }

    void despawn()
    {
        Destroy(gameObject);
    }
}