using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Merusenne.StageGimmick
{
    public class TutorialSpotController : MonoBehaviour
    {
        [SerializeField] private GameObject _tutorial;

        void Start()
        {

        }


        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _tutorial.GetComponent<PopUpController>().Open();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                _tutorial.GetComponent<PopUpController>().Close();
            }
        }
    }

}
