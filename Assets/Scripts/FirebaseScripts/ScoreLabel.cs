using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    [SerializeField] TMP_Text label;

    private void Reset() {
        label = GetComponent<TMP_Text>();
    }

    private void Start() {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.GetReference("users/" + userId + "/score")
            .ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args) {
        

        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        label.text = "" + args.Snapshot.Value;
    }
}
