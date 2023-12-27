using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float leftBoundary;
    [SerializeField] private float rightBoundary;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 translation = Vector3.zero;

        if (Input.mousePosition.x < (Screen.width * 1f / 8f))
            translation += Vector3.left * 0.1f;
        else if (Input.mousePosition.x > (Screen.width * 7f / 8f))
            translation += Vector3.right * 0.1f;

        transform.Translate(translation);
    }
}
