using CodeBase.Infrastracture.States;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Entity
{
    public class Police : MonoBehaviour
    {
        public float speed { get; set; }

        private float lifeTime = 10f;
        private bool isDisappear;

        private SpriteRenderer spriteRenderer;

        private GameStateMachine gameStateMachine;

        public void Construct(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Movement();
            LifeTimeCalc();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Detect(collision);
        }

        private void Detect(Collider2D collision)
        {
            if (collision.CompareTag(Constants.PlayerTag))
            {
                gameStateMachine.Enter<LoseModeState>();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }

        private void Movement()
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }

        private void LifeTimeCalc()
        {
            lifeTime -= Time.deltaTime;

            if (lifeTime < 0 && !isDisappear)
            {
                isDisappear = true;
                StartCoroutine(Disappear());
            }
        }

        private IEnumerator Disappear()
        {
            while (spriteRenderer.color.a > 0)
            {
                yield return new WaitForSeconds(0.01f);
                spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - 0.01f);
            }

            Destroy(gameObject);
        }
    }
}