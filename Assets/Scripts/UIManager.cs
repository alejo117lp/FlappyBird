using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using System.Linq;
using Firebase;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] GameObject leaderboardPanel;
    [SerializeField] private TextMeshProUGUI[] names;
    [SerializeField] private TextMeshProUGUI[] scores;
    public static int bestScore;

    [Header("Forgot Password")]
    [SerializeField] TMP_InputField forgotPasswordEmail;
    [SerializeField] private TMP_Text warningForgetPasswordText;
    [SerializeField] TMP_Text confirmationPasswordText;

    private void Start() {
        GetUserScore();
    }
    public void UpdateScore() {
        if(score != null) {
            score.text = GameManager.Instance.scoreCount.ToString();
        }
        
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
                }
            });
    }

    public void GetUsersHighestScores() {

        leaderboardPanel.SetActive(true);
        FirebaseDatabase.DefaultInstance
            .GetReference("users").OrderByChild("score").LimitToLast(3)
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    foreach (var userDoc in (Dictionary<string, object>)snapshot.Value) {
                        var userObject = (Dictionary<string, object>)userDoc.Value;
                        Debug.Log(userObject["username"] + " : " + userObject["score"]);
                    }

                    for (int i = 0; i < ((Dictionary<string, object>)snapshot.Value).Count; i++) {
                        var keyValuePair = ((Dictionary<string, object>)snapshot.Value).ElementAt(i);
                        var userValuePair = ((Dictionary<string, object>)keyValuePair.Value);
                        names[i].text = "" + userValuePair["username"];
                        scores[i].text = "" + userValuePair["score"];
                    }
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

    public void ClosePanel() {
        leaderboardPanel.SetActive(false);
    }

    public void ForgotPasswordButton() {
        if (string.IsNullOrEmpty(forgotPasswordEmail.text)) {
            warningForgetPasswordText.text = $"No has colocado correo electronico";
            return;
        }

        FogotPassword(forgotPasswordEmail.text);
    }

    void FogotPassword(string forgotPasswordEmail) {

        FirebaseAuth.DefaultInstance.SendPasswordResetEmailAsync(forgotPasswordEmail)
            .ContinueWithOnMainThread(RestoreTask => { 

                if (RestoreTask.IsCanceled) {
                Debug.LogError($"El cambio de contraseña ha sido cancelado");
                }

                else if (RestoreTask.IsFaulted) {
                    foreach (FirebaseException exception in RestoreTask.Exception.Flatten().InnerExceptions) {
                        FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                        if (firebaseEx != null) {
                            var errorCode = (AuthError)firebaseEx.ErrorCode;
                        }
                    }
                }
                confirmationPasswordText.text = "El correo para reestablecer la contraseña ha sido enviado";
            });
    }
}

public class UserData {
    public int score;
    public string username;
}
