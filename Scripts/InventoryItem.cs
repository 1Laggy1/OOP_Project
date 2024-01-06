using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class InventoryItem : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Image imageO;
    public string Name;
    public TMP_Text nameO;
    public int count;
    public TMP_Text count_txt;
    private float clickTime;
    private bool clickedOnce;
    //bool dragging;
    public event RemovedEvent removedEvent;
    public delegate void RemovedEvent(InventoryItem II);
    //Vector3 offset;
    RectTransform this_tr;
    public void Initialize(Item itemof)
    {
        Name = itemof.Name;
        if (imageO!=null)
        {
            imageO.sprite = Resources.Load<Sprite>("Sprites/" + itemof.Name);
            nameO.text = itemof.Name;
        }
        Add(itemof);
    }
    //public void PointerDown()
    //{
    //    if (!clickedOnce)
    //    {
    //        // Перший клік
    //        //dragging = true;
    //        clickedOnce = true;
    //        clickTime = Time.time;
    //    }
    //    else
    //    {
    //        // Другий клік
    //        clickedOnce = false;

    //        if (Time.time - clickTime < 0.5f) // Перевірка, чи пройшло менше 0.5 секунд між кліками
    //        {
    //            Debug.Log("Подвійний клік!");
    //            Use();
    //            //Destroy(this.gameObject);
    //        }
    //    }
    //}
    public void PointerUp()
    {
        //dragging = false;
        //DraggedEvent(this);
    }
    public void Use()
    {
        items[items.Count-1].Use();
        items.Remove(items[items.Count-1]);
        CountChanged();
    }
    public void Add(Item item)
    {
        items.Add(item);
        CountChanged();
    }
    public void CountChanged()
    {
        if (items.Count > 0)
        {
            count = items.Count;
            if (imageO != null)
            count_txt.text = items.Count.ToString();
        }
        else
        {
            removedEvent(this);
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this_tr = GetComponent<RectTransform>();
        this_tr.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //if (dragging)
        //{
        //    this_tr.parent = null;
        //    offset = this_tr.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    this_tr.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

        //}
    }
}
