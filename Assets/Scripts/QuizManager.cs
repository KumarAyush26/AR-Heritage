using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{

    [System.Serializable]
    public class QuestionData
    {
        public Question[] questions;
    }

    [System.Serializable]
    public class Question
    {
        [TextArea(2, 5)]
        public string questionText;

        public string optionA;
        public string optionB;
        public string optionC;
        public string optionD;

        [Range(0, 3)]
        public int correctAnswer;
    }

    [Header("Questions")]
    public Question[] questions;

    [Header("UI")]
    public TMP_Text questionText;
    public Button[] optionButtons;

    [Header("Settings")]
    public float nextQuestionDelay = 1.0f;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool answerSelected = false;

    private Color normalColor = Color.white;
    private Color correctColor = new Color(0.45f, 1f, 0.45f);
    private Color wrongColor = new Color(1f, 0.45f, 0.45f);

    private void Start()
    {
        LoadQuestionsFromJSON();

        if (questions == null || questions.Length == 0)
        {
            Debug.LogError("QuizManager: No questions have been added.");
            return;
        }

        if (optionButtons == null || optionButtons.Length != 4)
        {
            Debug.LogError("QuizManager: Please assign exactly 4 option buttons.");
            return;
        }

        SetupButtonListeners();
        ShowQuestion();
    }

    private void LoadQuestionsFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("quiz_questions");

        if (jsonFile == null)
        {
            Debug.LogError("QuizManager: quiz_questions.json not found in Resources folder.");
            return;
        }

        QuestionData data = JsonUtility.FromJson<QuestionData>(jsonFile.text);
        questions = data.questions;

        ShuffleQuestions();
    }

    private void ShuffleQuestions()
    {
        if (questions == null) return;

        for (int i = questions.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (questions[i], questions[randomIndex]) = (questions[randomIndex], questions[i]);
        }
    }

    private void SetupButtonListeners()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int answerIndex = i;

            optionButtons[i].onClick.RemoveAllListeners();

            optionButtons[i].onClick.AddListener(
                () => SelectAnswer(answerIndex)
            );
        }
    }

    private void ShowQuestion()
    {
        answerSelected = false;

        Question question = questions[currentQuestionIndex];

        questionText.text = question.questionText;

        string[] options =
        {
            question.optionA,
            question.optionB,
            question.optionC,
            question.optionD
        };

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].interactable = true;

            TMP_Text text = optionButtons[i]
                .GetComponentInChildren<TMP_Text>();

            if (text != null)
            {
                text.text = options[i];
            }

            Image image = optionButtons[i].GetComponent<Image>();

            if (image != null)
            {
                image.color = normalColor;
            }
        }
    }

    private void SelectAnswer(int selectedAnswer)
    {
    if (answerSelected)
        return;

    answerSelected = true;

    Debug.Log("Answer selected: " + selectedAnswer);

    Question question = questions[currentQuestionIndex];

    bool correct = selectedAnswer == question.correctAnswer;

    if (correct)
    {
        score++;
        Debug.Log("Correct!");
    }
    else
    {
        Debug.Log("Wrong!");
    }

    for (int i = 0; i < optionButtons.Length; i++)
    {
        optionButtons[i].interactable = false;

        Image image = optionButtons[i].GetComponent<Image>();

        if (image == null)
            continue;

        if (i == question.correctAnswer)
        {
            image.color = correctColor;
        }
        else if (i == selectedAnswer)
        {
            image.color = wrongColor;
        }
    }

    Debug.Log("Loading next question...");

    StartCoroutine(LoadNextQuestion());
}

    private IEnumerator LoadNextQuestion()
    {
        yield return new WaitForSecondsRealtime(nextQuestionDelay);

        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Length)
        {
            FinishQuiz();
        }
        else
        {
            ShowQuestion();
        }
}

    private void FinishQuiz()
    {
        Debug.Log(
            "Quiz finished! Score: " +
            score +
            "/" +
            questions.Length
        );

        questionText.text =
            "Quiz Complete!\n\nScore: " +
            score +
            " / " +
            questions.Length;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].gameObject.SetActive(false);
        }
    }
}