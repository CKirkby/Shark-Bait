using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction MoveAction;
    
    private Vector2 StartTouchPosition;
    private Vector2 StartPlayerPosition;
    private bool IsDragging = false;
    
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
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 CurrentTouchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            
            if (!IsDragging)
            {
                IsDragging =  true;
                StartTouchPosition = CurrentTouchPos;
                StartPlayerPosition = transform.position;
            }
            else
            {
                float deltaX = CurrentTouchPos.x - StartTouchPosition.x;

                // Sensitivity factor (tweak as needed)
                float dragFactor = 0.01f;

                // Calculate new X position
                Vector2 newPos = StartPlayerPosition + new Vector2(deltaX * dragFactor, 0);

                // Smoothly move player
                transform.position = Vector2.Lerp(transform.position, newPos, Time.deltaTime * 10f);
            }
        }
        else
        {
            IsDragging = false;
        }
        
        
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Convert finger screen position into world position
            Vector3 touchWorldPos = MainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, MainCamera.nearClipPlane));

            // Keep the player's Y/Z the same, only update X
            Vector3 NewPos = transform.position; 
            NewPos.x = Mathf.Lerp(transform.position.x, touchWorldPos.x, Time.deltaTime * 10f);

            transform.position = NewPos;
        }*/
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
