using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void GoToMonuments()
    {
        SceneManager.LoadScene("Monuments");
    }

    public void GoToQuiz()
    {
        SceneManager.LoadScene("Quiz");
    }

    public void GoToRamMandir()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}