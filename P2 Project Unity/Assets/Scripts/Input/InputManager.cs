using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Credit to Samyam Youtube

    // We need an instance of the controls script to be able to register the inputs
    private TouchControls touchControls;

    // StartTouchEvent is a decleration of a void function which takes a Vector2 and a float as input variables (arguments)
    public delegate void StartTouchEvent(Vector2 position, float time);
    // 
    public event StartTouchEvent OnStartTouch;

    // EndTouchEvent is a reference to a void function which takes a Vector2 and a float as input variables (arguments)
    // This EndTouchEvent is repetitive and unneeded at the moment, as it does the exact same as the StartTouchEvent
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnEndTouch;

    // Very first function called as the object is instantiated
    private void Awake()
    {
        // An instance of the controls script is instantiated
        touchControls = new TouchControls();
    }

    // When this is active, be able to register inputs
    private void OnEnable()
    {
        touchControls.Enable();
    }

    // When this is no longer active, stop being able to register inputs
    private void OnDisable()
    {
        touchControls.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // The following are setups of events, meaning that whenever the thing to the left of => happens, the function to the right of => will be run
        // When an input is registered to be started, get the context of the input, then call the StartTouch function and tell it that context
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);

        // When an input is registered to be stopped, get the context of the input, then call the EndTouch function and tell it that context
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    /// <summary>
    /// Prints where the user is touching
    /// </summary>
    /// <param name="context"></param>
    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Started" + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if (OnStartTouch != null)
        {
            OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    /// <summary>
    /// Prints that the user no longer touches the screen
    /// </summary>
    /// <param name="context"></param>
    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Ended");
        if (OnEndTouch != null) 
        {
            OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
    }  
}
