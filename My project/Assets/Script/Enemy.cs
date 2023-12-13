using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//�� �������
//1. FSM
//2. nav

public class Enemy : MonoBehaviour
{
    public int MaxHP;//�ִ� ü��
    public int CurrentHP;//���� ü��
    public Transform Target;//������ ���
    public bool isChase = false;//�����ϰ� �ִ���

    public GameObject cloth1;
    public GameObject cloth2;
    public GameObject cloth3;
    public GameObject cloth4;

    public GameObject head1;
    public GameObject head2;
    public GameObject head3;
    public GameObject head4;


    public Rigidbody rigid;//�߷�
    public CapsuleCollider capsuleCollider;//�⵿
    public Animator Anim;
    public Material Mat;//�� �ٲٱ�
    public NavMeshAgent nav;

    GameManager gm;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        Anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        Invoke("ChaseStart", 2f);
    }


    // Start is called before the first frame update
     void Start()
    {
        Target = GameObject.FindWithTag("Player").GetComponent<Transform>();

        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();

        int random = Random.Range(0, 4);

        switch(random)
        {
            case 0:
                cloth1.SetActive(true);
                head1.SetActive(true);
                break;
            case 1:
                cloth2.SetActive(true);
                head2.SetActive(true);
                break;
            case 2:
                cloth3.SetActive(true);
                head3.SetActive(true);
                break;
            case 3:
                cloth4.SetActive(true);
                head4.SetActive(true);
                break;
        }

        Mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
    }

    public void ChaseStart()
    {
        isChase = true;
    }

    // Update is called once per frame
     void Update()
    {
        if(isChase)
            nav.SetDestination(Target.position);
    }

    public void FixedUpdate()
    {
        if(isChase == true)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            CurrentHP -= bullet.damage;

            StartCoroutine(OnDamage());

            //Debug.Log("������ ü���� " + CurrentHP);
        }
    }

    public bool isDie = false;

    IEnumerator OnDamage()
    {
        Mat.color = Color.red;
        yield return new WaitForSeconds(0.3f);

        if(CurrentHP > 0)
        {
            Mat.color = Color.white;
        }
        else
        {
            Mat.color = Color.gray;
            isChase = false;
            if(isDie == false)
            {
                nav.enabled = false;
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                gm.countZombie++;
                Anim.SetTrigger("isDead");
                isDie = true;
            }
            Destroy(this.gameObject, 4f);
        }
    }
}
