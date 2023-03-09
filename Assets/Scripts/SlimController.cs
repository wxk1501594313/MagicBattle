using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimController : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStop)
        {
            agent.ResetPath();//Çå¿Õµ¼º½Â·¾¶
            return;
        }
        agent.SetDestination(player.position);
    }
}
