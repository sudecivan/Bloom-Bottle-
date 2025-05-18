using UnityEngine;
using System.Collections.Generic;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public Transform stopPoint;
    public Transform disappearPoint;
    public List<Transform> pathPoints;
    public ConversationManager conversationManager;
    public GameObject talkPromptUI;  

    public float spawnDelay = 1f;

    private Queue<GameObject> npcQueue = new Queue<GameObject>();
    

    void Start()
    {   Debug.Log("NPCSpawner Start() called!");

        
        for (int i = 0; i < 15; i++)
        {
            npcQueue.Enqueue(npcPrefab);
        }

        StartNextNPC();
    }

    public void StartNextNPC()
    {
        if (npcQueue.Count == 0)
        {
            Debug.Log("All NPCs are done!");
            return;
        }

        GameObject npcInstance = Instantiate(npcQueue.Dequeue(), spawnPoint.position, Quaternion.identity);
        NPCController npc = npcInstance.GetComponent<NPCController>();
        npc.pathPoints = new List<Transform> (pathPoints);
        npc.stopPoint = stopPoint;
        npc.disappearPoint = disappearPoint; 
        Debug.Log($"Assigned {npc.pathPoints.Count} path points to {npc.name}"); 
        npc.conversationManager = conversationManager;
        npc.talkPromptUI = talkPromptUI;
        npc.StartPath(OnNPCFinished);
    }

    private void OnNPCFinished(NPCController npc)
    {
        Debug.Log($"{npc.gameObject.name} reported finished.");
        Invoke(nameof(StartNextNPC), spawnDelay);
    }
}

