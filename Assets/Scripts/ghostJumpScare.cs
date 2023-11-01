using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    public GameObject ghost; // Drag your ghost GameObject here
    public AudioSource jumpscareAudio; // Drag your AudioSource component here
    public float ghostSpeed = 5f; // Speed at which ghost moves towards player
    private Transform playerFace; // Player's Camera (face) Transform component
    private bool hasTriggered = false; // To ensure the jumpscare happens only once

    private void Start()
    {
        // Using main camera as a reference for the player's face in XR
        playerFace = Camera.main.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger and the jumpscare hasn't been triggered yet
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            ActivateGhost();
        }
    }

    private void ActivateGhost()
    {
        ghost.SetActive(true);
        // Start audio playback
        if(jumpscareAudio != null && !jumpscareAudio.isPlaying)
        {
            jumpscareAudio.Play();
        }
        // Start moving the ghost towards the player's face
        StartCoroutine(MoveGhost());
    }

    private System.Collections.IEnumerator MoveGhost()
    {
        while (Vector3.Distance(ghost.transform.position, playerFace.position) > 0.5f) // Adjust the distance as needed
        {
            Vector3 moveDirection = (playerFace.position - ghost.transform.position).normalized;
            ghost.transform.position += moveDirection * ghostSpeed * Time.deltaTime;
            yield return null;
        }

        // Optional: Deactivate ghost after it reaches the player's face or after some time
        ghost.SetActive(false);
    }
}
