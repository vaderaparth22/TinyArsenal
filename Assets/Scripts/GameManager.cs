using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region CLASS INSTANCE
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public GameObject MainPlayer;
    public GameObject Torch;
    public GameObject BallBombs;
    public Transform dropPosition;

    public CharacterController PlayerController;

    public PlayerHealth PlayerHealth;
    public PlayerMove PlayerMove;
    public FlameHit FlameHit;
    public LaserBeamPlayer LaserBeamPlayer;
    public CameraShaker CameraShaker;
    public Weapon PlayerWeapon;
    public UIManager UIManager;

    [Header("ANIMATIONS")]
    public Animation LightFade;
    public Animation GroundBlack;

    public bool allowSlowTime;
    public bool isImmortal;

    public int maxHealthPlayer;
    public int maxHealthAI;
    public int maxHealthBigAI;
    public float maxBoxDestroyTime;
    public float timeBetweenAttackAI;

    public float delayToOver;

    public int killCounter;
    public int minUpgradeKills;
    public int minHealthKills;
    public int minBlackoutkills;

    [Header("WAVE SETTINGS")]
    public List<Transform> WavePosList;
    public int[] WaveKills;
    public int currentWave;
    public int minWaveKills;
    public float waveTime;
    public bool isDelayWave;

    private float storeTimeScale;
    private bool doSlow;

    [Header("PLAYER SETTINGS")]
    #region
    public ParticleSystem ChargeParticles;
    public ParticleSystem AftershockParticles;
    #endregion

    [Header("AI SETTINGS")]
    #region AI

    public float NormalAISpeed;
    public float RedAISpeed;
    public float BigAISpeed;

    public List<GameObject> AiPlayers;
    public List<GameObject> AiPlayersBig;
    public List<Transform> AiSpawnPositions;
    public float NextAiSpawnAfter;
    public int maxAIcount;
    public int AIWithBulletNum;
    public int AIBigNum;

    [HideInInspector]
    public int AiCount;

    private float AiTimer;

    #endregion

    #region
    [Header("DAMAGE LISTS")]
    public List<int> DamageList;
    public List<int> DamageListAI;
    #endregion

    void Start()
    {
        //SpawnAIPlayer(1);
        currentWave = 0;
        NextWave(currentWave);
        doSlow = false;

        Invoke("testing", 5f);
    }

    void testing()
    {
        Debug.Log("invoking...");
    }

    private void Update()
    {
        if (AiTimer > NextAiSpawnAfter && AiCount < WaveKills[currentWave] && isDelayWave == false)
        {
            SpawnAIPlayer(1);

            AiTimer = 0f;
        }

        AiTimer += Time.deltaTime;

        if(allowSlowTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SlowMotion(0.1f);

                PlayerMove.isHoldingShift = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                SlowMotion(1f);

                PlayerMove.isHoldingShift = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.K))
        {
            Instantiate(BallBombs, dropPosition.position, Quaternion.identity);
        }
    }

    private void SlowMotion(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void SpawnAIPlayer(int count)
    {
        if ((killCounter > 0 && AIWithBulletNum > 0) && (killCounter % AIWithBulletNum == 0))     //Spawn AI With Normal Bullet
        {
            for (int i = 0; i < count; i++)
            {
                int r_pos = Random.Range(0, AiSpawnPositions.Count);

                Instantiate(AiPlayers[0], AiSpawnPositions[r_pos].position, AiSpawnPositions[r_pos].rotation);
            }
        }
        else if ((killCounter > 0 && AIBigNum > 0) && (killCounter % AIBigNum == 0))     //Spawn AI With Normal Bullet
        {
            int r_pos = Random.Range(0, AiSpawnPositions.Count);

            Instantiate(AiPlayersBig[0], AiSpawnPositions[r_pos].position, AiSpawnPositions[r_pos].rotation);
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                int r_pl = Random.Range(1, AiPlayers.Count);
                int r_pos = Random.Range(0, AiSpawnPositions.Count);

                Instantiate(AiPlayers[r_pl], AiSpawnPositions[r_pos].position, AiSpawnPositions[r_pos].rotation);
            }
        }

        AiCount += count;
    }

    public void InitiateUpgrade()
    {
        maxAIcount += 2;

        //StartCoroutine(SlowMotion(1, 2f, 0.5f));
    }

    public void PlayerDeath()
    {
        StartCoroutine(SlowMotion(0, 5f, 0.2f));
    }

    public IEnumerator SlowMotion(int task, float delay, float slowness)
    {
        Time.timeScale = slowness;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        float slowEndTime = Time.realtimeSinceStartup + delay;

        while (Time.realtimeSinceStartup < slowEndTime)
        {
            yield return 0;
        }

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        if (task == 0)
        {
            SceneManager.LoadScene(0);
        }
        else if (task == 1)
        {
            UpgradeWeapon();
        }
    }

    public void UpgradeWeapon()
    {
        int w = PlayerWeapon.currentWeaponId;
        PlayerWeapon.WeaponCollection[w].SetActive(false);

        w++;

        if (w >= PlayerWeapon.WeaponCollection.Length)
        {
            w = 0;
        }

        PlayerWeapon.WeaponCollection[w].SetActive(true);
        PlayerWeapon.currentWeaponId = w;
    }

    #region LIGHT ANIMATION
    public void TurnLight(bool isActive)
    {
        if (isActive == true)
        {
            StartCoroutine(FadeLightToDefault());
        }
        else
        {
            StartCoroutine(FadeLightAnimation());
        }
    }

    private IEnumerator FadeLightAnimation()
    {
        LightFade.Play("LightFade");
        GroundBlack.Play("GroundBlack");

        yield return new WaitUntil(() => LightFade.IsPlaying("LightFade") == false && GroundBlack.IsPlaying("GroundBlack") == false);

        Torch.SetActive(true);
    }

    private IEnumerator FadeLightToDefault()
    {
        LightFade.Play("LightDefault");

        yield return new WaitUntil(() => LightFade.IsPlaying("LightDefault") == false);

        Torch.SetActive(false);
    }
    #endregion

    #region AI WAVES
    public void NextWave(int id)
    {
        Transform[] w = WavePosList[id].GetComponentsInChildren<Transform>();

        AiSpawnPositions.Clear();

        foreach (Transform item in w)
        {
            if (item != WavePosList[id])
            {
                AiSpawnPositions.Add(item);
            }
        }
    }

    public void ChangeWave()
    {
        StartCoroutine(WaveDelayStart());
    }

    private IEnumerator WaveDelayStart()
    {
        isDelayWave = true;

        yield return new WaitForSecondsRealtime(waveTime);

        isDelayWave = false;
        currentWave++;
        NextWave(currentWave);
    }

    #endregion

    #region GAME OVER
    private IEnumerator DelayGameOver()
    {
        yield return new WaitForSecondsRealtime(delayToOver);

        UIManager.GameOverScreen.SetActive(true);
    }

    public void GameOver()
    {
        StartCoroutine(DelayGameOver());
    }
    #endregion
}
