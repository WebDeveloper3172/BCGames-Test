using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Viteza de mișcare înainte
    public float moveSpeed = 3f; // Viteza de mișcare laterală
    public float range = 4f; // Diapazonul stânga-dreapta
    public float rotationSpeed = 2f; // Viteza de rotire la cotitură

    private float initialX;
    private bool isTurning = false; // Verifică dacă player-ul se rotește
    private Quaternion targetRotation; // Rotația spre care trebuie să se îndrepte

    void Start()
    {
        // Salvăm poziția inițială pe axa X
        initialX = transform.position.x;
        targetRotation = transform.rotation; // Rotația inițială
    }

    void Update()
    {
        // Mișcare înainte constantă
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Rotație automată dacă este necesar
        if (isTurning)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Verificăm dacă s-a terminat rotația
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                isTurning = false;
                transform.rotation = targetRotation; // Aliniere finală
            }
            return; // Blocăm mișcarea laterală în timpul rotației
        }

        // Obținem input-ul pentru stânga și dreapta
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculăm noua poziție pe axa X
        float newX = transform.position.x + horizontalInput * moveSpeed * Time.deltaTime;

        // Limităm poziția pe X în diapazonul dorit
        newX = Mathf.Clamp(newX, initialX - range, initialX + range);

        // Aplicăm poziția nouă
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
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
                isTurning = true;
            }
        }
    }
}
