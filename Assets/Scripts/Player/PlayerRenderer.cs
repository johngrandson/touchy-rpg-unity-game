using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRenderer : MonoBehaviour
{
    [Header("Dependencies")]
    public SpriteRenderer spriteRenderer;

    private PlayerController playerController;

    private void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 movementInput = value.ReadValue<Vector2>();

        if (movementInput.x > 0f && PlayerIsLookingLeft()) // Moving to the right
        {
            spriteRenderer.flipX = false;
        }
        else if (movementInput.x < 0f && !PlayerIsLookingLeft())
        {
            spriteRenderer.flipX = true;
        }
    }

    private bool PlayerIsLookingLeft()
    {
        return spriteRenderer.flipX;
    }
}
