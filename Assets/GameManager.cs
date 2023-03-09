using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static Action<bool> win;//胜负事件
    public static bool gameStop;//是否处于页面弹出，游戏停止状态
    public Transform unityChan, slime;
    public GameObject winPage, losePage;//胜负页面
    Pose initialUnityChan, initialSlime;//记录角色的初始状态
    void Start()
    {
        initialUnityChan = new Pose(unityChan.position, unityChan.rotation);//记录UnityChan姿态
        initialSlime = new Pose(slime.position, slime.rotation);//记录史莱姆姿态
        win = result =>//绑定事件
        {
            gameStop = true;//游戏停止
            winPage.SetActive(result);//true则打开胜利页面
            losePage.SetActive(!result);//false则打开失败页面
        };
    }
    public void ResetGame()//重制姿态与页面
    {
        gameStop = false;//游戏运行
        unityChan.position = initialUnityChan.position;
        unityChan.rotation = initialUnityChan.rotation;
        slime.position = initialSlime.position;
        slime.rotation = slime.rotation;
        winPage.SetActive(false);
        losePage.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
