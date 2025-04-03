using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour,IItem
{
    int plusHpAmount = 20;

    public void Use(PlayerController player)
    {
        if (player != null && player.CompareTag("Player"))
        {
            player.hp += plusHpAmount;
            if (player.hp > player.maxHp) player.hp = player.maxHp;

            GameManager.instance.UpdatePlayerHPBar(player.hp / player.maxHp);

            Destroy(gameObject);
        }
    }
}
