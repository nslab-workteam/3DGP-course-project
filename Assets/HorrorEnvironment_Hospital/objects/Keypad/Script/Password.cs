using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    string code = "1453";
    string Nr = null;
    int NrIndex = 0;
    string alpha;
    public Text uiText = null;
    public GameObject inGameUiManager;
    private Animator suitcaseAnimator;
    public GameObject Suitcase;
    private Animator[] temp; 
    private Animator recipeAnimator;
    public GameObject Recipe;

    void Start()
    {
        temp = Suitcase.GetComponentsInChildren<Animator>();
        suitcaseAnimator = temp[0];
        temp = Recipe.GetComponentsInChildren<Animator>();
        recipeAnimator = temp[0];
    }

    public void CodeFunction(string Numbers)
    {
        NrIndex++;
        Nr = Nr + Numbers;
        uiText.text = Nr;
    }

    public void Enter()
    {
        if(Nr == code)
        {
            inGameUiManager.GetComponent<IngameUI>().ShowHint("密碼正確");
            Debug.Log("Password is correct");
            suitcaseAnimator.SetTrigger("OpenSuitcase");
            recipeAnimator.SetTrigger("RecipeAnimation");
            GameObject.Find("suitcase").GetComponent<SuitcaseBehavior>().hasOpened = true;
            inGameUiManager.GetComponent<IngameUI>().OnKeypadBackClick();
        }
        else{
            inGameUiManager.GetComponent<IngameUI>().ShowHint("密碼錯誤");
            Debug.Log("Password is wrong");
        }
    }

    public void Delete()
    {
        NrIndex++;
        Nr = null;
        uiText.text = Nr;
    }
}
