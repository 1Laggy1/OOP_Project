using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    Chest playerChest;
    Chest openedChest;
    [SerializeField]
    RectTransform ContentLeft_tr;
    [SerializeField]
    RectTransform ContentRight_tr;
    [SerializeField]
    GameObject Canvas_go;

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;

    private Transform dragging = null;
    private Vector3 offset;
    //UIELEMENT anotherChest;
    public void Show()
    {
        if (Canvas_go.activeInHierarchy)
        {
            Canvas_go.SetActive(false);
            playerChest.Close(ContentLeft_tr);
            if (openedChest  != null)
            {
                openedChest.Close(ContentRight_tr);
            }
        }
        else
        {
            Canvas_go.SetActive(true);
            playerChest.Show(ContentLeft_tr);
        }
    }


    public void ShowChest(Chest chest)
    {
        openedChest=chest;
        Canvas_go.SetActive(true);
        playerChest.Show(ContentLeft_tr);
        chest.Show(ContentRight_tr);
    }

    public void PickUp(Item item)
    {
        Item[] items = { item };
        playerChest.Put(items);
        //Destroy(item.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerChest = new Chest();
        //playerChest.Content_tr = ContentLeft_tr;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            m_Raycaster.Raycast(m_PointerEventData, results);
            RaycastResult finalResult = results.FirstOrDefault(result => result.gameObject.tag == "InvItem");
            RaycastResult ButtonResult = results.FirstOrDefault(result => result.gameObject.name == "Button");
            if (finalResult.gameObject != null && ButtonResult.gameObject == null)
            {
                dragging = results[0].gameObject.transform.parent.transform;
                dragging.parent = canvasRect;
                if (playerChest.invitem.Contains(dragging.GetComponent<InventoryItem>()))
                {
                    playerChest.Remove(dragging.GetComponent<InventoryItem>());
                }
                else if (openedChest.invitem.Contains(dragging.GetComponent<InventoryItem>()))
                {
                    openedChest.Remove(dragging.GetComponent<InventoryItem>());
                }
                //offset = dragging.localPosition - Camera.main.ScreenToWorldPoint(position);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (dragging != null)
            {
                Vector2 localPosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out localPosition);

                if (RectTransformUtility.RectangleContainsScreenPoint(ContentLeft_tr, Input.mousePosition, Camera.main))
                {
                    //dragging.SetParent(ContentLeft_tr);
                    //playerChest.invitem.Add(dragging.GetComponent<InventoryItem>());
                    playerChest.Put(dragging.GetComponent<InventoryItem>().items.ToArray());
                    Destroy(dragging.gameObject);
                }
                else if (RectTransformUtility.RectangleContainsScreenPoint(ContentRight_tr, Input.mousePosition, Camera.main))
                {
                    if (openedChest != null)
                    {
                        openedChest.Put(dragging.GetComponent<InventoryItem>().items.ToArray());
                    }
                    else
                    {
                        playerChest.Put(dragging.GetComponent<InventoryItem>().items.ToArray());
                    }
                    Destroy(dragging.gameObject);
                    Debug.Log("Right");
                }
                else
                {
                    //dragging.SetParent(ContentLeft_tr);
                    //playerChest.invitem.Add(dragging.GetComponent<InventoryItem>());
                    playerChest.Put(dragging.GetComponent<InventoryItem>().items.ToArray());
                    Destroy(dragging.gameObject);
                    Debug.Log("None");
                }

                dragging = null;


            }
        }

        if (dragging != null)
        {
            Vector2 position = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, position, Camera.main, out position);
            dragging.position = canvasRect.TransformPoint(position);
        }



    }
}
