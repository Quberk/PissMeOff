using UnityEngine;

public class WestafelObjectInteraction : BaseObjectInteraction
{
    [SerializeField] private PanelCancelAction panelCancelAction;

    private Animator myAnim;

    private Camera mainCamera;

    [SerializeField] private GameObject body;

    [Header("Keran")]
    [SerializeField] private GameObject keran, peganganKeran, keranInHome;
    [SerializeField] private ParticleSystem waterFx;
    [SerializeField] private float keranOnRot, keranOffRot;
    [SerializeField] private Vector3 keranOnPos, keranOffPos;
    private bool keranActive = false;

    [Header("Cermin")]
    [SerializeField] private GameObject cermin;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        waterFx.Stop();
    }

    public override void ObjectStart()
    {
        myAnim.Play("Idle", -1, 0);

        if (keranActive == false)
        {
            waterFx.Stop();
            peganganKeran.transform.localRotation = Quaternion.Euler(0f, keranOffRot, 0f);
            peganganKeran.transform.localPosition = keranOffPos;
        }

        else
        {
            waterFx.Play();
            peganganKeran.transform.localRotation = Quaternion.Euler(0f, keranOnRot, 0f);
            peganganKeran.transform.localPosition = keranOnPos;
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keranInHome.SetActive(keranActive);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                #region Keran Check

                if (hit.transform.gameObject == keran)
                {
                    if (keranActive)
                    {
                        peganganKeran.transform.localRotation = Quaternion.Euler(0f, keranOffRot, 0f);
                        peganganKeran.transform.localPosition = keranOffPos;

                        waterFx.Stop();
                        keranActive = false;
                    }

                    else
                    {
                        peganganKeran.transform.localRotation = Quaternion.Euler(0f, keranOnRot, 0f);
                        peganganKeran.transform.localPosition = keranOnPos;

                        waterFx.Play();
                        keranActive = true;
                    }

                    return;
                }

                #endregion

                #region Cermin Check

                if (hit.transform.gameObject == cermin)
                {
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

    public void SetTheWestafelStatus(bool status)
    {
        keranActive = status;
    }

    public bool GetTheWestafelStatus()
    {
        return keranActive;
    }
}
