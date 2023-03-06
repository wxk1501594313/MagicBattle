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
            anim.SetBool("isRun", false);//如果主角不是处于寻路状态，则设置状态机中设置的参数isRun为false，使角色播放站立动画
            audioSource.Stop();
        }
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitInfo))
        {
            agent.SetDestination(hitInfo.point);
            anim.SetBool("isRun", true);//寻路时播放Run动画
            audioSource.clip = run;//更换至跑步音频
            audioSource.loop = true;//开启循环播放
            audioSource.Play();//播放音频
        }
        if (Input.GetMouseButtonDown(1))//如果按下鼠标右键，播放攻击动画
        {
            anim.SetTrigger("Attack");
            particle.Play();//攻击时播放粒子特效
            foreach (var item in trails) item.enabled = true;//攻击时打开拖尾渲染器
            audioSource.clip = attack;//更换至攻击音频
            audioSource.loop = false;//关闭循环播放
            audioSource.Play();//播放音频
        }
    }
}
