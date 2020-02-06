using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAttackCollider : WeaponCollider {
    bool coroutineStarted = false;
    // Update is called once per frame
    void Update ()
    {
        if (m_Collider.enabled == true && coroutineStarted == false)
        {
            StartCoroutine(DisableAttack());
            coroutineStarted = true;
        }
	}
    private IEnumerator DisableAttack()
    {
        //For physical attacks
        yield return new WaitForSeconds(0.2f);
        m_Collider.enabled = false;
        coroutineStarted = false;
    }
}
