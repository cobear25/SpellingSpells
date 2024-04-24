using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Word : MonoBehaviour
{
    public GameController gameController;
    public string word;
    public SpiritType spiritType;
    Color color;
    List<char> characters = new List<char>();
    TextMeshProUGUI text;

    List<string> availableWords = new List<string>()
    {
        "bumfuzzle",
        "lollygag",
        "widdershins",
        "gobbledygook",
        "blubber",
        "brouhaha",
        "higgledypiggledy",
        "nincompoop",
        "kerfuffle",
        "bamboozle",
        "codswallop",
        "balderdash",
        "flibbertigibbet",
        "fuddyduddy",
        "doozy",
        "fiddlefaddle",
        "hullabaloo",
        "malarkey",
        "shenanigans",
        "snollygoster",
        "gibberish",
        "barnacle",
        "ballyhoo",
        "fiddlesticks",
        "flummox",
        "rhubarb",
        "snickersnee",
        "wabbit",
        "gadzooks",
        "cattywampus",
        "doppelganger",
        "noodle",
        "pandemonium",
        "skedaddle",
        "blatherskite",
        "giggle",
        "hodgepodge",
        "mumbojumbo",
        "piffle",
        "pipsqueak",
        "razzledazzle",
        "whippersnapper",
        "abracadabra",
        "rumpus",
        "skullduggery",
        "tiddlywinks",
        "bibbidibobbidiboo",
        "whatchamacallit",
        "dillydally",
        "hocuspocus",
        "jibberjabber",
        "nambypamby",
        "nittygritty",
        "pitterpatter",
        "razzmatazz",
        "rigmarole",
        "willynilly",
        "xenophobia",
        "yoyo",
        "zany",
        "hoopla",
        "alakazam",
        "presto",
        "shazam",
        "fantasia",
        "flimflam",
        "Fartlek",
        "Everywhen",
        "meldrop",
        "obelus",
        "sozzled",
        "nimbus",
        "bumbershoot",
    };

    public List<Color> colors;

    // Start is called before the first frame update
    void Start()
    {
        word = availableWords[Random.Range(0, availableWords.Count)];
        int randColor = Random.Range(0, colors.Count);
        spiritType = (SpiritType)randColor;
        characters.AddRange(word.ToLower().ToCharArray());
        text = GetComponentInChildren<TextMeshProUGUI>();
        color = colors[randColor];
        text.color = color;
        text.text = word.ToUpper();
    }

    // Update is called once per frame
    void Update()
    {
        if (removed) {
            return;
        }
        if (gameController.gameOver) {
            return;
        }
        string inputString = Input.inputString.ToLower();
        if (inputString != "")
        {
            if (inputString[0] == characters[0])
            {
                characters.RemoveAt(0);
                string richText = "<color=#" + ColorUtility.ToHtmlStringRGB(Color.black) + ">" +
                          word.Substring(0, (word.Length - characters.Count)) + "</color>" +
                          "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" +
                          word.Substring((word.Length - characters.Count), characters.Count) + "</color>";
                text.text = richText.ToUpper();
                if (characters.Count == 0)
                {
                    gameController.WordTyped(word, spiritType);
                    RemoveWord();
                }
            } else if (characters.Count < word.Length)
            {
                // characters.Clear();
                // characters.AddRange(word.ToLower().ToCharArray());
                // text.color = color;
                // text.text = word.ToUpper();
            }
        }
    }

    public void ClearWord()
    {
        if (removed) {
            return;
        }
        characters.Clear();
        characters.AddRange(word.ToLower().ToCharArray());
        text.color = color;
        text.text = word.ToUpper();
    }

    public void SetNewWord()
    {
        word = availableWords[Random.Range(0, availableWords.Count)];
        int randColor = Random.Range(0, colors.Count);
        spiritType = (SpiritType)randColor;
        characters.Clear();
        characters.AddRange(word.ToLower().ToCharArray());
        text = GetComponentInChildren<TextMeshProUGUI>();
        color = colors[randColor];
        text.color = color;
        text.text = word.ToUpper();
        removed = false;
    }

    bool removed = false;
    public void RemoveWord()
    {
        removed = true;
        text.text = "";
    }
}
