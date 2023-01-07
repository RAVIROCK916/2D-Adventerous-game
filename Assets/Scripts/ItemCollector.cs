using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherriesCollected = 0;

    [SerializeField] private Text score;

    [SerializeField] private AudioSource collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cherry"))
        {
            collectSound.Play();
            Destroy(collision.gameObject);
            cherriesCollected++;
            score.text = "Score: " + cherriesCollected;
        }
    }
}
