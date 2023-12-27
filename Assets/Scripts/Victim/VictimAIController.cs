using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimAIController : MonoBehaviour
{
    private Rigidbody myRb;
    [SerializeField] private Animator myAnim;
    [SerializeField] private float speed;
    private Vector3 target;

    [SerializeField] private VictimActionController victimActionController;
    [SerializeField] private VictimRoomController victimRoomController;

    private string walkingDirection;

    public static VictimAIController Instance;

    public AiState state;

    private bool isWalking;
    private bool lastTimeIWalk = false;

    public static event Action<AiState> OnAiStateChanged;

    [Header("Action Pos")]
    [SerializeField] private GameObject sleepingPos, peeingPos, washTeethPos, eatingPos, watchingPos, turningOffAlarmPos;
    [SerializeField] private GameObject bathRoomLightPos, bedRoomLightPos, kitcherLightPos;

    [Header("Weker")]
    [SerializeField] private WekerObjectInteraction wekerObjectInteraction;

    [Header("TV")]
    [SerializeField] private TvObjectInteraction tvObjectInteraction;

    [Header("Westafel")]
    [SerializeField] private WestafelObjectInteraction westafelObjectInteraction;

    [Header("Emoticon")]
    [SerializeField] private ParticleSystem loudFx;

    private void Awake()
    {
        Instance = this;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();

        //Awalnya Bangun
        UpdateAIState(AiState.WakeUp);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

    }

    public void PauseTheAi()
    {
        if (isWalking)
        {
            isWalking = false;
            lastTimeIWalk = true;
        }
            

        victimActionController.PauseActionAnimation();
    }

    public void ResumeTheAi()
    {
        if (lastTimeIWalk)
        {
            isWalking = true;
            lastTimeIWalk = false;
        }
        
        victimActionController.ResumeActionAnimation();
    }

    public void UpdateAIState(AiState newState)
    {
        state = newState;

        switch (newState)
        {
            case AiState.WakeUp:
                
                break;
            case AiState.Sleeping:
                SetWalkTarget(sleepingPos.transform.position);
                break;
            case AiState.Peeing:
                SetWalkTarget(peeingPos.transform.position);
                break;
            case AiState.WashTeeth:
                SetWalkTarget(washTeethPos.transform.position);
                break;
            case AiState.Eating:
                SetWalkTarget(eatingPos.transform.position);
                break;
            case AiState.WatchingTV:
                SetWalkTarget(watchingPos.transform.position);
                break;
            case AiState.TurningOffAlarm:
                SetWalkTarget(turningOffAlarmPos.transform.position);
                break;
            case AiState.TurningOnLight:

                if (victimRoomController.GetTheRoomName() == "Kamar Mandi")
                {
                    SetWalkTarget(bathRoomLightPos.transform.position);
                }

                else if (victimRoomController.GetTheRoomName() == "Kamar")
                {
                    SetWalkTarget(bedRoomLightPos.transform.position);
                }

                else if (victimRoomController.GetTheRoomName() == "Dapur")
                {
                    SetWalkTarget(kitcherLightPos.transform.position);
                }

                break;
            default:
                break;
        }

        OnAiStateChanged?.Invoke(newState);
    }

    private void SetWalkTarget(Vector3 myTarget)
    {
        myAnim.Play("Walking", -1, 0f);
        isWalking = true;
        target = new Vector3(myTarget.x, transform.position.y, transform.position.z);

        if (myTarget.x - transform.position.x < 0)
        {
            walkingDirection = "Right";
            transform.rotation = Quaternion.Euler(0f, 180f, 0);
        }
        else if (myTarget.x - transform.position.x > 0)
        {
            walkingDirection = "Left";
            transform.rotation = Quaternion.Euler(0f, 0f, 0);
        }
    }

    public string GetWalkingDirection()
    {
        return walkingDirection;
    }

    public void ArrivedAtDestination()
    {
        isWalking = false;
    }

    //Tiap kali masuk ke ruangan baru cek lampu apa menyala atau tidak
    public void CheckingTheLightInTheRoom()
    {
        //Jika lampu pada ruangan itu tidak menyala
        if (victimRoomController.GetTheRoomLightStatus() == false)
        {
            victimActionController.SetTheLightToTurnOff(victimRoomController.GetTheLightToTurnOff());
            UpdateAIState(AiState.TurningOnLight);
            return;
        }

    }

    public void ChangingState(AiState lastState)
    {
        AiState currentState = lastState;

        //Jika alarm menyala maka akan mematikan alarm terdahulu
        if (wekerObjectInteraction.GetTheShakingInfo() == true)
        {
            loudFx.Play();
            currentState = AiState.TurningOffAlarm;
            UpdateAIState(currentState);
            return;
        }

        //Jika TV menyala maka akan mematikan TV terdahulu
        if (tvObjectInteraction.GetTheTvStatus() == true)
        {
            loudFx.Play();
            currentState = AiState.WatchingTV;
            UpdateAIState(currentState);
            return;
        }

        //Jika westafel menyala maka akan mematikan Westafel terdahulu
        if (westafelObjectInteraction.GetTheWestafelStatus() == true)
        {
            loudFx.Play();
            currentState = AiState.WashTeeth;
            UpdateAIState(currentState);
            return;
        }

        while (currentState == lastState)
        {
            float randomNum = UnityEngine.Random.Range(0f, 500f);
            
            //Jika permainan baru dimulai maka hindari untuk tidur lagi
            if (lastState == AiState.WakeUp)
                randomNum = UnityEngine.Random.Range(101f, 500f);

            if (randomNum <= 100f)
            {
                currentState = AiState.Sleeping;
                UpdateAIState(currentState);
            }

            else if (randomNum <= 200f)
            {
                currentState = AiState.Peeing;
                UpdateAIState(currentState);
            }

            else if (randomNum <= 300f)
            {
                currentState = AiState.WashTeeth;
                UpdateAIState(currentState);
            }

            else if (randomNum <= 400f)
            {
                currentState = AiState.Eating;
                UpdateAIState(currentState);
            }

            else if (randomNum <= 500f)
            {
                currentState = AiState.WatchingTV;
                UpdateAIState(currentState);
            }

        }

    }

}

public enum AiState
{
    WakeUp,
    Sleeping,
    Peeing,
    WashTeeth,
    Eating,
    WatchingTV,
    TurningOffAlarm,
    TurningOnLight
}
