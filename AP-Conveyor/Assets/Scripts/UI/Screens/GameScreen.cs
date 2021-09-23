using System.Collections;
using UnityEngine;
using DG.Tweening;


public class GameScreen : UIScreen
{
    [SerializeField] private RectTransform filler;

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

        filler.DOAnchorPos(new Vector2(0, 1000), 0.2f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        
    }

    public override void EnablingAnimation()
    {
        gameObject.SetActive(true);
        StartCoroutine(nameof(Enabling));
    }

    IEnumerator Enabling()
    {
        filler.anchoredPosition = new Vector2(0 , 1000);
        yield return null;
        filler.DOAnchorPos(new Vector2(0, 620), 0.2f).SetEase(Ease.OutBack);
    }
}
