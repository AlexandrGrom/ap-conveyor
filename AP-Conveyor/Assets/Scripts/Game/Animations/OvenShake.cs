using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class OvenShake : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    private void Awake()
    {
        GameStateManager.OnGameStateChange += HandleState;
    }

    private void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= HandleState;
    }

    private void HandleState(GameState state)
    {
        if (state == GameState.Game)
        {
            StartCoroutine(nameof(Shake));
            ps.Play();
        }
        else
        {
            StopCoroutine(nameof(Shake));
            ps.Stop();
        }
    }

    IEnumerator Shake()
    {
        var waitTime = new WaitForSeconds(0.5f);
        while (true)
        {
            transform.DOShakePosition(0.5f, 0.05f, 5 , 90 , false , false);
            yield return waitTime;
        }
    }
}
