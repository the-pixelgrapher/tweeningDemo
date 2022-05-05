using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepeatingTweens : MonoBehaviour
{
    public float TweenInterval = 0.1f;
    [SerializeField] private float m_maxTweenInterval = 1.0f;

    [SerializeField] GameObject m_playButton;
    [SerializeField] GameObject m_refreshButton;
    [SerializeField] GameObject m_contentPanel;

    [SerializeField] private Slider m_intervalSlider;
    [SerializeField] private TMP_Text m_intervalText;

    private bool m_tweenPlayed = false;

    void Start()
    {
        ResetTween();
        m_intervalSlider.value = TweenInterval / m_maxTweenInterval;
    }

    public void InitiateTween()
    {
        ResetTween();

        m_contentPanel.SetActive(true);

        m_tweenPlayed = true;
        UpdatePlayState();
    }

    public void ResetTween()
    {
        m_contentPanel.SetActive(false);
        m_tweenPlayed = false;
        UpdatePlayState();
    }

    private void UpdatePlayState()
    {
        m_playButton.SetActive(!m_tweenPlayed);
        m_refreshButton.SetActive(m_tweenPlayed);
    }

    public void UpdateTweenIntervalSlider()
    {
        TweenInterval = m_intervalSlider.value * m_maxTweenInterval;
        m_intervalText.text = TweenInterval.ToString("N3") + " s";
    }
}
