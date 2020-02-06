using UnityEngine;
using System.Collections;

public class CharacterNotification : MonoBehaviour {

    private CharacterInteractionModel m_Interaction;
    private SpriteRenderer m_Renderer;
    public bool m_IsCloseEnoughToInteract;

    void Awake ()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
        m_Interaction = GetComponentInParent<CharacterInteractionModel>();
    }
	
	// Update is called once per frame
	public void SetIcon ()
    {
        m_IsCloseEnoughToInteract = m_Interaction.m_IsCloseEnoughToInteract;
        if (m_IsCloseEnoughToInteract == true)
        {
            StartCoroutine(SetIconWait());
        }
        if (m_IsCloseEnoughToInteract == false)
        {
            m_Renderer.enabled = false;
        }
	}

    IEnumerator SetIconWait()
    {
        yield return new WaitForSeconds(0.1f);
        if (m_IsCloseEnoughToInteract == false)
        {
            yield break;
        }
        m_Renderer.enabled = true;
    }
}
