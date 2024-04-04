using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum CharacterType
{
    Bubble,Bomber,Snowbros,Tumblepop
}

public enum PlayerType
{
    Player0,Player1, Player2, Player3
}


[System.Serializable]
public class Character
{
    public CharacterType characterType;
   // public PlayerType playerType;
    public Sprite CharacterSprite;
    public RuntimeAnimatorController AnimatorController;

}


public class GameManager : MonoBehaviour
{
    [Header("CharacterSelect")]
    public List<Character> CharacterList = new List<Character>();

    public Animator playerAnimator;
    public  Text playerName;


    [Header("GManager=======================")]
    //==============================================================
    public static GameManager instance; //싱글톤환

    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";

    private HealthSystem playerHealthSystem;

    [SerializeField] private TextMeshProUGUI waveText; //using TMPro;
    [SerializeField] private Slider hpGaugeSlider;  //using UnityEngine.UI;
    [SerializeField] private GameObject gameOverUI; //using UnityEngine.UI;


    [SerializeField] private int currentWaveIndex = 0;
    private int currentSpawnCount = 0;
    private int waveSpawnCount = 6;
    private int waveSpawnPosCount = 6;

    public float spawnInterval = .5f;
    public List<GameObject> enemyPrefebs = new List<GameObject>();

    [SerializeField] private Transform spawnPositionsRoot;
    private List<Transform> spawnPostions = new List<Transform>();

    public List<GameObject> rewards = new List<GameObject>();

    [SerializeField] private CharacterStats defaultStats;
    [SerializeField] private CharacterStats rangedStats;



    public void SetCharacter(CharacterType characterType, string name)
    {
        var character = GameManager.instance.CharacterList.Find(item => item.characterType == characterType);

        playerAnimator.runtimeAnimatorController = character.AnimatorController;
        playerName.text = name;


    }

    //public void SetCharacter(PlayerType playerType, string name)
    //{
    //    var player = GameManager.instance.CharacterList.Find(item => item.playerType == playerType);

    //    playerAnimator.runtimeAnimatorController = player.AnimatorController;
    //    playerName.text = name;

    //    // player.SetActive(true);

    //}







    private void Awake()
    {
        instance = this;
        // FindGameObjectWithTag : 태그로 검색, 하이어라키에서 모두 검색하기 때문에 느려진다.
        // 매 프레임 실행하는 Update에서는 사용하지 않는다, 한번만 사용할때
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;

        playerHealthSystem = Player.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += UpdateHealthUI;
        playerHealthSystem.OnHeal += UpdateHealthUI;
        playerHealthSystem.OnDeath += GameOver;

        gameOverUI.SetActive(false);

        for (int i = 0; i < spawnPositionsRoot.childCount; i++)
        {
            //spawnPostions을 다 가져와서 저장
            spawnPostions.Add(spawnPositionsRoot.GetChild(i)); // GetChild : transform반환
        }

    }





    private void Start()
    {
        UpgradeStatInit();
        StartCoroutine("StartNextWave"); //지금 동작하고 gameOver()에서 StopAllCoroutines 멈추게
        //1. 루틴을 제공해서 코루틴을 반환 : 스트링값으로는 잘 안멈춘다??
        //2. 메서드 네임을 제공하고 코루틴 반환 : 메서드네임이나 코루틴으로 정지
       // SpawnMonster();

    }

    //private void Update()
    //{
    //    SpawnMonster();
    //}

    //private  void SpawnMonster()
    //{
    //    //while (true)

    //    while (true)
    //    {
    //        if (currentSpawnCount == 0) // 소환되어 있는 객체들의 갯수: 0이라면 처음이거나 다 잡았거나
    //        {




    //            GameObject enemy = Instantiate(enemyPrefebs[6], spawnPositionsRoot.position, Quaternion.identity);
    //            enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
    //            enemy.GetComponent<HealthSystem>().OnDeath += CreateReward;

    //            //몬스터를 생성할 때 지워줘야
    //            enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(defaultStats);
    //            enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(rangedStats);

