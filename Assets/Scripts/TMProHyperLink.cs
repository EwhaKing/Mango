using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMProHyperLink : MonoBehaviour, IPointerClickHandler
{
    TextMeshProUGUI m_TextMeshPro;
    Camera m_Camera;
    Canvas m_Canvas;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;

        m_Canvas = gameObject.GetComponentInParent<Canvas>();
        if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            m_Camera = null;
        }
        else
        {
            m_Camera = m_Canvas.worldCamera;
        }
        m_TextMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        m_TextMeshPro.ForceMeshUpdate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_TextMeshPro, Input.mousePosition, m_Camera);
        if(linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = m_TextMeshPro.textInfo.linkInfo[linkIndex];
            Application.OpenURL(linkInfo.GetLinkID());
        }
    }
}
