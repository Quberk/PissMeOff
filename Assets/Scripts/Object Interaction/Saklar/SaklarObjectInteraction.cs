using UnityEngine;

public class SaklarObjectInteraction : BaseObjectInteraction
{
    [SerializeField] private PanelCancelAction panelCancelAction;

    private Camera mainCamera;

    [SerializeField] private GameObject body;

    [SerializeField] private GameObject saklar;
    [SerializeField] private float saklarRotLightOn;
    [SerializeField] private float saklarRotLightOff;
    [SerializeField] private GameObject lampu;
    private bool lightsOn = true;

    private Animator myAnim;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myAnim = GetComponent<Animator>();
    }

    public override void ObjectStart()
    {
        myAnim.Play("Idle", -1, 0);

        lightsOn = lampu.activeInHierarchy;

        if (lightsOn == false)
        {
            saklar.transform.localRotation = Quaternion.Euler(0f, 0f, saklarRotLightOff);
        }

        else
        {
            saklar.transform.localRotation = Quaternion.Euler(0f, 0f, saklarRotLightOn);
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

                #region Lights Checking

                if (hit.transform.gameObject == saklar)
                {
                    if (lightsOn)
                    {
                        lightsOn = false;
                        lampu.SetActive(false);

                        saklar.transform.localRotation = Quaternion.Euler(0f,0f,saklarRotLightOff);
                    }

                    else
                    {
                        lightsOn = true;
                        lampu.SetActive(true);

                        saklar.transform.localRotation = Quaternion.Euler(0f, 0f, saklarRotLightOn);
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


}
