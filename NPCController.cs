using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    public List<Transform> pathPoints;      
    public Transform stopPoint;              
    public Transform disappearPoint;

    public Animator animator;
    public ConversationManager conversationManager;

    private NavMeshAgent agent;
    private int currentPoint = 0;
    private bool isLeaving = false;
    
    private bool hasReachedStopPoint = false;
    private Action<NPCController> onFinishedCallback;

    public GameObject talkPromptUI;  
    private bool playerInRange = false;

    void Awake()
{
    agent = GetComponent<NavMeshAgent>();
}
    void Start()
    {
        Debug.Log($"{gameObject.name}: NPCController Start() called!");

    }
    

    public void StartPath(Action<NPCController> onFinished)
    {   
        onFinishedCallback = onFinished;
        MoveToNextPoint();
        SetWalking(true); 
        
    }

    void Update()
    {
        
    if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
    {
        if (!isLeaving)
        {
            if (!hasReachedStopPoint && currentPoint < pathPoints.Count)
            {
                MoveToNextPoint();
            }
            else if (!hasReachedStopPoint)
            {
                ReachStopPoint();
            }
            
        }
        else
        {
            FinishAndDisappear();
        }
        
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
        talkPromptUI.SetActive(false);
        StartConversation();
        playerInRange = false;
        }
        
    }  

    }

    private void MoveToNextPoint()
    {   

    if (currentPoint >= pathPoints.Count)
    {
        Debug.LogWarning($"{gameObject.name}: Reached end of pathPoints.");
        ReachStopPoint();
        return;
    }
        agent.SetDestination(pathPoints[currentPoint].position);
        currentPoint++;
    }

    private void ReachStopPoint()
    { Debug.LogError("npc reached stop point");
        
        if (hasReachedStopPoint)
        return;
        hasReachedStopPoint = true;
        agent.SetDestination(stopPoint.position);
        SetWalking(false);
       
        
    }

    private void StartConversation()
    {
        SetWalking(false);
        Debug.Log($"{gameObject.name} started conversation at store!");

        conversationManager.StartConversation(this);

        
        conversationManager.OnConversationEnded += StartLeaving;
    }

    private void StartLeaving()
    {   conversationManager.OnConversationEnded -= StartLeaving;
        isLeaving = true;
        agent.SetDestination(disappearPoint.position);
        SetWalking(true);

    }

    private void FinishAndDisappear()
    {
        Debug.Log($"{gameObject.name} exited and disappeared.");
        onFinishedCallback?.Invoke(this);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
  {  Debug.Log("Trigger entered by: " + other.name);

    if (!isLeaving && hasReachedStopPoint)
    {   
        talkPromptUI.SetActive(true);
        playerInRange = true;
    }
  }

   void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("NPC"))
    {
        talkPromptUI.SetActive(false);
        playerInRange = false;
    }
  }

    private void SetWalking(bool walking)
{
    if (animator.GetBool("isWalking") != walking)
    {
        animator.SetBool("isWalking", walking);
    }
}

}



