using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] m_tabs;
    [SerializeField] private GameObject m_tabSelectionIndicator;
    [SerializeField] private CanvasGroup m_titlePanel;
    [SerializeField] private CanvasGroup[] m_contentPanels;

    private int m_selectedTab = -1;

    void Start()
    {
        UpdateSelectedTab(m_selectedTab);
    }

    public void SelectPanel(int panel)
    {
        if (m_selectedTab == -1)
        {
            m_tabSelectionIndicator.transform.DOMoveX(m_tabs[panel].transform.position.x, 0.0f);
        }

        if (panel != m_selectedTab)
        {
            float diff = Mathf.Abs((float)m_selectedTab - panel);
            m_selectedTab = panel;
            UpdateSelectedTab(panel, diff);
        }
    }

    private void UpdateSelectedTab(int tab, float diff = 0.0f)
    {
        if (tab < 0)
        {
            m_tabSelectionIndicator.SetActive(false);
            m_titlePanel.gameObject.SetActive(true);
            ShowPanel(m_titlePanel);
        }
        else
        {
            m_titlePanel.gameObject.SetActive(false);
            m_tabSelectionIndicator.SetActive(true);

            for (int i = 0; i < m_contentPanels.Length; i++)
            {
                if (i == tab)
                {
                    ShowPanel(m_contentPanels[i]);
                }
                else
                {
                    m_contentPanels[i].gameObject.SetActive(false);
                }
            }

            m_tabSelectionIndicator.transform.DOKill(true);
            m_tabSelectionIndicator.transform.DOPunchScale(new Vector3(diff, 0.0f, 0.0f), 0.3f, 1, 0.0f);

            m_tabSelectionIndicator.transform.DOMoveX(m_tabs[tab].transform.position.x, 0.3f);
        }
    }

    private void ShowPanel(CanvasGroup canvas)
    {
        canvas.gameObject.SetActive(true);
        canvas.alpha = 0.0f;
        canvas.DOKill();
        canvas.DOFade(1.0f, 0.2f);
    }
}
