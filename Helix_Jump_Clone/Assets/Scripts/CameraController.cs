using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	Transform _player;
	Vector3 initialPos;
	float deltaY;

	void Start() {
		//назначение переменной игрока
		_player = GameObject.FindGameObjectWithTag("Player").transform;

		//находим позицию игрока и вычисляем разницу в высоте между игроком и камерой
		initialPos = this.transform.position;
		deltaY = _player.position.y - initialPos.y;
	}

	void LateUpdate() {
		//позиция игрока по y
		float playerY = _player.position.y;

		//двигаем камеру на разницу по высоте deltaY
		if (transform.position.y + deltaY > playerY) {
			initialPos.y = playerY - deltaY;
			transform.position = initialPos;
		}
	}
}
