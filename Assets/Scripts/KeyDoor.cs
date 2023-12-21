using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType keyType;

    public Key.KeyType GetKeyType() { return keyType; }

    public GameObject colliderToRemove;

    public GameObject openDoor;
    public GameObject closeDoor;

    public AudioClip openDoorSound;
    
    public void OpenDoor()
    {
        colliderToRemove.SetActive(false);
        closeDoor.SetActive(false);
        openDoor.SetActive(true);
        AudioSource.PlayClipAtPoint(openDoorSound, transform.position);
    }

    
}
