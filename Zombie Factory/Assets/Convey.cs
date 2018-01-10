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
        if (Camera.main.gameObject.GetComponent<Controller>().mode == 1)
        {
            transform.Rotate(new Vector3(0, 0, -90));
        }
    }
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Item") {
            Item s = coll.gameObject.GetComponent <Item> ();
            if (s.mover == null)
            {
                s.mover = gameObject;
                coll.gameObject.transform.Translate(transform.up * 1 * Time.deltaTime);
                //coll.gameObject.transform.position = transform.position;
            } else
            {
                if (s.mover == gameObject)
                {
                    coll.gameObject.transform.Translate(transform.up * 1 * Time.deltaTime);
                }
            }
			
		}
	}
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Item")
        {
            Item s = coll.gameObject.GetComponent<Item>();
            s.mover = null;

        }
    }
}
