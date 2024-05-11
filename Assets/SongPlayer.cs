using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip song;

    // Boolean variable to track if the song is playing
    private bool isSongPlaying = false;

    // Function that turns on and off the song when button is pressed 
    public void ToggleSong()
    {
        // If the song is already playing, stop it
        if (isSongPlaying)
        {
            // Stops the Song
            audioSource.Stop();
            // Updates boolean to flase once the song is stopped
            isSongPlaying = false;
        }
        // If the song is not playing, play it
        else
        {
            // Song chosen to play
            audioSource.clip = song;
            // Plays the Song
            audioSource.Play();
            // Updates the boolean to true once the song is playing
            isSongPlaying = true;
        }
    }
}