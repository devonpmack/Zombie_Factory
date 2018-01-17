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
            if (s.itemID == "essence")
            {
                Debug.Log("+1 Brains");
                Destroy(coll.gameObject);
                scrList.brains += 1;
                scrList.RefreshBrains();
            }

        }
    }
}
