using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxHp = 100f;
    public float hp;
    float moveSpeed = 5f;
    float rotateSpeed = 3f;
    bool isDead = false;
    bool isReloading = false;
    bool isShotCoolTime = false;
    Rigidbody rb;
    Animator animator;
    public Transform gunPivot;
    public event Action OnDeath;


    public Gun gun;

    public void Init()
    {
        hp = maxHp;
        GameManager.instance.UpdatePlayerHPBar(hp / maxHp);
        rb = GetComponent<Rigidbody>();
        animator=GetComponent<Animator>();
        gun.Init();
        StartCoroutine(TakeInput());
    }


    private void OnAnimatorIK(int layerIndex)
    {
        gunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, gun.LeftHandle.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, gun.LeftHandle.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        animator.SetIKPosition(AvatarIKGoal.RightHand, gun.RightHandle.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, gun.RightHandle.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<IItem>();
        if (item!=null)
        {
            item.Use(this);
        }
    }
    IEnumerator TakeInput()
    {
        while (!isDead)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            //장전중이 아닐때 장전하면 장전됨
            if (Input.GetKeyDown(KeyCode.R) && !isReloading) StartCoroutine(Reloading());

            //장전중이 아니고 발사 쿨타임이 아닐떄 마우스클릭하면 쏨
            if (Input.GetMouseButton(0) && !isReloading && !isShotCoolTime) 
            {
                StartCoroutine(Shot());
            }

            rb.MovePosition(transform.position + transform.forward * v * moveSpeed * Time.deltaTime);
            transform.rotation *= Quaternion.Euler(0, h * rotateSpeed, 0);

            animator.SetFloat("Move", v);
            yield return null;
        }
    }

    IEnumerator Reloading()
    {
        isReloading = true;
        animator.SetTrigger("Reload");
        gun.Reloading();
        yield return new WaitForSeconds(0.8f);
        isReloading = false;
    }
    IEnumerator Shot()
    {
        isShotCoolTime = true;
        StartCoroutine(gun.Shot());
        yield return new WaitForSeconds(gun.gunData.coolTime);
        isShotCoolTime=false;
    }

    public void Damaged(float damage)
    {
        Debug.Log("피해 받음");
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        GameManager.instance.UpdatePlayerHPBar(hp / maxHp);
        if (hp == 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        OnDeath();
        yield return null;
    }
}
