using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Level";
   public void PlayGame() {
        SceneManager.LoadScene(sceneToLoad);
   }

    public void QuitGame() {
        Application.Quit();
    }
}
