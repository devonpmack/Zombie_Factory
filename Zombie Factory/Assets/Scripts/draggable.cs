using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class draggable : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 original;
	private SpriteRenderer sprite;
    public float price = 1f;
    public ShopScrollList scr;
	void Start() {
		Vector2 currentPos = transform.position;
		transform.position = new Vector2(Mathf.Round(currentPos.x),
			Mathf.Round(currentPos.y));
		sprite = GetComponent<SpriteRenderer> ();
		
	}
    void OnMouseDown()
    {
        if (Camera.main.gameObject.GetComponent<Controller>().mode == 2)
        {
            original = transform.position;
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			sprite.sortingOrder = 1;
			Time.timeScale = 0;
        }
		if (Camera.main.gameObject.GetComponent<Controller>().mode == 1)
		{
			transform.Rotate(new Vector3(0, 0, -90));
		}
        if (Camera.main.gameObject.GetComponent<Controller>().mode == 3)
        {
            scr.brains += price;
            scr.RefreshBrains();
            Destroy(gameObject);
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
			sprite.sortingOrder = 0;
            foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
            {
                if (block != this.gameObject && block.transform.position == transform.position)
                {
                    // fail
                    transform.position = original;
                    break;
                }
            }
            if (!Camera.main.gameObject.GetComponent<Controller>().onMap(currentPos))
            {
                transform.position = original;
            }
            if (!Camera.main.gameObject.GetComponent<Controller>().paused)
            {
                Time.timeScale = 1;
            }
        }
    }
}
