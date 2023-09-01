using UnityEngine;
using UnityEngine.Rendering.Universal;
using UniRx;
using System;

/// <summary>
/// �Z�[�u�|�C���g�̐��������N���X
/// �v���C���[���Z�[�u�|�C���g�ɐG�ꂽ��A���񃍁[�h���v���C���[�͂��̈ʒu����Q�[�����J�n����
/// </summary>
public class SavePointController : MonoBehaviour
{
    private Light2D _saveLight;             //�q�I�u�W�F�N�g��Light
    private Vector2 _savePointPos;          //�Z�[�u�|�C���g�̈ʒu

    //�Ō�ɐG�ꂽ�Z�[�u�|�C���g�̈ʒu��ێ�
    private Subject<Vector2> _savePoint = new Subject<Vector2>();

    //�Ō�ɐG�ꂽ�Z�[�u�|�C���g�̈ʒu�𑗐M
    public IObservable<Vector2> OnTriggerSave => _savePoint;
    void Start()
    {
        _saveLight = transform.GetChild(0).GetComponent<Light2D>();     //�q�I�u�W�F�N�g��Light�擾
        _savePointPos = transform.position;                             //���g�̈ʒu���Z�[�u�|�C���g�ɓo�^

        //OnDestroy����Dispose()�����悤�ɓo�^
        _savePoint.AddTo(this);
    }

    //�Z�[�u�|�C���g�ɐG�ꂽ��X�V
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")    
        {
            _saveLight.color = new Color32(252, 252, 252, 252);     //�Z�[�u�|�C���g�̐F���ς��
            Debug.Log("savepoint");
            _savePoint.OnNext(_savePointPos);
        }
    }
}
