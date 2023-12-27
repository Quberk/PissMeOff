using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPos : MonoBehaviour
{
    [SerializeField] private AiState myPosState;
    [SerializeField] private GameObject myActionPos;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private string actionAnimationString;

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
            if (VictimAIController.Instance.state == myPosState)
            {
                other.transform.position = new Vector3(myActionPos.transform.position.x, other.transform.position.y, myActionPos.transform.position.z);
                other.transform.rotation = Quaternion.Euler(rotation);
                other.transform.Find("GFX").GetComponent<VictimActionController>().StartAnAction(actionAnimationString);
            }    
        }
    }

}
