using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Game_D : MonoBehaviour
{
    public float speed = 5f; // 속도가 점점 빨라지게 
    public float speedStep = 0.5f;         // 속도 증가량
    public int scoreStepForSpeed = 100;    // 몇 점마다 속도 증가할지
    private Text timeScoreText;

    public Text hightext;
    public Text gameover;

    public Button re;

    public float scoreSpeed = 60f; // 초당 몇 점씩 오를지 설정

    public GameObject[] targets;

    private GameObject currentTile; //타일 생성
    public Transform spawnPoint; //위치값.
    private float timer = 0f;
    private int score = 0;
    private int highscore = 0;


    public bool endgame = false;
    void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0); // 저장된 값이 없으면 0
        if (timeScoreText == null)
            timeScoreText = GameObject.Find("Score")?.GetComponent<Text>();

        hightext.text = "HI " + highscore.ToString("D5");
        
        re.onClick.AddListener(Restart);
        SpawnTile(); // 첫 타일 생성
    }

    void FixedUpdate() // 프레임 속도 영향 없이 일정한 주기로 실행됨
    {
        if(!endgame) UpdateScore();
            
        if (currentTile == null)
        {
            SpawnTile(); // 기존 타일이 사라지면 새 타일 생성
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

            // 텍스트 업데이트 (00001 형식)
            if (timeScoreText != null)
                timeScoreText.text = score.ToString("D5");
            // 100점마다 속도 증가
            if (score % scoreStepForSpeed == 0)
            {
                speed += speedStep;
            }
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("HighScore", highscore); // 저장
                PlayerPrefs.Save();                         // 즉시 디스크에 기록
                hightext.text = "HI " + highscore.ToString("D5");
            }
        }
    }

    void SpawnTile()
    {
        if (targets == null || targets.Length == 0 || spawnPoint == null)
            return;

        // 프리팹 중 하나 랜덤 선택
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
        // 같은 씬을 다시 로드
        SceneManager.LoadScene(currentSceneIndex);
    }
}