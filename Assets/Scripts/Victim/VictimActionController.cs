using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimActionController : MonoBehaviour
{
    [SerializeField] private VictimAngerController victimAngerController;
    [SerializeField] private Animator myAnim;
    [SerializeField] private GameObject walkingSpacePos;
    [SerializeField] private GameObject parentObject;

    [Header("Watching TV")]
    [SerializeField] private GameObject[] watchingTvObject;
    [SerializeField] private TvObjectInteraction tvObjectInteraction;
    [SerializeField] private int mostLikedChannelNum;
    [SerializeField] private int mostHatedChannelNum;

    [Header("Weker")]
    [SerializeField] private Animator wekerAnim;
    [SerializeField] private WekerObjectInteraction wekerObjectInteraction;
    [SerializeField] private GameObject wekerSoundFx;

    [Header("Lights")]
    private GameObject theLight;

    [Header("Westafel")]
    [SerializeField] private GameObject waterFx;
    [SerializeField] private WestafelObjectInteraction westafelObjectInteraction;

    [Header("Makanan")]
    [SerializeField] private MejaMakanInteraction mejaMakanInteraction;
    [SerializeField] private string mostLikedFlavor;
    [SerializeField] private string mostHatedFlavor;

    [Header("Toilet")]
    [SerializeField] private ToiletObjectInteraction toiletObjectInteraction;

    [Header("Emoticon")]
    [SerializeField] private ParticleSystem happyFx, angryFx, veryAngryFx, loudFx;

    private void Awake()
    {
        VictimAIController.OnAiStateChanged += AiControllerStateChanged;


    }


    // Start is called before the first frame update
    void Start()
    {
        angryFx.Stop();
        veryAngryFx.Stop();
        loudFx.Stop();
        happyFx.Stop();
    }

    private void OnDestroy()
    {
        VictimAIController.OnAiStateChanged -= AiControllerStateChanged;
    }

    private void AiControllerStateChanged(AiState state)
    {

    }

    public void StartAnAction(string animationToPlay)
    {
        VictimAIController.Instance.ArrivedAtDestination();
        myAnim.Play(animationToPlay, -1, 0f);
    }


    public void FinishAnAction(AiState lastState)
    {
        parentObject.transform.position = new Vector3(parentObject.transform.position.x, parentObject.transform.position.y, walkingSpacePos.transform.position.z);
        VictimAIController.Instance.ChangingState(lastState);
    }

    public void PauseActionAnimation()
    {
        myAnim.SetFloat("animationSpeed", 0f);
    }

    public void ResumeActionAnimation()
    {
        myAnim.SetFloat("animationSpeed", 1f);
    }

    public void ChangingAction(AiState state)
    {
        parentObject.transform.position = new Vector3(parentObject.transform.position.x, parentObject.transform.position.y, walkingSpacePos.transform.position.z);
        myAnim.Play("Idle", -1, 0f);
        VictimAIController.Instance.UpdateAIState(state);
    }

    public void SetTheLightToTurnOff(GameObject myLight)
    {
        theLight = myLight;
    }

    public void ChangingTheEnvironmentState(AiState state)
    {

        //Tv
        if (state == AiState.WatchingTV)
        {
            for (int i = 0; i < watchingTvObject.Length; i++)
            {
                watchingTvObject[i].SetActive(true);
            }

            if (tvObjectInteraction.GetTheChannelNum() == mostHatedChannelNum)
            {
                angryFx.Play();
                victimAngerController.SetTheAngerLevel(20f);
            }

            if (tvObjectInteraction.GetTheChannelNum() == mostLikedChannelNum)
            {
                happyFx.Play();
                victimAngerController.SetTheAngerLevel(-10f);
            }

            return;
        }

        //Weker
        if (state == AiState.TurningOffAlarm)
        {
            wekerAnim.Play("Stop", -1, 0f);
            wekerSoundFx.SetActive(false);
            wekerObjectInteraction.TurningOffWeker();
            angryFx.Play();
            victimAngerController.SetTheAngerLevel(20f);

            return;
        }

        //Lampu
        if (state == AiState.TurningOnLight)
        {
            theLight.SetActive(true);
            angryFx.Play();
            victimAngerController.SetTheAngerLevel(20f);

            return;
        }

        //Westafel
        if (state == AiState.WashTeeth)
        {
            if (westafelObjectInteraction.GetTheWestafelStatus() == true)
            {
                angryFx.Play();
                victimAngerController.SetTheAngerLevel(20f);
            }

            waterFx.SetActive(false);
            westafelObjectInteraction.SetTheWestafelStatus(false);

            return;
        }

        //Eating
        if (state == AiState.Eating)
        {
            if (mejaMakanInteraction.GetTheFlavor() == mostLikedFlavor)
            {
                happyFx.Play();
                victimAngerController.SetTheAngerLevel(-10f);
            }

            if (mejaMakanInteraction.GetTheFlavor() == mostHatedFlavor)
            {
                angryFx.Play();
                victimAngerController.SetTheAngerLevel(20f);
            }

            return;
        }

        //Peeing
        if (state == AiState.Peeing)
        {
            Debug.Log(toiletObjectInteraction.GetTheItemState());

            if (toiletObjectInteraction.GetTheItemState() == true)
            {
                Debug.Log("Rubik berada dalam TOilet");
                angryFx.Play();
                victimAngerController.SetTheAngerLevel(20f);

            }

            return;
        }

    }

    public void DeactivatingObjectInTheAction(AiState state)
    {
        if (state == AiState.WatchingTV)
        {
            for (int i = 0; i < watchingTvObject.Length; i++)
            {
                watchingTvObject[i].SetActive(false);
            }

            tvObjectInteraction.SetTheTVStatus(false);
            tvObjectInteraction.SetTheTvChannelNum(mostLikedChannelNum);

            return;
        }

        if (state == AiState.Eating)
        {
            mejaMakanInteraction.SetTheFlavor("");

            return;
        }

        if (state == AiState.Peeing)
        {
            toiletObjectInteraction.SetTheItemState(false);

            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
