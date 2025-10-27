using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class NurseScript : MonoBehaviour {
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private InputAction moveAction;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;        //top-down
        rb.freezeRotation = true;    //avoid spin on collision

        moveAction = new InputAction(name: "Move", type: InputActionType.Value);

        //keyboard 2D composite (WASD + Arrows)
        var composite = moveAction.AddCompositeBinding("2DVector");
        composite.With("Up", "<Keyboard>/w");
        composite.With("Up", "<Keyboard>/upArrow");
        composite.With("Down", "<Keyboard>/s");
        composite.With("Down", "<Keyboard>/downArrow");
        composite.With("Left", "<Keyboard>/a");
        composite.With("Left", "<Keyboard>/leftArrow");
        composite.With("Right", "<Keyboard>/d");
        composite.With("Right", "<Keyboard>/rightArrow");
    }

    void OnEnable() => moveAction.Enable();
    void OnDisable() => moveAction.Disable();

    void Update() {
        movement = moveAction.ReadValue<Vector2>();
        if (movement.sqrMagnitude > 1f)
            movement = movement.normalized; // used for consistent diagonal speed
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
    }
}
