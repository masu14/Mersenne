using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Merusenne.Player;

/// <summary>
/// �Q�[����ʍ���̃V���b�gUI�̐���N���X
/// �v���C���[�̌��ݑI�����Ă���V���b�g�𖾂�߂ɁA����ȊO�̃V���b�g���Â߂ɕ\������
/// </summary>
public class ShotUICotroller : MonoBehaviour
{
    private PlayerShot _playerShot;

    private int _shotColor =0;                                      //�I�𒆂̃V���b�g�̐F�ԍ�(blue=0,green=1,red=2)
    private Color32 _onColor = new Color32(255, 255, 255, 255);     //�����
    private Color32 _offColor = new Color32(255, 255, 255, 100);    //�Â�
    void Start()
    {
        
        _playerShot = GameObject.FindWithTag("Player").GetComponent<PlayerShot>();  //�v���C���[�̃V���b�g����擾

        //�v���C���[���V���b�g�؂�ւ����w�ǁA�V���b�gUI�X�V�AOnDestroy����Dispose()�����悤�ɓo�^
        _playerShot.OnShotSwitch
            .Subscribe(x =>
            {
                _shotColor = x;
                UpdateColor();
            })
            .AddTo(this);
        
    }

    //�v���C���[���I�𒆂̃V���b�g�̐F�ɉ����ăV���b�gUI�̖��邳��ς���
    private void UpdateColor()
    {
        string expectedTag = GetExpectedTag(_shotColor);                                //�I�𒆂̃V���b�g�̐F�̃^�O�擾

        //�I�𒆂̃V���b�g�̐F�𖾂�߂ɁA����ȊO�̃V���b�g�̐F���Â߂�
        if (!string.IsNullOrEmpty(expectedTag) && gameObject.CompareTag(expectedTag))
        {
            GetComponent<Image>().color = _onColor;     //�����
            Debug.Log(expectedTag);
        }
        else
        {
            GetComponent<Image>().color = _offColor;    //�Â�
        }
        
    }

    //�V���b�g�̐F�ԍ�����^�O��Ԃ�(blue=0,green=1,red=2)
    private string GetExpectedTag(int colorIndex)
    {
        switch (colorIndex)
        {
            case 0:                     //�F
                return "Blue_shot_UI";
            case 1:                     //�ΐF
                return "Green_shot_UI";
            case 2:                     //�ԐF
                return "Red_shot_UI";
            default:
                return "";
        }
    }
}
        