    //            currentSpawnCount++;
    //            //yield return new WaitForSeconds(spawnInterval);
    //            return;

    //        }
    //    }
    //}

    IEnumerator StartNextWave()
    {
        while (true) // 무한루프에서 yield return null ---> Uodate() 쓰는 것과 같다. deltaTime사용가능
        {
            if (currentSpawnCount == 0) // 소환되어 있는 객체들의 갯수: 0이라면 처음이거나 다 잡았거나
            {
                //   UpdateWaveUI(); // 웨이브 최신화하기
                yield return new WaitForSeconds(2f); // 2초후 다음 코드로

                //if (currentWaveIndex % 1 == 0) // 20 --> 1 난이도 조절
                //{
                //    RandomUpgrade(); //적들에게 랜덤업그레이드 추가해줌
                //}

                //if (currentWaveIndex % 10 == 0) //10의 배수라면
                //{
                //    //waveSpawnPosCount : 몬스터 생성되는 곳의 갯수 증가
                //    // waveSpawnPosCount + 1이 spawnPostions.Count보다 더 크다면 원래 waveSpawnPosCount를 사용 아니라면  + 1
                //    waveSpawnPosCount = waveSpawnPosCount + 1 > spawnPostions.Count ? waveSpawnPosCount : waveSpawnPosCount + 1;
                //    waveSpawnCount = 0;
                //}

                //if (currentWaveIndex % 5 == 0)
                //{
                //    // 5번째 마다 보상 생성
                //    //   CreateReward();
                //}

                //if (currentWaveIndex % 3 == 0)
                //{
                //    waveSpawnCount += 1;
                //}

                // 10개마다 생성하는 포지션을 늘리고 3개마다 한번에 만들어지는 몬스터를 늘린다

                //   for (int i = 0; i < waveSpawnPosCount; i++)
                //  {
                // int posIdx = Random.Range(0, spawnPostions.Count);
                //for (int j = 0; j < waveSpawnCount; j++)
                //{
                //int prefabIdx = Random.Range(0, enemyPrefebs.Count);
                //  int prefabIdx = enemyPrefebs.Count;
                //GameObject enemy = Instantiate(enemyPrefebs[prefabIdx], spawnPostions[posIdx].position, Quaternion.identity);
                GameObject enemy = Instantiate(enemyPrefebs[6], spawnPositionsRoot.position, Quaternion.identity);
                enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
                enemy.GetComponent<HealthSystem>().OnDeath += CreateReward;
                //몬스터를 생성할 때 지워줘야
                enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(defaultStats);
                enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(rangedStats);

                currentSpawnCount++;
                yield return new WaitForSeconds(spawnInterval);
                //    }
                //}

                //  currentWaveIndex++;
            }

            yield return null;
        }
    }








    //IEnumerator StartNextWave()
    //{
    //    while (true) // 무한루프에서 yield return null ---> Uodate() 쓰는 것과 같다. deltaTime사용가능
    //    {
    //        if (currentSpawnCount == 0) // 소환되어 있는 객체들의 갯수: 0이라면 처음이거나 다 잡았거나
    //        {
    //          UpdateWaveUI(); // 웨이브 최신화하기
    //            yield return new WaitForSeconds(2f); // 2초후 다음 코드로

    //            if (currentWaveIndex % 1 == 0) // 20 --> 1 난이도 조절
    //            {
    //                RandomUpgrade(); //적들에게 랜덤업그레이드 추가해줌
    //            }

    //            if (currentWaveIndex % 10 == 0) //10의 배수라면
    //            {
    //                //waveSpawnPosCount : 몬스터 생성되는 곳의 갯수 증가
    //                // waveSpawnPosCount + 1이 spawnPostions.Count보다 더 크다면 원래 waveSpawnPosCount를 사용 아니라면  + 1
    //                waveSpawnPosCount = waveSpawnPosCount + 1 > spawnPostions.Count ? waveSpawnPosCount : waveSpawnPosCount + 1;
    //                waveSpawnCount = 0;
    //            }

