using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup m_blanket = null;
    [SerializeField] private CanvasGroup m_window = null;

    [SerializeField] private float m_blanketFadeTime = 0.25f;
    [SerializeField] private float m_windowFadeTime = 0.175f;
    [SerializeField] private float m_windowScaleTime = 0.35f;
    [SerializeField] private Vector3 m_windowStartScale = Vector3.one;
    [SerializeField] private Ease m_windowScaleEase = Ease.OutBack;

    private Tween m_blanketTween = null;
    private Tween m_windowFadeTween = null;
    private Tween m_windowScaleTween = null;

    private bool m_isShowing = false;

    public void OpenWindow()
    {
        KillAllTweens();

        if (m_blanket != null)
        {
            m_blanket.gameObject.SetActive(true);
            m_blanketTween = m_blanket.DOFade(1.0f, m_blanketFadeTime);
        }

        if (m_window != null)
        {
            m_window.gameObject.SetActive(true);
            m_windowFadeTween = m_window.DOFade(1.0f, m_windowFadeTime);
            m_window.transform.localScale = m_windowStartScale;
            m_windowScaleTween = m_window.transform.DOScale(Vector3.one, m_windowScaleTime).SetEase(m_windowScaleEase);
        }

        m_isShowing = true;
    }

    public void CloseWindow()
    {
        KillAllTweens();

        if (m_blanket != null)
        {
            m_blanketTween = m_blanket.DOFade(0.0f, m_blanketFadeTime)
                .OnComplete(() => 
                    m_blanket.gameObject.SetActive(false)
                );
        }

        if (m_window != null)
        {
            m_windowFadeTween = m_window.DOFade(0.0f, m_windowFadeTime)
                .OnComplete(() => 
                    m_window.gameObject.SetActive(false)
                );

            m_windowScaleTween = m_window.transform.DOScale(m_windowStartScale, m_windowScaleTime);
        }

        m_isShowing = false;
    }

    public void ToggleWindow()
    {
        if (m_isShowing)
        {
            CloseWindow();
        }
        else
        {
            OpenWindow();
        }
    }

    private void KillAllTweens()
    {
        m_blanketTween?.Kill(true);
        KillWindowTweens();
    }

    private void KillWindowTweens()
    {
        m_windowFadeTween?.Kill(true);
        m_windowScaleTween?.Kill(true);
    }
}
