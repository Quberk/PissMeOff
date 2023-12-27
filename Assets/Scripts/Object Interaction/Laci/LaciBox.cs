using UnityEngine;

public class LaciBox : MonoBehaviour
{

    private bool opened = false;
    [SerializeField] private float openedPos, closedPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetInteract()
    {
        if (opened)
        {
            transform.localPosition = new Vector3(closedPos, transform.localPosition.y, transform.localPosition.z);
            opened = false;
        }

        else
        {
            transform.localPosition = new Vector3(openedPos, transform.localPosition.y, transform.localPosition.z);
            opened = true;
        }
            
    }

    public bool GetStatus()
    {
        return opened;
    }
}
