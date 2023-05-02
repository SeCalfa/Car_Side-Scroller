using CodeBase.Infrastracture.States;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Timer
{
    public class TimerToStart : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;

        [SerializeField]
        private Animator Three;
        [SerializeField]
        private Animator Two;
        [SerializeField]
        private Animator One;
        [SerializeField]
        private Animator Start;

        private int AppearHash = Animator.StringToHash("Appear");

        public void Construct(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            Three.SetTrigger(AppearHash);
            yield return new WaitForSeconds(1);

            Two.SetTrigger(AppearHash);
            yield return new WaitForSeconds(1);

            One.SetTrigger(AppearHash);
            yield return new WaitForSeconds(1);

            Start.SetTrigger(AppearHash);
            yield return new WaitForSeconds(1);

            gameStateMachine.Enter<GameplayState>();
        }
    }
}