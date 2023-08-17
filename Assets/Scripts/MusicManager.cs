using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private SaveFileManager SFM;
    private AudioSource aSource;
    private float trackTimer;
    private float songsPlayed;
    private bool[] beenPlayed;
    private string[] volumes;
    [Header("Music and SFX")]
    public AudioClip[] songs;
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public string musicExposedParam;
    public string sfxExposedParam;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // If there is no instance, set this instance as the persistent one and don't destroy it
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //TODO: load volumes for music/sfx
        SFM = FindObjectOfType<SaveFileManager>();
        volumes = SFM.LoadOptionInfo();
        if (volumes == null)
        {
            volumes = new string[2];
            volumes[0] = "-25";
            volumes[1] = "-25";
            musicMixer.SetFloat(musicExposedParam, float.Parse(volumes[0]));
            sfxMixer.SetFloat(sfxExposedParam, float.Parse(volumes[1]));
        }
        else
        {
            Debug.Log("Music File has things");
            //Debug.Log(volumes[0].ToString());
            //Debug.Log(volumes[1].ToString());
            //musicMixer.SetFloat(musicExposedParam, float.Parse(volumes[0]));
            //sfxMixer.SetFloat(sfxExposedParam, float.Parse(volumes[1]));
        }
    }

    void Start()
    {
        //get a couple of variables before starting the game
        //and check if there's a song playing currently.
        aSource = GetComponent<AudioSource>();
        beenPlayed = new bool[songs.Length];
        if (!aSource.isPlaying)
        {
            ChangeSong(Random.Range(0, songs.Length));
        }
    }

    void Update()
    {
        //if there's no song playing or song is done, play next song
        if (!aSource.isPlaying || trackTimer >= aSource.clip.length)
        {
            ChangeSong(Random.Range(0, songs.Length));
        }
        //if song's playing, increase time of it by 1
        if (aSource.isPlaying)
        {
            trackTimer += 1 * Time.deltaTime;
        }
        //if we played all the songs, reset the playlist
        ResetShuffle();
    }

    public void ChangeSong(int songPick)
    {
        //We created a bool list of songs that were already played.
        //if a song hasn't been played yet, change the bool of it
        //reset the track timer, place the song in the audio source
        //and play it.
        if (!beenPlayed[songPick])
        {
            trackTimer = 0;
            songsPlayed++;
            beenPlayed[songPick] = true;
            aSource.clip = songs[songPick];
            aSource.Play();
        }
        //otherwise, stop the song
        else
        {
            aSource.Stop();
        }
    }

    public void SetMusicLevel()
    {
        //Step 1: Expose the volume parameter on the mixer.
        //Step 2: rename it to whatever you want
        //Step 3: set the float of the mixer to the value of the slider
        musicMixer.SetFloat(musicExposedParam, musicSlider.value);
        //If it goes to the min value, silence it since the person wants it to be 0
        if (musicSlider.value == -50)
        {
            musicMixer.SetFloat(musicExposedParam, -100);
        }
        volumes[0] = musicSlider.value.ToString();
        volumes[1] = sfxSlider.value.ToString();
        SFM.SaveOptionInfo(volumes);
        Debug.Log("Data saved: music");
    }

    public void SetSFXLevel()
    {
        //Step 1: Expose the volume parameter on the mixer.
        //Step 2: rename it to whatever you want
        //Step 3: set the float of the mixer to the value of the slider
        sfxMixer.SetFloat(sfxExposedParam, sfxSlider.value);
        //If it goes to the min value, silence it since the person wants it to be 0
        if (sfxSlider.value == -50)
        {
            sfxMixer.SetFloat(sfxExposedParam, -100);
        }
        volumes[0] = musicSlider.value.ToString();
        volumes[1] = sfxSlider.value.ToString();
        SFM.SaveOptionInfo(volumes);
        Debug.Log("Data saved: volume");
    }

    private void ResetShuffle()
    {
        //if all the songs have been played, reset the list back to false
        //and refresh the playlist to play all of them again
        if (songsPlayed == songs.Length)
        {
            songsPlayed = 0;
            for (int i = 0; i < songs.Length; i++)
            {
                if (i == songs.Length)
                {
                    break;
                }
                else
                {
                    beenPlayed[i] = false;
                }
            }
        }
    }

    public void ChangeToOptions()
    {
        //Main menu, when changing to the options screen, find the sliders to change the volume
        musicSlider = GameObject.Find("Music Slider").GetComponent<Slider>();
        sfxSlider = GameObject.Find("SFX Slider").GetComponent<Slider>();
        musicSlider.value = float.Parse(volumes[0]);
        sfxSlider.value = float.Parse(volumes[1]);
    }
}