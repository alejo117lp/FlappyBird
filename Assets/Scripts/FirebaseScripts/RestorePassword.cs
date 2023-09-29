using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestorePassword : MonoBehaviour
{
    [SerializeField] TMP_InputField forgotPasswordEmail;
    [SerializeField] private TMP_Text warningForgetPasswordText;
    [SerializeField] TMP_Text confirmationPasswordText;

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
