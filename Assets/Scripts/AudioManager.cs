using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource defaultSoundtrack;
    public AudioSource dragonQuestSoundtrack;
    public AudioSource questCompletionSound; // Add AudioSource for quest completion sound


    void Start()
    {
        PlayDefaultSoundtrack();
    }

    public void PlayDefaultSoundtrack()
    {
        if (dragonQuestSoundtrack.isPlaying)
        {
            dragonQuestSoundtrack.Stop();
        }
        defaultSoundtrack.Play();
    }

    public void PlayDragonQuestSoundtrack()
    {
        if (defaultSoundtrack.isPlaying)
        {
            defaultSoundtrack.Stop();
        }
        dragonQuestSoundtrack.Play();
    }

    public void PlayQuestCompletionSound()
    {
        questCompletionSound.Play(); // Play quest completion sound
    }
}
