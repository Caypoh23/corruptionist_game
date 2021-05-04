using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hand
{
    public class HandGenerator : MonoBehaviour
    {
        [SerializeField] private HandStruct[] hands;
        [SerializeField] private float movementTime = 2;

        [SerializeField] private float stayDuration = 1;
        [SerializeField] private float blockDurationForMent = 3;
        [SerializeField] private LevelController levelController;

        private int _index;
        private bool _isBlocked;
        public bool CanPlay { get; set; } = true;


        private void Update()
        {
            //InvokeRepeating($"MoveHandLerp", 0f, 0.2f);
            if (CanPlay)
            {
                var randomIndex = Random.Range(0, hands.Length);
                if (!_isBlocked)
                {
                    StartCoroutine(MoveObject(randomIndex));
                }
                else
                {
                    StartCoroutine(BlockHands());
                }
               
                // making hand avaibale again
                hands[randomIndex].cashGO.GetComponent<SpriteRenderer>().enabled = true;
                hands[randomIndex].cashGO.GetComponent<Cash.Cash>().CashCanBeTaken();
            }
        }

        /*private void MoveHand()
    {
        _index = Random.Range(0, hands.Length);
        hands[_index].handGO.transform.DOMove(hands[_index].target.position, duration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                hands[_index].handGO.transform.DOMove(hands[_index].initialPosition.position, duration);
            });
    }*/

        /*private void MoveHandLerp()
    {
        _index = Random.Range(0, hands.Length);
        hands[_index].handGO.transform.position = Vector3.Lerp(hands[_index].initialPosition.position,
            hands[_index].target.position,
            Mathf.PingPong(Time.time / duration, 2));
    }*/

        private IEnumerator BlockHands()
        {
            _isBlocked = true;
            yield return new WaitForSeconds(blockDurationForMent);
            _isBlocked = false;
        }

        private IEnumerator MoveObject(int index)
        {
            hands[index].handGO.transform.DOMove(hands[index].target.position, movementTime)
                .SetEase(Ease.OutCubic);

            CanPlay = false;
            yield return new WaitForSeconds(stayDuration + movementTime);

            if (!CanPlay)
            {
                hands[index].handGO.transform.DOMove(hands[index].initialPosition.position, movementTime);
                yield return new WaitForSeconds(movementTime);
                //pulsePanel.SetActive(false);
                if (levelController.loopNumber != 0)
                {
                    CanPlay = true;
                }
            }
        }

        public void BlockHandGenerator()
        {
            _isBlocked = true;
        }
    }
}