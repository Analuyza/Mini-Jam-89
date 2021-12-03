using System.Security.Cryptography.X509Certificates;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GmLevel3 : MonoBehaviour
{
    public static GmLevel3 inst;
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
    public GameObject caixa;
    public Text txtEnd;
    public bool gameOver;
    public GameObject goGameOver;
    public AudioClip message, messageDigit, damage, acerto, victory;
    public GameObject goVictory, txtVictory;
    bool getkey1, getkey2;

    void Start()
    {
        inst = this;
        startPanel = true;
        getkey1 = true;
        getkey2 = false;
    }

    void Update()
    {
        if (start.activeInHierarchy && startPanel) {
            StartCoroutine(StartLevel());
        }

        if (goVictory.activeInHierarchy && Input.GetKeyDown(KeyCode.X) && (getkey1 || getkey2)) {
            if (goVictory.GetComponent<SpriteRenderer>().sprite.name == "Victory 1") {
                goVictory.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Level3/Victory 2");
                txtVictory.GetComponent<Text>().text = "Press X to go to Menu";
                getkey1 = false;
                StartCoroutine(GetKeyVictory());
            }
            if (goVictory.GetComponent<SpriteRenderer>().sprite.name == "Victory 2" && getkey2) {
                SceneManager.LoadScene("Menu");
            }
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
                    SceneManager.LoadScene("Level3");
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
            float time = Random.Range(1, 1.5f);
            yield return new WaitForSeconds(time);
            
            int randomObs = Random.Range(0, 5);
            GameObject obstacle = Instantiate(randomObstaclesPref[randomObs], randomObstaclesPos[Random.Range(0, 4)].transform);

            speedObst += .2f;
        }
        if (!finished) StartCoroutine(InstanceObst());
    }

    IEnumerator WifeMessages() {
        yield return new WaitForSeconds(2);
        this.GetComponent<AudioSource>().PlayOneShot(message);
        StartCoroutine(WifeEffectTypeWriter("Hey baby, use the black diamond card, the password is 08wo1f. If the card doesn't work, pay in cash, use that old 20$ note."));
        yield return new WaitForSeconds(12);
        wifeMessage.text = "Hey baby, use the <color=magenta>black diamond card</color>, the password is <color=magenta>08wo1f</color>. If the card doesn't work, <color=magenta>pay in cash</color>, use that <color=magenta>old 20$ note</color>.";
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
                // case 5: StartCoroutine(QuestionsEffectTypeWriter("Which line should you use?")); break;
                case 4: StartCoroutine(QuestionsEffectTypeWriter("Which card will you use?")); break;
                case 3: StartCoroutine(QuestionsEffectTypeWriter("What’s the password?")); break;
                case 2: StartCoroutine(QuestionsEffectTypeWriter("The card isn’t working, you should…")); break;
                case 1: StartCoroutine(QuestionsEffectTypeWriter("Which note should you use?")); break;
                default: txtQuestions.text = ""; break;
            }
                
            yield return new WaitForSeconds(5); //aparecem as respostas
            switch (numQuestions) {
                // case 5: 
                //     ArrayList randomAnswers = new ArrayList();
                //     randomAnswers.Add("FEW ITENS LINE"); randomAnswers.Add("NORMAL ITENS LINE"); randomAnswers.Add("OLDER PEOPLE LINE");
                //     int randomPick = Random.Range(0, 3);
                //     answer1.text = randomAnswers[randomPick].ToString();
                //     randomAnswers.RemoveAt(randomPick);
                //     int randomPick12 = Random.Range(0, 2);
                //     answer2.text = randomAnswers[randomPick12].ToString();
                //     randomAnswers.RemoveAt(randomPick12);
                //     anwers3.text = randomAnswers[0].ToString();
                //     break;
                case 4: 
                    ArrayList randomAnswers2 = new ArrayList();
                    randomAnswers2.Add("BLACK DIAMOND"); randomAnswers2.Add("RED DIAMOND"); randomAnswers2.Add("GOLD EAGLE");
                    int randomPick2 = Random.Range(0, 3);
                    answer1.text = randomAnswers2[randomPick2].ToString();
                    randomAnswers2.RemoveAt(randomPick2);
                    int randomPick22 = Random.Range(0, 2);
                    answer2.text = randomAnswers2[randomPick22].ToString();
                    randomAnswers2.RemoveAt(randomPick22);
                    anwers3.text = randomAnswers2[0].ToString();
                    break;
                case 3: 
                    ArrayList randomAnswers3 = new ArrayList();
                    randomAnswers3.Add("08wo1f"); randomAnswers3.Add("72dog3"); randomAnswers3.Add("w0lf33");
                    int randomPick3 = Random.Range(0, 3);
                    answer1.text = randomAnswers3[randomPick3].ToString();
                    randomAnswers3.RemoveAt(randomPick3);
                    int randomPick32 = Random.Range(0, 2);
                    answer2.text = randomAnswers3[randomPick32].ToString();
                    randomAnswers3.RemoveAt(randomPick32);
                    anwers3.text = randomAnswers3[0].ToString();
                    break;
                case 2: 
                    ArrayList randomAnswers4 = new ArrayList();
                    randomAnswers4.Add("TRY IT AGAIN"); randomAnswers4.Add("PAY IN CASH"); randomAnswers4.Add("GIVE UP");
                    int randomPick4 = Random.Range(0, 3);
                    answer1.text = randomAnswers4[randomPick4].ToString();
                    randomAnswers4.RemoveAt(randomPick4);
                    int randomPick42 = Random.Range(0, 2);
                    answer2.text = randomAnswers4[randomPick42].ToString();
                    randomAnswers4.RemoveAt(randomPick42);
                    anwers3.text = randomAnswers4[0].ToString();
                    break;
                case 1: 
                    ArrayList randomAnswers5 = new ArrayList();
                    randomAnswers5.Add("20$"); randomAnswers5.Add("10$"); randomAnswers5.Add("50$");
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
        yield return new WaitForSeconds(9);
        StartCoroutine(QuestionTime());
    }

    public IEnumerator EndLevel1() {
        finished = true;
        caixa.SetActive(true);
        yield return new WaitForSeconds(5);
        goVictory.transform.parent.gameObject.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = victory;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
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

    IEnumerator GetKeyVictory() {
        yield return new WaitForSeconds(.5f);
        getkey2 = true;
    }
}
