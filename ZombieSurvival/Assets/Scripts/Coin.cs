using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    public void Use(PlayerController player)
    {
        if (player != null && player.CompareTag("Player"))
        {
            GameManager.instance.score += 50;
            GameManager.instance.UpdateScore();

            Destroy(gameObject);
        }
    }
}
