using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text HPText;
    public TMP_Text AmmoText;
    public TMP_Text killText;

    public Image HPBar;
    public Image AmmoBar;

    Player player;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        gm = GameObject.FindWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 체력을 이미지 fillAmount 대입
        HPBar.fillAmount = (float)player.CurrentHP / 100;
        AmmoBar.fillAmount = (float)player.CurrentAmmo / 10;

        HPText.text = player.CurrentHP.ToString();
        AmmoText.text = player.CurrentAmmo.ToString() + "/10";
        killText.text = gm.countZombie.ToString() + " / 20 Kill";
    }
}
