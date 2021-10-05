using System.Collections;
using DG.Tweening;
using EZCameraShake;
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
        [SerializeField] private float handSpeedMultiplier = 0.066f;
        [SerializeField] private float blockDurationForMent = 3;
        [SerializeField] private LevelController levelController;
        [SerializeField] private GameObject jailPanelGO;
        [SerializeField] private Animator jailAnimator;

        private int _index;

        [HideInInspector] public bool _isBlocked;
        private bool _isBlockedByMent;
        private bool _isAudioPlayed;
        private bool _canGoBack;
        private bool _canMoveHands = true;

        private float _elapsedMoveTime = 0.0f;
        private float _elapsedWaitTime = 0.0f;
        private float _elapsedBlockTime = 0.0f;

        private float _elapsedHandGeneratorBlockTime = 0.0f;
        private float _handGeneratorBlockTime = 1f;

        // to keep default values unchanged
        private float _handMovementInterval;
        private float _handMovementTime;
        private float _handStayDuration;
        private int _currentLevel;

        private AudioManager _audioManager;

        #region Cache

        private static readonly int MoveUp = Animator.StringToHash("MoveUp");

        #endregion

        private void Start()
        {
            _audioManager = FindObjectOfType<AudioManager>();
            // to set camera shaker to initial shake value
            CameraShaker.Instance.DefaultPosInfluence = new Vector3(0, 1f, 0f);
            CameraShaker.Instance.DefaultRotInfluence = new Vector3(0, 0, 0);
            _isBlocked = true;

            //_handMovementInterval = handMovementInterval;
            //_handMovementTime = handMovementTime;
            //_handStayDuration = handStayDuration;

            _currentLevel = levelController.currentLevel; // load current level here
            _handMovementInterval = handMovementInterval - (_currentLevel * handSpeedMultiplier);
            _handMovementTime = handMovementTime - (_currentLevel * handSpeedMultiplier);
            _handStayDuration = handStayDuration - (_currentLevel * handSpeedMultiplier);

            if (_currentLevel == 7)
            {
           
                OnLevelUp();
            }
         
            //lets suppose we do it when game loads once
        }

        private void Update()
        {
            _elapsedMoveTime += Time.deltaTime;

            if (_canMoveHands)
            {
                if (!_isBlocked && !_isBlockedByMent)
                {
                    MoveHandForward();
                }

                else if (_isBlockedByMent)
                {
                    ShowJail();
                }
            }

            MoveHandBack();
        }

        public void UnblockHandGenerator()
        {
            _isBlockedByMent = false;
            _isBlocked = false;
        }

        public void UnblockHandGeneratorAfterWait()
        {
            StartCoroutine(WaitAndUnblock(1f));
        }

        private IEnumerator WaitAndUnblock(float time)
        {
            //hands[_index].handGO.SetActive(true); зачем это?
            yield return new WaitForSeconds(time);
            _isBlocked = false;
        }

        public void BlockHandGenerator()
        {
            _isBlocked = true;
            // hands[_index].handGO.SetActive(false); зачем это? если isblocked он как обычно вернет руку назад 
            //а щас если рука осталась она пропадает по дибильному
            Debug.Log("Should lock generaor");
        }

        public void BlockHandGeneratorByMent()
        {
            _isBlockedByMent = true;
            _isBlocked = true;
        }

        public void StopHands()
        {
            Debug.Log("Stop Hands");
            _canMoveHands = false;
        }

        public void MoveHands()
        {
            Debug.Log("Stop Hands");
            _canMoveHands = true;
        }

        private void ShowJail()
        {
            jailPanelGO.SetActive(true);


            if (!_isAudioPlayed)
            {
                // audioSource.Play();
                _audioManager.Play("jail");
                _isAudioPlayed = true;
            }

            _elapsedBlockTime += Time.deltaTime;

            if (_elapsedBlockTime >= blockDurationForMent)
            {
                Debug.Log("MoveUP");
                _elapsedBlockTime = 0.0f;
                jailAnimator.SetTrigger(MoveUp);
                _isBlocked = false;
            }
        }

        public void DeactivateJail()
        {
            _isBlockedByMent = false;
            jailPanelGO.SetActive(false);
        }

        private void MoveHandForward()
        {
            if (_elapsedMoveTime >= _handMovementInterval && _canMoveHands)
            {
                _canMoveHands = false;
                _isAudioPlayed = false;
                _audioManager.Play("handSwoosh");
                _index = Random.Range(0, hands.Length);
                // go to target
                hands[_index].handGO.transform.DOMove(hands[_index].target.position, _handMovementTime)
                    .SetEase(Ease.OutCubic).OnComplete(() => { _canGoBack = true; });
            }
        }

        private void MoveHandBack()
        {
            // wait time
            if (_canGoBack)
            {
                _elapsedWaitTime += Time.deltaTime;

                if (_elapsedWaitTime >= _handStayDuration)
                {
                    // go to initial position
                    hands[_index].handGO.transform.DOMove(hands[_index].initialPosition.position, _handMovementTime)
                        .OnComplete(
                            () =>
                            {
                                hands[_index].cashGO.SetActive(true);
                                hands[_index].cashGO.GetComponent<Money.Cash>().CashCanBeTaken();
                                _canMoveHands = true;
                            });
                    _elapsedMoveTime = 0.0f;
                    _elapsedWaitTime = 0.0f;
                    _canGoBack = false;
                }
            }
        }

        public void OnLevelUp()
        {
            // accelerates hand movement speed according to current level
            // called in level controller
      
            
                _handMovementInterval -= handSpeedMultiplier;
                _handMovementTime -= handSpeedMultiplier;
                _handStayDuration -= handSpeedMultiplier;
           

          
            //this would still work, ты можешь вставить тоже самое что в Старте но я не уверена в порядке: тип что первое? Лвл повышается или ускоряются руки
            //probably need to remove this
            //0.066f

            //_handMovementInterval = handMovementInterval - (currentlevel * 0.066f)
            //_handMovementTime = handMovementTime - (currentlevel * 0.066f)
            //_handStayDuration = handStayDuration - (currentlevel * 0.066f)

            // i put this in start
        }
    }
}