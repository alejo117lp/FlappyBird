using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimeSelector : MonoBehaviour
{
    [Header("Prefab pipe")]
    [SerializeField] GameObject pipe;

    [Header("Background")]
    [SerializeField] SpriteRenderer backgroundRenderer;

    [Header("Sprites pipes Day")]
    [SerializeField] Sprite pipeUpDay;
    [SerializeField] Sprite pipeDownDay;

    [Header("Sprites pipes Night")]
    [SerializeField] Sprite pipeUpNight;
    [SerializeField] Sprite pipeDownNight;

    [Header("Sprites Background")]
    [SerializeField] Sprite backgroundNight;
    [SerializeField] Sprite backgoroundDay;

    SpriteRenderer[] pipeRenderer;

    private void Awake() {

        pipeRenderer = pipe.GetComponentsInChildren<SpriteRenderer>();

        int random = Random.Range(0, 2); // Night = 0, Day = 1.

        if(random == 0) {
            pipeRenderer[0].sprite = pipeUpNight;
            pipeRenderer[1].sprite = pipeDownNight;
            backgroundRenderer.sprite = backgroundNight;
        }

        else {
            pipeRenderer[0].sprite = pipeUpDay;
            pipeRenderer[1].sprite = pipeDownDay;
            backgroundRenderer.sprite = backgoroundDay;
        }
    }
}
