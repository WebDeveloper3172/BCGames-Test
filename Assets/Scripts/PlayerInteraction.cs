using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Slider slider; // Referință la slider-ul din UI (atașat în Canvas-ul player-ului)
    public int score = 0; // Scorul inițial al jucătorului
    [SerializeField] TextMeshProUGUI Score;

   
    [Header("Adjustable Values")]
    [SerializeField] private int alcoholDecrease = 20; // Cât se scade la alcool
    [SerializeField] private int moneyIncrease = 2; // Cât se adaugă la bani

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alcohol"))
        {
            // Scade scorul și adaugă alcool
            score -= alcoholDecrease;
            UpdateSlider();
            Destroy(other.gameObject); // Distruge obiectul (sticlă de alcool)
        }
        else if (other.CompareTag("Money"))
        {
            // Crește scorul și adaugă bani
            score += moneyIncrease;
            UpdateSlider();
            Destroy(other.gameObject); // Distruge obiectul (bani)
        }
    }

    private void UpdateSlider()
    {
        // Actualizează valoarea slider-ului
        slider.value = score;
        Score.text = score.ToString();
    }
}
