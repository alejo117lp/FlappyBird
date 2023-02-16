using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] RuntimeAnimatorController [] newControllers;

    private void Awake() {
        int random = Random.Range(0, newControllers.Length);
        animator.runtimeAnimatorController = newControllers[random];
    }

    /*[SerializeField] Sprite[] birdsBodies;
    [SerializeField] SpriteRenderer birdBody;
        int random = Random.Range(0, birdsBodies.Length);
        birdBody.sprite = birdsBodies[random];*/

    }
