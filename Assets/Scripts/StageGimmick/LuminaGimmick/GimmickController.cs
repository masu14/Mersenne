using System;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Merusenne.StageGimmick.LuminaGimmick
{
    /// <summary>
    /// �X�e�[�W��̔����M�~�b�N�𐧌䂷��N���X
    /// gimmickObject�Ƀv���C���[�̃V���b�g�𓖂Ă�ƃM�~�b�N���쓮
    /// �V���b�g�̏Փˎ���gimmickObject�̑��I�u�W�F�N�g�ɂ���LuminaBoard,Barrier�ɃV���b�g�̐F�𑗐M����
    /// </summary>
    public class GimmickController : MonoBehaviour
    {
        [SerializeField] private Light2D _point_light;                //�q�I�u�W�F�N�g��GimmickLight

        private Color32 _blue = new Color32(127, 255, 255, 255);    //�F
        private Color32 _green = new Color32(56, 241, 104, 255);    //�ΐF
        private Color32 _red = new Color32(231, 69, 69, 255);       //�ԐF
        private Color32 _clear = Color.clear;                       //���F
        private float _time = 0f;

        //�p�����[�^
        [SerializeField] float _flash_speed = 2.0f;                 //�_�Ŏ���

        //UniRx��Subject���`
        private Subject<Color32> _collisionColor = new Subject<Color32>();
        private Subject<GameObject> _collisionObject = new Subject<GameObject>();

        //�Փ˂����V���b�g�Ƃ��̐F�𑗐M
        public IObservable<GameObject> OnCollisionObj => _collisionObject;
        public IObservable<Color32> OnCollision => _collisionColor;

        private void Update()
        {
            _time += Time.deltaTime;
            _point_light.intensity = Mathf.Sin(_time * _flash_speed);
        }

        //�V���b�g�ƏՓ˂����Ƃ��ɃM�~�b�N�쓮
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //�^�O���擾
            string shotTag = collision.gameObject.tag;

            //�^�O����V���b�g�̐F���擾���A�Փ˂����V���b�g�Ƃ��̐F�𑗐M
            if (shotTag == "Shot_blue" || shotTag == "Shot_green" || shotTag == "Shot_red")
            {
                Color32 collisionColor = GetShotColor(shotTag);     //�F���擾

                _point_light.color = collisionColor;                  //�擾�����F�ɔ���
                _collisionColor.OnNext(collisionColor);             //�F�𑗐M
                _collisionObject.OnNext(collision.gameObject);      //�Փ˂����V���b�g�𑗐M
            }

        }

        //�^�O����F���擾����
        private Color32 GetShotColor(string shotTag)
        {
            switch (shotTag)
            {
                case "Shot_blue":   //�F
                    return _blue;
                case "Shot_green":  //�ΐF
                    return _green;
                case "Shot_red":    //�ԐF
                    return _red;
                default:            //�f�t�H���g�l
                    return _clear;
            }
        }
    }

}