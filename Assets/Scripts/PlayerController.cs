using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;             //����
    public bool playerxDirection = true;   //�E�������ǂ����̔���
    public bool playerRight = true;        //�����R�[�h�����̌���

    private bool goShot = true;            //�����R�[�h�g�p�\����

    

    [SerializeField] private float speed = 3.0f;      //�ړ����x
    [SerializeField] private float jump = 9.0f;       //�W�����v��
    [SerializeField] private LayerMask groundLayer;   //���n�ł��郌�C���[

    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�ڒn�t���O

    [SerializeField] private ShotController shotPrefab;     //shotPrefab���w��
    Vector3 shotPoint;                                      //�����R�[�h�̈ʒu

    

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();        //Player��Rigidbody2D�擾

        shotPoint = transform.Find("ShotPoint").localPosition;
    }

    
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");     //���������̓��͂��`�F�b�N����

        //�����̒���
        if(axisH > 0.0f)        //�E�ړ�
        {
            Debug.Log("�E�ړ�");
            transform.localScale = new Vector2(1, 1);
            playerxDirection = true;
        }
        else if(axisH < 0.0f)   //���ړ�
        {
            Debug.Log("���ړ�");
            transform.localScale = new Vector2(-1, 1);  //���E���]������
            playerxDirection = false;
        }

        //�W�����v
        if(Input.GetButtonDown("Jump"))
        {
            Jump(); //�W�����v���\�b�h���Ăяo��
        }

        //�����R�[�h����
        if(Input.GetButtonDown("Fire1")�@&& goShot == true)
        {
            Shot(); //�V���b�g���\�b�h���Ăяo��
            goShot = false;
            Invoke(nameof(GoShot), 2.0f);
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

    public void Shot()
    {
        if(playerxDirection)
        {
            Instantiate(this.shotPrefab, transform.position + shotPoint, Quaternion.identity);    //�����R�[�h����
            playerRight = true;
        }
        else
        {
            Instantiate(this.shotPrefab, transform.position + new Vector3(-shotPoint.x, shotPoint.y, 0), Quaternion.identity);    //�����R�[�h����
            playerRight = false;
        }
    }

    public void GoShot()
    {
        goShot = true;
    }
}
