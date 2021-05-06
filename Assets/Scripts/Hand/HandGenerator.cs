using System.Collections;
using DG.Tweening;
using Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hand
{
    public class HandGenerator : MonoBehaviour
    {
        [SerializeField] private HandStruct[] hands;
        [SerializeField] private float handMovementInterval = 2;
        [SerializeField] private float handMovementTime = .5f;

        [SerializeField] private float handStayDuration = .5f;
        [SerializeField] private float blockDurationForMent = 3;
        [SerializeField] private LevelController levelController;
        [SerializeField] private GameObject jailPanelGO;
        [SerializeField] private Animator jailAnimator;

        private int _index;

        private bool _isBlocked;

        private bool _canGoBack;

        private bool _canMoveHands = true;

        private float _elapsedMoveTime = 0.0f;
        private float _elapsedWaitTime = 0.0f;
        private float _elapsedBlockTime = 0.0f;


        private void Update()
        {
            _elapsedMoveTime += Time.deltaTime;

            if (_canMoveHands)
            {
                if (!_isBlocked)
                {
                    // возможно сделать так чтобы руки не спавнились до того времени пока решетки не поднимутся
                    // сейчас руки показываются когда решетки еще не исчезли
                    MoveHandForward();
                }
                else
                {
                    ShowJail();
                }
            }

            MoveHandBack();
        }

        public void UnblockHandGenerator()
        {
            _isBlocked = false;
        }

        private void ShowJail()
        {
            jailPanelGO.SetActive(true);
            _elapsedBlockTime += Time.deltaTime;

            if (_elapsedBlockTime >= blockDurationForMent)
            {
                _elapsedBlockTime = 0.0f;
                jailAnimator.SetTrigger("MoveUp");
                _isBlocked = false;
            }
        }

        public void BlockHandGenerator()
        {
            _isBlocked = true;
        }

        private void MoveHandForward()
        {
            if (_elapsedMoveTime >= handMovementInterval && _canMoveHands)
            {
                _canMoveHands = false;
                _index = Random.Range(0, hands.Length);
                // go to target
                hands[_index].handGO.transform.DOMove(hands[_index].target.position, handMovementTime)
                    .SetEase(Ease.OutCubic).OnComplete(() => { _canGoBack = true; });
            }
        }

        private void MoveHandBack()
        {
            // wait time
            if (_canGoBack)
            {
                _elapsedWaitTime += Time.deltaTime;

                if (_elapsedWaitTime >= handStayDuration)
                {
                    // go to initial position
                    hands[_index].handGO.transform.DOMove(hands[_index].initialPosition.position, handMovementTime)
                        .OnComplete(
                            () =>
                            {
                                hands[_index].cashGO.SetActive(true);
                                hands[_index].cashGO.GetComponent<Cash.Cash>().CashCanBeTaken();
                                _canMoveHands = true;
                            });
                    _elapsedMoveTime = 0.0f;
                    _elapsedWaitTime = 0.0f;
                    _canGoBack = false;
                }
            }
        }
    }
}