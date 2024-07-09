using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : EnemyBase
{
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();

        //Test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }
    public override void Patrol()
    {
        Debug.Log("Patrolling");
        if ((!agent.pathPending && agent.remainingDistance < 0.5f) || isPatrolling == false)
        {
            anim.SetBool(ANIM_ATTACK_BOOL_NAME, false);

            patrolPointsParent.transform.SetParent(this.transform.parent);

            patrolPointsIndex = (patrolPointsIndex + 1) % patrolPointsParent.childCount;
            Transform patrolTarget = patrolPointsParent.transform.GetChild(patrolPointsIndex);

            agent.SetDestination(patrolTarget.position);

            isPatrolling = true;
        }
    }

    public override void Chase()
    {
        Debug.Log("Chasing");

        anim.SetBool(ANIM_ATTACK_BOOL_NAME, false);

        patrolPointsParent.transform.SetParent(this.transform);


        Vector3 offset = target.position - transform.forward * 1.0f;
        agent.SetDestination(offset);

        isPatrolling = false;
    }

    public override void Attack()
    {
        Debug.Log("Attacking");
        transform.LookAt(target.transform.position);
        anim.SetBool(ANIM_ATTACK_BOOL_NAME, true); //Damage transactions in animation event 
    }

}
