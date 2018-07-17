using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public void CallbackBtnStart()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void CallbackBtnOption()
    {
        SceneManager.LoadScene("OptionScene");
    }

    public void CallbackBtnExit()
    {
        Application.Quit();
    }
}
