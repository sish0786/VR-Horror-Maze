using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    private Animator doorAnimator;   // The Animator on the DoorHinge
    private AudioSource audioSource; // The AudioSource on the DoorHinge
    public AudioClip doorOpenClip;  // Drag your doorOpen sound clip here in the inspector
    public AudioClip doorCloseClip; // Drag your doorClose sound clip here in the inspector
    private bool isOpen = false;    // Track door state

    void Start(){
        doorAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    // This function will be called to toggle the door animation
    public void ToggleDoorAnimation()
    {
        Debug.Log("TriggeredAction Door");
        isOpen = !isOpen;

        doorAnimator.SetTrigger("toggleDoor");

        // Play the respective sound
        if (isOpen)
        {
            audioSource.clip = doorOpenClip;
        }
        else
        {
            audioSource.clip = doorCloseClip;
        }
        audioSource.Play();
    }
}
