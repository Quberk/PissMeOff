using UnityEngine;

public class TvObjectInteraction : BaseObjectInteraction
{
    [SerializeField] private PanelCancelAction panelCancelAction;

    private Camera mainCamera;

    [SerializeField] private GameObject body;


    [Header("Channels")]
    [SerializeField] private GameObject channelsPanel;
    [SerializeField] private float channel1Pos, channel2Pos, channel3Pos;
    private int channelNum;

    [Header("Screen")]
    [SerializeField] private GameObject tvScreenInHome;
    [SerializeField] private GameObject tvScreen;
    [SerializeField] private GameObject soundFx;
    private bool tvOn = false;

    [Header("Buttons")]
    [SerializeField] private GameObject powerBtn;
    [SerializeField] private GameObject channel1Btn, channel2Btn, channel3Btn;

    private Animator myAnim;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myAnim = GetComponent<Animator>();

        channelNum = 3;
    }

    public override void ObjectStart()
    {
        tvScreen.SetActive(tvScreenInHome.activeInHierarchy);
        soundFx.SetActive(tvScreenInHome.activeInHierarchy);
        myAnim.Play("Idle", -1, 0);

        if (channelNum == 1)
        {
            channelsPanel.transform.localPosition = new Vector3(channel1Pos,
                            channelsPanel.transform.localPosition.y,
                            channelsPanel.transform.localPosition.z);

            return;
        }

        if (channelNum == 2)
        {
            channelsPanel.transform.localPosition = new Vector3(channel2Pos,
                                        channelsPanel.transform.localPosition.y,
                                        channelsPanel.transform.localPosition.z);

            return;
        }

        if (channelNum == 3)
        {
            channelsPanel.transform.localPosition = new Vector3(channel3Pos,
                                        channelsPanel.transform.localPosition.y,
                                        channelsPanel.transform.localPosition.z);

            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tvScreen.SetActive(false);
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
                Debug.Log(hit.transform.gameObject.name);

                #region Button Check

                //If Power Btn
                if (hit.transform.gameObject == powerBtn)
                {
                    Debug.Log("Tombol Power");

                    if (tvOn)
                    {
                        soundFx.SetActive(false);
                        tvOn = false;
                        tvScreen.SetActive(false);
                        tvScreenInHome.SetActive(false);
                    }
                        
                    else
                    {
                        soundFx.SetActive(true);
                        tvOn = true;
                        tvScreen.SetActive(true);
                        tvScreenInHome.SetActive(true);
                    }

                    return;
                }

                //If Ch1 Btn
                if (hit.transform.gameObject == channel1Btn)
                {
                    Debug.Log("CH1");

                    channelNum = 1;
                    channelsPanel.transform.localPosition = new Vector3(channel1Pos,
                                                channelsPanel.transform.localPosition.y,
                                                channelsPanel.transform.localPosition.z);
                    return;
                }

                //If Ch2 Btn
                if (hit.transform.gameObject == channel2Btn)
                {
                    Debug.Log("CH2");

                    channelNum = 2;
                    channelsPanel.transform.localPosition = new Vector3(channel2Pos,
                                                channelsPanel.transform.localPosition.y,
                                                channelsPanel.transform.localPosition.z);
                    return;
                }

                //If Ch3 Btn
                if (hit.transform.gameObject == channel3Btn)
                {
                    Debug.Log("CH3");

                    channelNum = 3;
                    channelsPanel.transform.localPosition = new Vector3(channel3Pos,
                                                channelsPanel.transform.localPosition.y,
                                                channelsPanel.transform.localPosition.z);

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

    public void SetTheTVStatus(bool status)
    {
        tvOn = status;
    }

    public bool GetTheTvStatus()
    {
        return tvOn;
    }

    public void SetTheTvChannelNum(int num)
    {
        channelNum = num;

        if (channelNum == 1)
        {
            channelsPanel.transform.localPosition = new Vector3(channel1Pos,
                            channelsPanel.transform.localPosition.y,
                            channelsPanel.transform.localPosition.z);

            return;
        }

        if (channelNum == 2)
        {
            channelsPanel.transform.localPosition = new Vector3(channel2Pos,
                                        channelsPanel.transform.localPosition.y,
                                        channelsPanel.transform.localPosition.z);

            return;
        }

        if (channelNum == 3)
        {
            channelsPanel.transform.localPosition = new Vector3(channel3Pos,
                                        channelsPanel.transform.localPosition.y,
                                        channelsPanel.transform.localPosition.z);

            return;
        }
    }

    public int GetTheChannelNum()
    {
        return channelNum;
    }

}
