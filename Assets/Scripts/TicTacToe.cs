using UnityEngine;
using System.Collections;

public class TicTacToe : MonoBehaviour {

    public int SideWon = 0; 
   // public char SideToPlay = 'O';
   // public char[] Grid;
    private int AtMove = 0;

    public UILabel Instructions;
    public enum Seed { EMPTY, CROSS, NOUGHT };
    public Seed[] Cells;
    public Seed turn, player1, player2;
    System.Random rnd = new System.Random();
	void Start() { 
      /*  Grid = new char[9]; 
        for (int i = 0; i < 9; i++) 
            Grid[i] = ' ';*/


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
	
	void SwitchTurn() { 
       // SideToPlay = SideToPlay == 'O' ? 'X' : 'O'; 
        turn = turn == Seed.NOUGHT ? Seed.CROSS : Seed.NOUGHT; 
    }

    void MakeMove(int move) { 
      //  Grid[move] = SideToPlay;
        Cells[move] = turn;
        SwitchTurn(); 
        AtMove++; 
    }

    void UndoMove(int move) { 
       // Grid[move] = ' ';
        Cells[move] = Seed.EMPTY; 
        SwitchTurn(); 
        AtMove--; 
    }

    int Evaluate() { //Horizontal check 
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

     int SideWonTheGame() {
      /*  if (SideToPlay == 'O')
            return 1;
        else
            return -1;*/

        if (turn == Seed.NOUGHT)
            return 1;
        else
            return -1;
    }
    bool NoMoreMoves() { 
        if (Evaluate() != 0)
            return true;
        for (int i = 0; i < Cells.Length; i++)
        {
            if (Cells[i] == Seed.EMPTY) 
                return false; 
        } return true; 
    } 
    void OnGUI() { 
        for (int x = 0; x < 3; x++) { 
            for (int y = 0; y < 3; y++) 
            {
                if (GUI.Button(new Rect(x * 70, y * 70, 70, 70), Cells[x + y * 3].ToString()) && !NoMoreMoves() && SideWon == 0 && Cells[x + y * 3] == Seed.EMPTY)
                { 
                    MakeMove(x + y * 3);
                    SideWon = Evaluate(); 
                    if (!NoMoreMoves() && SideWon == 0) { 
                        //Computer to play here 
                        MakeMove(MiniMax(AtMove));
                        SideWon = Evaluate(); 
                    } 
                }
            }
        } 
    } 

    int NegateScore(int score) { 
      /*  if (SideToPlay == 'O') {
            return -score; 
        } else return score;
        */
        if (turn == Seed.NOUGHT)
        {
            return -score;
        }
        else  return score; 
    } 
    
    int MiniMax(int atMove) { 
        if (NoMoreMoves()) { 
            return NegateScore(Evaluate()); //From different perspectives
        } 
        int move = GetNextMove(0); //First move 
        int bestValue = -2; //Worst score 
        int bestMove = 0;
        while (move != -1) { //We have no more moves to perform 
            MakeMove(move); 
            int score = -MiniMax(-1); //Call opponent with -1 since atMove == AtMove will always be false
            if (score > bestValue) { //Beaten the worst score - must be beaten 
                bestValue = score; 
                bestMove = move; 
            } 
            UndoMove(move);
            move = GetNextMove(move+1); //Getting the next move
        } if (atMove == AtMove) //Instead of score returning best move which is also int 
            return bestMove; 
        return bestValue; 
    } 
    int GetNextMove(int last) {
        for (int i = last; i < Cells.Length; i++)
        {
            if (Cells[i]== Seed.EMPTY)
                return i; 
        } 
        return -1; 
    }
}
