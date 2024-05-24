using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioRepair : MonoBehaviour, IInteractable
{
    [Header("Sprites")]
    [SerializeField] private Sprite brokenRadio;
    [SerializeField] private Sprite patchedRadio;
    [SerializeField] private Sprite fixedRadio;

    private SpriteRenderer _spriteRenderer;
    [SerializeField] private int RadioState = 1; //1 - 2 Broken, 3 - 4 Semi-Fixed, 5-6 Fixed;

    [SerializeField] private TMP_Text text;

    private void Start()
    {
        RadioState = 0;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Interact()
    {
        if (GameManager.Instance.CanFixRadio)
            RepairRadio();
    }
    
    private void RepairRadio()
    {
        //Replace Radio Spitesheet
        //Display dialog
        //Itirate Sequence
        AudioManager.instance.PlayOneShot(FMODEvents.instance.radioStatic, this.transform.position);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.fixingRadio, this.transform.position);
        Debug.Log("Fixing Radio");

        SwapSprite();
        //Display dialog
        if (RadioState > 5)
        {
            Debug.Log("Game End");
            //Play Final Cutscene
            SceneManagerScript.Instance.LoadScene("EndingCutScene");
        }
        else if (RadioState < 6)
        {
            Debug.Log("Playing Radio Dialogue");
            //play dialgoue based on radio state
        }

        RadioState++;
        text.text = RadioState.ToString("D2");
        GameManager.Instance.CanFixRadio = false;
    }

    private void SwapSprite()
    {
        switch (RadioState)
        {
            case 1:
            case 2:
                _spriteRenderer.sprite = brokenRadio;
                break;
            case 3:
            case 4:
                _spriteRenderer.sprite = patchedRadio;
                break;
            case 5:
            case 6:
                _spriteRenderer.sprite = fixedRadio;
                break;
        }
    } 
}