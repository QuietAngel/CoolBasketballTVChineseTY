﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //是否有广告
    private bool isUseVideo = false;
    public bool IsUseVideo { get { return isUseVideo; } }

    //是否是TV
    private bool isUseHand = false;
    public bool IsUseHand { get { return isUseHand; } }


    public static GameController _instance;
    public GameObject colliderObject;
    public GameObject npc;
    public GameObject player;
    public GameObject ballF;
     public ball ball_script;

    public GameObject scene; 
    public AudioManager audioManager;
    public ChangeGameManager changeGameManager;
    public GameObject hand;
    public GameObject daitianniu;

    public player player_script;

      
    public bool SoundOpen { get; set; }
     

    public enum WhoHaveBall { none,player,npc}
    public WhoHaveBall whoHaveBall = WhoHaveBall.none;

    public FlyBall flyBall;
    public GameObject flyBallF;
    public GameObject downBall;
    public Transform rightPoint_UP;
    public Transform leftPoint_UP;

    public LanKuangAnim leftLankuang;
    public LanKuangAnim rightLankuang;

    public GameObject targetObj;
    public void StopBall()
    {
        if (ballF.activeSelf == true)
        {//当前显示的是中间球
            ball_script.GetComponent<Rigidbody2D>().Sleep();

        }
        else if (flyBallF.activeSelf == true)
        {//当前显示的是飞行球

        }
        else if (downBall.activeSelf == true)
        {
            downBall.GetComponent<Rigidbody2D>().Sleep();
        }
    }

    public void StopBallOver()
    {
        if (ballF.activeSelf == true)
        {//当前显示的是中间球
            ball_script.GetComponent<Rigidbody2D>().WakeUp();

        }
        else if (flyBallF.activeSelf == true)
        {//当前显示的是飞行球
            flyBall.Move(flyBall.gameObject,targetObj);
        }
        else if (downBall.activeSelf == true)
        {
            downBall.GetComponent<Rigidbody2D>().WakeUp();
        }
    }


    //这个是用来跟随所有显示中球的坐标，npc根据这个在无人持球是判断走位
    public Vector3 tagetBall { get; set; }
    private void Awake()
    {
        _instance = this;

        hand.SetActive(IsUseHand);
        isStop = false;

        InitValue();
    }
    private void OnEnable()
    {
        StartCoroutine(StartPlay());
    }
    //当前使用的角色ID
    public int NowUsePlayerID { get; set; }

    //当前管卡
    public int NowPlayLevel { get; set; }

    //当前球
    public GameObject Ball { get; set; }
     

    //玩家是否投中
    public bool isShootInPlayer = false;
    //npc是否投中
    public bool isShootInNPC = false;

    public bool isSanFenPlayer = false;
    public bool isSanFenNPC = false;

    //是否可以抢断
    public bool isCanQiangDuan = false;

    public Vector3 vel { get; set; }
     
    public bool isStop { get; set; }

    public bool isstart = false;
    private float jishiTime = 0;
    private float playTime = 60;
    private float showTime = 0;
    public int ShowTime
    {
        set { showTime = value; }
        get { return (int)showTime; }
    }
    public float JishiTime { get { return jishiTime; } set { jishiTime = value; } }
    bool boolDaojishi1 = false;
    bool boolDaojishi2 = false;
    bool boolDaojishi3 = false;
    bool boolDaojishi4 = false;
    bool boolDaojishi5 = false;
    bool boolDaojishi6 = false;
    bool boolDaojishi7 = false;
    bool boolDaojishi8 = false;
    bool boolDaojishi9 = false;
    bool boolDaojishi10 = false;
    void JiShi()
    { 
        if (isstart == true)
        {
            if (jishiTime < playTime)
            {
                jishiTime += Time.deltaTime;
                PlayDaojishiSound();
            }
            else
            {
                isstart = false;
                jishiTime = 0;
                ResDaojishiBool(); 
                if (grade.redgrade >= text.bluegrade)
                {//胜利
                    if (IsUseHand == true)
                    {
                        hand.GetComponent<UISelect>().ShowInOverWin();
                    }
                    UIManager._instance.ShowOrHideGameOver(true);
                    UIManager._instance.uiStep = UIManager.UIStep.gameOver;
                    UIManager._instance.gameOver.ShowShengli();
                    UIManager._instance.playSprite.enabled = true;
                    UIManager._instance.audioManager.PlayOne(2);
                    
                    changeGameManager.Des(); 
                }
                else
                {
                    if (IsUseHand == true)
                    {
                        hand.GetComponent<UISelect>().ShowInOverLose();
                    }
                    UIManager._instance.ShowOrHideGameOver(true);
                    UIManager._instance.uiStep = UIManager.UIStep.gameOver;
                    UIManager._instance.gameOver.ShowShiBai();
                    UIManager._instance.audioManager.PlayOne(3);

                    changeGameManager.Des(); 
                }
                player_script.StopPlay(true);
                npc.GetComponent<Npc>().StopPlay(true);
            }
            showTime = playTime - jishiTime;
        }
    }

    void PlayDaojishiSound()
    {
        if (boolDaojishi1 == false)
        {
            if (jishiTime > 50)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi1 = true;
            }
        }
        if (boolDaojishi2 == false)
        {
            if (jishiTime > 51)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi2 = true;
            }
        }
        if (boolDaojishi3 == false)
        {
            if (jishiTime > 52)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi3 = true;
            }
        }
        if (boolDaojishi4 == false)
        {
            if (jishiTime > 53)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi4 = true;
            }
        }
        if (boolDaojishi5 == false)
        {
            if (jishiTime > 54)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi5 = true;
            }
        }
        if (boolDaojishi6 == false)
        {
            if (jishiTime > 55)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi6 = true;
            }
        }
        if (boolDaojishi7 == false)
        {
            if (jishiTime > 56)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi7 = true;
            }
        }
        if (boolDaojishi8 == false)
        {
            if (jishiTime > 57)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi8 = true;
            }
        }
        if (boolDaojishi9 == false)
        {
            if (jishiTime > 58)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi9 = true;
            }
        }
        if (boolDaojishi10 == false)
        {
            if (jishiTime > 59)
            {
                UIManager._instance.audioManager.PlayOne(5);
                boolDaojishi10 = true;
            }
        }
    }
    void ResDaojishiBool()
    {
        boolDaojishi1 = false;
        boolDaojishi2 = false;
        boolDaojishi3 = false;
        boolDaojishi4 = false;
        boolDaojishi5 = false;
        boolDaojishi6 = false;
        boolDaojishi7 = false;
        boolDaojishi8 = false;
        boolDaojishi9 = false;
        boolDaojishi10 = false;
    }


    IEnumerator StartPlay()
    {
        yield return new WaitForSeconds(1f);
        if (UIManager._instance.uiStep == UIManager.UIStep.game)
        {
            isstart = true;
            ResDaojishiBool();
        }
    }
    private void Update()
    {
        JiShi();
        if (UIManager._instance.uiStep == UIManager.UIStep.game)
        {
        //    if (ballFather.childCount > 1)
         //   {
                //  Destroy(ballFather.GetChild(0).gameObject);
        //    }
        }
        //  print(isStop);
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIManager._instance.CoinNum += 1000;
        }


        float dis = Vector3.Distance(player.transform.position,npc.transform.position);
        //print("玩家和npc距离：  "+dis);
        if (dis < 80)
        {//可以抢断
            isCanQiangDuan = true;
        }
        else
        {//不可以
            isCanQiangDuan = false;
        }

        if (ballF.activeSelf == true)
        {
            tagetBall = ball_script.transform.position;
        }
        else if (flyBallF.activeSelf == true)
        {
            tagetBall = flyBall.transform.position;
        }
        else if (downBall.activeSelf == true)
        {
            tagetBall = downBall.transform.position;
        } 
        else
        {
            if (whoHaveBall == WhoHaveBall.npc)
                tagetBall = npc.transform.position;
            else if(whoHaveBall == WhoHaveBall.player)
                tagetBall = player.transform.position;
        }
    }

    public void ResetPos()
    {
        player.transform.localPosition = new Vector3(-1, 0f, 5);
        player.GetComponent<player>().ResAnimator();
        npc.transform.localPosition = new Vector3(1, 0f, 5);
        npc.GetComponent<Npc>().ResAnimator();
        leftLankuang.InIt();
        rightLankuang.InIt();
        flyBallF.SetActive(false);
        downBall.SetActive(false);
        if(ballF.activeSelf == false)
        ballF.SetActive(true);
        whoHaveBall = WhoHaveBall.none;

    }

    public void ShowOrHideGame(bool isShow)
    {
        scene.SetActive(isShow); 
    }

  public  int[,] playerAllValue;
    void InitValue()
    {
        playerAllValue = new int[20, 6] { { 6200,  4400,   2040,   2070,   5600,   3400 }, { 5350, 4600,   1710,   1650,   4240,   2500 }, { 5400, 4800,   1590,   1530,   4400,   2800}, {6500,   5800,   1890,   1890,   4960,   3300 }, {6950,  7800,   2160,   2100,   5840,   3550},
                                       {7300,   6600,   2160,   2190,   6000,   3600 }, {8000,  5000,   1980,   2040,   6000,   4000 }, {6800,  6400,   2100,   2100,   5600,   3500 }, { 7300, 6800,   2100,   2100,   5760,   3750 }, { 7400, 7000,   2190,   2160,   5760,   3550 },
                                       { 7750,  7400,   2490,   2490,   6640,   4150 }, {7950,  7000,   2400,   2400,   6720,   4150 }, { 8200, 7800,   2550,   2550,   6880,   4350 }, { 8350, 7500,   2640,   2640,   7040,   4300 }, { 7950, 7200,   2670,   2670,   7120,   3900 },
                                       { 8300,  7200,   2550,   2490,   6800,   4450 }, { 7950, 7200,   2400,   2430,   6400,   4150 }, { 7950, 7200,   2400,   2430,   6400,   4150 }, {7900,  7200,   2550,   2490,   6560,   4150}, {7900,   7200,   2550,   2490,   6560,   4150 }};

        InitNpcValue();
    }


    public int[,] npcAllValue;
     void InitNpcValue()
    {
        npcAllValue = new int[30, 6] { {7445,   7315,   2511,   1528,   7193,   3484}, 
                                       {7661,   7054,   2436,   2356,   5929,   3052 }, 
                                       { 8327,  6312,   1917,   1935,   3754,   2854}, 
                                       {8805,   7838,   2264,   1516,   5695,   2521},
                                       {6416,   4966,   2432,   2049,   4675,   3663},

                                       {8275,   4539,   1808,   2454,   5344,   2717 },
                                       {5214,   7594,   2544,   1628,   4203,   4303 }, 
                                       {6915,   6501,   1751,   2313,   4529,   3757 }, 
                                       {6520,   7706,   2102,   2074,   7280,   3564},
                                       { 6489,  5931,   2481,   1917,   5116,   3253 },

                                       { 6409,  5600,   2428,   2481,   7286,   3980 },
                                       {6059,   6719,   2396,   2285,   5468,   3606 },
                                       { 5780,  4069,   2560,   2207,   6393,   3199 },
                                       { 5367,  4175,   2089,   2592,   5386,   4140 },
                                       { 8494,  4787,   2226,   2284,   5138,   3688 },

                                       { 8764,  7469,   2489,   2466,   5109,   2844},
                                       { 6295,  7652,   2240,   1503,   6696,   3714},
                                       {8186,   6358,   2624,   2105,   6282,   3593 },
                                       {7533,   6700,   2120,   2214,   4471,   3356},
                                       {5406,   6127,   2134,   2487,   6648,   2640 },

                                       { 6319,  6009,   2015,   1716,   3991,   4251},
                                       {5761,   6498,   2258,   2494,   4051,   3365},
                                       { 8556,  5387,   1704,   2625,   4674,   3265 },
                                       {7978,   7509,   1882,   1960,   6087,   3060 },
                                       { 5467,  5009,   1949,   2477,   5883,   2752 },

                                       { 7723,  6329,   2436,   1938,   3972,   2694 },
                                       { 6261,  4137,   2025,   2506,   7416,   2835 },
                                       { 7044,  5738,   2339,   2040,   4332,   4327 },
                                       {6873,   7921,   2228,   2048,   6250,   3957},
                                       {8798,   4016,   2451,   2476,   7428,   4447 }};
    }
     
    

    public int[,] playerValue =  new int[20, 2] { {84,84 }, { 91, 94 }, { 63, 69 }, { 84, 70 }, { 80, 90 }, { 70, 70 }, { 70, 73 }, { 84, 89 }, { 98, 91 }, { 77, 77 },
                                                  {90,91 },{84,91 },{77,84 },{85,94 },{99,99 },{77,86 },{92,87 },{84,89 },{63,70 },{95,95 }};

    public int[,] levelValue = new int[30, 2] { {80,81 }, { 97, 82 }, { 90, 83 }, { 97, 84 }, { 99, 85 }, { 84, 84 }, { 98, 85 }, { 84, 86 }, { 80, 87 }, { 99, 88 },
                                                {94,87 },{94,88 },{85,89 },{92,90 },{95,91 },{96,90 },{92,91 },{89,92 },{81,93 },{97,94 },
                                                {89,93 },{99,94 },{97,95 },{84,96 },{98,97 },{97,96 },{92,97 },{81,98 },{85,99 },{100,100 }};
    //npc显示顺序
    private int[] npcUse = new int[30] {5,18,11,4,19,19,19,1,5,17,1,5,8,11,5,4,12,10,18,15,7,7,19,16,9,12,9,2,14,19};
    public int[] GetNpcUse { get { return npcUse; } }

  
    private  string[] animName_ = new string[] { "daiji_meiqiu","daiji_daiqiu","fangshou","fangshou_yidong","tiaoqi", "qiang_meiqiu", "qiang_youqiu", "touqiu_shanglan", "touqiu_sanfenqiu","touqiu_guanlan","zou","zou_qiu"};
    /// <summary>
    /// 0.待机没球
    /// 1.待机有球
    /// 2.防守
    /// 3.防守移动
    /// 4.跳起
    /// 5.跳起没抢到球
    /// 6.跳起抢到球
    /// 7.投球上篮
    /// 8.投球三分
    /// 9.投球灌篮
    /// 10.走
    /// 11.走带球
    /// </summary>
    public string[] animName { get { return animName_; } }



    //动画播放速度
    private int animSpeed = 3;
    public int AnimSpeed { get { return animSpeed; } }

    //    1	80	81
    //2	97	82
    //3	90	83
    //4	97	84
    //5	99	85
    //6	84	84
    //7	98	85
    //8	84	86
    //9	80	87
    //10	99	88

    //11	94	87
    //12	94	88
    //13	85	89
    //14	92	90
    //15	95	91
    //16	96	90
    //17	92	91
    //18	89	92
    //19	81	93
    //20	97	94

    //21	89	93
    //22	99	94
    //23	97	95
    //24	84	96
    //25	98	97
    //26	97	96
    //27	92	97
    //28	81	98
    //29	85	99
    //30	100	100

}
