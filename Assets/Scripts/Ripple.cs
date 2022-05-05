using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Ripple : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform m_rippleContainer = null;
    [SerializeField] private GameObject m_rippleObject = null;
    [SerializeField] private float m_tweenDuration = 0.5f;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject rippleObj = Instantiate(m_rippleObject, Input.mousePosition, Quaternion.identity);
        CanvasGroup rippleCanvas = rippleObj.GetComponent<CanvasGroup>();

        Vector3 endScale = rippleObj.transform.localScale;
        rippleObj.transform.SetParent(m_rippleContainer);
        rippleObj.transform.localScale = Vector3.zero;

        rippleObj.transform.DOScale(endScale, m_tweenDuration);
        rippleCanvas
            .DOFade(0.0f, m_tweenDuration)
            .OnComplete(() => Destroy(rippleObj) );
    }
}
