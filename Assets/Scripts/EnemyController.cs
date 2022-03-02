using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class EnemyController : MonoBehaviour
{
    #region Navigation Date 
    GameObject[] patrolPoints;
    [SerializeField] int currentPatrolPoint = 0;
    [SerializeField] float waitAtPatrolPoint = 2.0f;
    float waitAtPatrolPointCounter;
    [SerializeField] float waitBetweenAttacks = 2.0f;
    float waitBetweenAttacksCounter; 
    NavMeshAgent myNavMeshAgent;
    #endregion

    #region Animation Data
    Animator myAnimator;
    [SerializeField] float chaseRange = 10.0f;
    [SerializeField] float attackRange = 2.5f;
    #endregion

    #region AI Data
    public enum AIState
    { IDLE, PATROLLING, CHASING, ATTACKING }
    public AIState CurrentAIState { get; private set; } = AIState.IDLE;
    #endregion

    [SerializeField] int sfxToPlay = 6;

    // Start is called before the first frame update
    void Start()
    {
        patrolPoints =
             GameObject.FindGameObjectsWithTag("Patrol Point");
        myNavMeshAgent =
            GetComponent<NavMeshAgent>();
        myAnimator =
            GetComponentInChildren<Animator>();
        CurrentAIState = AIState.IDLE;
        waitAtPatrolPointCounter = waitAtPatrolPoint; 

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 temp = new Vector3(0.0f, 0.0f, 7.5f); 
        float distanceToPlayer = Vector3.Distance(
            transform.position,
            PlayerController.Instance.transform.position
            ); 
        switch(CurrentAIState)
        {
            case AIState.IDLE:
                IsIdle(distanceToPlayer);
                break;
            case AIState.PATROLLING:
                IsPatrolling(distanceToPlayer);
                break;
            case AIState.CHASING:
                IsChasing(distanceToPlayer);
                break;
            case AIState.ATTACKING:
                IsAttacking(distanceToPlayer);
                break;
        }
    }

    void IsIdle(float distance)
    {
        myAnimator.SetBool("IsMoving", false);
        if (waitAtPatrolPointCounter <= Mathf.Epsilon)
        {
            waitAtPatrolPointCounter -= Time.deltaTime;
        }
        else
        {
            myNavMeshAgent.SetDestination(
                   patrolPoints[currentPatrolPoint].transform.position);
            CurrentAIState = AIState.PATROLLING;
        }
        if (distance <= chaseRange)
        {
            CurrentAIState = AIState.CHASING;
        }
        
    }

    void IsPatrolling(float distance)
    {
        myAnimator.SetBool("IsMoving", true);
        if(myNavMeshAgent.remainingDistance <= 1.0f)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
            myNavMeshAgent.SetDestination(patrolPoints[currentPatrolPoint].transform.position);
            
        }
        if(distance <= chaseRange)
        {
            CurrentAIState = AIState.CHASING; 
        }
    }

    void IsChasing(float distance)
    {
        myAnimator.SetBool("IsMoving", true);
        if(distance < attackRange)
        {
            myNavMeshAgent.velocity = Vector3.zero;
            myNavMeshAgent.isStopped = true;
            myAnimator.SetBool("IsMoving", false);
            myAnimator.SetTrigger("Attack");
            waitAtPatrolPointCounter = waitAtPatrolPoint;
            CurrentAIState = AIState.ATTACKING; 
        }
        else if(distance < chaseRange)
        {
            myNavMeshAgent.SetDestination(
                 PlayerController.Instance.transform.position);
        }
        else
        {
            myNavMeshAgent.SetDestination(
                patrolPoints[currentPatrolPoint].transform.position);
            CurrentAIState = AIState.PATROLLING; 
        }
    }
    
    void IsAttacking(float distance)
    {
        transform.LookAt(
            PlayerController.Instance.transform, Vector3.up);
        transform.rotation =
            Quaternion.Euler(
                0.0f,
                transform.eulerAngles.y,
                0.0f
                );
        waitBetweenAttacksCounter -= Time.deltaTime; 
        if(waitBetweenAttacksCounter <= Mathf.Epsilon)
        {
            if(distance > attackRange)
            {
                myNavMeshAgent.SetDestination(
                    patrolPoints[currentPatrolPoint].transform.position);
                myNavMeshAgent.velocity = Vector3.forward;
                myNavMeshAgent.isStopped = false;
                CurrentAIState = AIState.PATROLLING; 
            }
            else
            {
                
                myAnimator.SetTrigger("Attack");
                AudioController.Instance.PlaySFX(sfxToPlay);
                waitBetweenAttacksCounter = waitBetweenAttacks; 
            }
        }
    }
}
