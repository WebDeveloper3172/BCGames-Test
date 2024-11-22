using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Viteza de mișcare înainte
    public float moveSpeed = 3f; // Viteza de mișcare laterală
    public float range = 4f; // Diapazonul stânga-dreapta

    private float initialX;

    void Start()
    {
        // Salvăm poziția inițială pe axa X
        initialX = transform.position.x;
    }

    void Update()
    {
        // Mișcare înainte constantă
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Obținem input-ul pentru stânga și dreapta
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculăm noua poziție pe axa X
        float newX = transform.position.x + horizontalInput * moveSpeed * Time.deltaTime;

        // Limităm poziția pe X în diapazonul dorit
        newX = Mathf.Clamp(newX, initialX - range, initialX + range);

        // Aplicăm poziția nouă
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
