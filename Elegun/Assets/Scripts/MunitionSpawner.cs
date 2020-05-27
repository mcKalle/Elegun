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

		private void Start()
		{
			SpawnedMunitions = new List<Munition>();

			InitialSpawn();

			StartCoroutine(SpawnMunitionTimeBased());

		}

		private void Update()
		{
			// debug
			UiManager.Instance.UpdateSpawnedMunitionCount(SpawnedMunitions.Count(item => item != null));
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
			munition.elementId = RandomizeElement();
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

		public int RandomizeElement()
		{
			return Random.Range(0, 3);
		}

		public void SetElementColor(GameObject munitionGameObject)
		{
			SpriteRenderer spriteRenderer = munitionGameObject.GetComponentsInChildren<SpriteRenderer>().FirstOrDefault(g => g.tag == "MunitionBackground");

			Munition munition = munitionGameObject.GetComponent<Munition>();

			if (spriteRenderer != null)
			{
				spriteRenderer.material = GameManager.Instance.Elements[munition.elementId].ElementMaterial;
			}
		}
	}
}
