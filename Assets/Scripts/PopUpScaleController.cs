using UnityEngine;
using UniRx;
using System;

/// <summary>
/// �`���[�g���A���摜�̃|�b�v�A�b�v�̃T�C�Y���R���g���[������N���X
/// PopUpController�N���X�ŃC���X�^���X�𐶐����AOpen(),Close()���\�b�h���Ă΂ꂽ�Ƃ�
/// �|�b�v�A�b�v�̃T�C�Y���w��̃p�����[�^�ŕύX����
/// �p�����[�^�̕ύX��tutorial�I�u�W�F�N�g��PopUpController�R���|�[�l���g���ŕύX�ł���
/// </summary>

[Serializable]
public class PopUpScaleController
{
    [SerializeField] Vector2 _from_size, _to_size;                                                          //PopUp�̍ŏ��ƍŌ�̑傫��
    [SerializeField] float _duration;                                                                       //PopUp�̃T�C�Y�ύX�ɂ����鎞��
    [SerializeField] AnimationCurve _curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));    //�T�C�Y�ύX�̃A�j���[�V������ύX�ł���
    
    private GameObject _target;         //�T�C�Y�ύX���s���|�b�v�A�b�v�̃I�u�W�F�N�g���i�[
    
    private Subject<Unit> _scaleEndStream = new Subject<Unit>();                                    //�|�b�v�A�b�v�̃T�C�Y�ύX��̒ʒm
    public IObservable<Unit> OnscaleEnd { get { return _scaleEndStream.AsObservable(); } }          //�|�b�v�A�b�v�̃T�C�Y�ύX��̒ʒm�𑗐M


    //�|�b�v�A�b�v���J���Ƃ��A����Ƃ��p�ɂ��ꂼ��C���X�^���X����Ăяo�����
    public void Setup(GameObject t)
    {
        _target = t;    //�|�b�v�A�b�v�̃I�u�W�F�N�g�i�[
    }

    //�J���Ƃ��A����Ƃ��ɃT�C�Y�ύX���s��
    public void Play()
    {
        //�t���[���Ԃ̎��Ԃ��g���ď������s���A�T�C�Y�ύX���I�������ʒm�𑗐M���ăI�u�W�F�N�g��j������
        Observable.EveryFixedUpdate()                   
            .Take(TimeSpan.FromSeconds(_duration))
            .Select(_ => Time.fixedDeltaTime)
            .Scan((acc, current) => acc + current)
            .Subscribe(time => {
                float t = time / _duration;
                _target.transform.localScale = Vector3.Lerp(_from_size, _to_size, _curve.Evaluate(t));  //�T�C�Y�ύX���s������
            },
            _ => {
            },
            () => _scaleEndStream.OnNext(Unit.Default)
        ).AddTo(_target);
    }
}