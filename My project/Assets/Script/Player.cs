using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 변수입니다.
    public float speed = 2;
    public float MaxHP = 100;
    public float CurrentHP = 100;
    private bool isHit = false;

    public int MaxAmmo = 10;
    public int CurrentAmmo = 10;

    Animator anim;
    CharacterController cc;

    public AudioClip fireSound;
    public GameObject bullet;
    public Transform Shootpoint;
    public GameObject fire;
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Shot();
        Reload();
    }

    void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            //에니메이션 재장전
            CurrentAmmo = MaxAmmo;
        }
    }

    void Shot()
    {
        if(CurrentAmmo > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Shot");

                SoundManager.instance.SFXPlay("fireSound", fireSound);

                Instantiate(bullet, Shootpoint.transform.position, Camera.main.transform.rotation);

                SoundManager.instance.VFXPlay(fire, Shootpoint.transform.position, 0.3f);

                CurrentAmmo--;
            }
        }
        else
        {
            //발사가 안될 때
            //방아쇠만 당기는 사운드 재생
            //UI로 표시
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", h);
        anim.SetFloat("Direction", v);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 4;
            anim.SetBool("Run", true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 2;
            anim.SetBool("Run", false);
        }

        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);

        cc.Move(dir * speed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag == "Enemy")
        {
            if(!isHit)
            {
                CurrentHP -= 10;
                isHit = true;
                Invoke("Mujuck", 2f);
            }
        }
    }

    void Mujuck()
    {
        isHit = false;
    }

    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        //transform.rotation = Quaternion.Euler(new Vector3(3, 3, 3));
    //        //징벌 : 회전값
    //    }
    //}

    //private void FixedUpdate()
    //{
    //    Move();
    //}

    //void Move()
    //{
    //    float h = Input.GetAxis("Horizontal");
    //    float v = Input.GetAxis("Vertical");

    //    Vector3 derection = new Vector3(h, 0, v);

    //    transform.Translate(derection * Speed * Time.deltaTime);
    //}
}
