using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class draggable : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 original;
    void OnMouseDown()
    {
        if (Camera.main.gameObject.GetComponent<Controller>().mode == 2)
        {
            original = transform.position;
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }
    void OnMouseDrag()
    {
        if (Camera.main.gameObject.GetComponent<Controller>().mode == 2)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }
    private void OnMouseUp()
    {
        if (Camera.main.gameObject.GetComponent<Controller>().mode == 2)
        {
            Vector2 currentPos = transform.position;
            transform.position = new Vector2(Mathf.Round(currentPos.x),
                                 Mathf.Round(currentPos.y));
            foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (block != this.gameObject && block.transform.position == transform.position)
                {
                    transform.position = original;
                    break;
                }
            }

        }
    }
}
