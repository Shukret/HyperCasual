using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	bool gameStarted = false;

	void Update () {
	//начинаем игру по щелчку левой кнопкой мыши
		if (!gameStarted && Input.GetMouseButtonDown(0)) {
			FindObjectOfType<Text>().transform.parent.gameObject.SetActive(false);
			Ball.Move = gameStarted = true;
		}
	}
}
