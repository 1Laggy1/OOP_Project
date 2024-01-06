using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string Name;
    public string description;
    public SpriteRenderer icon;
    public bool Stackable;
    //public InventoryItem invItem;
    // Start is called before the first frame update
    public virtual void Start()
    {
        icon = GetComponent<SpriteRenderer>();
        icon.sprite = Resources.Load<Sprite>("Sprites/"+Name);
    }
    public virtual void Initialize()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PickUp()
    {
        Destroy(gameObject);
    }

    public virtual void Use()
    {
        Destroy(gameObject);
    }
    public virtual object Clone()
    {
        return MemberwiseClone();
    }
}
