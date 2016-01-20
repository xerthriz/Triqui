using UnityEngine;
using System.Collections;

public class OnHit : MonoBehaviour {
    public int idx;
    GameScript Script;
    public bool AI;
    public GameObject Camera;

    void Awake()
    {
        //camera = GameObject.FindGameObjectWithTag("MainCamera");
        Script = Camera.GetComponent<GameScript>();
    }

    void Move(){
          if (!Script.NoMoreMoves() && Script.SideWon == 0){
                    Script.SideWon = Script.Evaluate();
                    if (!Script.NoMoreMoves() && Script.SideWon == 0){
                        //CPU
                        Script.SpawnNew(Script.references[Script.MiniMax(Script.AtMove)], Script.MiniMax(Script.AtMove));
                        Script.SideWon = Script.Evaluate();
                    }
               }
    }

    void  OnClick() {
        if (Script.playing){
              Script.SpawnNew(this.gameObject, idx);
              if (AI == true){
                  Move();
              }   
        }
         
     }

}
