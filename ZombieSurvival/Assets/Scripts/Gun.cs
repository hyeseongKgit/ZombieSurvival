using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;

    // Fps게임의 탄창. 현재탄알수/한탄창에 최대로 들어가는 총알수 | 가지고있는 총알 총량
    // [currentMagAmmoCount / gunData.magCount] | currentAmmoCount
    public int currentMagAmmoCount;
    public int currentAmmoCount;

    public Transform firePos;
    public Transform LeftHandle;
    public Transform RightHandle;
    public ParticleSystem muzzleFx;
    public ParticleSystem shellFx;
    LineRenderer lineRenderer;

    public void Init()
    {
        currentMagAmmoCount = gunData.magCount;
        currentAmmoCount = gunData.maxAmmoCount;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        GameManager.instance.UpdateAmmoUI($"{currentMagAmmoCount}/{gunData.magCount} | {currentAmmoCount}");
    }
    public void Reloading()
    {
        int reloadingAmount = gunData.magCount - currentMagAmmoCount;
        if (reloadingAmount > currentAmmoCount)
            reloadingAmount = currentAmmoCount;
        currentAmmoCount -= reloadingAmount;
        currentMagAmmoCount += reloadingAmount;
        GameManager.instance.UpdateAmmoUI($"{currentMagAmmoCount}/{gunData.magCount} | {currentAmmoCount}");
    }

    public IEnumerator Shot()
    {
        if (currentMagAmmoCount != 0)
        {
            currentMagAmmoCount--;
            muzzleFx.Play();
            shellFx.Play();

            var endPos = firePos.position + firePos.forward * gunData.maxDistance;
            RaycastHit hit;
            if (Physics.Raycast(firePos.position, firePos.forward, out hit, gunData.maxDistance))
            {
                endPos = hit.point;
                var target = hit.transform.GetComponent<Zombie>();
                if (hit.transform.gameObject.CompareTag("Enemy") && target != null)
                {
                    target.Damaged(gunData.damage, hit);
                }
            }
            lineRenderer.enabled = true;
            lineRenderer.SetPositions(new Vector3[] { firePos.position, endPos });
            GameManager.instance.UpdateAmmoUI($"{currentMagAmmoCount}/{gunData.magCount} | {currentAmmoCount}");
            yield return new WaitForSeconds(0.1f);
            lineRenderer.enabled = false;
        }
    }


}
