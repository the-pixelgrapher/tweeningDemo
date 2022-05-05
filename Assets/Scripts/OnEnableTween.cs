using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnEnableTween : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_canvas;
    [SerializeField] private StackedTweens m_tweenSettings;
    [SerializeField] private RepeatingTweens m_repeatingTweens;

    private Tween m_fadeTween = null;
    private Tween m_scaleTween = null;

    private void OnEnable()
    {
        ResetTween();

        m_fadeTween = m_canvas.DOFade(1.0f, m_tweenSettings.FadeDuration)
            .SetDelay(m_repeatingTweens.TweenInterval * transform.GetSiblingIndex());

        m_scaleTween = transform.DOScale(Vector3.one, m_tweenSettings.ScaleDuration)
            .SetDelay(m_repeatingTweens.TweenInterval * transform.GetSiblingIndex())
            .SetEase(m_tweenSettings.ScaleEase);
    }

    public void ResetTween()
    {
        m_fadeTween?.Kill(true);
        m_scaleTween?.Kill(true);

        m_canvas.alpha = 0.0f;
        transform.localScale = m_tweenSettings.ScaleStart;
    }
}
