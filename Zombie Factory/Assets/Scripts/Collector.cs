using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
    public ShopScrollList scrList;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Item")
        {
            Item s = coll.gameObject.GetComponent<Item>();
            if (s.itemID == "zombie")
            {
                ZomStats stats = s.gameObject.GetComponent<Zombie>().stats;
                Debug.Log("Got Zombie");
                Destroy(coll.gameObject);
                scrList.brains += stats.GetSTR() + stats.GetSPD() + stats.GetHP() + stats.GetINT();
                scrList.RefreshBrains();
            }

        }
    }
}
