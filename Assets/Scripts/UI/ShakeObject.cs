using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShakeObject : MonoBehaviour
{
    public Ease IntroEaseType = Ease.OutBack;
    public float duration = 3.0f;
    public float strength = 2.0f;
    public RectTransform rect;
    void Start()
    {
        StartCoroutine(animate());
    }

    private IEnumerator animate()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(rect.DOShakeAnchorPos(duration, new Vector3(strength, strength, strength)).SetEase(IntroEaseType));

        yield return new WaitForSeconds(duration);

        StartCoroutine(animate());
    }
}
