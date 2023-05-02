using CodeBase.Infrastracture.States;
using CodeBase.Logic.Panels;
using CodeBase.Logic.Presenters;
using CodeBase.Logic.Spawners;
using CodeBase.Services;
using CodeBase.Services.Locator;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic.Entity
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private GameObject shield;
        [SerializeField]
        private GameObject magnet;
        [SerializeField]
        private GameObject nitro;
        [Space]
        [SerializeField]
        private GameObject police;

        private int hp = 100;
        private float speed = 2;
        private float speedCor = 1;
        private float speedChangeTime;
        private float policeSpawnTime = 60;
        private float posX;
        private bool isSpeedUp;
        private bool isSpeedDown;
        private bool isTurnLeft;
        private bool isTurnnRight;

        private float time;
        private float startPosY;
        private float currentPosY;
        private int previousSpawnDistance;
        private Coroutine currentEffect;

        public int score { get; private set; } = 0;

        public event Action<int> OnCollectCoin;
        public event Action<int> OnScoreAdd;

        private GameStateMachine gameStateMachine;
        private GameFactory gameFactory;
        private GameObjectsLocator gameObjectsLocator;

        public void Construct(GameStateMachine gameStateMachine, GameFactory gameFactory, GameObjectsLocator gameObjectsLocator)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameFactory = gameFactory;
            this.gameObjectsLocator = gameObjectsLocator;
        }

        private void Start()
        {
            posX = transform.position.x;
            startPosY = transform.position.y;
        }

        private void Update()
        {
            Movement();
            SpeedUp();
            SpeedDown();
            TurnLeft();
            TurnRight();

            DistanceCalc();
            SpeedChangeTimer();
            PoliceSpawner();

            ScoreByTime();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Detect(collision);
        }

        private void ScoreByTime()
        {
            time += Time.deltaTime;

            if (score == (int)time)
                return;

            score = (int)time;
            OnScoreAdd?.Invoke(score);
            print(score);
        }

        private void Detect(Collider2D collision)
        {
            StatsPanel statsPanel = gameObjectsLocator.GetGameObjectByName(Constants.StatsPanelName).GetComponent<StatsPanel>();

            if (collision.CompareTag(Constants.DetectorTag))
            {
                gameObjectsLocator.GetGameObjectByName(Constants.BackgroundSpawnerName).GetComponent<BackgroundSpawner>().SpawnRoad(false);

                if (!GetComponent<CameraMove>().enabled)
                    GetComponent<CameraMove>().enabled = true;
            }
            else if (collision.CompareTag(Constants.CoinTag))
            {
                score += 1;
                time += 1;
                OnCollectCoin?.Invoke(score);

                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag(Constants.BlockTag))
            {
                gameStateMachine.Enter<LoseModeState>();
            }
            else if (collision.CompareTag(Constants.CrackTag))
            {
                speedChangeTime = 5f;
                speedCor = 0.5f;
                TakeDamage(35);
                statsPanel.GetHealthPresenter.FillAmount((float)hp / 100);

                Destroy(collision.GetComponent<BoxCollider2D>());
            }
            else if (collision.CompareTag(Constants.OilTag))
            {
                speedChangeTime = 10f;
                speedCor = 0.5f;

                Destroy(collision.GetComponent<BoxCollider2D>());
            }
            else if (collision.CompareTag(Constants.HeartTag))
            {
                Heal(25);
                statsPanel.GetHealthPresenter.FillAmount((float)hp / 100);

                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag(Constants.ShieldTag))
            {
                TakeShield();
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag(Constants.MagnetTag))
            {
                TakeMagnet();
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag(Constants.NitroTag))
            {
                speedChangeTime = 15f;
                speedCor = 2f;
                TakeNitro();
                Destroy(collision.gameObject);
            }
        }

        private void SpeedChangeTimer()
        {
            speedChangeTime -= Time.deltaTime;

            if (speedChangeTime < 0)
                speedCor = 1;
        }

        private void Movement()
        {
            transform.position += transform.up * Time.deltaTime * speed * speedCor;
        }

        private void PoliceSpawner()
        {
            policeSpawnTime -= Time.deltaTime;

            if(policeSpawnTime < 0)
            {
                GameObject pol = gameFactory.Create(police, new Vector2(transform.position.x, transform.position.y - 8));
                pol.transform.localScale = transform.localScale;
                pol.GetComponent<Police>().Construct(gameStateMachine);
                pol.GetComponent<Police>().speed = speed * speedCor + 1;

                policeSpawnTime = 60f;
            }
        }

        private void SpeedUp()
        {
            if (!isSpeedUp)
                return;

            speed = Mathf.Clamp(speed + Time.deltaTime * 2, 1, 4);

            ControlPanel controlPanel = gameObjectsLocator.GetGameObjectByName(Constants.ControlPanelName).GetComponent<ControlPanel>();

            if (speed == 4)
                controlPanel.GetGas.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            else
            {
                controlPanel.GetBrake.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                controlPanel.GetGas.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }

        private void SpeedDown()
        {
            if (!isSpeedDown)
                return;

            speed = Mathf.Clamp(speed - Time.deltaTime * 2, 1, 4);

            ControlPanel controlPanel = gameObjectsLocator.GetGameObjectByName(Constants.ControlPanelName).GetComponent<ControlPanel>();

            if (speed == 1)
                controlPanel.GetBrake.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            else
            {
                controlPanel.GetBrake.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                controlPanel.GetGas.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }

        private void TurnLeft()
        {
            if (!isTurnLeft)
                return;

            posX = Mathf.Clamp(posX - Time.deltaTime * 2f, -1.45f, 1.45f);

            transform.position = new Vector2(posX, transform.position.y);
        }

        private void TurnRight()
        {
            if (!isTurnnRight)
                return;

            posX = Mathf.Clamp(posX + Time.deltaTime * 2f, -1.45f, 1.45f);

            transform.position = new Vector2(posX, transform.position.y);
        }

        private void DistanceCalc()
        {
            int distance = 0;

            currentPosY = transform.position.y;
            distance = (int)(currentPosY - startPosY);

            if (distance % 5 == 0 && distance != previousSpawnDistance)
            {
                gameObjectsLocator.GetGameObjectByName(Constants.EffectsSpawnerName).GetComponent<EffectsSpawner>().CreateEffect();
                previousSpawnDistance = distance;
            }
        }

        private void TakeDamage(int amount)
        {
            hp -= amount;
            if (hp < 0)
                gameStateMachine.Enter<LoseModeState>();
        }

        private void Heal(int amount)
        {
            hp += amount;
            if (hp > 100)
                hp = 100;
        }

        private void TakeShield()
        {
            if(currentEffect != null)
                StopCoroutine(currentEffect);

            currentEffect = StartCoroutine(EffectLifeCycle(EffectType.Shield));
        }

        private void TakeMagnet()
        {
            if (currentEffect != null)
                StopCoroutine(currentEffect);

            currentEffect = StartCoroutine(EffectLifeCycle(EffectType.Magnet));
        }

        private void TakeNitro()
        {
            if (currentEffect != null)
                StopCoroutine(currentEffect);

            currentEffect = StartCoroutine(EffectLifeCycle(EffectType.Nitro));
        }

        private IEnumerator EffectLifeCycle(EffectType effectType)
        {
            StatsPanel statsPanel = gameObjectsLocator.GetGameObjectByName(Constants.StatsPanelName).GetComponent<StatsPanel>();
            ImageBarPresenter imageBar = null;
            float time = 15f;

            if (effectType == EffectType.Shield)
            {
                shield.SetActive(true);
                magnet.SetActive(false);
                nitro.SetActive(false);
                imageBar = statsPanel.GetShieldPresenter;
                statsPanel.GetShieldPresenter.gameObject.SetActive(true);
                statsPanel.GetMagnetPresenter.gameObject.SetActive(false);
                statsPanel.GetNitroPresenter.gameObject.SetActive(false);
                speedCor = 1;
            }
            else if (effectType == EffectType.Magnet)
            {
                shield.SetActive(false);
                magnet.SetActive(true);
                nitro.SetActive(false);
                imageBar = statsPanel.GetMagnetPresenter;
                statsPanel.GetShieldPresenter.gameObject.SetActive(false);
                statsPanel.GetMagnetPresenter.gameObject.SetActive(true);
                statsPanel.GetNitroPresenter.gameObject.SetActive(false);
                speedCor = 1;
            }
            else if (effectType == EffectType.Nitro)
            {
                shield.SetActive(false);
                magnet.SetActive(false);
                nitro.SetActive(true);
                imageBar = statsPanel.GetNitroPresenter;
                statsPanel.GetShieldPresenter.gameObject.SetActive(false);
                statsPanel.GetMagnetPresenter.gameObject.SetActive(false);
                statsPanel.GetNitroPresenter.gameObject.SetActive(true);
            }

            while (time > 0)
            {
                yield return new WaitForSeconds(0.01f);
                time -= 0.01f;
                float amount = time / 10 * 2 / 3;

                imageBar.FillAmount(amount);
            }

            shield.SetActive(false);
            magnet.SetActive(false);
            nitro.SetActive(false);
            statsPanel.GetShieldPresenter.gameObject.SetActive(false);
            statsPanel.GetMagnetPresenter.gameObject.SetActive(false);
            statsPanel.GetNitroPresenter.gameObject.SetActive(false);
            speedCor = 1;
        }

        public void SpeedUpOn() => isSpeedUp = true;
        public void SpeedUpOff() => isSpeedUp = false;
        public void SpeedDownOn() => isSpeedDown = true;
        public void SpeedDownOff() => isSpeedDown = false;
        public void LeftOn() => isTurnLeft = true;
        public void LeftOff() => isTurnLeft = false;
        public void RightOn() => isTurnnRight = true;
        public void RightOff() => isTurnnRight = false;
    }
}