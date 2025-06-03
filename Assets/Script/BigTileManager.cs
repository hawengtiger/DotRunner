using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform target = transform.Find("Target");
        if (target != null)
        {
            float randomY = Random.Range(-0.8f, 0.4f);
            Vector3 newPos = target.position; // 월드 좌표 기준
            newPos.y = randomY;
            target.position = newPos;
        }
        else
        {
            Debug.LogWarning("Target not found under BigTile.");
        }
    }
}
