using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Expedition : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Go to Expedition");
        if (!GameManager.Instance.CanExped()) return;
            EventManager.ON_DOOR_OPEN?.Invoke();
            GoToExpedition();
    }
    
    private void GoToExpedition()
    {
        StartCoroutine("ExpedCutscene");
        //consume resources
    }

    IEnumerator ExpedCutscene()
    {
        yield return new WaitForSeconds(2);
        FinishExpedition();
    }

    private void FinishExpedition()
    {
        EventManager.ON_DOOR_CLOSE?.Invoke();
        GameManager.Instance.CanFixRadio = true;

        //Set tooltip for radio fix
    }
}