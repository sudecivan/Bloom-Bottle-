using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    public Transform direction; // Reference to the Direction GameObject aligned with the camera

    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Get movement direction relative to camera
        Vector3 moveDir = direction.forward * z + direction.right * x;
        moveDir.y = 0f; // prevent upward/downward movement

        if (moveDir.magnitude > 0.1f)
        {
            // Move and rotate
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(moveDir);
           
        }
        else
        {
            
        }
    }
}


