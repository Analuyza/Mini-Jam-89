using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Background : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 1f;
    public float clamppos;
    Vector3 startPos;


    void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Level1") {
            if (!GmLevel1.inst.finished) {
                float newPos = Mathf.Repeat(Time.time * speed, clamppos);
                transform.position = startPos - Vector3.up * newPos;
            }
        }
        if (SceneManager.GetActiveScene().name == "Level2") {
            if (!GmLevel2.inst.finished) {
                float newPos = Mathf.Repeat(Time.time * speed, clamppos);
                transform.position = startPos - Vector3.up * newPos;
            }
        }
        if (SceneManager.GetActiveScene().name == "Level3") {
            if (!GmLevel3.inst.finished) {
                float newPos = Mathf.Repeat(Time.time * speed, clamppos);
                transform.position = startPos - Vector3.up * newPos;
            }
        }
    }
}
