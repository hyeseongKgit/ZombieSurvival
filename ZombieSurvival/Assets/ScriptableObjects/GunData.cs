using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Gun Data" , fileName ="GunData")]
public class GunData : ScriptableObject
{
    public Vector3 gunPos = new Vector3(-0.302f, -0.038f, 0.199f);

    public Vector3 leftHandlePos = new Vector3(-0.064f, 0.043f, 0.013f);
    public Quaternion leftHandleRot = Quaternion.Euler(21.968f, 58.911f, 150.515f);

    public Vector3 rightHandlePos = new Vector3(0.082f, -0.024f, -0.132f);
    public Quaternion rightHandleRot = Quaternion.Euler(-177.185f, -219.569f, 91.34499f);
}
