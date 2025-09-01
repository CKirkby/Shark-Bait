using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed = 5;
    
    
    private InputAction MoveAction;
    private Camera MainCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets the camera 
        MainCamera = Camera.main;
        
        // Finds the relevant input actions
        MoveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        // Temp jank movement code, needs sorting properly
        Vector2 MoveValue = MoveAction.ReadValue<Vector2>();
        transform.Translate(new Vector2(MoveValue.x, 0) * (MovementSpeed * Time.deltaTime), Space.World);
    }

    private void LateUpdate()
    {
        // Convert the object's position into viewport space (0–1)
        Vector3 pos = MainCamera.WorldToViewportPoint(transform.position);

        // Clamp values so object stays in the screen
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);

        // Convert back to world space
        transform.position = MainCamera.ViewportToWorldPoint(pos);
    }
}
