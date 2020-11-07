using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Meteor : MonoBehaviour
{

	[SerializeField] protected Rigidbody2D rb;
	[SerializeField] protected int minHP;
	[SerializeField] protected int maxHP;
	[SerializeField] protected int health;

	[SerializeField] protected TextMeshPro textHealth;
	[SerializeField] protected float jumpForce;

	protected float[] leftAndRight = new float[2]{ -1f, 1f };

	public bool isResultOfFission = true;

	protected bool isShowing;

	void Start ()
	{
		//устанавливаем жизни метеорита
		health = Random.Range(minHP, maxHP + 1);
		UpdateHealthUI ();

		isShowing = true;
		rb.gravityScale = 0f;


		if (isResultOfFission) {
			FallDown ();
		} 
		else {
			float direction = leftAndRight [Random.Range (0, 2)];
			float screenOffset = Game.Instance.screenWidth * 1.3f;
			transform.position = new Vector2 (screenOffset * direction, transform.position.y);

			rb.velocity = new Vector2 (-direction, 0f);
			Invoke ("FallDown", Random.Range (screenOffset - 2.5f, screenOffset - 1f));
		}

	}

	//функция падения метеорита вниз на сцену
	void FallDown ()
	{
		isShowing = false;
		rb.gravityScale = 1f;
		rb.AddTorque (Random.Range (-20f, 20f));
	}

	//проверка всех столкновений метеорита
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag.Equals ("cannon")) {//--------------------------------
			//gameover
			Points.start = false;
			SceneManager.LoadScene("level1");
		}

		if (other.tag.Equals ("missile")) {//--------------------------------
			//takedamage
			TakeDamage (1);
			Points.point += 1;
			//destroy missile
			Missiles.Instance.DestroyMissile (other.gameObject);

		}

		if (!isShowing && other.tag.Equals ("wall")) {//-----------------------------------
			//hit wall
			float posX = transform.position.x;
			if (posX > 0) {
				//hit right wall
				rb.AddForce (Vector2.left * 150f);
			} else {
				//hit left wall
				rb.AddForce (Vector2.right * 150f);
			}

			rb.AddTorque (posX * 4f);
		}

		if (other.tag.Equals ("ground")) {//----------------------------------
			
			rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
			rb.AddTorque (-rb.angularVelocity * 4f);
		}
	}

	//получение урона от выстрелов и начисление очков
	public void TakeDamage (int damage)
	{
		if (health > 1) {
			health -= damage;
		} else {
			Die ();
		}
		//отображение изменения жизней
		UpdateHealthUI ();
	}
	


	//уничтожение метеорита
	virtual protected void Die ()
	{
		Destroy (gameObject);
	}

	//функция отображения жизней
	protected void UpdateHealthUI ()
	{
		textHealth.text = health.ToString ();
	}
}
