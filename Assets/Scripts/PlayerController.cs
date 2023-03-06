using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    RaycastHit hitInfo;
    Animator anim;
    ParticleSystem particle;
    public TrailRenderer[] trails;
    AudioSource audioSource;
    public AudioClip run, attack;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.hasPath)
        {
            anim.SetBool("isRun", false);//������ǲ��Ǵ���Ѱ·״̬��������״̬�������õĲ���isRunΪfalse��ʹ��ɫ����վ������
            audioSource.Stop();
        }
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitInfo))
        {
            agent.SetDestination(hitInfo.point);
            anim.SetBool("isRun", true);//Ѱ·ʱ����Run����
            audioSource.clip = run;//�������ܲ���Ƶ
            audioSource.loop = true;//����ѭ������
            audioSource.Play();//������Ƶ
        }
        if (Input.GetMouseButtonDown(1))//�����������Ҽ������Ź�������
        {
            anim.SetTrigger("Attack");
            particle.Play();//����ʱ����������Ч
            foreach (var item in trails) item.enabled = true;//����ʱ����β��Ⱦ��
            audioSource.clip = attack;//������������Ƶ
            audioSource.loop = false;//�ر�ѭ������
            audioSource.Play();//������Ƶ
        }
    }
}
