using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public PlayerController player;
    public ZombieSpawner zombieSpawner;
    public ItemSpawner itemSpawner;
    void Start()
    {
        Init();
        player.OnDeath += () => 
        {
            GameManager.instance.GameOverGO.SetActive(true);
        };
    }

    void Init()
    {
        player.Init();
        zombieSpawner.Init();
        itemSpawner.Init();
    }
}
