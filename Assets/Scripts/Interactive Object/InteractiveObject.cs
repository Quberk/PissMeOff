using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private AiState stateAction;
    VictimActionController victimActionController;
    private VictimRoomController victimRoomController;

    private GameObject mainCamera;

    [Header("The material")]
    [SerializeField] private GameObject[] materialAssign;
    [SerializeField] private string myRoomName;

    [Header("Object Interactive")]
    [SerializeField] private GameObject objectInteraction;
    [SerializeField] private Vector3 objectPos;
    [SerializeField] private Vector3 objectRotation;
    private GameObject objectInteractionPanel;

    private BoxCollider myColl;

    private void Awake()
    {
        objectInteractionPanel = GameObject.FindGameObjectWithTag("ObjectInteractionPanel");
        victimRoomController = FindObjectOfType<VictimRoomController>();
        myColl = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        victimActionController = FindObjectOfType<VictimActionController>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        objectInteraction.SetActive(false);
        objectInteractionPanel.SetActive(false);

        ChangeTheHighlightMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangingState()
    {
        victimActionController.ChangingAction(stateAction);
    }

    public void InteractingWithObject()
    {
        //Jika berada di ruangan yg sama dengan Victim maka tdk dapat di interaksikan
        if (victimRoomController.GetTheRoomName() == myRoomName)
            return;

        myColl.enabled = false;
        Debug.Log("Saya terpanggil kok");

        objectInteraction.SetActive(true);
        objectInteraction.GetComponent<BaseObjectInteraction>().ObjectStart();

        //objectInteraction.transform.localPosition = new Vector3(0f, objectPos.y, objectPos.z);
        //objectInteraction.transform.localRotation = Quaternion.Euler(objectRotation);

        objectInteractionPanel.SetActive(true);
        objectInteractionPanel.GetComponent<PanelCancelAction>().SetTheActiveInteractiveObject(this);

        //Pause The Game
        VictimAIController.Instance.PauseTheAi();
    }

    public void StopInteractingWithObject()
    {
        myColl.enabled = true;

        objectInteraction.SetActive(false);
        objectInteraction.transform.localPosition = new Vector3(0f, 5000f, 0f);

        objectInteractionPanel.GetComponent<PanelCancelAction>().SetTheActiveInteractiveObject(null);
        objectInteractionPanel.SetActive(false);

        //Resume The Game
        VictimAIController.Instance.ResumeTheAi();
    }

    //Mengganti efek Highlight Material
    public void ChangeTheHighlightMaterial()
    {
        if (victimRoomController.GetTheRoomName() != myRoomName)
        {
            for(int i = 0; i < materialAssign.Length; i++)
            {
                Material myMaterial = materialAssign[i].GetComponent<Renderer>().material;
                myMaterial.SetFloat("Rim_Power", myMaterial.GetFloat("Highlighted_Power"));
            }

        }

        else
        {
            for (int i = 0; i < materialAssign.Length; i++)
            {
                Material myMaterial = materialAssign[i].GetComponent<Renderer>().material;
                myMaterial.SetFloat("Rim_Power", 5000f);
            }
        }
    }
}
