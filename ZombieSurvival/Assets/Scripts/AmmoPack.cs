using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour , IItem
{
    int plusAmmoAmount = 20;
    public void Use(PlayerController player)
    {
        if (player != null && player.CompareTag("Player"))
        {
            player.gun.currentAmmoCount += plusAmmoAmount;
            if (player.gun.currentAmmoCount > player.gun.gunData.maxAmmoCount)
                player.gun.currentAmmoCount = player.gun.gunData.maxAmmoCount;
            GameManager.instance.UpdateAmmoUI($"{player.gun.currentMagAmmoCount}/{player.gun.gunData.magCount} | {player.gun.currentAmmoCount}");

            Destroy(gameObject);
        }
    }
}
