using UnityEngine;

public class RoomController : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private string roomName;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<VictimRoomController>().ChangeRoom(roomName);

            InteractiveObject[] interactiveObjects = FindObjectsOfType<InteractiveObject>();

            foreach(InteractiveObject interactiveObject in interactiveObjects)
            {
                interactiveObject.ChangeTheHighlightMaterial();
            }
        }
    }
}
