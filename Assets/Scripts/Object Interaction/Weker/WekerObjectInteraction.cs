using UnityEngine;

public class WekerObjectInteraction : BaseObjectInteraction
{
    [SerializeField] private PanelCancelAction panelCancelAction;

    private Camera mainCamera;

    [SerializeField] private GameObject body;

    [Header("Shaking")]
    private Animator myAnim;
    private bool shaking = false;

    [SerializeField] private Animator wekerInHomeAnim;
    [SerializeField] private GameObject wekerSoundFx;

    public override void ObjectStart()
    {
        wekerSoundFx.SetActive(shaking);

        if (shaking)
        {
            myAnim.Play("Shake", -1, 0f);
            
            return;
        }

        myAnim.Play("Stop", -1, 0f);
    }

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myAnim = GetComponent<Animator>();
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
                    if (shaking)
                    {
                        wekerSoundFx.SetActive(false);
                        shaking = false;
                        myAnim.Play("Stop", -1, 0f);
                        wekerInHomeAnim.Play("Stop", -1, 0f);
                        return;
                    }

                    wekerSoundFx.SetActive(true);
                    shaking = true;
                    myAnim.Play("Shake", -1, 0f);
                    wekerInHomeAnim.Play("Shake", -1, 0f);

                    return;
                }

                panelCancelAction.QuitInteractWithObject();
            }
        }
    }

    public void TurningOffWeker()
    {
        shaking = false;
    }

    public bool GetTheShakingInfo()
    {
        return shaking;
    }

}
