using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterAnimationController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip desiredSong;
    public Transform mapBounds; // Reference to the map's bounding box
    public GameObject flagObject; // Flag Object
    public float xRangeMin = -5f; // Minimum x value relative to map bounds
    public float xRangeMax = 5f; // Maximum x value relative to map bounds
    // public float animationDelay = 5f; // Delay before triggering animation
    public float timeLimit = 30f; // Time limit for the player to touch the flag
    public TextMeshProUGUI objectiveText; // Reference to the UI Text element
    public TextMeshProUGUI  countdownText;  // Reference to the UI Text element
    public Sprite newCharacterSprite; // New sprite for the character


    private bool isSongPlaying = false;
    // private bool isAnimationTriggered = false;
    private bool isObjectiveDisplayed = false;
    private bool isFlagTouched = false;
    private float timer = 0f;

    void Update()
    {
        // Check if desired song is playing
        if (audioSource.isPlaying && audioSource.clip == desiredSong)
        {
            isSongPlaying = true;
        }
        else
        {
            isSongPlaying = false;
            // isAnimationTriggered = false; // Reset animation trigger if song changes
        }

        // Check character's x coordinate against range
        if (isSongPlaying && IsCharacterInXRange())        //&& !isAnimationTriggered)
        {
            timer += Time.deltaTime;
            
            // Display objective message and start countdown timer
            if (!isObjectiveDisplayed)
            {
                // Start countdown timer
                DisplayObjectiveMessage();
                isObjectiveDisplayed = true;
            }

            // Update countdown timer text
            float timeRemaining = Mathf.Max(0f, timeLimit - timer);
            UpdateCountdownText(timeRemaining);

            // Check if player touches the flag before time runs out
            if (isFlagTouched && timeRemaining > 0f)
            {
                ChangeCharacterSprite();
                //DisplayCelebratoryMessage();
                //isAnimationTriggered = true;
            }
        }
         else
         {
            // Reset timer if conditions are not met
            timer = 0f;
            isObjectiveDisplayed = false;
         }  
    } 
            
        

    bool IsCharacterInXRange()
    {
        // Map character's position to local space relative to map bounds
        Vector3 localPosition = mapBounds.InverseTransformPoint(transform.position);

        // Check if character's x coordinate is within the defined range
        return localPosition.x >= xRangeMin && localPosition.x <= xRangeMax;
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            isFlagTouched = true;
            // Perform actions when the player touches the flag
            Debug.Log("Player touched the flag!");
        }
    }
   
    void ChangeCharacterSprite()
    {
        // Change character sprite to the new sprite
        GetComponent<SpriteRenderer>().sprite = newCharacterSprite;
        
        // Swaps the character sprite
       // SpriteRenderer characterSpriteRenderer = GetComponent<SpriteRenderer>();
        //if (characterSpriteRenderer != null && newCharacterSprite != null)
        //{
       //     characterSpriteRenderer.sprite = newCharacterSprite;
       // }
    }

    void DisplayObjectiveMessage()
    {
    objectiveText.text = "The Tama likes this scenery and song! Jump to the top and touch the flag!    If you successed before the time runs out, your Tama will transform!";
    }

    void UpdateCountdownText(float timeRemaining)
    {
        countdownText.text = "Time Remaining: " + Mathf.CeilToInt(timeRemaining).ToString();
    }

    //void DisplayCelebratoryMessage()
   // {
   // countdownText.text = "Congratulations! You completed the objective!";
   // }

}