using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScript : MonoBehaviour {
    public GameObject[] references;
    public enum Seed {EMPTY,CROSS,NOUGHT};
    public Seed[] Cells;
    public GameObject Cross, Nought,Empty,Bar,CrossBar,Cellls;
    //public enum Players {PLAYER, CPU};
    public Seed turn,player1,player2;
    Vector2 pos1, pos2;
    public UILabel Instructions;
    public bool playing = true;
    int delay = 30;
    public UIButton playagain;
    System.Random rnd = new System.Random();
    public int SideWon = 0;
    public int AtMove = 0;


	// Use this for initialization
	void Start () {
        playagain.isEnabled = false;
        Cells = new Seed[9];
        for (int i = 0; i < 9; i++)
        {
            Cells[i] = Seed.EMPTY;
        }
        int x = rnd.Next(0, 2);
        if (x == 0)
            turn = Seed.CROSS;
        else
            turn = Seed.NOUGHT;

        Instructions.text = "Turn: " + turn.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        if (playing){
            Instructions.text = "Turn: " + turn.ToString();
         
        }  
        else if (isDraw())
            Instructions.text = "It's a Draw!!";
        else
            Instructions.text = whoWon().ToString() + " Won!";
        //if (!playing)
        //{
        //    GetDelay();
        //    Application.LoadLevel("main");
        //}
           
	
    }
    

    public void SpawnNew(GameObject empty,int idx){
        
        Cells[idx] = turn;
        if (turn == Seed.CROSS)
        {
            turn = Seed.NOUGHT;
            GameObject Child = NGUITools.AddChild(Cellls.gameObject, Cross);
            Child.transform.position = empty.transform.position;
            references[idx] = Child;
        }
        else if(turn == Seed.NOUGHT)
        {
            turn = Seed.CROSS;
            GameObject Child = NGUITools.AddChild(Cellls.gameObject, Nought);
            Child.transform.position = empty.transform.position;
            references[idx] =Child;
        }
        /*else
        {
            references[idx] = Instantiate(Empty, empty.transform.position, Quaternion.identity) as GameObject;
        }*/
        if (isDraw() == true)
        {
            turn = Seed.EMPTY;
            playing = false;
        }
        if (whoWon() != Seed.EMPTY && playing)
        {
            Vector2 center = calCenter();
            if (pos1.x == pos2.x)
            {
                GameObject Child = NGUITools.AddChild(Cellls.gameObject, Bar);
                Child.transform.position = center;
                Child.transform.rotation = Quaternion.identity;
            }
            else if (pos1.y == pos2.y)
            {
                GameObject Child = NGUITools.AddChild(Cellls.gameObject, Bar);
                Child.transform.position = center;
                Child.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                if (pos1.x < 0)
                {
                    GameObject Child = NGUITools.AddChild(Cellls.gameObject, CrossBar);
                    Child.transform.position = center;
                    Child.transform.rotation = Quaternion.Euler(0, 0, 45);
                }

                else
                {
                    GameObject Child = NGUITools.AddChild(Cellls.gameObject, CrossBar);
                    Child.transform.position = center;
                    Child.transform.rotation = Quaternion.Euler(0, 0, -45);
                }
              
            }
            playing = false;
        }
        if (!playing)
        {
            //playagain.interactable = true;
            playagain.isEnabled = true;
        }
        Destroy(empty);
        
    }
  
    bool isDraw()
    {
        if(whoWon() != Seed.EMPTY)
            return false;
        for (int i = 0; i < 9;i++)
            if(Cells[i]==Seed.EMPTY)
                return false;
        return true;
    }

    Seed whoWon()
    {
        Seed flag=Seed.EMPTY;
        if (Cells[0] != Seed.EMPTY)
        {
            pos1 = references[0].transform.position;
            if ((Cells[0] == Cells[1]) && Cells[2] == Cells[0])
            {
                flag = Cells[0];              
                pos2 = references[2].transform.position;
            }
            else if ((Cells[0] == Cells[3]) && Cells[0] == Cells[6])
            {
                flag = Cells[0];
                pos2 = references[6].transform.position;
            }
            else if ((Cells[0] == Cells[4]) && Cells[0] == Cells[8])
            {
                flag = Cells[0];
                pos2 = references[8].transform.position;
            }
        }
        if (flag == Seed.EMPTY && Cells[8] != Seed.EMPTY)
        {
            pos1 = references[8].transform.position;
            if ((Cells[8] == Cells[7]) && Cells[6] == Cells[8])
            {
                flag = Cells[8];
                pos2 = references[6].transform.position;
            }
            else if ((Cells[8] == Cells[5]) && Cells[2] == Cells[8])
            {
                flag = Cells[8];
                pos2 = references[2].transform.position;
            }
        }
        if (flag == Seed.EMPTY && Cells[4] != Seed.EMPTY)
        {
            if ((Cells[4] == Cells[3]) && Cells[4] == Cells[5])
            {
                flag = Cells[4];
                pos1 = references[3].transform.position;
                pos2 = references[5].transform.position;
            }
            else if ((Cells[4] == Cells[1]) && Cells[4] == Cells[7])
            {
                flag = Cells[4];
                pos1 = references[1].transform.position;
                pos2 = references[7].transform.position;
            }
            else if ((Cells[4] == Cells[2]) && Cells[4] == Cells[6])
            {
                flag = Cells[4];
                pos1 = references[2].transform.position;
                pos2 = references[6].transform.position;
            }
        }
        return flag;
    }

    Vector2 calCenter()
    {
        float x = (pos1.x + pos2.x)/2,
              y = (pos1.y + pos2.y)/2;
        return new Vector2(x, y);
    }

    void GameOver(string display)
    {

    }




    public void SwitchTurn()
    {
        // SideToPlay = SideToPlay == 'O' ? 'X' : 'O'; 
        turn = turn == Seed.NOUGHT ? Seed.CROSS : Seed.NOUGHT;
    }

    public void MakeMove(int move)
    {
        //  Grid[move] = SideToPlay;
        Cells[move] = turn;
        SwitchTurn();
        AtMove++;
    }

    public void UndoMove(int move)
    {
        // Grid[move] = ' ';
        Cells[move] = Seed.EMPTY;
        SwitchTurn();
        AtMove--;
    }

    public int Evaluate()
    { //Horizontal check 
        if (Cells[0] != Seed.EMPTY && Cells[0] == Cells[1] && Cells[1] == Cells[2])
            return SideWonTheGame();
        if (Cells[3] != Seed.EMPTY && Cells[3] == Cells[4] && Cells[4] == Cells[5])
            return SideWonTheGame();
        if (Cells[6] != Seed.EMPTY && Cells[6] == Cells[7] && Cells[7] == Cells[8])
            return SideWonTheGame();
        //Vertical check 
        if (Cells[0] != Seed.EMPTY && Cells[0] == Cells[3] && Cells[3] == Cells[6])
            return SideWonTheGame();
        if (Cells[1] != Seed.EMPTY && Cells[1] == Cells[4] && Cells[4] == Cells[7])
            return SideWonTheGame();
        if (Cells[2] != Seed.EMPTY && Cells[2] == Cells[5] && Cells[5] == Cells[8])
            return SideWonTheGame();
        //Diagonal check 
        if (Cells[0] != Seed.EMPTY && Cells[0] == Cells[4] && Cells[4] == Cells[8])
            return SideWonTheGame();
        //Anti diagonal check 
        if (Cells[2] != Seed.EMPTY && Cells[2] == Cells[4] && Cells[4] == Cells[6])
            return SideWonTheGame();
        return 0;
    }
    public int SideWonTheGame()
    {
        if (turn == Seed.NOUGHT)
            return 1;
        else
            return -1;
    }
    public bool NoMoreMoves()
    {
        if (Evaluate() != 0)
            return true;
        for (int i = 0; i < Cells.Length; i++)
        {
            if (Cells[i] == Seed.EMPTY)
                return false;
        } 
        
        return true;
    }


    public int NegateScore(int score)
    {
        if (turn == Seed.NOUGHT)
        {
            return -score;
        }
        else return score;
    }

    public int MiniMax(int atMove)
    {
   
        if (NoMoreMoves())
        {
            return NegateScore(Evaluate()); //From different perspectives
        } 
        int move = GetNextMove(0); //First move 
        int bestValue = -2; //Worst score 
        int bestMove = 0;
        while (move != -1)
        { //We have no more moves to perform 
            MakeMove(move);
            int score = -MiniMax(-1); //Call opponent with -1 since atMove == AtMove will always be false
            if (score > bestValue)
            { //Beaten the worst score - must be beaten 
                bestValue = score;
                bestMove = move;
            }
            UndoMove(move);
            move = GetNextMove(move + 1); //Getting the next move
        } 
        
        if (atMove == AtMove) //Instead of score returning best move which is also int 
            return bestMove;
        return bestValue;
    }
    public int GetNextMove(int last)
    {
        for (int i = last; i < Cells.Length; i++)
        {
            if (Cells[i] == Seed.EMPTY)
                return i;
        }
        return -1;
    }
}
