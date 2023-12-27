using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] slotUi;
    private List<GameObject> pickedUpItem = new List<GameObject>();
    private List<string> pickedUpItemName = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddiPickedUpItem(GameObject myItem, string name)
    {
        pickedUpItem.Add(myItem);
        pickedUpItemName.Add(name);

        int theNum = pickedUpItem.Count - 1;

        pickedUpItem[theNum].transform.SetParent(null);
        pickedUpItem[theNum].transform.SetParent(slotUi[theNum].transform);
        pickedUpItem[theNum].GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    public List<string> GetAllTheStringItem()
    {
        return pickedUpItemName;
    }
}
