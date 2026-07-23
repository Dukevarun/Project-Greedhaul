using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody playerRB;
    private Vector2 movementInputValue, mousePositionValue;
    private Vector3 playerMovement, lookDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        moveSpeed = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePositionValue);

        if (Physics.Raycast(ray, out hit))
        {
            lookDirection = hit.point;
        }        
    }

    private void FixedUpdate()
    {
        movePlayerWithAim();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInputValue = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mousePositionValue = context.ReadValue<Vector2>();
    }

    /*public void movePlayer()
    {
        playerMovement = new Vector3(movementInputValue.x, 0f, movementInputValue.y);
    }*/

    public void movePlayerWithAim()
    {
        var lookPos = lookDirection - transform.position;
        lookPos.y = 0f;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDirection = new Vector3(lookDirection.x, 0f, lookDirection.z);

        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }
        
        playerMovement = new Vector3(movementInputValue.x, 0f, movementInputValue.y);
        if (playerMovement != Vector3.zero)
        {
            playerRB.AddForce(playerMovement * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);
        }
    }
}
