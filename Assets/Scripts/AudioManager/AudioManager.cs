using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    private AudioClip shootAudioClip;
    private AudioClip hitAudioClip;

    public void PlayShootAudio() {
        if (shootAudioClip) { 
            //audioClip.
        }
    }

    public void PlayHitAudio() {
        if (hitAudioClip) { 
        
        }
    }
}
