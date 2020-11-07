using UnityEngine;
using System.Collections;

public class MeteorSpawner : MonoBehaviour
{

	[SerializeField] GameObject[] meteorPrefabs;
	[SerializeField] int meteorsCount;
	[SerializeField] float spawnDelay;

	GameObject[] meteors;

	#region Singleton class: MeteorSpawner

	public static MeteorSpawner Instance;

	void Awake ()
	{
		Instance = this;
	}

	#endregion

	void Start ()
	{
		PrepareMeteors ();
		//запуск корутины спавна
		StartCoroutine (SpawnMeteors ());
	}

	//корутина спавна метеоритов
	IEnumerator SpawnMeteors ()
	{
		for (int i = 0; i < meteorsCount; i++) {
			meteors [i].SetActive (true);
			yield return new WaitForSeconds (spawnDelay);
		}
	}

	void PrepareMeteors ()
	{
		meteors = new GameObject[meteorsCount];
		int prefabsCount = meteorPrefabs.Length;
		for (int i = 0; i < meteorsCount; i++) {
			meteors [i] = Instantiate (meteorPrefabs [Random.Range (0, prefabsCount)], transform);
			meteors [i].GetComponent <Meteor> ().isResultOfFission = false;
			meteors [i].SetActive (false);
		}
	}
}
