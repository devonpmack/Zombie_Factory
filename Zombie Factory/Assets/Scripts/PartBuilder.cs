using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReqElement {
	public string element_id;
	public int quantity;
	[System.NonSerialized]
	public int remaining;
}

[System.Serializable]
public class Requisites {
	public List<ReqElement> reqs;
}

public class PartBuilder : MonoBehaviour {
	// Use this for initialization
	public Requisites toBuild;
	public GameObject output;
	public float BuildTime;

	private bool building = false;

	void Start () {
		//Invoke ("Spawn", 1);
		reset();
	}

	void reset() {
		foreach (ReqElement ele in toBuild.reqs) {
			ele.remaining = ele.quantity;
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Item")
		{
			Item s = coll.gameObject.GetComponent<Item>();
			foreach (ReqElement ele in toBuild.reqs) {
				if (s.itemID == ele.element_id) {
					Debug.Log (s.itemID + " Remaining:" + ele.remaining.ToString());
					Destroy (coll.gameObject);
					ele.remaining -= 1;
					CheckCompletion ();
				}
			}
		}
	}

	void CheckCompletion() {
		foreach (ReqElement ele in toBuild.reqs) {
			if (ele.remaining > 0) {
				return;
			}
		}
		if (!building) {
			build ();
		}

	}

	void build() {
		building = true;
		Invoke ("Complete", BuildTime);
	}

	void Complete () {
		building = false;
		Debug.Log ("Created " + output.name);
		GameObject s = Instantiate (output);
		s.transform.position = transform.position;
		reset ();
		//s.transform.Translate (transform.up*0.5f);
	}
}
