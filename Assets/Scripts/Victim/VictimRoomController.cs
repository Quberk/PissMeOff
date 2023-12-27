using UnityEngine;

public class VictimRoomController : MonoBehaviour
{
    [SerializeField] private string currentRoom = "Kamar";
    [SerializeField] private GameObject bathRoomLight, bedRoomLight, kitchenLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeRoom(string roomName)
    {
        currentRoom = roomName;
        VictimAIController.Instance.CheckingTheLightInTheRoom();
        Debug.Log("saya berada di ruangan" + roomName);
    }

    public string GetTheRoomName()
    {
        return currentRoom;
    }

    public bool GetTheRoomLightStatus()
    {
        if (currentRoom == "Kamar Mandi")
        {
            return bathRoomLight.activeInHierarchy;
        }

        else if (currentRoom == "Kamar")
        {
            return bedRoomLight.activeInHierarchy;
        }

        else if (currentRoom == "Dapur")
        {
            return kitchenLight.activeInHierarchy;
        }

        return true;
    }

    public GameObject GetTheLightToTurnOff()
    {
        if (currentRoom == "Kamar Mandi")
        {
            return bathRoomLight;
        }

        else if (currentRoom == "Kamar")
        {
            return bedRoomLight;
        }

        else if (currentRoom == "Dapur")
        {
            return kitchenLight;
        }
        return null;
    }
}
