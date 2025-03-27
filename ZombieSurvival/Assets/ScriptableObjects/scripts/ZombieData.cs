using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create Zombie Data",fileName ="ZombieData")]
public class ZombieData : ScriptableObject
{
    public float hp;
    public float speed;
    public float damage;
    public Color skinColor;
}
