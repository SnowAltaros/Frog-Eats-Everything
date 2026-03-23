using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    public Vector2 MousePosition {get; private set;}
    public bool ClickTriggered {get; private set;}
    public bool mouseWasClicked;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Attack.performed += OnClick;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Attack.performed -= OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        ClickTriggered = true;
        mouseWasClicked = true;
    }

    private void Update()
    {
        GetMouseWorldPosition();
        ClickTriggered = false;
    }

    // Find the world possition for the mouse and store it in public variable
    private void GetMouseWorldPosition()
    {
        Vector2 mouseScreenPos = inputActions.UI.Point.ReadValue<Vector2>();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;
        MousePosition = mouseWorldPos;
    }
}
