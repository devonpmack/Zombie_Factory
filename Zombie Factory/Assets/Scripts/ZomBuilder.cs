using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomPart {
    public string type;
    public int quality;

    public ZomPart(string ztype, int zquality)
    {
        type = ztype;
        quality = zquality;
    }
}
[System.Serializable]
public class ZomStats
{
    public List<ZomPart> parts;

    public int SPD;
    public int HP;
    public int INT;
    public int STR;
    
    private int max_torso = 1;
    private int max_arm = 2;
    private int max_leg = 2;
    private int max_head = 1;

    public ZomStats()
    {
        parts = new List<ZomPart>();
    }
    public ZomStats(int m_torso, int m_arm, int m_leg, int m_head)
    {
        parts = new List<ZomPart>();
        max_torso = m_torso;
        max_arm = m_arm;
        max_leg = m_leg;
        max_head = m_head;
    }

    public int GetSPD()
    {
        return SPD;
    }
    public int GetHP()
    {
        return HP;
    }
    public int GetSTR()
    {
        return STR;
    }
    public int GetINT()
    {
        return INT;
    }

    bool RefreshStats()
    {
        int legs = 0;
        int arms = 0;
        int heads = 0;
        int torsos = 0;

        STR = 0;
        SPD = 0;
        INT = 0;
        HP = 0;
        foreach (ZomPart part in parts)
        {
            switch (part.type)
            {
                case "arm":
                    STR += part.quality;
                    arms++;
                    break;
                case "leg":
                    SPD += part.quality;
                    legs++;
                    break;
                case "head":
                    INT += part.quality;
                    heads++;
                    break;
                case "torso":
                    HP += part.quality;
                    torsos++;
                    break;
            }
        }
        return true;
        //return (legs > max_leg || arms > max_arm || heads > max_head || torsos > max_torso);
    }

    public void AddPart(Item part)
    {
        if (part.itemID == "arm" || part.itemID == "leg" || part.itemID == "torso" || part.itemID == "head")
        {
            ZomPart zpart = new ZomPart(part.itemID, part.quality);
            parts.Add(zpart);
            if (!RefreshStats())
            {
                parts.Remove(zpart);
            }
        }
    }
}

public class ZomBuilder : MonoBehaviour {
	// Use this for initialization
	public Requisites toBuild;
	public GameObject output;
	public float BuildTime;

    private ZomStats zom;


    private bool building = false;

	void Start () {
		//Invoke ("Spawn", 1);
		reset();
        zom = new ZomStats();

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
            if (building)
            {
                Destroy(coll.gameObject);
                return;
            }
            // not building so take the thing in
			Item s = coll.gameObject.GetComponent<Item>();
			foreach (ReqElement ele in toBuild.reqs) {
				if (s.itemID == ele.element_id) {
					Debug.Log (s.itemID + " Remaining:" + ele.remaining.ToString());
					Destroy (coll.gameObject);
					ele.remaining -= 1;
                    if(ele.remaining >= 0)
                    {
                        zom.AddPart(s);
                    }
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
        s.GetComponent<Zombie>().stats = zom;
        zom = new ZomStats();

        //s.transform.Translate (transform.up*0.5f);
    }
}
