 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;
    public Sprite[] puzzles;
    public List<Sprite> gamePuzzles=new List<Sprite>();
    public List<Button> btns=new List<Button>();
    private bool firstGuess,secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex,secondGuessIndex;
    private string firstGuessPuzzle,secondGuessPuzzle;
    private void Awake()
    {
        puzzles=Resources.LoadAll<Sprite>("Images/Animals");
    }
    void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles );

        gameGuesses=gamePuzzles.Count/2;
    }

    // Update is called once per frame
    void GetButtons()
    {
        GameObject[] objects= GameObject.FindGameObjectsWithTag("puzzleBtn");

        for (int i =0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite=bgImage;
        }

        
        
    }
    void AddGamePuzzles()
    {
        int looper=btns.Count;
        int index=0;
        for (int i= 0 ; i < looper;i++)
        {
            if (index==looper/2)
            {
                index=0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;


        }
    }
    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(()=>PickPuzzle());

        }
        
    }
    public void PickPuzzle()
        {
            //string name =UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
            if (!firstGuess)
            {
                firstGuess=true;
                firstGuessIndex=int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
                firstGuessPuzzle=gamePuzzles[firstGuessIndex].name;

                btns[firstGuessIndex].image.sprite=gamePuzzles[firstGuessIndex];


            }
            else if (!secondGuess)
            {
                secondGuess=true;
                secondGuessIndex=int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);


                secondGuessPuzzle=gamePuzzles[secondGuessIndex].name;

                btns[secondGuessIndex].image.sprite=gamePuzzles[secondGuessIndex];

                if(firstGuessPuzzle==secondGuessPuzzle)
                {
                    print("Puzzle Match");

                }
                else
                {
                    print("Puzzle Does not match ");
                }
                StartCoroutine(checkThePuzzleMatch());

            }

        }
        IEnumerator checkThePuzzleMatch(){
            yield return new WaitForSeconds(0.5f);
            if(firstGuessPuzzle==secondGuessPuzzle){
                
                btns[firstGuessIndex].interactable=false;
                btns[secondGuessIndex].interactable=false;
                btns[firstGuessIndex].image.color=new Color(0,0,0);
                btns[secondGuessIndex].image.color=new Color(0,0,0);

                CheckTheGameFinished();
            }
            else{
                btns[firstGuessIndex].image.sprite=bgImage;
                btns[secondGuessIndex].image.sprite=bgImage;
            }
            yield return new WaitForSeconds(0.5f);
            firstGuess=secondGuess=false;

        }
        void CheckTheGameFinished(){
            countCorrectGuesses++;
            if(countCorrectGuesses==gameGuesses){
                print("game finished");
                print("tooks you"+countGuesses+" ");
            }

        }
        void Shuffle(List<Sprite>list){
            for(int i =0;i<list.Count;i++){
                Sprite temp=list[i];
                int randomIndex=Random.Range(i,list.Count);
                list[i]=list[randomIndex];
                list[randomIndex]=temp;
            }
        }
}
