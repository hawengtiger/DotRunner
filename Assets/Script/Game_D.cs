using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Game_D : MonoBehaviour
{
    public float speed = 5f; // �ӵ��� ���� �������� 
    public float speedStep = 0.5f;         // �ӵ� ������
    public int scoreStepForSpeed = 100;    // �� ������ �ӵ� ��������
    private Text timeScoreText;

    public Text hightext;
    public Text gameover;

    public Button re;

    public float scoreSpeed = 60f; // �ʴ� �� ���� ������ ����

    public GameObject[] targets;

    private GameObject currentTile; //Ÿ�� ����
    public Transform spawnPoint; //��ġ��.
    private float timer = 0f;
    private int score = 0;
    private int highscore = 0;


    public bool endgame = false;
    void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0); // ����� ���� ������ 0
        if (timeScoreText == null)
            timeScoreText = GameObject.Find("Score")?.GetComponent<Text>();

        hightext.text = "HI " + highscore.ToString("D5");
        
        re.onClick.AddListener(Restart);
        SpawnTile(); // ù Ÿ�� ����
    }

    void FixedUpdate() // ������ �ӵ� ���� ���� ������ �ֱ�� �����
    {
        if(!endgame) UpdateScore();
            
        if (currentTile == null)
        {
            SpawnTile(); // ���� Ÿ���� ������� �� Ÿ�� ����
            return;
        }


        float moveStep = speed * Time.fixedDeltaTime * Time.timeScale;
        currentTile.transform.Translate(Vector2.left * moveStep);
    }

    void UpdateScore()
    {
        timer += Time.fixedDeltaTime * 60;
        if (timer >= 1f)
        {
            timer -= 1f;
            score++;

            // �ؽ�Ʈ ������Ʈ (00001 ����)
            if (timeScoreText != null)
                timeScoreText.text = score.ToString("D5");
            // 100������ �ӵ� ����
            if (score % scoreStepForSpeed == 0)
            {
                speed += speedStep;
            }
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("HighScore", highscore); // ����
                PlayerPrefs.Save();                         // ��� ��ũ�� ���
                hightext.text = "HI " + highscore.ToString("D5");
            }
        }
    }

    void SpawnTile()
    {
        if (targets == null || targets.Length == 0 || spawnPoint == null)
            return;

        // ������ �� �ϳ� ���� ����
        int index = Random.Range(0, targets.Length);

        Vector3 prefabPos = targets[index].transform.position;
        Vector3 spawnPos = new Vector3(spawnPoint.position.x, prefabPos.y, spawnPoint.position.z);

        currentTile = Instantiate(targets[index], spawnPos, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (currentTile == other.gameObject)
            {
                Destroy(currentTile);
                currentTile = null;
            }
        }
    }
    
    void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // ���� ���� �ٽ� �ε�
        SceneManager.LoadScene(currentSceneIndex);
    }
}