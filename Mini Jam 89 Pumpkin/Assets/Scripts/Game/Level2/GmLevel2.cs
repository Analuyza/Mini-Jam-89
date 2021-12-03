using System.Security.Cryptography.X509Certificates;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GmLevel2 : MonoBehaviour
{
    public static GmLevel2 inst;
    public GameObject start;
    public bool startPanel, started, coroutineStarted = false, canInstObs;
    public GameObject[] randomObstaclesPos;
    public GameObject[] randomObstaclesPref;
    public GameObject[] goLifes;
    public float speedObst = 1;
    public int life = 3;
    public Text wifeMessage, txtQuestions;
    public int numQuestions = 4;
    public Text answer1, answer2, anwers3;
    public GameObject awnswers;
    public bool finished;
    public GameObject punpkim;
    public GameObject goEnd;
    public Text txtEnd;
    public bool gameOver;
    public GameObject goGameOver;
    public AudioClip message, messageDigit, damage, acerto;

    void Start()
    {
        inst = this;
        startPanel = true;
    }

    void Update()
    {
        if (start.activeInHierarchy && startPanel) {
            StartCoroutine(StartLevel());
        }

        if (started) {
            if (coroutineStarted) {
                coroutineStarted = false;
                StartCoroutine(InstanceObst());
                StartCoroutine(WifeMessages());
                StartCoroutine(StartQuestions());
            }

            if (goGameOver.activeInHierarchy) {
                awnswers.SetActive(false);
                wifeMessage.gameObject.SetActive(false);
                txtQuestions.gameObject.SetActive(false);
                if (Input.GetKey(KeyCode.X)) {
                    SceneManager.LoadScene("Level2");
                }
            }
        }
    }

    IEnumerator InstanceObst() {
        if (!finished && !canInstObs) {
            yield return new WaitForSeconds(11);
            canInstObs = true;
        }
        if (!finished && canInstObs) {
            float time = Random.Range(1, 2);
            yield return new WaitForSeconds(time);
            
            int randomObs = Random.Range(0, 5);
            GameObject obstacle = Instantiate(randomObstaclesPref[randomObs], randomObstaclesPos[Random.Range(0, 3)].transform);

            speedObst += .05f;
        }
        if (!finished) StartCoroutine(InstanceObst());
    }

    IEnumerator WifeMessages() {
        yield return new WaitForSeconds(2);
        this.GetComponent<AudioSource>().PlayOneShot(message);
        StartCoroutine(WifeEffectTypeWriter("Hey baby, I want a green pumpkin, a medium one, I’m not that hungry. And could you buy me a blue toothbrush?"));
        yield return new WaitForSeconds(14);
        wifeMessage.text = "Hey baby, I want a <color=magenta>green pumpkin</color>, a <color=magenta>medium</color> one, I’m not that hungry. And could you buy me a <color=magenta>blue toothbrush</color>?";
    }

    IEnumerator WifeEffectTypeWriter(string text) {
        foreach(char character in text.ToCharArray()) {
            this.GetComponent<AudioSource>().PlayOneShot(messageDigit);
            wifeMessage.text += character;
            yield return new WaitForSeconds(.07f);
            yield return null;
        }
    }
    
    IEnumerator QuestionsEffectTypeWriter(string text) {
        foreach(char character in text.ToCharArray()) {
            txtQuestions.text += character;
            yield return new WaitForSeconds(.07f);
            yield return null;
        }
    }

    IEnumerator QuestionTime() {
        if (!finished || !gameOver) {
            yield return new WaitForSeconds(5);
            
            answer1.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
            answer2.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
            anwers3.transform.parent.GetComponent<BoxCollider2D>().enabled = true;

            switch (numQuestions) {
                // case 5: StartCoroutine(QuestionsEffectTypeWriter("The market is big, you should go to the…")); break;
                case 4: StartCoroutine(QuestionsEffectTypeWriter("The food section is big, you should go to the…")); break;
                case 3: StartCoroutine(QuestionsEffectTypeWriter("So many pumpkins, you should get a…")); break;
                case 2: StartCoroutine(QuestionsEffectTypeWriter("What pumpkin size do you get?")); break;
                case 1: StartCoroutine(QuestionsEffectTypeWriter("There’s something missing, you should buy a...")); break;
                default: txtQuestions.text = ""; break;
            }
                
            yield return new WaitForSeconds(5); //aparecem as respostas
            switch (numQuestions) {
                // case 5: 
                //     ArrayList randomAnswers = new ArrayList();
                //     randomAnswers.Add("FOOD SECTION"); randomAnswers.Add("CLOTHES SECTION");
                //     int randomPick = Random.Range(0, 2);
                //     answer1.text = randomAnswers[randomPick].ToString();
                //     randomAnswers.RemoveAt(randomPick);
                //     answer2.text = randomAnswers[0].ToString();
                //     break;
                case 4: 
                    ArrayList randomAnswers2 = new ArrayList();
                    randomAnswers2.Add("FRUIT AISLE"); randomAnswers2.Add("MEAT AISLE");
                    int randomPick2 = Random.Range(0, 2);
                    answer1.text = randomAnswers2[randomPick2].ToString();
                    randomAnswers2.RemoveAt(randomPick2);
                    answer2.text = randomAnswers2[0].ToString();
                    break;
                case 3: 
                    ArrayList randomAnswers3 = new ArrayList();
                    randomAnswers3.Add("GREEN PUMPKIN"); randomAnswers3.Add("ORANGE PUMPKIN");
                    int randomPick3 = Random.Range(0, 2);
                    answer1.text = randomAnswers3[randomPick3].ToString();
                    randomAnswers3.RemoveAt(randomPick3);
                    answer2.text = randomAnswers3[0].ToString();
                    break;
                case 2: 
                    ArrayList randomAnswers4 = new ArrayList();
                    randomAnswers4.Add("BIG"); randomAnswers4.Add("MEDIUM"); randomAnswers4.Add("SMALL");
                    int randomPick4 = Random.Range(0, 3);
                    answer1.text = randomAnswers4[randomPick4].ToString();
                    randomAnswers4.RemoveAt(randomPick4);
                    int randomPick42 = Random.Range(0, 2);
                    answer2.text = randomAnswers4[randomPick42].ToString();
                    randomAnswers4.RemoveAt(randomPick42);
                    anwers3.text = randomAnswers4[0].ToString();
                    
                    anwers3.transform.parent.gameObject.SetActive(true);
                    answer1.transform.parent.transform.localPosition = new Vector3(-3.434f, 1.46f, -2279.156f);
                    answer2.transform.parent.transform.localPosition = new Vector3(-0.296f, 1.46f, -2279.156f);
                    break;
                case 1: 
                    ArrayList randomAnswers5 = new ArrayList();
                    randomAnswers5.Add("BLUE TOOTHBRUSH"); randomAnswers5.Add("PINK HAIRBRUSH"); randomAnswers5.Add("GREEN PAINTBRUSH");
                    int randomPick5 = Random.Range(0, 3);
                    answer1.text = randomAnswers5[randomPick5].ToString();
                    randomAnswers5.RemoveAt(randomPick5);
                    int randomPick52 = Random.Range(0, 2);
                    answer2.text = randomAnswers5[randomPick52].ToString();
                    randomAnswers5.RemoveAt(randomPick52);
                    anwers3.text = randomAnswers5[0].ToString();
                    break;
            }
            awnswers.SetActive(true);

            yield return new WaitForSeconds(5);
            numQuestions--;
            awnswers.SetActive(false);
            StartCoroutine(QuestionTime());
        }
    }

    IEnumerator StartQuestions() {
        yield return new WaitForSeconds(15);
        StartCoroutine(QuestionTime());
    }

    public IEnumerator EndLevel1() {
        finished = true;
        punpkim.SetActive(true);
        yield return new WaitForSeconds(4);
        goEnd.SetActive(true);
        StartCoroutine(EndEffectTypeWriter("LEVEL 2 COMPLETE!\n\nJust one step left, you have to pay the pumpkin at the cashier"));
        yield return new WaitForSeconds(9);
        SceneManager.LoadScene("Level3");
    }

    IEnumerator EndEffectTypeWriter(string text) {
        foreach(char character in text.ToCharArray()) {
            txtEnd.text += character;
            yield return new WaitForSeconds(.07f);
            yield return null;
        }
    }

    IEnumerator StartLevel() {
        startPanel = false;
        
        float alphaColor = start.GetComponent<SpriteRenderer>().color.a;
        alphaColor -= .1f;
        start.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alphaColor);
        
        yield return new WaitForSeconds(.1f);
        if (alphaColor > 0) StartCoroutine(StartLevel());
        else {
            started = true;
            coroutineStarted = true;
            start.SetActive(false);
        }
    }
}
