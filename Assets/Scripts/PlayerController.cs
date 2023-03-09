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
            anim.SetBool("isRun", false);//如果主角不是处于寻路状态，则设置状态机中设置的参数isRun为false，使角色播放站立动画
            audioSource.Stop();
        }
        if (GameManager.gameStop)//游戏停止时禁止操作
        {
            agent.ResetPath();//清空导航路径
            return;
        }
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitInfo))
        {
            agent.SetDestination(hitInfo.point);
            anim.SetBool("isRun", true);//寻路时播放Run动画
            audioSource.clip = run;//更换至跑步音频
            audioSource.loop = true;//开启循环播放
            audioSource.Play();//播放音频
        }
        if (Input.GetMouseButtonDown(1) && !isAttacking)//如果按下鼠标右键，播放攻击动画
        {
            anim.SetTrigger("Attack");
            particle.Play();//攻击时播放粒子特效
            foreach (var item in trails) item.enabled = true;//攻击时打开拖尾渲染器
            audioSource.clip = attack;//更换至攻击音频
            audioSource.loop = false;//关闭循环播放
            audioSource.Play();//播放音频
            isAttacking = true;
            Invoke("EndAttack", 1);//1秒后执行EndAttack
        }
    }

    void EndAttack()//关闭相应特效
    {
        particle.Stop();
        foreach (var item in trails) item.enabled = false;
        isAttacking = false;//记录攻击结束
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
                print("失败");
                GameManager.win?.Invoke(false);
            }
        }
    }
}
