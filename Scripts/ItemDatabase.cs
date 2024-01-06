using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public GameObject itemGO;
    public List<string> items = new List<string>();
    public bool loaded = false;
    private TaskCompletionSource<bool> loadingTask = new TaskCompletionSource<bool>();

    async void Start()
    {
        itemGO = Resources.Load<GameObject>("Prefabs/Item");
        await LoadFileNamesAsync("Assets/Resources/Items");
        loaded = true;
        loadingTask.SetResult(true);
    }

    async Task LoadFileNamesAsync(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);

            foreach (string filePath in files)
            {
                if (!filePath.ToLower().EndsWith("meta"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePath);

                    // ≤гноруЇмо файли, що зак≥нчуютьс€ на "meta"
                    items.Add(fileName);
                }
            }
        }
        else
        {
            Debug.LogError("Folder does not exist: " + folderPath);
        }

        await Task.Delay(0); // ≤м≥тац≥€ асинхронного завданн€
    }

    public async Task<Item> GetItem(string itemName)
    {
        await loadingTask.Task; // ќч≥куванн€ завершенн€ завантаженн€

        // ќтримуЇмо тип за допомогою ≥мен≥ класу
        Type itemType = Type.GetType(itemName);

        if (itemType != null && typeof(Item).IsAssignableFrom(itemType))
        {
            // якщо тип усп≥шно знайдений ≥ Ї п≥дтипом Item
            GameObject newItem = Instantiate(itemGO);
            newItem.AddComponent(itemType);
            Item item = newItem.GetComponent<Item>();
            item.Initialize();
            return item;
        }

        return null; // якщо щось п≥шло не так
    }
}
