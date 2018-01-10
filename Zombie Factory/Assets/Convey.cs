using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convey : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector2 currentPos = transform.position;
        transform.position = new Vector2(Mathf.Round(currentPos.x),
                             Mathf.Round(currentPos.y));

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseDown()
    {
        transform.Rotate(new Vector3(0,0,-90));
    }
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Item") {
			Debug.Log ("c");
			coll.gameObject.transform.Translate (transform.up * 1 * Time.deltaTime);
		}
	}
}
