using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static Action<bool> win;//ʤ���¼�
    public static bool gameStop;//�Ƿ���ҳ�浯������Ϸֹͣ״̬
    public Transform unityChan, slime;
    public GameObject winPage, losePage;//ʤ��ҳ��
    Pose initialUnityChan, initialSlime;//��¼��ɫ�ĳ�ʼ״̬
    void Start()
    {
        initialUnityChan = new Pose(unityChan.position, unityChan.rotation);//��¼UnityChan��̬
        initialSlime = new Pose(slime.position, slime.rotation);//��¼ʷ��ķ��̬
        win = result =>//���¼�
        {
            gameStop = true;//��Ϸֹͣ
            winPage.SetActive(result);//true���ʤ��ҳ��
            losePage.SetActive(!result);//false���ʧ��ҳ��
        };
    }
    public void ResetGame()//������̬��ҳ��
    {
        gameStop = false;//��Ϸ����
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
