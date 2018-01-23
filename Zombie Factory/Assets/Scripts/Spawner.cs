using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject essence;
    public int sDelay = 4;
	// Use this for initialization
	void Start () {
		Invoke ("Spawn", 1);
	}

	// Update is called once per frame
	void Update () {

	}

	void Spawn () {
		GameObject s = Instantiate (essence);
		Invoke ("Spawn", sDelay);
		s.transform.position = transform.position;
		//s.transform.Translate (transform.up*0.5f);
	}
}
