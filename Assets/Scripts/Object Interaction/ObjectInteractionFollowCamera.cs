using UnityEngine;

public class ObjectInteractionFollowCamera : MonoBehaviour
{
    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, transform.position.z);
    }
}
