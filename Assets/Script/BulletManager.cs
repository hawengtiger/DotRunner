using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float maxX = 10f; // 오른쪽 벽 위치

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DestroyBullet();
    }

    void DestroyBullet()
    {
        if (this.transform.position.x > maxX)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Big_Wall"))
        {
            Destroy(this.gameObject); // 벽에 닿아도 삭제
            GameObject bigTile = other.transform.parent.gameObject;
            Destroy(bigTile);     // BigTile 전체 삭제
            Destroy(gameObject);  // 총알 삭제
        }
    }
}
