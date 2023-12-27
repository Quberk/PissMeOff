using UnityEngine;

public class KulkasObjectInteraction : BaseObjectInteraction
{
    [SerializeField] private PanelCancelAction panelCancelAction;

    private Camera mainCamera;

    [SerializeField] private GameObject body;

    private Animator myAnim;

    [Header("Kaleng makanan")]
    [SerializeField] private GameObject lombok;
    [SerializeField] private GameObject garam;

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
                #region Kaleng makanan Check

                if (hit.transform.gameObject == lombok)
                {
                    lombok.GetComponent<ItemObjectPick>().GetPickedUp();
                    lombok.SetActive(false);

                    return;
                }

                if (hit.transform.gameObject == garam)
                {
                    garam.GetComponent<ItemObjectPick>().GetPickedUp();
                    garam.SetActive(false);

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

}
