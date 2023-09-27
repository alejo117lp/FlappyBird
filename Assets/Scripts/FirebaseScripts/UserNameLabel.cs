using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserNameLabel : MonoBehaviour {
    [SerializeField] TMP_Text label;

    private void Reset() {
        label = GetComponent<TMP_Text>();
    }

    private void Start() {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthChange;
    }

    private void HandleAuthChange(object sender, EventArgs e) {
        var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;

        if(currentUser != null) {
            SetLabelUsername(currentUser.UserId);
        }
    }

    private void SetLabelUsername(string userId) {
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + userId + "/username")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted) {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted) {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log(snapshot.Value);
                    label.text = (string)snapshot.Value;
                }
            });
    }
}
