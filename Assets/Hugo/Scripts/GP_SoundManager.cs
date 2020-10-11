using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GP_SoundManager : MonoBehaviour
{
    public static GP_SoundManager I { get; private set; }

    [SerializeField] AudioSource dialogue = null;
    [SerializeField] AudioSource sound = null;

    [SerializeField] AudioClip sonAlbi = null;
    [SerializeField] AudioClip blasonAlbi = null;
    [SerializeField] AudioClip infoAlbi = null;
    [SerializeField] AudioClip doneAlbi = null;

    [SerializeField] AudioClip sonMTP = null;
    [SerializeField] AudioClip blasonMTP = null;
    [SerializeField] AudioClip infoMTP = null;
    [SerializeField] AudioClip doneMTP = null;

    [SerializeField] AudioClip sonLourdes = null;
    [SerializeField] AudioClip blasonLourdes = null;
    [SerializeField] AudioClip infoLourdes = null;
    [SerializeField] AudioClip doneLourdes = null;



    private void Awake()
    {
        I = this;
    }


    public void Playdialogue(SoundsDialogue _s)
    {
        AudioClip _clip = null;
        switch (_s)
        {
            case SoundsDialogue.sonAlbi:
                _clip = sonAlbi;
                break;
            case SoundsDialogue.sonMTP:
                _clip = sonMTP;
                break;
            case SoundsDialogue.sonLourdes:
                _clip = sonLourdes;
                break;
            case SoundsDialogue.blasonAlbi:
                _clip = blasonAlbi;
                break;
            case SoundsDialogue.blasonMTP:
                _clip = blasonMTP;
                break;
            case SoundsDialogue.blasonLourdes:
                _clip = blasonLourdes;
                break;
            case SoundsDialogue.infoAlbi:
                _clip = infoAlbi;
                break;
            case SoundsDialogue.infoMTP:
                _clip = infoMTP;
                break;
            case SoundsDialogue.infoLourdes:
                _clip = infoLourdes;
                break;
            case SoundsDialogue.doneAlbi:
                _clip = doneAlbi;
                break;
            case SoundsDialogue.doneMTP:
                _clip = doneMTP;
                break;
            case SoundsDialogue.doneLourdes:
                _clip = doneLourdes;
                break;
        }
        if (dialogue.isPlaying) dialogue.Stop();
        dialogue.clip = _clip;
        dialogue.Play();
    }

    public void PlaySound(Sounds _s)
    {

    }










}

public enum Sounds
{

}

public enum SoundsDialogue
{
    sonAlbi,
    sonMTP,
    sonLourdes,
    blasonAlbi,
    blasonMTP,
    blasonLourdes,
    infoAlbi,
    infoMTP,
    infoLourdes,
    doneAlbi,
    doneLourdes,
    doneMTP
}
