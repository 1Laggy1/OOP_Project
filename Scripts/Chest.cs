using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Chest : MonoBehaviour
{
    //[SerializeField]
    GameObject itemPref;
    //UIELEMENT ui;
    //public List<List<Item>> items = new List<List<Item>>();
    public List<InventoryItem> invitem = new List<InventoryItem>();
    public List<GameObject> invGO = new List<GameObject>();
    public Transform this_tr;
    public Transform Content_tr;
    public Chest()
    {
    }
    public void Show(Transform Content)
    {
        Content_tr = Content;
        float childCount = this_tr.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = this_tr.GetChild(0);
            child.GetComponent<Transform>().SetParent(Content_tr);
            child.gameObject.SetActive(true);
        }
        //foreach (InventoryItem invItemf in invitem)
        //{
        //    GameObject invitemO = Instantiate(itemPref, Content_tr);
        //    InventoryItem II = invitemO.GetComponent<InventoryItem>();
        //    II.Initialize(invItemf.items[0]);
        //    foreach (Item item in invItemf.items)
        //    {
        //        invItemf.Add(item);
        //    }
        //    //invitem.Add(II);
        //    //invGO.Add(invitemO);
        //}
        //foreach (List<Item> item in items)
        //{
        //    GameObject invitemO = Instantiate(itemPref, Content_tr);
        //    invitemO.GetComponent<InventoryItem>().Initialize(item.name, item);
        //    invitem.Add(invitemO.GetComponent<InventoryItem>());
        //    invGO.Add(invitemO);

        //}
    }
    public void Close(Transform Content)
    {
        for (int i = 0; i < Content.childCount; i++)
        {
            Content.GetChild(i).SetParent(this_tr);
        }
        Content_tr = null;
    }
    public void Remove(InventoryItem II)
    {
        //InventoryItem foundInvItem = invitem.FirstOrDefault(itemf => itemf.Name == II.Name);
        invitem.Remove(II);
    }
    // Start is called before the first frame update
    async void Start()
    {
        this_tr = GetComponent<Transform>();
        itemPref = Resources.Load<GameObject>("Prefabs/InvItem");
        Item item = await GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>().GetItem("Cat");
        Item[] items = { item };
        Put(items);
        Item item2 = await GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>().GetItem("HealPotion");
        Item[] items2 = { item2 };
        Put(items2);
    }
    //void Awake()
    //{
    //    itemPref = Resources.Load<GameObject>("Prefabs/InvItem");
    //}
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Put(Item[] items)
    {
        foreach (Item item in items)
        {
            InventoryItem foundInvItem = invitem.FirstOrDefault(itemf => itemf.Name == item.Name);

            if (foundInvItem != null)
            {
                foundInvItem.Add(item);
                //Item foundItem = items.FirstOrDefault(itemf => itemf.FirstOrDefault(itemff => itemff.Name == item.Name));
            }
            else
            {
                GameObject invitemO;
                if (Content_tr == null)
                {
                    invitemO = Instantiate(itemPref, this_tr);
                    invitemO.gameObject.SetActive(false);
                }
                else
                {
                    invitemO = Instantiate(itemPref, Content_tr);
                    invitemO.gameObject.SetActive(true);
                }
                invitemO.GetComponent<InventoryItem>().Initialize(item);
                invitem.Add(invitemO.GetComponent<InventoryItem>());
                invitemO.GetComponent<InventoryItem>().removedEvent += Removed;


                //items.Add(item);

                //invGO.Add(invitemO);
            }
            if (item.gameObject.activeInHierarchy)
                item.gameObject.SetActive(false);
        }
    }
    public void Removed(InventoryItem II)
    {
        invitem.Remove(II);
    }
}
