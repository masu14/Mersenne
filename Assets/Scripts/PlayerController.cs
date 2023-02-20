using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;     //����
    public float speed = 3.0f;      //�ړ����x

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();        //Player��Rigidbody2D�擾
    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");     //���������̓��͂��`�F�b�N����

        //�����̒���
        if(axisH > 0.0f)        //�E�ړ�
        {
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(1, 1);
        }
        else if(axisH < 0.0f)   //���ړ�
        {
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-1, 1);  //���E���]������
        }
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2(speed*axisH, rbody.velocity.y);   //���x���X�V����   
    }
}
