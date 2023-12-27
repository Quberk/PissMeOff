using UnityEngine;

public class ItemObjectPick : MonoBehaviour
{
    [SerializeField] private GameObject itemUi;
    [SerializeField] private string itemName;

    private ItemInventory itemInventory;

    // Start is called before the first frame update
    void Start()
    {
        itemInventory = FindObjectOfType<ItemInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPickedUp()
    {
        itemInventory.AddiPickedUpItem(itemUi, itemName);
    }
}
