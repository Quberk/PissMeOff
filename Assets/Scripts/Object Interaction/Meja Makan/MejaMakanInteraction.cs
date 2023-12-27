using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejaMakanInteraction : BaseObjectInteraction
{
    [SerializeField] private PanelCancelAction panelCancelAction;

    private Camera mainCamera;

    [SerializeField] private GameObject body;

    private Animator myAnim;

    [Header("Food")]
    [SerializeField] private GameObject food;
    [SerializeField] private GameObject spiceUi;
    [SerializeField] private GameObject saltUi;
    [SerializeField] private GameObject spiceFx;
    [SerializeField] private GameObject saltFx;
    private string flavor;


    private ItemInventory itemInventory;

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

        if (flavor == "Spicy")
        {
            spiceFx.SetActive(true);
            saltFx.SetActive(false);
        }

        else if (flavor == "Salty")
        {
            saltFx.SetActive(true);
            spiceFx.SetActive(false);
        }

        else if (flavor == "")
        {
            saltFx.SetActive(false);
            spiceFx.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
                #region Checking Food Ui

                if (hit.transform.gameObject == spiceUi)
                {
                    spiceUi.SetActive(false);
                    spiceFx.SetActive(true);
                    saltFx.SetActive(false);
                    flavor = "Spicy";

                    return;
                }

                if (hit.transform.gameObject == saltUi)
                {
                    saltUi.SetActive(false);
                    saltFx.SetActive(true);
                    spiceFx.SetActive(false);
                    flavor = "Salty";

                    return;
                }

                #endregion


                #region Checking Food Flavor

                if (hit.transform.gameObject == food)
                {
                    List<string> itemNames = itemInventory.GetAllTheStringItem();

                    foreach(string item in itemNames)
                    {
                        if (item == "Lombok")
                        {
                            spiceUi.SetActive(true);
                        }

                        if (item == "Garam")
                        {
                            saltUi.SetActive(true);
                        }
                    }

                    return;
                }

                #endregion

                if (hit.transform.gameObject == body)
                {
                    SetTheUIInactive();

                    return;
                }

                SetTheUIInactive();

                panelCancelAction.QuitInteractWithObject();
            }
        }
    }

    void SetTheUIInactive()
    {
        spiceUi.SetActive(false);
        saltUi.SetActive(false);
    }

    public void SetTheFlavor(string myFlavor)
    {
        flavor = myFlavor;
    }
    public string GetTheFlavor()
    {
        return flavor;
    }
}
