using UnityEngine;

public class ModelAndAnimationSwitcher : MonoBehaviour
{
    [Header("Models and Animations")]
    [SerializeField] private GameObject poorModel;     // Modelul "poor"
    [SerializeField] private GameObject casualModel;   // Modelul "casual"
    [SerializeField] private GameObject middleModel;   // Modelul "middle"
    [SerializeField] private GameObject blingModel;    // Modelul "bling"
    [SerializeField] private GameObject cocktailModel; // Modelul "cocktail"

    [SerializeField] private Animator playerAnimator; // Animator-ul care controlează animațiile

    private void OnEnable()
    {
        // Abonăm acest script la evenimentul de schimbare a scorului
        PlayerInteraction.OnScoreChanged += CheckScoreAndChangeModel;
    }

    private void OnDisable()
    {
        // Dezabonăm scriptul de la evenimentul de schimbare a scorului pentru a preveni problemele de performanță
        PlayerInteraction.OnScoreChanged -= CheckScoreAndChangeModel;
    }

    private void CheckScoreAndChangeModel(int newScore)
    {
        if (newScore <= 50)
        {
            ChangeModelAndAnimation("poor");
        }
        else if (newScore > 50 && newScore <= 100)
        {
            ChangeModelAndAnimation("casual");
        }
        else if (newScore > 100 && newScore <= 150)
        {
            ChangeModelAndAnimation("middle");
        }
        else if (newScore > 150 && newScore <= 200)
        {
            ChangeModelAndAnimation("bling");
        }
        else if (newScore > 200)
        {
            ChangeModelAndAnimation("cocktail");
        }
    }

    private void ChangeModelAndAnimation(string modelType)
    {
        // Dezactivează toate modelele
        poorModel.SetActive(false);
        casualModel.SetActive(false);
        middleModel.SetActive(false);
        blingModel.SetActive(false);
        cocktailModel.SetActive(false);

        // Activează modelul corespunzător în funcție de scor
        switch (modelType)
        {
            case "poor":
                poorModel.SetActive(true);
                playerAnimator.Play("PoorWalk"); // Animația pentru modelul "poor"
                break;

            case "casual":
                casualModel.SetActive(true);
                playerAnimator.Play("CasualWalk"); // Animația pentru modelul "casual"
                break;

            case "middle":
                middleModel.SetActive(true);
                playerAnimator.Play("MiddleWalk"); // Animația pentru modelul "middle"
                break;

            case "bling":
                blingModel.SetActive(true);
                playerAnimator.Play("BlingWalk"); // Animația pentru modelul "bling"
                break;

            case "cocktail":
                cocktailModel.SetActive(true);
                playerAnimator.Play("CocktailWalk"); // Animația pentru modelul "cocktail"
                break;

            default:
                Debug.LogWarning("Modelul selectat nu există.");
                break;
        }
    }
}
