using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;

    public void UpdateScore() {
        if(score != null) {
            score.text = GameManager.Instance.scoreCount.ToString();
        }
    }
}
