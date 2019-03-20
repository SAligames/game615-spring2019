using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		Debug.Log ("Load level requested for " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest () {
		Debug.Log ("I quit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
	}
}
