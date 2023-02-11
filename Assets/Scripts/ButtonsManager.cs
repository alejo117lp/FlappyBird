using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
   public void PlayGame() {
        SceneManager.LoadScene("Main");
   }

    public void QuitGame() {
        Application.Quit();
    }
}
