using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 3f;
    float rotateSpeed = 3f;
    bool isDead = false;
    Rigidbody rb;
    Animator animator;
    public Transform gunPivot;

    public Gun gun;

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
        animator=GetComponent<Animator>();
        StartCoroutine(TakeInput());
    }


    private void OnAnimatorIK(int layerIndex)
    {
        gunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, gun.gunData.leftHandlePos);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, gun.gunData.leftHandleRot);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        animator.SetIKPosition(AvatarIKGoal.RightHand, gun.gunData.rightHandlePos);
        animator.SetIKRotation(AvatarIKGoal.RightHand, gun.gunData.rightHandleRot);
    }
    IEnumerator TakeInput()
    {
        while (!isDead)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            rb.MovePosition(transform.position + transform.forward * v * moveSpeed * Time.deltaTime);
            transform.rotation *= Quaternion.Euler(0, h * rotateSpeed, 0);

            animator.SetFloat("Move", v);
            yield return null;
        }
    }
}
