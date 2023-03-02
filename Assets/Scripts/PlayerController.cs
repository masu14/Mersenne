using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;                    //����
    public bool playerxDirection = true;   //�E�������ǂ����̔���
    public bool playerRight = true;        //�����R�[�h�����̌���

    private bool goShot = true;                         //�����R�[�h�t���O
    [SerializeField] private float goShotTime = 1.5f;   //�����R�[�h�ҋ@����

    

    [SerializeField] private float speed = 3.0f;      //�ړ����x
    [SerializeField] private float jump = 9.0f;       //�W�����v��
    [SerializeField] private LayerMask groundLayer;   //���n�ł��郌�C���[

    bool goJump = false;            //�W�����v�J�n�t���O
    bool onGround = false;          //�ڒn�t���O

    [SerializeField] private ShotController shotBluePrefab;     //shotPrefab���w��
    [SerializeField] private ShotController shotGreenPrefab;
    [SerializeField] private ShotController shotRedPrefab;
    Vector3 shotPoint;                                      //�����R�[�h�̈ʒu

    private int shotSwitch = 0;

    

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();                        //Player��Rigidbody2D�擾

        shotPoint = transform.Find("ShotPoint").localPosition;      //�v���C���[�̎q�I�u�W�F�N�g�̈ʒu�擾
    }

    
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");     //���������̓��͂��`�F�b�N����

        //�����̒���
        if(axisH > 0.0f)        //�E�ړ�
        {
            transform.localScale = new Vector2(1, 1);
            playerxDirection = true;
        }
        else if(axisH < 0.0f)   //���ړ�
        {
            transform.localScale = new Vector2(-1, 1);  //���E���]������
            playerxDirection = false;
        }

        //�W�����v
        if(Input.GetButtonDown("Jump"))
        {
            Jump(); //�W�����v���\�b�h���Ăяo��
        }

        //�����R�[�h�؂�ւ��@blue=0, green=1, red=2
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            shotSwitch++;
            if (shotSwitch > 2)
            {
                shotSwitch = 0;
            }
        }

        //�����R�[�h����
        if(Input.GetButtonDown("Fire1")�@&& goShot == true)
        {
            Shot();                                     //�V���b�g���\�b�h���Ăяo��
            goShot = false;                             //�����R�[�h�t���O�����낷
            Invoke(nameof(GoShot), goShotTime);         //�����R�[�h�ҋ@���Ԍo�ߌ�AGoShot���\�b�h���Ăяo��
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

    public void Shot()      //�����R�[�h����
    {
        if(shotSwitch == 0) //�����R�[�h�v���n�u����
        {
            if (playerxDirection)
            {
                Instantiate(this.shotBluePrefab, transform.position + shotPoint, Quaternion.identity);    //�E����
                playerRight = true;
            }
            else
            {
                Instantiate(this.shotBluePrefab, transform.position + new Vector3(-shotPoint.x, shotPoint.y, 0), Quaternion.identity);    //������
                playerRight = false;
            }
        }

        if(shotSwitch == 1) //�Ή����R�[�h�v���n�u����
        {
            if (playerxDirection)
            {
                Instantiate(this.shotGreenPrefab, transform.position + shotPoint, Quaternion.identity);    //�E����
                playerRight = true;
            }
            else
            {
                Instantiate(this.shotGreenPrefab, transform.position + new Vector3(-shotPoint.x, shotPoint.y, 0), Quaternion.identity);    //������
                playerRight = false;
            }
        }

        if(shotSwitch == 2) //�ԉ����R�[�h�v���n�u����
        {
            if (playerxDirection)
            {
                Instantiate(this.shotRedPrefab, transform.position + shotPoint, Quaternion.identity);    //�E����
                playerRight = true;
            }
            else
            {
                Instantiate(this.shotRedPrefab, transform.position + new Vector3(-shotPoint.x, shotPoint.y, 0), Quaternion.identity);    //������
                playerRight = false;
            }
        }
        
    }

    public void GoShot()
    {
        goShot = true;      //�����R�[�h�t���O�𗧂Ă�
    }
}
