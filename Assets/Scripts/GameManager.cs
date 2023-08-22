using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    float waitTime = 2.0f;
    private string sceneName = "StageScene";
    private GameObject _player;
    private PlayerCore _playerCore;
    private SavePointController[] savePoints;

    string _filePath;
    SaveDataManager _save;
    
    private Vector2 playerPos;
    private Vector2 _playerStartPos = new Vector2(-5, 0);

    void Awake()
    {
        //�v���C���[��Dead�����Ƃ��Q�[�����X�^�[�g
        _player = GameObject.FindWithTag("Player");
        _playerCore = _player.GetComponent<PlayerCore>();
        _playerCore.OnDead.Subscribe(_=>WaitGameRestart()).AddTo(this);


        _filePath = Application.dataPath + "/.savedata.json";
        _save = new SaveDataManager();

        //�Z�[�u�|�C���g�ɐG�ꂽ�Ƃ����W���Z�[�u
        savePoints = FindObjectsOfType<SavePointController>();
        foreach(var savePoint in savePoints)
        {
            savePoint.OnTriggerSave
                .Where(x => x != Vector2.zero)
                .Subscribe(x =>
            {
                _save._nowSavePos = x;
                
                Debug.Log($"�Z�[�u�|�C���g�̈ʒu��ύX���܂���:{x}");
            }).AddTo(this);
        }
        

    }

    private void Start()
    {
        Load();     //�Z�[�u�f�[�^�̃��[�h
        _player.transform.position = _save._nowSavePos;
        
    }
    /*
    void SaveGame(Vector2 data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(_savePath, jsonData);
    }*/


    public void Save()
    {
        
        Debug.Log($"�Z�[�u����nowSavePos:{_save._nowSavePos}");
        string json = JsonUtility.ToJson(_save);
        StreamWriter streamWriter = new StreamWriter(_filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
    }

    public void Load()
    {
        if (File.Exists(_filePath))
        {
            StreamReader streamReader = new StreamReader(_filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            _save = JsonUtility.FromJson<SaveDataManager>(data);
            //playerPos = _save._nowSavePos;
            Debug.Log($"���[�h����nowSavePos:{_save._nowSavePos}");
        }
        else
        {
            Debug.Log("�Z�[�u�f�[�^��������܂���B�V�����Q�[�����J�n���܂��B");
            playerPos = _playerStartPos;
        }
    }

    void WaitGameRestart()
    {
        Observable.Timer(TimeSpan.FromSeconds(waitTime)).Subscribe(_ => GameRestart());
    }
    private void GameRestart()
    {
        Save();
        SceneManager.LoadScene(sceneName);
    }


}
