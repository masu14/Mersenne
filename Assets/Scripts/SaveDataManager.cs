using System;
using UnityEngine;
using System.IO;
using UniRx;

[Serializable]
class SaveDataManager : MonoBehaviour
{
    private GameObject _savePoint;
    private SavePointController _savePointController;

    //�Z�[�u�f�[�^
    public Vector2 _nowSavePos;                    //�Z�[�u�|�C���g���W


    private IDisposable _subSavePos;          //�w�ǂ��������邽�߂̕ϐ�
    private IDisposable _subNowSavePos;

    private ReactiveProperty<Vector2> savePos = new ReactiveProperty<Vector2>();
    public IReadOnlyReactiveProperty<Vector2> SavePosition => savePos;
    

    void Start()
    {
        //�v���C���[���Z�[�u�|�C���g�ɐG�ꂽ��Z�[�u�|�C���g���X�V
        _savePoint = GameObject.FindWithTag("SavePoint");
        _savePointController = _savePoint.GetComponent<SavePointController>();
        _subSavePos =�@_savePointController.OnTrigger.Subscribe(x => savePos.Value = x);
        _subNowSavePos = _savePointController.OnTrigger.Subscribe(x => _nowSavePos = x);
    }


    private void OnDestroy()
    {
        //�I�u�W�F�N�g���j�������Ƃ��ɍw�ǂ�����
        _subSavePos.Dispose();
        _subNowSavePos.Dispose();
    }
}
