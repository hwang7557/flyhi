using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnityChan_Enemy : MonoBehaviour {

    enum Action
    {
        E_MOVE,
        E_ATTACK,
    }

    public GameObject target;
    NavMeshAgent agent;
    Action status = Action.E_MOVE;

    float AttackTime = 0.0f;

    Animator animator;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if(status == Action.E_MOVE)
        {
            agent.destination = target.transform.position;
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        else if(status == Action.E_ATTACK)
        {
            AttackTime += Time.deltaTime;
            if (animator.GetCurrentAnimatorStateInfo(0).length + 2.0f < AttackTime)
            {
               
                animator.SetBool("Attack", false);
                status = Action.E_MOVE;
                AttackTime = 0.0f;
                agent.speed = 20.0f;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ControlPlayer")
        {
            agent.speed = 0.1f;
            status = Action.E_ATTACK;
            animator.SetBool("Attack", true);
        }
    }
}
