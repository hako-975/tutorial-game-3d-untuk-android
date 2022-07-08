using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int attack = 5;

    [HideInInspector]
    public int currentHealth;

    [HideInInspector]
    public bool isDying;

    Animator animator;
    
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsDying"))
        {
            agent.enabled = false;
        }

        if (currentHealth <= 0 && isDying == false)
        {
            Dying();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Dying()
    {
        animator.SetBool("IsDying", true);
        isDying = true;
        StartCoroutine(WaitDying());
    }

    IEnumerator WaitDying()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("IsDying", false);
    }
}
