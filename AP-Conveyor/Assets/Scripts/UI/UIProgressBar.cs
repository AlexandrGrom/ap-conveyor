using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform fillImage;
    [Range(0f, 1f)]
    [SerializeField] private float fillAmount;

    public float FillAmount
    {
        get => fillAmount;
        set
        {
            fillAmount = value;
            UpdateImage();
        }
    }

    private void Reset()
    {
        fillImage = GetComponent<RectTransform>();
    }

    private void OnValidate()
    {
        UpdateImage();
    }

    private void UpdateImage()
    {
        fillImage.anchorMax = new Vector2(FillAmount, 1f);
    }
}
