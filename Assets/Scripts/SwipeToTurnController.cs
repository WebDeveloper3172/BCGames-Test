using UnityEngine;

public class SwipeToTurnController : MonoBehaviour
{
    public GameObject[] uiElementsToDisable; // Elemente UI care trebuie dezactivate
    public GameObject[] uiElementsToEnable;  // Elemente UI care trebuie activate
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
        // Dezactivăm toate elementele din array-ul `uiElementsToDisable`
        foreach (GameObject element in uiElementsToDisable)
        {
            if (element != null)
            {
                element.SetActive(false);
            }
        }

        // Activăm toate elementele din array-ul `uiElementsToEnable`
        foreach (GameObject element in uiElementsToEnable)
        {
            if (element != null)
            {
                element.SetActive(true);
            }
        }

        // Activăm mișcarea playerului
        playerController.EnablePlayerMovement();

        gameStarted = true;
    }
}
