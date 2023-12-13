using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    Vector3 lookVec;
    Vector3 taunVec;

    Vector3 dir;
    Quaternion rot;
    Missile missile;

    //보스 스킬 1
    public GameObject bossMissile;
    public Transform missilePosition;

    //보스 스킬 2
    public GameObject skill2VFX;//발사 될 파티클

    //보스 스킬 3
    public GameObject skill3VFX;

    public bool isLook = false;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();

        Target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        MaxHP = 300;
        CurrentHP = MaxHP;
        //missilePosition = GameObject.Find("ShotPos").GetComponent<Transform>();
        StartCoroutine(pattern());
    }

    // Update is called once per frame
    void Update()
    {
        dir = Target.transform.position - transform.position;
        //방향 벡터값을 구하기 위해서 목표 벡터(플레이어) - 시작 벡터(보스)

        dir.y = 0;

        rot = Quaternion.LookRotation(dir.normalized);

        if (isLook)
        {
            transform.rotation = rot;
        }
        else
        {
            nav.SetDestination(Target.position);
        }
    }

    IEnumerator pattern()
    {
        if (CurrentHP <= 0)
        {
            gm.isbossDie = true;
        }

        yield return new WaitForSeconds(3.5f);

        int randomAction = Random.Range(0, 5);

        switch (randomAction)
        {
            case 0:
            case 1:
                Debug.Log("1");
                StartCoroutine(skill1());
                break;
            case 2:
            case 3:
                Debug.Log("2");
                StartCoroutine(skill2());
                break;
            case 4:
                Debug.Log("3");
                StartCoroutine(skill3());
                break;
        }
    }

    IEnumerator skill1()
    {
        GameObject instantMissileA = Instantiate(bossMissile, missilePosition.position, missilePosition.rotation);
        Missile bossMissileA = instantMissileA.GetComponent<Missile>();
        bossMissileA.target = Target;

        yield return new WaitForSeconds(3f);

        GameObject instantMissileB = Instantiate(bossMissile, missilePosition.position, missilePosition.rotation);
        Missile bossMissileB = instantMissileB.GetComponent<Missile>();
        bossMissileA.target = Target;

        StartCoroutine(pattern());
    }

    IEnumerator skill2()
    {
        SoundManager.instance.VFXPlay(skill2VFX, missilePosition.transform.position, Target.transform, 3f);
        yield return new WaitForSeconds(1f);
        StartCoroutine(pattern());
    }
    IEnumerator skill3()
    {
        GameObject Janpan = Instantiate(skill3VFX, transform.position, transform.rotation, transform);

        yield return new WaitForSeconds(5f);
        Destroy(Janpan);
        StartCoroutine(pattern());
    }
}
