using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Action<bool> win;//ʤ���¼�
    public static Action addSilme;
    public static Action<int> resetText;
    public static bool gameStop;//�Ƿ���ҳ�浯������Ϸֹͣ״̬
    public Transform unityChan, slime;
    public GameObject winPage, losePage;//ʤ��ҳ��
    public GameObject slimecopy;//ʷ��ķԤ����
    public Text text;
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
            text.gameObject.SetActive(false);
        };
        addSilme = () =>
        {
            GameObject newSlime = Instantiate(slimecopy, initialSlime.position, initialSlime.rotation);
            newSlime.GetComponent<SlimController>().player = unityChan;
        };
        resetText = count =>
        {
            text.text = ("�÷�" + count);
        };
        ResetGame();
    }
    public void ResetGame()//������̬��ҳ��
    {
        resetText(0);
        gameStop = false;//��Ϸ����
        unityChan.position = initialUnityChan.position;
        unityChan.rotation = initialUnityChan.rotation;
        GameObject newSlime = Instantiate(slimecopy, initialSlime.position, initialSlime.rotation);
        newSlime.GetComponent<SlimController>().player = unityChan;
        winPage.SetActive(false);
        losePage.SetActive(false);
        text.gameObject.SetActive(true);
    }
}
