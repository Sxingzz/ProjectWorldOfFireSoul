using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAITest : MonoBehaviour
{
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private Transform playerTF;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        playerTF = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //navMeshAgent.SetDestination(playerTF.position); // di chuyển tự động tránh các vật thể

        // Lấy hướng di chuyển của Enemy từ NavMeshAgent
        Vector3 movementDirection = navMeshAgent.velocity.normalized;

        animator.SetFloat("InputX", movementDirection.x);
        animator.SetFloat("InputY", movementDirection.y); 

        if (respawnPoints.Length > 0)
        {
            // Kiểm tra xem enemy đã đến gần điểm đến hay chưa
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
            {
                Transform newDestination = GetRandomRespawnPoint();

                if (newDestination != null)
                {
                    navMeshAgent.SetDestination(newDestination.position);
                }
            }
        }
    }
    // Hàm lấy một điểm respawn ngẫu nhiên từ mảng
    Transform GetRandomRespawnPoint()
    {
        if (respawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, respawnPoints.Length);
            return respawnPoints[randomIndex];
        }
        else
        {
            Debug.LogWarning("Không có điểm respawn nào được cấu hình.");
            return null;
        }
    }
    
}
