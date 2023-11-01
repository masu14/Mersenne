using System.Collections;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// ���[�h���̃t�F�[�h�C���A�S�[�����̃t�F�[�h�A�E�g���s���N���X
/// �t�F�[�h�C����Start���珙�X�ɃA���t�@�l������������
/// �t�F�[�h�A�E�g��Goal�I�u�W�F�N�g�ɐG�ꂽ�Ƃ��ɃA���t�@�l��傫������
/// TitleScene��StageScene�̗����Ŏg��
/// 
/// </summary>
public class FadeController : MonoBehaviour
{
    private Image _fade;                        //UI>Image�I�u�W�F�N�g�A��Ԏ�O�̃��C���[�Ŋ�{����
    private GameObject _goal;                   //Goal�I�u�W�F�N�g�A�G���ƃQ�[���N���A�ƂȂ�A��ʂ��t�F�[�h�A�E�g(��)����
    private GoalController _goalController;     //Goal�I�u�W�F�N�g���v���C���[�Ƃ̐ڐG�����m�����Ƃ��ɂ��̏�Ԃ𑗐M���A���̃N���X��������w�ǂ���

    [SerializeField] private  float _fade_in_time = 2.0f;   //�t�F�[�h�C������܂ł̎���
    [SerializeField] private float _fade_out_time = 2.0f;   //�t�F�[�h�A�E�g����܂ł̎���


    private Color32 _startInColor = new Color32(0, 0, 0, 255);          //�t�F�[�h�C���O�̐F(��)
    private Color32 _startOutColor = new Color32(255, 255, 255, 0);     //�t�F�[�h�A�E�g�O�̐F(����)
    private Color32 _endOutColor = new Color32(255, 255, 255, 255);     //�t�F�[�h�A�E�g��̐F(��)

    private void Awake()
    {
        //�X�e�[�W�V�[�����Goal�I�u�W�F�N�g�Ƃ��̃X�N���v�g���擾���A�w�ǂ̎葱��������
        if (SceneManager.GetActiveScene().name == "StageScene")
        {
            _goal = GameObject.FindWithTag("Goal");
            _goalController = _goal.GetComponent<GoalController>();

            //Goal�I�u�W�F�N�g�ɐڐG�����ۂ�FadeOut()�̏��������s
            _goalController.OnGoal
           .Subscribe(_ => StartFadeOut())
           .AddTo(this);
        }

        //�t�F�[�h�C���A�A�E�g���s���I�u�W�F�N�g���擾
        if (_fade == null)
        {
            _fade = GetComponent<Image>();
        }
    }

    void Start()
    {
        StartCoroutine(FadeIn());       //�V�[�����[�h���Ƀt�F�[�h�C��������
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    //�Â���ʂ��珙�X�ɃA���t�@�l�����������ɂ���
    private IEnumerator FadeIn()        //StartCoroutine�ŌĂ�
    {
        var timer = 0.0f;   //�A���t�@�l�����ԕω������邽�߂Ɏg��

        //�A���t�@�l�̑傫�������ԕω��ɂ�菙�X�ɏ���������
        while (timer < _fade_in_time)
        {
            timer += Time.deltaTime;
            float alpha = 1.0f -timer / _fade_in_time;      //�ꎟ�֐�
            Color newColor = _startInColor;
            newColor.a = alpha;
            _fade.color = newColor;

            yield return null;
        }
    }

    //Goal�I�u�W�F�N�g�ɐG�ꂽ��Ăяo����A��ʂ����X�ɔ����Ȃ�
    private IEnumerator FadeOut()
    {
        Debug.Log("FadeOut");
        var timer = 0.0f;
        Color startColor = _startOutColor;  //����
        Color endColor = _endOutColor;      //��

        //�A���t�@�l�̑傫�������ԕω��ɂ�菙�X�ɑ傫������
        while(timer<_fade_out_time)
        {
            timer += Time.deltaTime;
            float alpha = timer / _fade_out_time;
            Color newColor = _startOutColor;
            newColor.a = alpha;
            _fade.color = newColor;

            yield return null;

        }
    }
}
