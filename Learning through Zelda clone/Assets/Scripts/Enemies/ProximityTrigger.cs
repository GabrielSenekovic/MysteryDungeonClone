using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTrigger : MonoBehaviour {
    CharacterEnemyGeneralControl m_Control;
    private void Awake()
    {
        m_Control = GetComponentInParent<CharacterEnemyGeneralControl>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            m_Control.SetCharacterInRange(collider.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            m_Control.SetCharacterInRange(null);
        }
    }
}
