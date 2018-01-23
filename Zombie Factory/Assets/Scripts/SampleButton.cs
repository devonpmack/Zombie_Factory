using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour {
    public Button button;
    public Text nameLabel;
    public Text priceLabel;
    public Image iconImage;
    public GameObject itemPrefab;
    private ShopElement item;
    private ShopScrollList scrolllist;

	// Use this for initialization
	void Start () {
        button.onClick.AddListener(handleClick);
	}
    void handleClick()
    {
        scrolllist.buyItem(item);
    }
    public void Setup(ShopElement currentItem, ShopScrollList currentScrollList)
    {
        item = currentItem;
        nameLabel.text = item.itemName;
        priceLabel.text = item.price.ToString();
        item.icon = item.prefab.GetComponent<SpriteRenderer>().sprite;
        iconImage.sprite = item.icon;
        itemPrefab = item.prefab;

        scrolllist = currentScrollList;

    }

}
