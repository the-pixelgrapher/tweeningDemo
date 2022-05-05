using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BasicMoveTo : MonoBehaviour
{
    [SerializeField] private Transform m_moveObject = null;
    [SerializeField] private float m_tweenDuration = 0.5f;
    [SerializeField] private float m_maxTweenDuration = 2.0f;
    [SerializeField] private Ease m_easeMode = Ease.OutQuad;

    [SerializeField] private TMP_Dropdown m_easeDropdown;
    [SerializeField] private Slider m_tweenDurationSlider;
    [SerializeField] private TMP_Text m_tweenDurationText;

    private Tween m_tween = null;

    private void Start()
    {
        m_easeDropdown.ClearOptions();

        int easeCount = System.Enum.GetValues(typeof(Ease)).Length;
        List<string> dropdownOptions = new List<string>();

        for (int i = 0; i < easeCount - 6; i++)
        {
            Ease curEase = (Ease)i;
            dropdownOptions.Add(curEase.ToString());
        }

        m_easeDropdown.AddOptions(dropdownOptions);
        m_easeDropdown.value = (int)m_easeMode;

        m_tweenDurationSlider.value = m_tweenDuration / m_maxTweenDuration;
    }

    public void OnClick()
    {
        m_tween?.Kill();

        m_tween = m_moveObject.DOMove(Input.mousePosition, m_tweenDuration).SetEase(m_easeMode);
    }

    public void UpdateEaseModeFromDropdown()
    {
        m_easeMode = (Ease)m_easeDropdown.value;
    }

    public void UpdateTweenDurationFromSlider()
    {
        m_tweenDuration = m_tweenDurationSlider.value * m_maxTweenDuration;
        m_tweenDurationText.text = m_tweenDuration.ToString("N2") + " s";
    }

}
