using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float moveSpeed = 3f;
    public float range = 4f;
    public float rotationSpeed = 2f;
    public VariableJoystick variableJoystick;

    private float initialPosition;
    private bool isTurning = false;
    private Quaternion targetRotation;
    private bool isMovingOnZ = false;
    private bool canMove = false; // Adăugăm un flag pentru a controla mișcarea

    void Start()
    {
        initialPosition = transform.position.x;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        // Mișcare constantă înainte
        if (canMove)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (isTurning)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
                {
                    isTurning = false;
                    transform.rotation = targetRotation;
                    isMovingOnZ = !isMovingOnZ;
                    initialPosition = isMovingOnZ ? transform.position.z : transform.position.x;
                }

                return;
            }

            float horizontalInput = variableJoystick.Horizontal;

            if (isMovingOnZ)
            {
                float newZ = transform.position.z + horizontalInput * moveSpeed * Time.deltaTime;
                newZ = Mathf.Clamp(newZ, initialPosition - range, initialPosition + range);
                transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
            }
            else
            {
                float newX = transform.position.x + horizontalInput * moveSpeed * Time.deltaTime;
                newX = Mathf.Clamp(newX, initialPosition - range, initialPosition + range);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
        }
    }

    // Aceasta metodă va fi apelată pentru a începe mișcarea playerului
    public void EnablePlayerMovement()
    {
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Turn"))
        {
            TurnTrigger turnTrigger = other.GetComponent<TurnTrigger>();
            if (turnTrigger != null)
            {
                targetRotation = Quaternion.Euler(0, turnTrigger.targetYRotation, 0);
                isTurning = true;
            }
        }
    }
}
