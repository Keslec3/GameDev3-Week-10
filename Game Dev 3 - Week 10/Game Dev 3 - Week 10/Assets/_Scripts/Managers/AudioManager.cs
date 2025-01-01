using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevWithMarco.DataSO;

namespace GameDevWithMarco.Managers
{
public class AudioManager : Singleton<AudioManager>
{
    /// <summary>
    /// This script will drive anything related to Audio
    /// </summary>

    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] private SoundSO goodPickupSound;
    [SerializeField] private SoundSO badPickupSound;
    [SerializeField] private SoundSO dashSound;
    [SerializeField] private SoundSO lifeSoundData;
    [SerializeField] AudioSource audioSource_Music;
    [SerializeField] AudioSource audioSource_Sounds;

    // Start is called before the first frame update
    void Start()
    { 
        if (backgroundMusic != null)
        {
            PlayBackgroundMusic();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (backgroundMusic != null)
        {
            audioSource_Music.volume += 0.1f * Time.deltaTime;
            if (audioSource_Music.volume >= 0.2f)
            {
                audioSource_Music.volume = 0.2f;
            }
        }
    }
    
    private void PlaySound(float lowPitchRange, float highPitchRange, AudioClip clipToPlay, float volume)
    {
       audioSource_Sounds.pitch = Random.Range(lowPitchRange, highPitchRange);
       audioSource_Sounds.PlayOneShot(clipToPlay);
       audioSource_Sounds.volume = volume;
    }


    public void GoodPickupSound() // good pickup sounds
{
        PlaySound(
        goodPickupSound.lowPitchRange,
        goodPickupSound.highPitchRange,
        goodPickupSound.clipToUse,
        goodPickupSound.volume);
}

public void BadPickupSound() // red pick up sounds
{
        PlaySound(
        badPickupSound.lowPitchRange,
        badPickupSound.highPitchRange,
        badPickupSound.clipToUse,
        badPickupSound.volume);
}

    public void PlayBackgroundMusic() // background music
    {

        audioSource_Music.volume = 0f;
        audioSource_Music.clip = backgroundMusic;
        audioSource_Music.Play();
        audioSource_Music.loop = true;
    }
    public void DashSound() // dash pickup sounds
{
        PlaySound(
        dashSound.lowPitchRange,
        dashSound.highPitchRange,
        dashSound.clipToUse,
        dashSound.volume);
}

public void LifePickupSound() //life pickup sounds
{
        PlaySound(
        lifeSoundData.lowPitchRange,
        lifeSoundData.highPitchRange,
        lifeSoundData.clipToUse,
        lifeSoundData.volume);
}

}
}