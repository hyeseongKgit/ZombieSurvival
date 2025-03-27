using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Gun Data" , fileName ="GunData")]
public class GunData : ScriptableObject
{
    public float coolTime;
    public int magCount;
    public int maxAmmoCount;
    public float maxDistance;
    public float damage;
}
