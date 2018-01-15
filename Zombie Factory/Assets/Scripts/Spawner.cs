using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject essence;
	// Use this for initialization
	void Start () {
		Invoke ("Spawn", 1);
	}

	// Update is called once per frame
	void Update () {

	}

	void Spawn () {
		GameObject s = Instantiate (essence);
		Invoke ("Spawn", 4);
		s.transform.position = transform.position;
		s.transform.Translate (transform.up*0.5f);
	}
}
