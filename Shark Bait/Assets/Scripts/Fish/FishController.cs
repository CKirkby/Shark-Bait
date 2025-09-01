using UnityEngine;

public class FishController : MonoBehaviour
{
    public float MovementSpeed = 20.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * (MovementSpeed * Time.deltaTime), Space.World);
    }
}
