using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

namespace Assets.Scripts
{
	public class MunitionSpawner : MonoBehaviour
	{
		[Header("Spawning")]
		public float SpawingRateInSeconds;
		public bool SpawnerActive = true;
		public GameObject MunitionPrefab;
		public int maxSpawnedElements;
		public int InitialSpawnedElements;

		public Transform SpawnPointLeftBottom;
		public Transform SpawnPointTopRight;

		[Header("Colors")]
		public Color[] RandomColors;

		// private and inspector-hidden fields
		[HideInInspector]
		public List<Munition> SpawnedMunitions;


		//for debugging / testing only
		private UiManager _uiManager;

		private void Start()
		{
			_uiManager = FindObjectOfType<UiManager>();

			SpawnedMunitions = new List<Munition>();

			InitialSpawn();

			StartCoroutine(SpawnMunitionTimeBased());

		}

		private void Update()
		{
			// debug
			_uiManager.UpdateSpawnedMunitionCount(SpawnedMunitions.Count(item => item != null));
		}

		//The transform is a quad and this method spawns 
		// the given prefabs at the border of the quad.
		IEnumerator SpawnMunitionTimeBased()
		{
			while (SpawnerActive)
			{
				yield return new WaitForSeconds(SpawingRateInSeconds);

				SpawnedMunitions.RemoveAll(item => item == null);

				if (SpawnedMunitions.Count < maxSpawnedElements)
				{
					SpawnMunition();
				}
			}
		}

		private void SpawnMunition()
		{
			GameObject munitionObject = Instantiate(MunitionPrefab, transform, false);
			Munition munition = munitionObject.GetComponent<Munition>();
			munition.element = RandomizeElement();
			SetElementColor(munitionObject);

			SpawnedMunitions.Add(munition);
			
			munitionObject.transform.localPosition = RandomizeCoordinates();
		}

		private void InitialSpawn()
		{
			for (int i = 0; i < InitialSpawnedElements; i++)
			{
				SpawnMunition();
			}
		}

		//public void SetEnemyColorRandomly(GameObject enemy)
		//{
		//	SpriteRenderer[] renderer = enemy.GetComponentsInChildren<SpriteRenderer>();

		//	SpriteRenderer shape = renderer.FirstOrDefault(x => x.tag == "Shape");
		//	SpriteRenderer glow = renderer.FirstOrDefault(x => x.tag == "Glow");

		//	Color randomColor = RandomColors[Random.Range(0, RandomColors.GetLength(0))];

		//	// random fill color (either one of the color arary or white)
		//	int fillEnemyShape = Random.Range(0, 2);

		//	if (fillEnemyShape == 1)
		//	{
		//		shape.color = randomColor;
		//	}

		//	glow.color = randomColor;
		//}

		// randomizes 
		public Vector2 RandomizeCoordinates()
		{
			float randomX = Random.Range(SpawnPointLeftBottom.position.x, SpawnPointTopRight.position.x);
			float randomY = Random.Range(SpawnPointLeftBottom.position.y, SpawnPointTopRight.position.y);
			return new Vector2(randomX, randomY);
		}

		public string RandomizeElement()
		{
			string result = "";

			int randomValue = Random.Range(1, 4);

			switch (randomValue)
			{
				case 1:
					result = "fire";
					break;
				case 2:
					result = "grass";
					break;
				case 3:
					result = "water";
					break;
			}

			return result;
		}

		public void SetElementColor(GameObject munitionGameObject)
		{
			SpriteShapeRenderer spriteShapeRenderer = munitionGameObject.GetComponentInChildren<SpriteShapeRenderer>();

			Munition munition = munitionGameObject.GetComponent<Munition>();

			switch (munition.element)
			{
				case "fire":
					spriteShapeRenderer.color = Color.red;
					break;
				case "grass":
					spriteShapeRenderer.color = Color.green;
					break;
				case "water":
					spriteShapeRenderer.color = Color.blue;
					break;
			}
		}
	}
}
