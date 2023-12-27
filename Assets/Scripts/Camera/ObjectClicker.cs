using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.GetComponent<InteractiveObject>() != null)
                {
                    hit.transform.GetComponent<InteractiveObject>().InteractingWithObject();

                }
            }
        }
    }
}
