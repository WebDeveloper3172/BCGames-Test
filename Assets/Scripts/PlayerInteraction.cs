using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Slider slider; // Referință la slider-ul din UI (atașat în Canvas-ul player-ului)
    public int score = 0; // Scorul inițial al jucătorului

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
