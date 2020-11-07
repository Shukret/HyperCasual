using UnityEngine;
using UnityEngine.SceneManagement;

//обязательные компоненты на объекте
[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Ball : MonoBehaviour {

	
	public static bool Move = false;
	[SerializeField] float jumpStrength = 100;
	[SerializeField] float gravityForce = 10;
	[SerializeField] string nextLevel;

	Cylinder cylinder;
	Rigidbody rb;
	float nextBallPosToJump;
	int skippedCounter = 0;
	float vel;

	void Start() {
		//назначение переменных
		rb = GetComponent<Rigidbody>();
		cylinder = FindObjectOfType<Cylinder>();

		nextBallPosToJump = cylinder.firstCirclePos + GetComponent<SphereCollider>().bounds.size.y / 2 + cylinder.circlesHeight;
	}


	void FixedUpdate() {
		if (!Move)
			return;

		//гравитация
		vel -= gravityForce * Time.deltaTime;


		//перемещение
		float overlap = nextBallPosToJump - (transform.position.y + vel);
		if (overlap >= 0) {
			transform.Translate(Vector3.up * (vel + overlap));
			CheckCollision();
		}
		transform.Translate(Vector3.up * vel);
	}

	//функция проверки соприкосновений, использующая Raycast
	void CheckCollision() {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, cylinder.distanceBtwCircles / 2,
			LayerMask.GetMask("Circles"))) {
			if (hit.collider.CompareTag("Good")) {
				if (skippedCounter >= 2) {
					if (hit.collider.transform.parent.CompareTag("Cylinder Object")) {
						Destroy(hit.collider.gameObject);
					}
					else {
						Destroy(hit.collider.transform.parent.gameObject);
					}
				}

				skippedCounter = 0;
				Jump();
			}
			else if (hit.collider.CompareTag("Bad")) {
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}
			else if (hit.collider.CompareTag("Finish")) {
				if (nextLevel != null)
				SceneManager.LoadScene(nextLevel);

			}
		}
		else {
			++skippedCounter;
			nextBallPosToJump -= cylinder.distanceBtwCircles;
		}
	}
	//функция прыжка
	void Jump() {
		vel = jumpStrength;
	}

	//функция остановки движения
	void Stop() {
		Move = false;
		vel = 0;
	}
}
