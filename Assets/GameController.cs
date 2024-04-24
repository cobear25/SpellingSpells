using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject wordPrefab;
    public GameObject spiritPrefab;
    public Canvas canvas;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public AudioClip popSound;
    public AudioClip missSound;
    public AudioClip gameOverSound;
    public List<Word> words = new List<Word>();
    List<Spirit> spirits = new List<Spirit>();
    float spawnRate = 2.5f;
    int score = 0;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        AddSpirit();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Return)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AddWords();
        }
    }

    public void AddSpirit() {
        GameObject spiritObj = Instantiate(spiritPrefab);
        Spirit spirit = spiritObj.GetComponent<Spirit>();
        spirit.gameController = this;
        spirits.Add(spirit);
        spiritObj.transform.position = new Vector2(10, 3.75f);
        Invoke("AddSpirit", spawnRate);
        spawnRate -= 0.03f;
    }

    public void AddWords() {
        foreach (Word word in words) {
            word.SetNewWord();
        }
        // for (int i = 0; i < 4; i++) {
        //     for (int j = 0; j < 7; j++) {
        //         GameObject wordObj = Instantiate(wordPrefab);
        //         Word word = wordObj.GetComponent<Word>();
        //         word.gameController = this;
        //         words.Add(word);
        //         wordObj.transform.SetParent(canvas.transform);
        //         wordObj.transform.localScale = new Vector3(1, 1, 1);
        //         wordObj.transform.position = new Vector3(280 + i * 370, 150 + j * 75, 0);
        //         if (i == 0 || i == 3) {
        //             wordObj.transform.position = new Vector2(wordObj.transform.position.x, wordObj.transform.position.y + 50);
        //         }
        //         wordObj.transform.eulerAngles = new Vector3(0, 0, Random.Range(-2, 2));
        //     }
        // }
    }

    public void SpiritReachedEnd() {
        if (!gameOver) {
            GetComponent<AudioSource>().PlayOneShot(gameOverSound, 0.5f);
            foreach (Word word in words)
            {
                if (word != null)
                {
                    Destroy(word.gameObject);
                }
            }
            gameOverPanel.SetActive(true);
        }
        gameOver = true;
    }

    public void WordTyped(string word, SpiritType spiritType)
    {
        foreach (Word w in words)
        {
            if (w.word != word) {
                w.ClearWord();
            }
        }
        foreach (Spirit s in spirits)
        {
            if (!s.isDestroyed && spiritType == s.spiritType)
            {
                GetComponent<AudioSource>().PlayOneShot(popSound);
                s.Kill();
                score++;
                scoreText.text = $"{score}";
                spirits.Remove(s);
                break;
            }
        }
    }
}
