using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConfirmPurchase : MonoBehaviour {
    public GameObject placeholder;
    public ShopScrollList scrList;
    public GameObject decliner;
    private ShopElement toConfirm;
    private bool chosen = false;
    private Vector2 spawnPosition;
	// Use this for initialization
	public void confirm(ShopElement item)
    {
        placeholder.SetActive(false);
        Time.timeScale = 0;
        toConfirm = item;
        chosen = false;
        GetComponent<Button>().interactable = false;
        decliner.SetActive(true);
        SpriteRenderer img = placeholder.GetComponent<SpriteRenderer>();
        img.sprite = item.icon;
        Color color = img.color;
        color.a = 0.5f;
        img.color = color;
    }

    public void ClickHandler()
    {
        scrList.finalizePurchase(toConfirm, true);
        GameObject newBlock = Instantiate(toConfirm.prefab);
        newBlock.GetComponent<draggable>().price = toConfirm.price;
        newBlock.GetComponent<draggable>().scr = scrList;
        newBlock.transform.position = spawnPosition;
        gameObject.SetActive(false);
        placeholder.SetActive(false);
        decliner.SetActive(false);
    }
    public void DeclinePurchase()
    {
        placeholder.SetActive(false);
        gameObject.SetActive(false);
        decliner.SetActive(false);
        scrList.finalizePurchase(toConfirm, false);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (GetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition))) {
                GetComponent<Button>().interactable = true;
            }
        }
    }
    private bool GetPosition(Vector2 position)
    {
        Vector2 currentPos = position;
        currentPos = new Vector2(Mathf.Round(currentPos.x),
                             Mathf.Round(currentPos.y));
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            if ((Vector2)block.transform.position == currentPos)
            {
                return false;
            }
        }
        if (!Camera.main.gameObject.GetComponent<Controller>().onMap(currentPos))
        {
            return false;
        }
        spawnPosition = currentPos;
        placeholder.SetActive(true);
        placeholder.transform.position = spawnPosition;
        return true;
    }
}
