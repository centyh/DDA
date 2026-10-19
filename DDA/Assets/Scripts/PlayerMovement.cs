using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private float moveSpeed = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            movement.y = 1f;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            movement.y = -1f;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            movement.x = 1f;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            movement.x = -1f;
        }
    }


    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}
