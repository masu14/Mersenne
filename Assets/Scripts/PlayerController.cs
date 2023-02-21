using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;     //����
    public float speed = 3.0f;      //�ړ����x
    public float jump = 9.0f;       //�W�����v��
    public LayerMask groundLayer;   //���n�ł��郌�C���[

    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�ڒn�t���O

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();        //Player��Rigidbody2D�擾
    }

    
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

        //�W�����v
        if(Input.GetButtonDown("Jump"))
        {
            Jump(); //�W�����v���\�b�h���Ăяo��
        }
    }

    void FixedUpdate()
    {
        //�n�㔻��
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(onGround || axisH != 0)  //�n�ʂ̏� or ���x��0�łȂ�
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);   //���x���X�V����
        }

        if(onGround && goJump)      //�n�ʂ̏�ŃW�����v�L�[�������ꂽ
        {
            Debug.Log("�W�����v");
            Vector2 jumpPw = new Vector2(0, jump);                          //������̃x�N�g��
            rbody.AddForce(jumpPw, ForceMode2D.Impulse);                    //�u�ԓI�ȗ͂�������

            goJump = false;                                                 //�W�����v�t���O�����낷
        }

           

    }

    public void Jump()
    {
        goJump = true;      //�W�����v�t���O�𗧂Ă�
        Debug.Log("�W�����v�{�^������");
    }
}
