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
                    Vector2 pos = coll.gameObject.transform.position;
                    pos = Vector2.MoveTowards(pos, (Vector2)transform.position + (Vector2)transform.up*0.5f + (Vector2)transform.up*coll.gameObject.GetComponent<BoxCollider2D>().bounds.size.x, Time.deltaTime*1);
                    coll.gameObject.transform.position = pos;
                    //coll.gameObject.transform.Translate(transform.up * 1 * Time.deltaTime);
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
