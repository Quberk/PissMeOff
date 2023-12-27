using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletObjectInteraction : BaseObjectInteraction
{
    [SerializeField] private PanelCancelAction panelCancelAction;

    private Camera mainCamera;

    [SerializeField] private GameObject body;

    private Animator myAnim;

    private ItemInventory itemInventory;

    [Header("Toilet Water")]
    [SerializeField] private GameObject toiletWater;
    [SerializeField] private string objectName;
    [SerializeField] private GameObject objectToThrowUi;
    [SerializeField] private GameObject objectToThrow;
    private bool objectToThrowStatus;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        itemInventory = FindObjectOfType<ItemInventory>();
        myAnim = GetComponent<Animator>();
    }

    public override void ObjectStart()
    {
        myAnim.Play("Idle", -1, 0);
        SetTheUIInactive();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTheObjectInactive();
        SetTheUIInactive();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                #region Throwing UI Check

                if (hit.transform.gameObject == objectToThrowUi)
                {
                    SetTheUIInactive();
                    objectToThrow.SetActive(true);
                    objectToThrowStatus = true;

                    return;
                }

                #endregion

                #region Object To Throw Check

                if (hit.transform.gameObject == toiletWater)
                {
                    Debug.Log("Air toilet tertekan");

                    List<string> itemNames = itemInventory.GetAllTheStringItem();

                    foreach (string item in itemNames)
                    {
                        if (item == objectName && !objectToThrow.activeInHierarchy)
                        {
                            objectToThrowUi.SetActive(true);
                        }
                    }
                    return;
                }

                #endregion

                if (hit.transform.gameObject == body)
                {
                    return;
                }

                panelCancelAction.QuitInteractWithObject();
            }
        }
    }

    void SetTheObjectInactive()
    {
        objectToThrow.SetActive(false);
        objectToThrowStatus = false;
    }

    void SetTheUIInactive()
    {
        objectToThrowUi.SetActive(false);
    }

    public void SetTheItemState(bool myStete)
    {
        objectToThrow.SetActive(myStete);
        objectToThrowStatus = myStete;
    }

    public bool GetTheItemState()
    {
        return objectToThrowStatus;
    }
}
