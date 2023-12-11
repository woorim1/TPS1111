using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region º¯¼öÀÔ´Ï´Ù.
    public float speed = 2;
    Animator anim;
    CharacterController cc;
    public AudioClip fireSound;
    public GameObject bullet;
    public Transform Shotpoint;
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
    }

    void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Shot");

            SoundManager.instance.SFXPlay("fireSound", fireSound);

            Instantiate(bullet, Shotpoint.transform.position, transform.rotation);
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

    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        //transform.rotation = Quaternion.Euler(new Vector3(3, 3, 3));
    //        //Â¡¹ú : È¸Àü°ª
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
