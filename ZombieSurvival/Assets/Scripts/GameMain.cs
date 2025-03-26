using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public PlayerController player;
    void Start()
    {
        Init();
    }

    void Init()
    {
        player.Init();
    }
}
