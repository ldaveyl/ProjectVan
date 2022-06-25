using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace CreatingWithCharlotte.View
{
    public class AttractedToPlayer : MonoBehaviour
    {
        public NavMeshAgent Agent;
        //This should be the tag of the player
        public string PlayerTag = "Player";
        public float DurationToFollowBeforeGivingUp = 5f;

        private bool _isFollowingPlayer = false;
        private Transform _player;
        private Coroutine _runWait;

        private void Update()
        {
            if (_isFollowingPlayer && _player != null)
            {
                Agent?.SetDestination(_player.position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == PlayerTag)
            {
                _isFollowingPlayer = true;
                if (_player == null)
                {
                    _player = other.gameObject.transform;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_isFollowingPlayer && _player != null)
            {
                Agent?.SetDestination(_player.position);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == PlayerTag)
            {
                if (_runWait != null)
                    StopCoroutine(_runWait);

                _runWait = StartCoroutine(RunWaitAndTurnOff());
            }
        }

        private IEnumerator RunWaitAndTurnOff()
        {
            yield return new WaitForSeconds(DurationToFollowBeforeGivingUp);
            _isFollowingPlayer = false;
        }
    }
}
