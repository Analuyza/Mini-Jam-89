using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaclesMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float speed;
    bool toDodge;
    int direction;
    float time;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        if (SceneManager.GetActiveScene().name == "Level1") speed = GmLevel1.inst.speedObst;
        if (SceneManager.GetActiveScene().name == "Level2") speed = GmLevel2.inst.speedObst;
        if (SceneManager.GetActiveScene().name == "Level3") speed = GmLevel3.inst.speedObst;

        if (SceneManager.GetActiveScene().name != "Level1") {
            int random = Random.Range(1, 2);
            if (random == 2) {
                toDodge = true;
                direction = Random.Range(1, 2);
            }
            else toDodge = false;
        }

        Destroy(this.gameObject, 7);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level1") 
            if (GmLevel1.inst.finished || GmLevel1.inst.gameOver) Destroy(this.gameObject);

        if (SceneManager.GetActiveScene().name == "Level2") 
            if (GmLevel2.inst.finished || GmLevel2.inst.gameOver) Destroy(this.gameObject);
        
        if (SceneManager.GetActiveScene().name == "Level3") 
            if (GmLevel3.inst.finished || GmLevel3.inst.gameOver) Destroy(this.gameObject);
        
        if (toDodge)  {
            time = Time.deltaTime;
            if (time >= 2 && time <= 3) {
                if (direction == 2) {
                    Debug.Log("aaa");
                    rb.velocity = Vector2.left * speed;
                }
                else {
                    Debug.Log("bbbbbbbb");
                    rb.velocity = Vector2.right * speed;
                }
            }
        }

        rb.velocity += -Vector2.up * speed * Time.deltaTime;
    }

    IEnumerator StartDodge() {
        yield return new WaitForSeconds(2);
    }
}
