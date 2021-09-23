using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Bests : MonoBehaviour
{
    [SerializeField] private Transform popup;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI[] texts;
    private bool opened = false;
    private Tween t;

   

    private void Awake()
    {
        var data = SaveManager.LoadData();
        for (int i = 0; i < texts.Length; i++)
        {
            if (data != null && data.score[i] > 0)
            {
                texts[i].text = $"{i+1} | {data.datetime[i]} | {data.score[i]}";
            }
        }

        button.onClick.AddListener(() =>
        {
            t.Kill();
            if (opened)
            {
                t = popup.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
            }
            else
            {

                t = popup.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            }
            opened = !opened;
        });

    }
}
