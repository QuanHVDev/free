
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : SingletonBehaviourDontDestroy<SFX> {
    [SerializeField] private AudioSource source;

    [SerializeField] private AudioClip correctClip;
    [SerializeField] private AudioClip incorrectClip;

    private bool isCanPlay = true;
    public void PlayOneShoot(AudioClip clip) {
        if (isCanPlay) {
            source.clip = clip;
            source.Play();
        }
    }
    
    public void PlayCorrect() {
        PlayOneShoot(correctClip);
    }
    
    public void PlayIncorrect() {
        PlayOneShoot(incorrectClip);
    }

    public void PlaySoundButton()
    {
    }

    public void PlayCurrencyCounting()
    {
        
    }
}
