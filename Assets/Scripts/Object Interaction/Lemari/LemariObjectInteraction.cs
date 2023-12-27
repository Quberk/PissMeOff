using UnityEngine;

public class LemariObjectInteraction : BaseObjectInteraction
{
    [SerializeField] private PanelCancelAction panelCancelAction;

    private Camera mainCamera;

    [SerializeField] private GameObject body;

    private Animator myAnim;

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
                if (hit.transform.gameObject == body)
                {
                    return;
                }

                panelCancelAction.QuitInteractWithObject();
            }
        }
    }

}
