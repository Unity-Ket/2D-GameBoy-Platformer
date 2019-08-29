using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip diamondPickUpSFX;
    [SerializeField] int coinPoint = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().addPoint(coinPoint);
        AudioSource.PlayClipAtPoint(diamondPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
