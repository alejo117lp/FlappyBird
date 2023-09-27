using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] float timeToReloadScene;
    [Space,SerializeField] UnityEvent onGameOver;
    [Space, SerializeField] UnityEvent onIncreaseScore;

    public bool isGameOver { get; private set; }
    public int scoreCount { get; private set; }
    //Creamos un Singleton
    public static GameManager Instance {
        get;
        private set;
    }

    private void Awake() {
        if (Instance != null) DestroyImmediate(gameObject);
        else Instance = this;
        scoreCount = 0;
    }

    public void GameOver() {

        UIManager.SetScore();
        isGameOver = true;
        if(onGameOver != null) {
            onGameOver.Invoke();
        }
        StartCoroutine(ReloadScene());
    }

    public void IncreaseScore() {
        scoreCount++;
        if (onIncreaseScore != null) onIncreaseScore.Invoke();
    }

    IEnumerator ReloadScene() {
        yield return new WaitForSeconds(timeToReloadScene);
        SceneManager.LoadScene("Level");
    }
}
