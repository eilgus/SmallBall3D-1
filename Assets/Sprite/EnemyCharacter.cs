﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{

    private MainManager mainManager;

    private Transform mCharacter;
    private bool isDestroy = false;

    private Rigidbody eRigbody;
    private Vector3 toMCharacter;
    public float max = 20;


    // Use this for initialization
    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        // 获取本机玩家的对象
        mCharacter = GameObject.Find("Player").transform;
        //mCharacterComponent = mCharacter.GetComponent<Player>();
        // 显示血量和ID的组件


        txID = transform.Find("Name");
        txIDText = transform.Find("Name").GetComponent<TextMesh>();
        txHP = transform.Find("HP");
        txHPText = transform.Find("HP").GetComponent<TextMesh>();

        eRigbody = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {


        // 更新对象属性
        UpdataProperties();


        // 摧毁对象
        if (isDestroy)
        {
            Destroy(gameObject);
        }


    }

    private void FixedUpdate()
    {

        if (mainManager.pauseOrOver)
            return;
        Ai();
        LimitVelocity(max);

    }

    public void Destroy()
    {
        isDestroy = true;
    }

    public void Move(Vector3 pos, Vector3 rot, Vector3 velocity)
    {
        if (pos != transform.position)
        {
            transform.position = pos;
        }
        transform.eulerAngles = rot;
    }


    // 人物变量
    private string _name;
    public void SetName(string name)
    {
        _name = name;
    }


    public int _hp = 100;
    public void SetHP(int hpChanged)
    {
        _hp -= hpChanged;
    }
    public int GetHp()
    {
        return _hp;
    }

    private Transform txID;
    private TextMesh txIDText;
    private Transform txHP;
    private TextMesh txHPText;
    

    // 更新角色变量/属性
    private void UpdataProperties()
    {

        // 显示血量和ID
        txIDText.text = "Name:" + _name.ToString();
        txHPText.text = "HP:" + _hp.ToString();

        // 血量和ID的方向，面向着本机玩家
        txID.rotation = mCharacter.rotation;
        txHP.rotation = mCharacter.rotation;
    }

    private void Ai()
    {
        toMCharacter = mCharacter.transform.position - this.transform.position;
        eRigbody.AddForce(toMCharacter.normalized*20, ForceMode.Force);
    }

    private void LimitVelocity(float max)
    {
        if (eRigbody.velocity.magnitude >= max)
        {
            eRigbody.velocity = eRigbody.velocity.normalized * max;
        }
    }
}
