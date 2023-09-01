using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System;
using System.IO;

/// <summary>
/// �Q�[���S�̂̊Ǘ����s���N���X
/// �V�[���̑J�ځA�Z�[�u�A���[�h�̐�����s��
/// </summary>
public class GameManager : MonoBehaviour
{
    
    private GameObject _player;
    private PlayerCore _playerCore;
    private SavePointController[] _savePoints;
    private SaveDataManager _save;
    
    //�p�����[�^
    [SerializeField] private float _loadWaitTime = 2.0f;                    //�v���C���[��Dead���Ă��烍�[�h�����܂ł̎���

    private string _sceneName = "StageScene";                               //���[�h����V�[����
    private string _filePath;                                               //�Z�[�u�f�[�^�̕ۑ���
    private Vector2 _playerStartPos = new Vector2(-5, 0);                   //�Z�[�u�f�[�^���Ȃ��Ƃ��̃v���C���[�̊J�n�ʒu
    private Vector2 _playerPosUp = new Vector2(0, 2);                       //�Z�[�u�|�C���g���̃v���C���[�̊J�n�ʒu

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");                         //�v���C���[�擾
        _playerCore = _player.GetComponent<PlayerCore>();                   //�v���C���[�̏�Ԏ擾
        _filePath = Application.dataPath + "/.savedata.json";               //�Z�[�u�f�[�^�̕ۑ���o�^
        _save = new SaveDataManager();                                      //�Z�[�u�f�[�^�̊Ǘ���擾
        _savePoints = FindObjectsOfType<SavePointController>();             //�V�[����̑S�ăZ�[�u�|�C���g���擾

        //�Z�[�u�f�[�^�̃��[�h
        Load();     

        //�v���C���[Dead���Ɉ�莞�Ԃ������ă����[�h�AOnDestroy����Dispose()�����悤�ɓo�^
        _playerCore.OnDead.Subscribe(_ => WaitGameRestart()).AddTo(this);

        //�S�ẴZ�[�u�|�C���g���w��
        foreach (var savePoint in _savePoints)
        {
            //�Z�[�u�|�C���g�ɐG�ꂽ�Ƃ��Z�[�u�f�[�^���X�V�AOnDestroy����Dispose()�����悤�ɓo�^
            savePoint.OnTriggerSave
                .Subscribe(x =>
                {
                    _save._nowSavePos = x;

                    Debug.Log($"�Z�[�u�|�C���g�̈ʒu��ύX���܂���:{x}");
                }).AddTo(this);
        }

    }

    private void Start()
    {
        _player.transform.position = _save._nowSavePos + _playerPosUp;      //���[�h���̃v���C���[�̊J�n�ʒu

    }
    //�Z�[�u�f�[�^���X�V���Z�[�u����
    public void Save()
    {
        
        Debug.Log($"�Z�[�u����nowSavePos:{_save._nowSavePos}");
        string json = JsonUtility.ToJson(_save);                    //�Z�[�u�f�[�^��json������ɕϊ�
        StreamWriter streamWriter = new StreamWriter(_filePath);    //_filePath��json�������ۑ�����e�L�X�g�t�@�C���쐬
        streamWriter.Write(json); streamWriter.Flush();             //json������̏������݁A�������ݑ���̊m��
        streamWriter.Close();                                       //�t�@�C������ď������ݏI��
    }

    //�Q�[���J�n���A�v���C���[Dead���Ƀ��[�h����
    public void Load()
    {
        if (File.Exists(_filePath))     //�t�@�C�������݂���Ƃ�
        {
            StreamReader streamReader = new StreamReader(_filePath);    //_filePath��json�������ǂݍ��ރt�@�C���쐬
            string data = streamReader.ReadToEnd();                     //�t�@�C���S�̂�ǂݍ���data�Ɋi�[
            streamReader.Close();                                       //�t�@�C������ēǂݍ��ݏI��
            _save = JsonUtility.FromJson<SaveDataManager>(data);        //json��������Z�[�u�f�[�^�ɕϊ�
            Debug.Log($"���[�h����nowSavePos:{_save._nowSavePos}");
        }
        else                           //�t�@�C�������݂��Ȃ��Ƃ�
        {
            Debug.Log("�Z�[�u�f�[�^��������܂���B�V�����Q�[�����J�n���܂��B");
            _save._nowSavePos = _playerStartPos;                        //�f�t�H���g�̃v���C���[�̊J�n�ʒu���i�[
        }
    }

    //�v���C���[Dead���Ɉ�莞�ԑ҂�
    void WaitGameRestart()
    {
        Observable.Timer(TimeSpan.FromSeconds(_loadWaitTime)).Subscribe(_ => GameRestart());
    }

    //�v���C���[Dead���Ɉ�莞�Ԍo�ߌ�A�Z�[�u�f�[�^���Z�[�u�����ナ���[�h����
    private void GameRestart()
    {
        Save();
        SceneManager.LoadScene(_sceneName);
    }


}
