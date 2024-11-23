using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Viteza de mișcare înainte
    public float moveSpeed = 3f; // Viteza de mișcare laterală
    public float range = 4f; // Diapazonul stânga-dreapta
    public float rotationSpeed = 2f; // Viteza de rotire la cotitură
    public VariableJoystick variableJoystick; // Referință la joystick

    private float initialPosition; // Poziția centrală (pe X sau Z)
    private bool isTurning = false; // Verifică dacă player-ul se rotește
    private Quaternion targetRotation; // Rotația spre care trebuie să se îndrepte
    private bool isMovingOnZ = false; // Verifică dacă playerul se mișcă pe Z (după cotire)

    void Start()
    {
        // Salvăm poziția inițială pe axa X
        initialPosition = transform.position.x;
        targetRotation = transform.rotation; // Rotația inițială
    }

    void Update()
    {
        // Mișcare înainte constantă
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Gestionăm rotația dacă este necesar
        if (isTurning)
        {
            // Rotește player-ul către rotația țintă
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Verificăm dacă rotația este completă
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isTurning = false;
                transform.rotation = targetRotation; // Asigurăm alinierea exactă

                // După cotire, actualizăm axa pe care se mișcă player-ul
                isMovingOnZ = !isMovingOnZ; // Schimbăm axa
                initialPosition = isMovingOnZ ? transform.position.z : transform.position.x; // Actualizăm poziția inițială
            }

            return; // Blocăm mișcarea laterală în timpul rotației
        }

        // Mișcare laterală bazată pe joystick
        float horizontalInput = variableJoystick.Horizontal;

        // Mișcare pe axa X sau Z, în funcție de direcția curentă
        if (isMovingOnZ)
        {
            // Calculăm noua poziție pe axa Z
            float newZ = transform.position.z + horizontalInput * moveSpeed * Time.deltaTime;

            // Limităm poziția pe Z în diapazonul dorit
            newZ = Mathf.Clamp(newZ, initialPosition - range, initialPosition + range);

            // Aplicăm poziția nouă
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        else
        {
            // Calculăm noua poziție pe axa X
            float newX = transform.position.x + horizontalInput * moveSpeed * Time.deltaTime;

            // Limităm poziția pe X în diapazonul dorit
            newX = Mathf.Clamp(newX, initialPosition - range, initialPosition + range);

            // Aplicăm poziția nouă
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificăm dacă trigger-ul indică o cotitură
        if (other.CompareTag("Turn"))
        {
            // Obținem rotația indicată de trigger
            TurnTrigger turnTrigger = other.GetComponent<TurnTrigger>();
            if (turnTrigger != null)
            {
                targetRotation = Quaternion.Euler(0, turnTrigger.targetYRotation, 0);
                isTurning = true; // Setăm flag-ul pentru a începe rotația
            }
        }
    }
}
