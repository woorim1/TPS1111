using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int countZombie;
    //��ġ�� ���� 20����

    public GameObject boss;
    public GameObject zbSpawn;

    public GameObject winScreen;
    public bool isbossDie = false;

    private void Update()
    {
        if (countZombie >= 20)
        {
            boss.SetActive(true);
            //���� ����
            zbSpawn.SetActive(false);
            //�������� ��Ȱ��ȭ
        }

        if(isbossDie)
        {
            winScreen.SetActive(true);
        }
    }




    //����óġ
    //�¸�
}
