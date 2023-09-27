using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Cerr� Sesi�n");
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene("Registration");
    }
}
