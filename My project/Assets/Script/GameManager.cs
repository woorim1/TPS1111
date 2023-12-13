using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int countZombie;
    //초치한 좀비가 20마리

    public GameObject boss;
    public GameObject zbSpawn;

    public GameObject winScreen;
    public bool isbossDie = false;

    private void Update()
    {
        if (countZombie >= 20)
        {
            boss.SetActive(true);
            //보스 등장
            zbSpawn.SetActive(false);
            //좀비스포너 비활성화
        }

        if(isbossDie)
        {
            winScreen.SetActive(true);
        }
    }




    //보스처치
    //승리
}
