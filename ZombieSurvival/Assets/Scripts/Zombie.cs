using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public ZombieData zombieData;

    float hp;
    bool isDead = false;
    bool isAttacking = false;
    Animator animator;
    NavMeshAgent navMeshAgent;
    SkinnedMeshRenderer skinMeshRenderer;
    public LayerMask playerLayerMask;
    public ParticleSystem bloodFx;
    public event Action OnDeath;

    public void Init()
    {
        hp = zombieData.hp;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed *= zombieData.speed;
        skinMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        skinMeshRenderer.material.color = zombieData.skinColor;
        StartCoroutine(FindTarget());
    }

    IEnumerator FindTarget()
    {
        while(!isDead)
        {
            var targetColls = Physics.OverlapSphere(transform.position, 20f, playerLayerMask);
            // 이 게임에서 플레이어는 한명뿐임. 확장하려면 수정이 필요함
            if (targetColls.Length == 1)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetColls[0].transform.position);
                animator.SetBool("hasTarget", true);
            }
            else
            {
                navMeshAgent.isStopped = true;
                animator.SetBool("hasTarget", false);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var target = other.GetComponent<PlayerController>();
        if (target != null && target.CompareTag("Player") && !isAttacking)
        {
            StartCoroutine(Attack(target));
        }
    }

    IEnumerator Attack(PlayerController target)
    {
        isAttacking = true;
        target.Damaged(zombieData.damage);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    public void Damaged(float damage, RaycastHit hit)
    {
        hp -= damage;
        bloodFx.transform.position = hit.point;
        bloodFx.Play();
        if (hp <= 0) StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        var colls = GetComponents<Collider>();
        foreach ( var coll in colls ) coll.enabled = false;
        OnDeath();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
