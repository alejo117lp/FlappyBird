using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private string sceneGame = "Level";
    [SerializeField] private string sceneBack = "Registration";
    public void PlayGame() {
        SceneManager.LoadScene(sceneGame);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void GoBack() {
        SceneManager.LoadScene(sceneBack);
    }
}
