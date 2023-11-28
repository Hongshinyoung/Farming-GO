using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Enemy : MonoBehaviour
{
    public Transform target;
    Rigidbody rigid;
    BoxCollider boxCollider;
    NavMeshAgent nav;
    Animator anim;
    bool isChase;
    

    public void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        Invoke("ChaseStart", 2);
    }

    void FreezVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void ChaseStart()
    {
        if(PlayerStats.Money == 0)
        {
            isChase = true;
            anim.SetBool("IsRun", true);
        }
    }
    void Update()
    {
        if(isChase)
        nav.SetDestination(target.position);
    }

    void FixedUpdate()
    {
        FreezVelocity();
    }
}
