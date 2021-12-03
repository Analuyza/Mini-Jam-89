using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel3 : MonoBehaviour
{
    Rigidbody2D rb;
    public Material originalMat, flashMat;
    bool canMove = true;
    Vector3 destination;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        destination = new Vector3(-3.48f, .42f, 0);
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
            // if (GmLevel3.inst.numQuestions == 5 && other.GetComponentInChildren<Text>().text != "FEW ITENS LINE") Damage();

            if (GmLevel3.inst.numQuestions == 4 && other.GetComponentInChildren<Text>().text != "BLACK DIAMOND") Damage();
            else if (GmLevel3.inst.numQuestions == 4 && other.GetComponentInChildren<Text>().text == "BLACK DIAMOND") GmLevel3.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel3.inst.acerto);

            if (GmLevel3.inst.numQuestions == 3 && other.GetComponentInChildren<Text>().text != "08wo1f") Damage();
            else if (GmLevel3.inst.numQuestions == 3 && other.GetComponentInChildren<Text>().text == "08wo1f") GmLevel3.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel3.inst.acerto);

            if (GmLevel3.inst.numQuestions == 2 && other.GetComponentInChildren<Text>().text != "PAY IN CASH") Damage();
            else if (GmLevel3.inst.numQuestions == 2 && other.GetComponentInChildren<Text>().text == "PAY IN CASH") GmLevel3.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel3.inst.acerto);

            if (GmLevel3.inst.numQuestions == 1 && other.GetComponentInChildren<Text>().text != "20$") Damage();
            else if (GmLevel3.inst.numQuestions == 1 && other.GetComponentInChildren<Text>().text == "20$") GmLevel3.inst.GetComponent<AudioSource>().PlayOneShot(GmLevel3.inst.acerto);
            if (GmLevel3.inst.numQuestions == 1 && GmLevel3.inst.life >= 1) {
                canMove = false;
                StartCoroutine(GmLevel3.inst.EndLevel1());
            }
            GmLevel3.inst.answer1.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GmLevel3.inst.answer2.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GmLevel3.inst.anwers3.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GmLevel3.inst.txtQuestions.text = "";
        }
    }

    private void Damage() {
        StartCoroutine(FlashMaterialCar());
        GmLevel3.inst.life--;
        GmLevel3.inst.gameObject.GetComponent<AudioSource>().PlayOneShot(GmLevel3.inst.damage);
        if (GmLevel3.inst.life == 2) {
            GmLevel3.inst.goLifes[0].SetActive(true);
            GmLevel3.inst.goLifes[1].SetActive(false);
        }
        else if (GmLevel3.inst.life == 1) {
            GmLevel3.inst.goLifes[1].SetActive(true);
            GmLevel3.inst.goLifes[2].SetActive(false);
        }
        else if (GmLevel3.inst.life == 0) {
            GmLevel3.inst.goLifes[2].SetActive(true);
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            this.GetComponent<SpriteRenderer>().sortingOrder = 0;
            GmLevel3.inst.goGameOver.SetActive(true);
            GmLevel3.inst.gameOver = true;
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
