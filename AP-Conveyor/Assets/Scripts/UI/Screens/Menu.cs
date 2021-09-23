using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using TMPro;

public class Menu : UIScreen
{
    [SerializeField] private Button playButton;
    [SerializeField] private Transform bests;
    [SerializeField] private Transform popup;
    [SerializeField] private TextMeshProUGUI timer;

    Tween t;
    private void Awake()
    {
        playButton.onClick.AddListener(()=>GameStateManager.CurrentState = GameState.Game);
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
        t.Kill();
        timer.transform.localScale = Vector3.zero;
        playButton.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        popup.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        bests.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0.3f);

        for (int i = 3; i > 0; i--)
        {
            timer.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
            timer.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.4f);
            timer.transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.OutCubic);
            timer.transform.DORotate(Vector3.forward * 720, 0.6f,RotateMode.FastBeyond360);
            yield return new WaitForSeconds(0.6f);
        }

        timer.text = "GO!";
        yield return new WaitForSeconds(0.2f);
        timer.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(0.4f);
        timer.transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.OutCubic);
        timer.transform.DORotate(Vector3.forward * 720, 0.6f, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(0.6f);

        gameObject.SetActive(false);
        
    }

    public override void EnablingAnimation()
    {
        gameObject.SetActive(true);
        StartCoroutine(nameof(Enabling));
    }

    IEnumerator Enabling()
    {
        playButton.transform.localScale = Vector3.zero;
        bests.transform.localScale = Vector3.zero;
        popup.transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        playButton.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(()=> 
        {
            t = playButton.transform.
                DOScale(Vector3.one * 1.1f, 0.7f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        });

        bests.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
    }

}
