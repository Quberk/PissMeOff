using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<VictimAIController>().GetWalkingDirection() == "Right")
            {
                myAnim.Play("OpenLeft", -1, 0f);
                
            }
            else if (other.GetComponent<VictimAIController>().GetWalkingDirection() == "Left")
                myAnim.Play("OpenRight", -1, 0f);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<VictimAIController>().GetWalkingDirection() == "Right")
            {
                myAnim.Play("CloseLeft", -1, 0f);
                
            }
            else if (other.GetComponent<VictimAIController>().GetWalkingDirection() == "Left")
                myAnim.Play("CloseRight", -1, 0f);
        }
    }

}
