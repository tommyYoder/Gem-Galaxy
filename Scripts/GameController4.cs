using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Use this for fifth level.

//[RequireComponent(typeof(AudioSource))]
public class GameController4 : MonoBehaviour {

    [SerializeField]
    private Sprite bgImage;

    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles = new List<Sprite>();

    public List<Button> btns = new List<Button>();

    private bool firstGuess, secondGuess;
    public float timeDelay = 1f;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
   // public AudioClip[] clips;
    Animator anim;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    
    // Looks for sprite assets in the resource folder and looks for animator component.
    void Awake()
    {
        puzzles = Resources.LoadAll<Sprite>("Assets");
        anim = GetComponent<Animator>();
    }

    // Looks for commands.
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
       
    }
    
    // Looks for array of buttons with the tag of PuzzleButton. Then sets the BG images thru a array.
        void GetButtons()
    {
       GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for (int i = 0; i < objects.Length; i++)
        {
           btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    // Adds puzzle pieces in a for loop through the index set above and divides the sprite images by 2.
    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
        for (int i = 0; i < looper; i++)
        { 

        if (index == looper / 2)
        {
            index = 0;
        }
        gamePuzzles.Add(puzzles[index]);
        index++;
    }
}
    // Listens for when each button is pressed and executes command.
        void AddListeners()
        {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(() => PickAButton());
        }
    }

    // Allows player to select each button to reveal whats behind each one. This is the player's first selection and audio is played when button is pressed.
    public void PickAButton()
    {

        if (!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            GetComponent<AudioSource>().Play();

        }
       
     // This is the second choice the player can make. Audio is played and count guesses is increased. Will execute start couratine to see if the cards are a match.
        else if (!secondGuess)
        {
            secondGuess = true;

            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            countGuesses++;

            GetComponent<AudioSource>().Play();

            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }

    // If selection is a match, then the buttons will not respond to player. Will look to see if the game is finished.
    IEnumerator CheckIfThePuzzlesMatch()
    {
        yield return new WaitForSeconds(1f);

        if(firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.2f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;


            
            anim.SetTrigger("Match");

            CheckIfTheGameIsFinished();

       // If selections were not a match, then BG will cover up button sprites. After .5 seconds buttons will become interactable again.     
        }else
        {
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
        }
        yield return new WaitForSeconds(.5f);
        firstGuess = secondGuess = false;
    }

    // If game is finished, then game will count the correct amount of guesses to game guesses. Then the game will load to next level.
    void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;

        if(countCorrectGuesses == gameGuesses)
        {

            Debug.Log("Game Finished");
            Debug.Log("It took you"+ countGuesses + "many guesses to finish the game");
            SceneManager.LoadScene("Puzzle_YouWin");
        }
        
    }

    // Will shuffle puzzle sprites to allow the player to guess where each correct piece is.
    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

