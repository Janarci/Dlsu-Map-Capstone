using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionTxt;
    [SerializeField] GameObject wordContainer;
    [SerializeField] GameObject keyboardContainer;
    [SerializeField] GameObject letterContainer;
    [SerializeField] GameObject[] hangmanStages;
    [SerializeField] GameObject letterButton;
    [SerializeField] TextAsset possibleWorld;

    ChillspaceQuizPool.Quizzes.Quiz currentQuiz = null;
    public Action<bool> OnFinishQuiz;
    

    private string word;
    private int incorrectGuesses, correctGuesses;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeGame(string question, string answer)
    {
        incorrectGuesses = 0;
        correctGuesses = 0;

        foreach(Button b in keyboardContainer.GetComponentsInChildren<Button>())
        {
            b.interactable = true;
        }

        foreach(Transform t in wordContainer.GetComponentInChildren<Transform>())
        {
            Destroy(t.gameObject);
        }

        foreach(GameObject stage in hangmanStages)
        {
            stage.SetActive(false);
        }

        foreach(char _letter in answer)
        {
            var temp = Instantiate(letterContainer, wordContainer.transform);
        }

        questionTxt.text = question;
        word = answer.ToUpper();
        InitializeButtons();
    }

    void InitializeButtons()
    {
        for (int i = 65; i <= 90; i++)
        {
            CreateButton(i);
        }
    }

    public void CreateButton(int i)
    {
        GameObject temp = Instantiate(letterButton, keyboardContainer.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate { CheckLetter(((char)i).ToString()); });
    }

   

    private void CheckLetter(string InputLetter)
    {
        bool letterInWord = false;

        for (int i = 0; i < word.Length; i++)
        {
            if(InputLetter == word[i].ToString())
            {
                letterInWord = true;
                correctGuesses++;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = InputLetter;
                //break;
            }
        }

        if(!letterInWord)
        {
            incorrectGuesses++;
            hangmanStages[incorrectGuesses - 1].SetActive(true);
        }

        CheckOutcome();
    }

    private void CheckOutcome()
    {
        if(correctGuesses == word.Length)
        {
            for (int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.green;
            }
            FinishQuiz(false);
        }

        if (incorrectGuesses == hangmanStages.Length)
        {
            for (int i = 0; i < word.Length; i++)
            {
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.red;
                wordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = word[i].ToString();
            }

            FinishQuiz(false);
        }
    }

    void FinishQuiz(bool _isSuccess)
    {
        if(OnFinishQuiz != null)
        {
            OnFinishQuiz(_isSuccess);
        }
    }
}
