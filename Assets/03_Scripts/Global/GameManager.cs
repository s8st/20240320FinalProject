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
    public static GameManager instance; //�̱���ȯ

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
        // FindGameObjectWithTag : �±׷� �˻�, ���̾��Ű���� ��� �˻��ϱ� ������ ��������.
        // �� ������ �����ϴ� Update������ ������� �ʴ´�, �ѹ��� ����Ҷ�
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;

        playerHealthSystem = Player.GetComponent<HealthSystem>();
        playerHealthSystem.OnDamage += UpdateHealthUI;
        playerHealthSystem.OnHeal += UpdateHealthUI;
        playerHealthSystem.OnDeath += GameOver;

        gameOverUI.SetActive(false);

        for (int i = 0; i < spawnPositionsRoot.childCount; i++)
        {
            //spawnPostions�� �� �����ͼ� ����
            spawnPostions.Add(spawnPositionsRoot.GetChild(i)); // GetChild : transform��ȯ
        }

    }





    private void Start()
    {
        UpgradeStatInit();
        StartCoroutine("StartNextWave"); //���� �����ϰ� gameOver()���� StopAllCoroutines ���߰�
        //1. ��ƾ�� �����ؼ� �ڷ�ƾ�� ��ȯ : ��Ʈ�������δ� �� �ȸ����??
        //2. �޼��� ������ �����ϰ� �ڷ�ƾ ��ȯ : �޼�������̳� �ڷ�ƾ���� ����
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
    //        if (currentSpawnCount == 0) // ��ȯ�Ǿ� �ִ� ��ü���� ����: 0�̶�� ó���̰ų� �� ��Ұų�
    //        {




    //            GameObject enemy = Instantiate(enemyPrefebs[6], spawnPositionsRoot.position, Quaternion.identity);
    //            enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
    //            enemy.GetComponent<HealthSystem>().OnDeath += CreateReward;

    //            //���͸� ������ �� �������
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
        while (true) // ���ѷ������� yield return null ---> Uodate() ���� �Ͱ� ����. deltaTime��밡��
        {
            if (currentSpawnCount == 0) // ��ȯ�Ǿ� �ִ� ��ü���� ����: 0�̶�� ó���̰ų� �� ��Ұų�
            {
                //   UpdateWaveUI(); // ���̺� �ֽ�ȭ�ϱ�
                yield return new WaitForSeconds(2f); // 2���� ���� �ڵ��

                //if (currentWaveIndex % 1 == 0) // 20 --> 1 ���̵� ����
                //{
                //    RandomUpgrade(); //���鿡�� �������׷��̵� �߰�����
                //}

                //if (currentWaveIndex % 10 == 0) //10�� ������
                //{
                //    //waveSpawnPosCount : ���� �����Ǵ� ���� ���� ����
                //    // waveSpawnPosCount + 1�� spawnPostions.Count���� �� ũ�ٸ� ���� waveSpawnPosCount�� ��� �ƴ϶��  + 1
                //    waveSpawnPosCount = waveSpawnPosCount + 1 > spawnPostions.Count ? waveSpawnPosCount : waveSpawnPosCount + 1;
                //    waveSpawnCount = 0;
                //}

                //if (currentWaveIndex % 5 == 0)
                //{
                //    // 5��° ���� ���� ����
                //    //   CreateReward();
                //}

                //if (currentWaveIndex % 3 == 0)
                //{
                //    waveSpawnCount += 1;
                //}

                // 10������ �����ϴ� �������� �ø��� 3������ �ѹ��� ��������� ���͸� �ø���

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
                //���͸� ������ �� �������
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
    //    while (true) // ���ѷ������� yield return null ---> Uodate() ���� �Ͱ� ����. deltaTime��밡��
    //    {
    //        if (currentSpawnCount == 0) // ��ȯ�Ǿ� �ִ� ��ü���� ����: 0�̶�� ó���̰ų� �� ��Ұų�
    //        {
    //          UpdateWaveUI(); // ���̺� �ֽ�ȭ�ϱ�
    //            yield return new WaitForSeconds(2f); // 2���� ���� �ڵ��

    //            if (currentWaveIndex % 1 == 0) // 20 --> 1 ���̵� ����
    //            {
    //                RandomUpgrade(); //���鿡�� �������׷��̵� �߰�����
    //            }

    //            if (currentWaveIndex % 10 == 0) //10�� ������
    //            {
    //                //waveSpawnPosCount : ���� �����Ǵ� ���� ���� ����
    //                // waveSpawnPosCount + 1�� spawnPostions.Count���� �� ũ�ٸ� ���� waveSpawnPosCount�� ��� �ƴ϶��  + 1
    //                waveSpawnPosCount = waveSpawnPosCount + 1 > spawnPostions.Count ? waveSpawnPosCount : waveSpawnPosCount + 1;
    //                waveSpawnCount = 0;
    //            }

    //            if (currentWaveIndex % 5 == 0)
    //            {
    //                // 5��° ���� ���� ����
    //                CreateReward();
    //            }

    //            if (currentWaveIndex % 3 == 0)
    //            {
    //                waveSpawnCount += 1;
    //            }
    //            // 10������ �����ϴ� �������� �ø��� 3������ �ѹ��� ��������� ���͸� �ø���

    //            for (int i = 0; i < waveSpawnPosCount; i++)
    //            {
    //                int posIdx = Random.Range(0, spawnPostions.Count);
    //                for (int j = 0; j < waveSpawnCount; j++)
    //                {
    //                    int prefabIdx = Random.Range(0, enemyPrefebs.Count);
    //                    GameObject enemy = Instantiate(enemyPrefebs[prefabIdx], spawnPostions[posIdx].position, Quaternion.identity);
    //                    enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;

    //                    //���͸� ������ �� �������
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
        currentSpawnCount--; // 10���� ���Ͱ� �������� ���������� ���ֱ�
    }



    private void UpdateHealthUI()
    {
        // �ۼ�Ʈ�� ����� ���� --> 0~1 value
        hpGaugeSlider.value = playerHealthSystem.CurrentHealth / playerHealthSystem.MaxHealth;
    }



    private void GameOver()
    {
        gameOverUI.SetActive(true);
        StopAllCoroutines(); // �����ϴ� ��� �ڷ�ƾ�� �����
    }

    //private void UpdateWaveUI()
    //{
    //    // waveText.text = 
    //    waveText.text = (currentWaveIndex + 1).ToString();
    //}

    public void RestartGame()
    {
        // Application.LoadScene  ---> ���̻� ������� �ʴ´�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex/*���弼���� ��ȣ�� �����Ѵ�*/);
    }

    public void ExitGame()
    {
        // ����� ���� �˻糪 ���� ������ Application���� �Ѵ�
        Application.Quit();
    }


    void CreateReward()
    {
        int idx = Random.Range(0, rewards.Count);
        int posIdx = Random.Range(0, spawnPostions.Count);

        GameObject obj = rewards[idx];
        Instantiate(obj, spawnPostions[posIdx].position, Quaternion.identity);
    }


    //���� �ʱ�ȭ
    void UpgradeStatInit()
    {
        defaultStats.statsChangeType = StatsChangeType.Add;
        defaultStats.attackSO = Instantiate(defaultStats.attackSO); //�̸� �����ؼ� ������ �ְ�
        // �ɸ��Ϳ�����Ʈ�� �̸� �����ϸ� ���ƹ����� ������ �̸� �����ؼ� ������ �ְ�

        rangedStats.statsChangeType = StatsChangeType.Add;
        rangedStats.attackSO = Instantiate(rangedStats.attackSO);
    }

    void RandomUpgrade()
    {
        switch (Random.Range(0, 6))
        {
            case 0:
                defaultStats.maxHealth += 2; // ������ �ֵ��� 2�� �����ؼ� ���´�
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