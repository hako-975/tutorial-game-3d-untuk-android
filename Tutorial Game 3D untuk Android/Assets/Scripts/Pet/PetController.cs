using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetController : MonoBehaviour
{
    PlayerController playerController;
    Transform target;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        target = playerController.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);

        // buat pet menghadap ke player
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        // buat pet berhenti
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance < agent.stoppingDistance)
        {
            animator.SetBool("IsWalk", false);
        }

        // buat pet berjalan
        if (agent.velocity.magnitude >= 0.1f)
        {
            animator.SetBool("IsWalk", true);
        }

        
    }
}
