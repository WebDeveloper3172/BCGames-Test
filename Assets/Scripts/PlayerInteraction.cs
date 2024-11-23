using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Slider slider; // Referință la slider-ul din UI (atașat în Canvas-ul player-ului)
    public int score = 0; // Scorul inițial al jucătorului
    private void Start()
    {
        // Inițializează slider-ul cu valoarea scorului
        if (slider != null)
        {
            slider.maxValue = 200; // Poți ajusta valoarea maximă după cum dorești
            slider.minValue = 0;   // Valoarea minimă
            slider.value = score;  // Setează scorul inițial
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alcohol"))
        {
            // Scade scorul
            score -= 10;
            UpdateSlider();
            Destroy(other.gameObject); // Distruge obiectul (sticlă de alcool)
        }
        else if (other.CompareTag("Money"))
        {
            // Crește scorul
            score += 10;
            UpdateSlider();
            Destroy(other.gameObject); // Distruge obiectul (bani)
        }
    }

    private void UpdateSlider()
    {
        // Actualizează valoarea slider-ului
        slider.value = score;
    }

}
