using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Player player;
    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;

    public bool active;
    public int selected;
    const float timeDelay = 0.2f;
    float delay;


    List<RectTransform> inventoryList;
    public Color selectedBackground, selectedBorder, originalBackground, originalBorder;

    private List<string> itemDescriptions;
    [SerializeField] private TextMeshProUGUI description;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        selectedBackground = new Color(0.8f, 0.8f, 0.9f);
        selectedBorder = new Color(0.6f, 0.6f, 0.7f);
        originalBackground = itemSlotTemplate.Find("Background").GetComponent<Image>().color;
        originalBorder = itemSlotTemplate.Find("Border").GetComponent<Image>().color;

    }
    private void Update()
    {
        if (active && inventory.GetItemList().Count > 0)
        {
            if (delay <= 0)
            {
                float h = Input.GetAxis("Horizontal");
                if (h != 0 && inventory.GetItemList().Count > 1)
                {
                    Navigate(h);
                    delay = timeDelay;
                }
            }
            else
            {
                delay -= Time.deltaTime;
            }
            if (Input.GetButtonDown("Z"))
            {
                switch (inventory.GetItemList()[selected].itemName)
                {
                    case Item.ItemName.Key:
                        player.Key(inventory.GetItemList()[selected]);
                        break;
                    case Item.ItemName.Crowbar:
                        player.Crowbar(inventory.GetItemList()[selected]);
                        break;
                    case Item.ItemName.Brush:
                        player.Brush(inventory.GetItemList()[selected]);
                        break;
                    case Item.ItemName.Photo:
                        player.Photo(inventory.GetItemList()[selected], inventory.GetItemList()[selected].amount);
                        break;
                    case Item.ItemName.Lighter:
                        player.Lighter(inventory.GetItemList()[selected]);
                        break;
                }
            }
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventory();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventory();
    }

    private void RefreshInventory()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        float itemSlotCellSize = 80f;
        inventoryList = new List<RectTransform>();
        itemDescriptions = new List<string>();

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0);
            Image image = itemSlotRectTransform.Find("itemIcon").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI textAmount = itemSlotRectTransform.Find("textAmount").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                textAmount.SetText(item.amount.ToString());
            }

            else
            {
                textAmount.SetText("");
            }


            itemDescriptions.Add(item.GetItemDescription());
            inventoryList.Add(itemSlotRectTransform);
            x++;
        }

    }

    public void Navigate(float h)
    {
        inventoryList[selected].Find("Background").GetComponent<Image>().color = originalBackground;
        inventoryList[selected].Find("Border").GetComponent<Image>().color = originalBorder;

        if (h > 0 && selected < inventoryList.Count - 1)
        {
            selected++;
        }
        if (h < 0 && selected > 0)
        {
            selected--;
        }

        inventoryList[selected].Find("Background").GetComponent<Image>().color = selectedBackground;
        inventoryList[selected].Find("Border").GetComponent<Image>().color = selectedBorder;

        description.SetText(itemDescriptions[selected]);



    }

    public void UpdateActiveItem(int item)
    {
        foreach (RectTransform icon in inventoryList)
        {
            if (inventoryList.IndexOf(icon) == item)
            {
                icon.Find("Background").GetComponent<Image>().color = selectedBackground;
                icon.Find("Border").GetComponent<Image>().color = selectedBorder;
            }
            else
            {
                icon.Find("Background").GetComponent<Image>().color = originalBackground;
                icon.Find("Border").GetComponent<Image>().color = originalBorder;
            }
        }
        description.SetText(itemDescriptions[item]);

    }

    public void CloseInventory()
    {
        foreach (RectTransform icon in inventoryList)
        {
            icon.Find("Background").GetComponent<Image>().color = originalBackground;
            icon.Find("Border").GetComponent<Image>().color = originalBorder;
        }
        description.SetText("");

    }
}
