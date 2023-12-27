using UnityEngine;

public class CollisionPrevent : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        Physics.IgnoreLayerCollision(8, 9);
    }

    void Start()
    {
        Physics.IgnoreLayerCollision(8, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       // Physics.IgnoreLayerCollision(8, 9);
    }
}
