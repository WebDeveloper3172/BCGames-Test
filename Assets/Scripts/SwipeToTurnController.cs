using UnityEngine;
using UnityEngine.UI; // Pentru UI

public class SwipeToTurnController : MonoBehaviour
{
    public GameObject swipeText; // Referința la Text-ul "Swipe to turn"
    public RectTransform finger; // Referința la deget (obiectul care se mișcă)
    public RectTransform swipeArea; // Zona pe care se mișcă degetul
    public float swipeSpeed = 10f; // Viteza de mișcare a degetului
    private float direction = 1f; // Direcția de mișcare (1 = dreapta, -1 = stânga)
    private bool gameStarted = false; // Verifică dacă jocul a început

    // Referință la obiectul sau scriptul care controlează playerul
    public PlayerController playerController;

    void Update()
    {
        // Dacă jocul nu a început, gestionăm mișcarea automată și verificăm intrările utilizatorului
        if (!gameStarted)
        {
            MoveFingerAutomatically();
            CheckForStartInput();
        }
    }

    void MoveFingerAutomatically()
    {
        // Calculăm noua poziție pe axa X
        float newX = finger.localPosition.x + (swipeSpeed * direction * Time.deltaTime);

        // Verificăm limitele și schimbăm direcția dacă este necesar
        if (newX > swipeArea.rect.width / 2 || newX < -swipeArea.rect.width / 2)
        {
            direction *= -1f; // Inversăm direcția de mișcare
        }

        // Actualizăm poziția degetului
        finger.localPosition = new Vector2(newX, finger.localPosition.y);
    }

    void CheckForStartInput()
    {
        // Verificăm dacă utilizatorul atinge ecranul sau face clic pe mouse
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        // Ascundem UI-ul de start
        swipeText.gameObject.SetActive(false);
        swipeArea.gameObject.SetActive(false);
        finger.gameObject.SetActive(false);

        // Activăm mișcarea playerului
        playerController.EnablePlayerMovement();

        gameStarted = true;
    }
}
