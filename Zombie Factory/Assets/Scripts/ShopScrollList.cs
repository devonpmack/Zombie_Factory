using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ShopElement
{
    public string itemName;
    public Sprite icon;
    public float price = 1f;
    public GameObject prefab;
}

public class ShopScrollList : MonoBehaviour {
    public GameObject shopCanvas;
    public Transform contentPanel;
    public GameObject buttonPrefab;
    public GameObject confirmer;
    public List<ShopElement> itemList;
    public Text brainDisplay;
    public Text brainDisplay2;
    public float brains = 20f;
    public Image toPlace;
    public Controller controller;

    // Use this for initialization
    void Start() {
        RefreshDisplay();
        RefreshBrains();
    }

    private void RefreshDisplay()
    {
        AddButtons();
    }

    public void RefreshBrains()
    {
        brainDisplay.text = brains.ToString();
        brainDisplay2.text = brains.ToString();
    }

    private void AddButtons() {
        for (int i = 0; i < itemList.Count; i++)
        {
            ShopElement item = itemList[i];
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(contentPanel, false);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this);
        }
    }
    public void buyItem(ShopElement item)
    {
        if (brains >= item.price)
        {
            Debug.Log("Buying " + item.itemName);
            placeItem(item);
            
        }
    }
    private void placeItem(ShopElement item)
    {
        controller.MenuOpen = false;
        shopCanvas.SetActive(false);
        toPlace.sprite = item.icon;
        Color color = toPlace.color;
        color.a = 0.5f;
        toPlace.color = color;
        toPlace.gameObject.SetActive(true);
        confirmer.SetActive(true);
        confirmer.GetComponent<ConfirmPurchase>().confirm(item);
    }
    public void finalizePurchase(ShopElement item, bool success)
    {
        if (success)
        {
            brains -= item.price;
            RefreshBrains();
        }
        if (!Camera.main.gameObject.GetComponent<Controller>().paused)
        {
            Time.timeScale = 1;
        }
        toPlace.gameObject.SetActive(false);
    }
}
