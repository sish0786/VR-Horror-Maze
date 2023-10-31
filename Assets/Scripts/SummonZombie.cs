using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonZombie : MonoBehaviour
{
    public GameObject zombie;
    public Collider trigger;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            zombie.SetActive(true);
            trigger.enabled = false;
        }
    }
}
