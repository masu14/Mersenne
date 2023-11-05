using UnityEngine;
using UnityEngine.Rendering.Universal;
using UniRx;
using System;

namespace Merusenne.StageGimmick
{
    /// <summary>
    /// Goal�I�u�W�F�N�g�ɕt����X�N���v�g�R���|�[�l���g
    /// �v���C���[���G���ƃS�[���ʒm�𑗐M���ăQ�[���N���A�ƂȂ�
    /// Goal�I�u�W�F�N�g�͔������œ_�ł��Ă��鋅�̂̃I�u�W�F�N�g
    /// </summary>

    public class GoalController : MonoBehaviour
    {
        [SerializeField] private Light2D _goal_light;           //���C�g�A���������ē_�ł���
        [SerializeField] private float _duration = 1.0f;        //�_�ł̑����p�����[�^
        [SerializeField] private float _max_intensity = 2.0f;   //�������̋��x�p�����[�^
        [SerializeField] private float _min_intensity = 0.2f;   //�������̋��x�p�����[�^

        private bool _isGoal = false;       //�S�[���t���O�A�G���ƃt���O������
        private float _time = 0.0f;         //���Ԃ�deltatime���瓾��A�_�ŏ����Ɏg��

        private Subject<Unit> _onGoal = new Subject<Unit>();    //�S�[�����ɒʒm�𑗐M����
        public IObservable<Unit> OnGoal => _onGoal;

        void Start()
        {
            _isGoal = false;        //�t���O�̏�����
            _onGoal.AddTo(this);
        }


        void Update()
        {
            //�_�ŏ���
            _time += Time.deltaTime;
            _goal_light.intensity = _min_intensity + _max_intensity * Mathf.Abs(Mathf.Sin(_time * _duration));

        }

        //�v���C���[�ڐG���ɃS�[���ʒm�𑗐M����
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag == "Player")
            {
                if (_isGoal) return;
                _isGoal = true;
                _onGoal.OnNext(Unit.Default);
                Debug.Log("Goal");
            }
        }
    }

}