    //            if (currentWaveIndex % 5 == 0)
    //            {
    //                // 5번째 마다 보상 생성
    //                CreateReward();
    //            }

    //            if (currentWaveIndex % 3 == 0)
    //            {
    //                waveSpawnCount += 1;
    //            }
    //            // 10개마다 생성하는 포지션을 늘리고 3개마다 한번에 만들어지는 몬스터를 늘린다

    //            for (int i = 0; i < waveSpawnPosCount; i++)
    //            {
    //                int posIdx = Random.Range(0, spawnPostions.Count);
    //                for (int j = 0; j < waveSpawnCount; j++)
    //                {
    //                    int prefabIdx = Random.Range(0, enemyPrefebs.Count);
    //                    GameObject enemy = Instantiate(enemyPrefebs[prefabIdx], spawnPostions[posIdx].position, Quaternion.identity);
    //                    enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;

    //                    //몬스터를 생성할 때 지워줘야
    //                    enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(defaultStats);
    //                    enemy.GetComponent<CharacterStatsHandler>().AddStatModifier(rangedStats);

    //                    currentSpawnCount++;
    //                    yield return new WaitForSeconds(spawnInterval);
    //                }
    //            }

    //            currentWaveIndex++;
    //        }

    //        yield return null;
    //    }
    //}





    private void OnEnemyDeath()
    {
        currentSpawnCount--; // 10개의 몬스터가 생겼을때 죽을때마다 빼주기
    }



    private void UpdateHealthUI()
    {
        // 퍼센트로 만들기 위해 --> 0~1 value
        hpGaugeSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
    }



    private void GameOver()
    {
        gameOverUI.SetActive(true);
        StopAllCoroutines(); // 동작하는 모든 코루틴을 멈춰라
    }

    //private void UpdateWaveUI()
    //{
    //    // waveText.text = 
    //    waveText.text = (currentWaveIndex + 1).ToString();
    //}

    public void RestartGame()
    {
        // Application.LoadScene  ---> 더이상 사용하지 않는다
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex/*빌드세팅의 번호로 실행한다*/);
    }

    public void ExitGame()
    {
        // 기기의 상태 검사나 끄는 행위는 Application에서 한다
        Application.Quit();
    }


    void CreateReward()
    {
        int idx = Random.Range(0, rewards.Count);
        int posIdx = Random.Range(0, spawnPostions.Count);

        GameObject obj = rewards[idx];
        Instantiate(obj, spawnPostions[posIdx].position, Quaternion.identity);
    }


    //스탯 초기화
    void UpgradeStatInit()
    {
        defaultStats.statsChangeType = StatsChangeType.Add;
        defaultStats.attackSO = Instantiate(defaultStats.attackSO); //미리 복사해서 가지고 있게
        // 케릭터오브젝트를 미리 수정하면 남아버리기 때문에 미리 복사해서 가지고 있게

        rangedStats.statsChangeType = StatsChangeType.Add;
        rangedStats.attackSO = Instantiate(rangedStats.attackSO);
    }

    void RandomUpgrade()
    {
        switch (Random.Range(0, 6))
        {
            case 0:
                defaultStats.maxHealth += 2; // 나오는 애들이 2씩 증가해서 나온다
                break;

            case 1:
                defaultStats.attackSO.power += 1;
                break;

            case 2:
                defaultStats.speed += 0.1f;
                break;

            case 3:
                defaultStats.attackSO.isOnKnockback = true;
                defaultStats.attackSO.knockbackPower += 1;
                defaultStats.attackSO.knockbackTime = 0.1f;
                break;

            case 4:
                defaultStats.attackSO.delay -= 0.05f;
                break;

            case 5:
                RangedAttackData rangedAttackData = rangedStats.attackSO as RangedAttackData; 
                rangedAttackData.numberofProjectilesPerShot += 1;
                break;

            default:
                break;
        }
    }




}