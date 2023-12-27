using UnityEngine;

public class PanelCancelAction : MonoBehaviour
{
    private InteractiveObject interactiveObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitInteractWithObject()
    {
        interactiveObject.StopInteractingWithObject();
    }

    public void SetTheActiveInteractiveObject(InteractiveObject myInteractiveObj)
    {
        interactiveObject = myInteractiveObj;
    }
}
