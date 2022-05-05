using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class StackedTweens : MonoBehaviour
{
    public float FadeDuration { get { return m_fadeDuration; } }
    public float ScaleDuration { get { return m_scaleDuration; } }
    public Vector3 ScaleStart { get { return m_scaleStart; } }
    public Ease ScaleEase { get { return m_scaleEase; } }

    [SerializeField] GameObject m_playButton;
    [SerializeField] GameObject m_refreshButton;

    [SerializeField] CanvasGroup m_tweenObject;

    [SerializeField] private float m_defaultTweenDuration = 0.5f;
    [SerializeField] private float m_maxTweenDuration = 2.0f;

    [SerializeField, Header("Fade")] private float m_fadeDuration = 0.5f;
    [SerializeField] private Slider m_fadeDurationSlider;
    [SerializeField] private TMP_Text m_fadeDurationText;

    [SerializeField, Header("Scale")] private float m_scaleDuration = 0.5f;
    [SerializeField] private Vector3 m_scaleStart = Vector3.zero;
    [SerializeField] private Ease m_scaleEase = Ease.OutQuad;
    [SerializeField] private TMP_Dropdown m_scaleEaseDropdown;
    [SerializeField] private Slider m_scaleDurationSlider;
    [SerializeField] private TMP_Text m_scaleDurationText;
    [SerializeField] private TMP_InputField m_scaleXInput;
    [SerializeField] private TMP_InputField m_scaleYInput;

    private Tween m_fadeTween = null;
    private Tween m_scaleTween = null;
    private bool m_tweenPlayed = false;

    private void Start()
    {
        InitializeSettingsUI();
        ResetTween();
        UpdatePlayState();
    }

    private void InitializeSettingsUI()
    {
        m_scaleEaseDropdown.ClearOptions();

        int easeCount = System.Enum.GetValues(typeof(Ease)).Length;
        List<string> dropdownOptions = new List<string>();

        for (int i = 0; i < easeCount - 6; i++)
        {
            Ease curEase = (Ease)i;
            dropdownOptions.Add(curEase.ToString());
        }

        m_scaleEaseDropdown.AddOptions(dropdownOptions);
        m_scaleEaseDropdown.value = (int)m_scaleEase;

        m_scaleXInput.text = m_scaleStart.x.ToString();
        m_scaleYInput.text = m_scaleStart.y.ToString();
        m_scaleDurationSlider.value = m_defaultTweenDuration / m_maxTweenDuration;
        m_fadeDurationSlider.value = m_defaultTweenDuration / m_maxTweenDuration;
    }

    public void InitiateTween()
    {
        ResetTween();

        m_fadeTween = m_tweenObject.DOFade(1.0f, m_fadeDuration);

        m_scaleTween = m_tweenObject.transform.DOScale(Vector3.one, m_scaleDuration)
            .SetEase(m_scaleEase);

        m_tweenPlayed = true;
        UpdatePlayState();
    }

    public void ResetTween()
    {
        m_fadeTween?.Kill(true);
        m_scaleTween?.Kill(true);

        m_tweenObject.alpha = 0.0f;
        m_tweenObject.transform.localScale = m_scaleStart;

        m_tweenPlayed = false;
        UpdatePlayState();
    }

    private void UpdatePlayState()
    {
        m_playButton.SetActive(!m_tweenPlayed);
        m_refreshButton.SetActive(m_tweenPlayed);
    }

    public void UpdateScaleEaseDropdown()
    {
        m_scaleEase = (Ease)m_scaleEaseDropdown.value;
    }

    public void UpdateScaleDurationFromSlider()
    {
        m_scaleDuration = m_scaleDurationSlider.value * m_maxTweenDuration;
        m_scaleDurationText.text = m_scaleDuration.ToString("N2") + " s";
    }

    public void UpdateStartScaleInput()
    {
        m_scaleStart = new Vector3(float.Parse(m_scaleXInput.text), float.Parse(m_scaleYInput.text), 1.0f);

        m_scaleStart.x = Mathf.Clamp(m_scaleStart.x, 0.0f, 10.0f);
        m_scaleStart.y = Mathf.Clamp(m_scaleStart.y, 0.0f, 10.0f);

        m_scaleXInput.text = m_scaleStart.x.ToString();
        m_scaleYInput.text = m_scaleStart.y.ToString();
    }

    public void UpdateFadeDurationFromSlider()
    {
        m_fadeDuration = m_fadeDurationSlider.value * m_maxTweenDuration;
        m_fadeDurationText.text = m_fadeDuration.ToString("N2") + " s";
    }
}
