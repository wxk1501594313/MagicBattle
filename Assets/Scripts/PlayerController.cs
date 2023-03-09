using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    RaycastHit hitInfo;
    Animator anim;
    ParticleSystem particle;
    public TrailRenderer[] trails;
    AudioSource audioSource;
    public AudioClip run, attack;
    bool isAttacking;
    int count;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        particle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.hasPath)
        {
            anim.SetBool("isRun", false);//������ǲ��Ǵ���Ѱ·״̬��������״̬�������õĲ���isRunΪfalse��ʹ��ɫ����վ������
            audioSource.Stop();
        }
        if (GameManager.gameStop)//��Ϸֹͣʱ��ֹ����
        {
            agent.ResetPath();//��յ���·��
            return;
        }
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitInfo))
        {
            agent.SetDestination(hitInfo.point);
            anim.SetBool("isRun", true);//Ѱ·ʱ����Run����
            audioSource.clip = run;//�������ܲ���Ƶ
            audioSource.loop = true;//����ѭ������
            audioSource.Play();//������Ƶ
        }
        if (Input.GetMouseButtonDown(1) && !isAttacking)//�����������Ҽ������Ź�������
        {
            anim.SetTrigger("Attack");
            particle.Play();//����ʱ����������Ч
            foreach (var item in trails) item.enabled = true;//����ʱ����β��Ⱦ��
            audioSource.clip = attack;//������������Ƶ
            audioSource.loop = false;//�ر�ѭ������
            audioSource.Play();//������Ƶ
            isAttacking = true;
            Invoke("EndAttack", 1);//1���ִ��EndAttack
        }
    }

    void EndAttack()//�ر���Ӧ��Ч
    {
        particle.Stop();
        foreach (var item in trails) item.enabled = false;
        isAttacking = false;//��¼��������
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Slim" || other.name == "Slim(Clone)")
        {
            Destroy(other.gameObject);
            if (isAttacking)
            {
                if(count >= 3)
                {
                    count = 0;
                    GameManager.win?.Invoke(true);
                    return;
                }              
                count += 1;
                GameManager.resetText(count);
                GameManager.addSilme?.Invoke();
            }
            else
            {
                count = 0;
                print("ʧ��");
                GameManager.win?.Invoke(false);
            }
        }
    }
}
