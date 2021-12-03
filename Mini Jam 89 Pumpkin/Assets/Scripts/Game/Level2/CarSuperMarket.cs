using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSuperMarket : MonoBehaviour
{
    Rigidbody2D rb;
    public Material originalMat, flashMat;
    bool canMove = true;
    Vector3 destination;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        destination = new Vector3(-2.8f, .86f, 0);
    }

    void Update()
    {
        if (canMove) {
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), 0);
            rb.velocity = movement * 8;
        }
        else {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * 3.5f);
            rb.MovePosition(newPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle") Damage();

        if (other.tag == "Answer") {
            // if (GmLevel2.inst.numQuestions == 5 && other.GetComponentInChildren<Text>().text != "FOOD SECTION") Damage();
            
            if (GmLevel2.inst.numQuestions == 4 && other.GetComponentInChildren<Text>().text != "FRUIT AISLE") Damage();
            else if (GmLevel2.inst.numQuestions == 4 && other.GetComponentInChildren<Text>().text == "FRUIT AISLE") GmLevel2.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel2.inst.acerto);

            if (GmLevel2.inst.numQuestions == 3 && other.GetComponentInChildren<Text>().text != "GREEN PUMPKIN") Damage();
            else if (GmLevel2.inst.numQuestions == 3 && other.GetComponentInChildren<Text>().text == "GREEN PUMPKIN") GmLevel2.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel2.inst.acerto);

            if (GmLevel2.inst.numQuestions == 2 && other.GetComponentInChildren<Text>().text != "MEDIUM") Damage();
            else if (GmLevel2.inst.numQuestions == 2 && other.GetComponentInChildren<Text>().text == "MEDIUM") GmLevel2.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel2.inst.acerto);

            if (GmLevel2.inst.numQuestions == 1 && other.GetComponentInChildren<Text>().text != "BLUE TOOTHBRUSH") Damage();
            else if (GmLevel2.inst.numQuestions == 1 && other.GetComponentInChildren<Text>().text == "BLUE TOOTHBRUSH") GmLevel2.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel2.inst.acerto);
            if (GmLevel2.inst.numQuestions == 1 && GmLevel2.inst.life >= 1) {
                canMove = false;
                StartCoroutine(GmLevel2.inst.EndLevel1());
            }
            GmLevel2.inst.answer1.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GmLevel2.inst.answer2.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GmLevel2.inst.anwers3.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GmLevel2.inst.txtQuestions.text = "";
        }
    }

    private void Damage() {
        StartCoroutine(FlashMaterialCar());
        GmLevel2.inst.life--;
        GmLevel2.inst.gameObject.GetComponent<AudioSource>().PlayOneShot(GmLevel2.inst.damage);
        if (GmLevel2.inst.life == 2) {
            GmLevel2.inst.goLifes[0].SetActive(true);
            GmLevel2.inst.goLifes[1].SetActive(false);
        }
        else if (GmLevel2.inst.life == 1) {
            GmLevel2.inst.goLifes[1].SetActive(true);
            GmLevel2.inst.goLifes[2].SetActive(false);
        }
        else if (GmLevel2.inst.life == 0) {
            GmLevel2.inst.goLifes[2].SetActive(true);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            this.GetComponent<SpriteRenderer>().sortingOrder = 0;
            GmLevel2.inst.goGameOver.SetActive(true);
            GmLevel2.inst.gameOver = true;
        }
    }

    IEnumerator FlashMaterialCar() {
        this.GetComponent<SpriteRenderer>().material = flashMat;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().material = originalMat;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().material = flashMat;
        yield return new WaitForSeconds(.1f);
        this.GetComponent<SpriteRenderer>().material = originalMat;
    }
}
