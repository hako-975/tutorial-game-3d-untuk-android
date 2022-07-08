using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Transform startTarget;
    public Transform endTarget;

    NPCStats nPCStats;
    PlayerController playerController;
    NavMeshAgent agent;
    Animator animator;

    [HideInInspector]
    public bool isInteract = false;

    [HideInInspector]
    public bool isArrived = false;

    [HideInInspector]
    public bool isCombat = false;

    [HideInInspector]
    public bool isDying = false;

    [HideInInspector]
    public bool isGetHit = false;

    [HideInInspector]
    public bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        nPCStats = GetComponent<NPCStats>();

        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        playerController = FindObjectOfType<PlayerController>();

        agent.transform.position = startTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        // jika npc pingsan
        if (nPCStats.isDying)
        {
            isDying = true;
            isInteract = false;
            isCombat = false;
            isGetHit = false;
            agent.enabled = false;
        }

        // jika npc terkena hit
        if (isGetHit)
        {
            isGetHit = false;
            int damage = playerController.GetComponent<PlayerStats>().attack;
            nPCStats.TakeDamage(damage);
            animator.SetBool("IsGetHit", true);
        } 
        else if (isGetHit == false)
        {
            animator.SetBool("IsGetHit", false);
        }

        if (isInteract)
        {
            agent.enabled = false;

            // buat npc menghadap player
            Vector3 direction = (playerController.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else if (isCombat)
        {
            agent.enabled = false;

            // buat npc menghadap player
            Vector3 direction = (playerController.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // buat random attack
            float random = Random.Range(1, 4);
            
            if (random == 2f)
            {
                if (animator.GetBool("IsGetHit") == false)
                {
                    Attack();
                }
            }

            // jika player pingsan
            if (PlayerPrefsManager.instance.GetHealth() <= 0)
            {
                animator.SetBool("IsCombat", false);
                animator.SetBool("IsTalk", false);
                isCombat = false;
            }
        }
        else
        {
            agent.enabled = true;

            animator.SetBool("IsCombat", false);
            animator.SetBool("IsTalk", false);

            if (isArrived == false)
            {
                agent.SetDestination(endTarget.position);
            }
            else if (isArrived == true)
            {
                agent.SetDestination(startTarget.position);
            }

            // buat npc berjalan
            if (agent.velocity.magnitude >= 0.1f)
            {
                animator.SetBool("IsWalk", true);
            }
        }
    }

    public void Interact()
    {
        isInteract = true;
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsTalk", true);
    }

    public void Combat()
    {
        isCombat = true;
        isInteract = false;
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsCombat", true);
    }

    public void Attack()
    {
        if (isAttack == false)
        {
            isAttack = true;
            animator.SetBool("IsAttack", true);
            StartCoroutine(WaitNextAttack());
        }
    }

    IEnumerator WaitNextAttack()
    {
        float duration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);
        animator.SetBool("IsAttack", false);
        isAttack = false;
    }
}
