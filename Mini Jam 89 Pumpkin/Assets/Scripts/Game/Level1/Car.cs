using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    Rigidbody2D rb;
    public Material originalMat, flashMat;
    bool canMove = true;
    Vector3 destination;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        destination = new Vector3(-2.8f, .88f, 0);
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
            // if (GmLevel1.inst.numQuestions == 5 && other.GetComponentInChildren<Text>().text != "GO") Damage();
            // else if (GmLevel1.inst.numQuestions == 5 && other.GetComponentInChildren<Text>().text == "GO") GmLevel1.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel1.inst.acerto);

            if (GmLevel1.inst.numQuestions == 4 && other.GetComponentInChildren<Text>().text != "WAIT") Damage();
            else if (GmLevel1.inst.numQuestions == 4 && other.GetComponentInChildren<Text>().text == "WAIT") GmLevel1.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel1.inst.acerto);

            if (GmLevel1.inst.numQuestions == 3 && other.GetComponentInChildren<Text>().text != "43KM/H") Damage();
            else if (GmLevel1.inst.numQuestions == 3 && other.GetComponentInChildren<Text>().text == "43KM/H") GmLevel1.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel1.inst.acerto);

            if (GmLevel1.inst.numQuestions == 2 && other.GetComponentInChildren<Text>().text != "612 ROAD") Damage();
            else if (GmLevel1.inst.numQuestions == 2 && other.GetComponentInChildren<Text>().text == "612 ROAD") GmLevel1.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel1.inst.acerto);

            if (GmLevel1.inst.numQuestions == 1 && other.GetComponentInChildren<Text>().text != "PERSEPHONE") Damage();
            else if (GmLevel1.inst.numQuestions == 1 && other.GetComponentInChildren<Text>().text == "PERSEPHONE") GmLevel1.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel1.inst.acerto);
            if (GmLevel1.inst.numQuestions == 1 && GmLevel1.inst.life >= 1) {
                canMove = false;
                StartCoroutine(GmLevel1.inst.EndLevel1());
            }
            GmLevel1.inst.answer1.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GmLevel1.inst.answer2.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GmLevel1.inst.txtQuestions.text = "";
        }
    }

    private void Damage() {
        GmLevel1.inst.gameObject.GetComponent<AudioSource>().PlayOneShot(GmLevel1.inst.damage);
        StartCoroutine(FlashMaterialCar());
        GmLevel1.inst.life--;
        if (GmLevel1.inst.life == 2) {
            GmLevel1.inst.goLifes[0].SetActive(true);
            GmLevel1.inst.goLifes[1].SetActive(false);
        }
        else if (GmLevel1.inst.life == 1) {
            GmLevel1.inst.goLifes[1].SetActive(true);
            GmLevel1.inst.goLifes[2].SetActive(false);
        }
        else if (GmLevel1.inst.life == 0) {
            GmLevel1.inst.goLifes[2].SetActive(true);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            this.GetComponent<SpriteRenderer>().sortingOrder = 0;
            GmLevel1.inst.goGameOver.SetActive(true);
            GmLevel1.inst.gameOver = true;
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
