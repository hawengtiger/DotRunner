using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerJump : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePosition;
    public float bulletSpeed = 10f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private GameObject bullet;

    private Game_D game;
    void Start()
    {
        game = GameObject.FindObjectOfType<Game_D>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3.5f; // 빠르게 떨어지게 함
    }

    void Update()
    {
        if (!game.endgame)
        {
            PlayerMove();

            MakeBullet();
        }
    }

    void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // 순간 튕김
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce * 3);
        }
    }

    void MakeBullet()
    {
        if (bullet == null)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                bullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
                Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
                rbBullet.velocity = Vector2.right * bulletSpeed;
            }
        }
    }



    //  2D 충돌 처리로 수정
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall") || (other.gameObject.CompareTag("Big_Wall")))
        {
            Debug.Log("아잇!");
            game.endgame = true;
            game.speed = 0;
            game.hightext.gameObject.SetActive(true);
            game.re.gameObject.SetActive(true);
            game.gameover.gameObject.SetActive(true);
        }
    }

}
