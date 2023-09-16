using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource aSource;
    public AudioClip[] songs;

    //This is attached to music manager
    //don't need to create an instance of it
    public void PlaySound()
    {
        //if for some reason we forget to find audio source
        //find it and place it as this
        if (aSource == null)
        {
            aSource = this.GetComponent<AudioSource>();
        }
        //Randomize the sound that it's going to play
        int pick = Random.Range(0, songs.Length);
        //place the sound in the audio source
        aSource.clip = songs[pick];
        //play it
        aSource.Play();
    }
}
