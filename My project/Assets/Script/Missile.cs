using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Missile : MonoBehaviour
{
    NavMeshAgent nav;
    public Transform target;

    Player player;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    private void Update()
    {
        nav.SetDestination(target.position);

        transform.rotation = Quaternion.Euler(transform.position - player.transform.position);
        //미사일이 날라갈 때 방향 설정
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            player.CurrentHP -= 10;
            Destroy(this.gameObject);
        }
    }
}
