using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;

    public Transform direction; 

    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        
        Vector3 moveDir = direction.forward * z + direction.right * x;
        moveDir.y = 0f; 

        if (moveDir.magnitude > 0.1f)
        {
            
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(moveDir);
           
        }
        else
        {
            
        }
    }
}


