using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

   public  string LevelName;
	public void Load(){
        Application.LoadLevel(LevelName);
    }
}
