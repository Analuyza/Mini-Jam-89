using System.Security.Cryptography.X509Certificates;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GmLevel1 : MonoBehaviour
{
    public static GmLevel1 inst;
    public GameObject start, txtStart;
    public bool startPanel, started, coroutineStarted = false, canInstObs;
    public GameObject[] randomObstaclesPos;
    public GameObject[] randomObstaclesPref;
    public GameObject[] goLifes;
    public float speedObst = 1;
    public int life = 3;
    public Text wifeMessage, txtQuestions;
    public int numQuestions = 4;
    public Text answer1, answer2;
    public GameObject awnswers;
    public bool finished;
    public GameObject market;
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
        if (start.activeInHierarchy && Input.GetKey(KeyCode.X) && startPanel) {
            StartCoroutine(StartLevel());
        }

        if (started && !finished) {
            if (coroutineStarted) {
                coroutineStarted = false;
                StartCoroutine(InstanceObst());
                StartCoroutine(WifeMessages());
                StartCoroutine(StartQuestions());
            }            
        }
        if (goGameOver.activeInHierarchy) {
            awnswers.SetActive(false);
            wifeMessage.gameObject.SetActive(false);
            txtQuestions.gameObject.SetActive(false);
            if (Input.GetKey(KeyCode.X)) {
                SceneManager.LoadScene("Level1");
            }
        }
    }

    IEnumerator InstanceObst() {
        if (!finished && !canInstObs) {
            yield return new WaitForSeconds(11);
            canInstObs = true;
        }
        if (!finished && canInstObs) {
            float time = Random.Range(1, 5);
            yield return new WaitForSeconds(time);
            
            int randomObs = Random.Range(0, 4);
            GameObject obstacle = Instantiate(randomObstaclesPref[randomObs], randomObstaclesPos[Random.Range(0, 3)].transform);

            speedObst += .2f;
        }
        if (!finished) StartCoroutine(InstanceObst());
    }

    IEnumerator WifeMessages() {
        yield return new WaitForSeconds(2);
        this.GetComponent<AudioSource>().PlayOneShot(message);
        StartCoroutine(WifeEffectTypeWriter("Hey baby, remember, I want the pumpkin from the Persephone Market, it's the best. Also, take the 612 road, please, it's the fastest way."));
        yield return new WaitForSeconds(14);
        wifeMessage.text = "Hey baby, remember, I want the pumpkin from the <color=magenta>Persephone Market</color>, it's the best. Also, take the <color=magenta>612 road</color>, please, it's the fastest way.";
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
            
            switch (numQuestions) {
                // case 5: StartCoroutine(QuestionsEffectTypeWriter("The traffic light is green, what should you do?")); break;
                case 4: StartCoroutine(QuestionsEffectTypeWriter("The traffic light is yellow, what should you do?")); break;
                case 3: StartCoroutine(QuestionsEffectTypeWriter("The speed limit is 50km/here, you should drive at…")); break;
                case 2: StartCoroutine(QuestionsEffectTypeWriter("There are two roads you can take, you take the…")); break;
                case 1: StartCoroutine(QuestionsEffectTypeWriter("In which market do you enter?")); break;
                default: txtQuestions.text = ""; break;
            }
                
            yield return new WaitForSeconds(5); //aparecem as respostas
            switch (numQuestions) {
                // case 5: 
                //     ArrayList randomAnswers = new ArrayList();
                //     randomAnswers.Add("GO"); randomAnswers.Add("STOP");
                //     int randomPick = Random.Range(0, 2);
                //     answer1.text = randomAnswers[randomPick].ToString();
                //     randomAnswers.RemoveAt(randomPick);
                //     answer2.text = randomAnswers[0].ToString();
                //     // answer1.text = "GO"; answer2.text = "STOP"; 
                //     break;
                case 4: 
                    ArrayList randomAnswers2 = new ArrayList();
                    randomAnswers2.Add("WAIT"); randomAnswers2.Add("STOP");
                    int randomPick2 = Random.Range(0, 2);
                    answer1.text = randomAnswers2[randomPick2].ToString();
                    randomAnswers2.RemoveAt(randomPick2);
                    answer2.text = randomAnswers2[0].ToString();
                    break;
                case 3: 
                    ArrayList randomAnswers3 = new ArrayList();
                    randomAnswers3.Add("52KM/H"); randomAnswers3.Add("43KM/H");
                    int randomPick3 = Random.Range(0, 2);
                    answer1.text = randomAnswers3[randomPick3].ToString();
                    randomAnswers3.RemoveAt(randomPick3);
                    answer2.text = randomAnswers3[0].ToString();
                    break;
                case 2: 
                    ArrayList randomAnswers4 = new ArrayList();
                    randomAnswers4.Add("612 ROAD"); randomAnswers4.Add("912 ROAD");
                    int randomPick4 = Random.Range(0, 2);
                    answer1.text = randomAnswers4[randomPick4].ToString();
                    randomAnswers4.RemoveAt(randomPick4);
                    answer2.text = randomAnswers4[0].ToString();
                    break;
                case 1: 
                    answer1.GetComponent<RectTransform>().sizeDelta = new Vector2(8.5177f, 3.7053f);
                    answer1.GetComponent<RectTransform>().localScale = new Vector3(0.1793293f, 0.1793293f, 0.1793293f);

                    answer2.GetComponent<RectTransform>().sizeDelta = new Vector2(8.5177f, 3.7053f);
                    answer2.GetComponent<RectTransform>().localScale = new Vector3(0.1793293f, 0.1793293f, 0.1793293f);

                    ArrayList randomAnswers5 = new ArrayList();
                    randomAnswers5.Add("APHRODITE"); randomAnswers5.Add("PERSEPHONE");
                    int randomPick5 = Random.Range(0, 2);
                    answer1.text = randomAnswers5[randomPick5].ToString();
                    randomAnswers5.RemoveAt(randomPick5);
                    answer2.text = randomAnswers5[0].ToString();
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
        yield return new WaitForSeconds(10);
        StartCoroutine(QuestionTime());
    }

    public IEnumerator EndLevel1() {
        finished = true;
        market.SetActive(true);
        yield return new WaitForSeconds(4);
        goEnd.SetActive(true);
        StartCoroutine(EndEffectTypeWriter("LEVEL 1 COMPLETE!\n\nNow you have to enter the market and find a pumpkin"));
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Level2");
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
        start.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alphaColor);
        txtStart.GetComponent<Text>().color = new Color(255, 255, 255, alphaColor);
        
        yield return new WaitForSeconds(.1f);
        if (alphaColor > 0) StartCoroutine(StartLevel());
        else {
            started = true;
            coroutineStarted = true;
            start.transform.parent.gameObject.SetActive(false);
        }
    }
}
