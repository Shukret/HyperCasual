using UnityEngine;

public class MeteorFissionable : Meteor
{
	//массив спавнящихся после уничтожения метеоритов
	[SerializeField] GameObject[] splitsPrefabs;

	//уничтожение метеорита
	override protected void Die ()
	{
		SplitMeteor ();

		Destroy (gameObject);
	}

	//функция спавна метеоритов после уничтожения основного метеорита
	void SplitMeteor ()
	{
		GameObject g;
		for (int i = 0; i < 2; i++) {
			g = Instantiate (splitsPrefabs [i], transform.position, Quaternion.identity, MeteorSpawner.Instance.transform);
			g.GetComponent <Rigidbody2D> ().velocity = new Vector2 (leftAndRight [i], 5f);
		}
	}
}
