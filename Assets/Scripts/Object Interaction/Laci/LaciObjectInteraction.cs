using UnityEngine;

public class LaciObjectInteraction : BaseObjectInteraction
{
    [SerializeField] private PanelCancelAction panelCancelAction;

    private Camera mainCamera;

    [SerializeField] private GameObject body;

    [Header("Laci Box")]
    [SerializeField] private GameObject[] laciBox;

    private Animator myAnim;

    [SerializeField] private GameObject myItem;
    [SerializeField] private LaciBox itemLaci;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myAnim = GetComponent<Animator>();
    }

    public override void ObjectStart()
    {
        myAnim.Play("Idle", -1, 0);
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
                #region Item Checked

                if ((itemLaci.GetStatus() == true) && (hit.transform.gameObject == myItem))
                {
                    myItem.GetComponent<ItemObjectPick>().GetPickedUp();
                    myItem.SetActive(false);

                    return;
                }

                #endregion

                #region Checking Box Being Open or Not

                for (int i = 0; i < laciBox.Length; i++)
                {
                    if (hit.transform.gameObject == laciBox[i])
                    {
                        laciBox[i].GetComponent<LaciBox>().GetInteract();

                        return;
                    }
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

}
