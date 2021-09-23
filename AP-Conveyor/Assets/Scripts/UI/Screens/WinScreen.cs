using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using ManagerPooling;


public class WinScreen : UIScreen
{
    [SerializeField] private Image back;
    [SerializeField] private Image youWin;
    [SerializeField] private Button retry;
    [SerializeField] private TextMeshProUGUI retryText;

    private void Awake()
    {
        retry.onClick.AddListener(() => GameStateManager.CurrentState = GameState.None);
        retryText.transform.
            DOScale(Vector3.one * 1.1f, 0.7f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    public override void DisablingAnimation()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(nameof(Disabling));
        }
    }
    IEnumerator Disabling()
    {
        yield return null;

        youWin.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0.2f);
        PoolManager.ReturnToRoot();
        DOTween.KillAll();
        yield return null;
        SceneManager.LoadScene(0);
        yield return null;
        gameObject.SetActive(false);

    }

    public override void EnablingAnimation()
    {
        gameObject.SetActive(true);
        StartCoroutine(nameof(Enabling));
    }

    IEnumerator Enabling()
    {
        youWin.DOFade(0, 0);
        retryText.DOFade(0, 0);
        back.DOFade(0, 0);
        yield return new WaitForSeconds(0.5f);
        yield return null;
        back.DOFade(0.65f, 0.2f);
        youWin.DOFade(1, 0.2f);
        youWin.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InCubic);
        yield return null;
        yield return new WaitForSeconds(1.5f);
        retryText.DOFade(1, 0.2f);
    }
}
