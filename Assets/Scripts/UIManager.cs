using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI recordScore;
    public static int bestScore;

    private void Start() {
        GetUserScore();
        recordScore.text = bestScore.ToString();
    }
    public void UpdateScore() {
        if(score != null) {
            score.text = GameManager.Instance.scoreCount.ToString();
        }
        recordScore.text = bestScore.ToString();
    }

    public void GetUserScore() {

        string UserId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + UserId + "/score")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log("Best Score: " + snapshot.Value);
                    bestScore = (int)snapshot.Value;
                    recordScore.text = bestScore.ToString();
                    //GameObject.Find("LabelScore").GetComponent<TMPro.TMP_Text>().text = "Score: " + score;
                }
            });
    }

    internal static void SetScore() {

        string UserId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        if (GameManager.Instance.scoreCount > bestScore) {
            FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(UserId).Child("score")
                .SetValueAsync(GameManager.Instance.scoreCount);
        }
    }
}
