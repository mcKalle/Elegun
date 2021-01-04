using UnityEngine;

namespace Assets.Scripts
{
	public class Munition : MonoBehaviour
	{
		public int capacity;
		public int elementId;

		public float minTimeAlive;
		public float maxTimeAlive;

		public SpriteRenderer minimapRenderer;

		void Start()
		{
			// destroy with the given time the munition element is kept alive
			Destroy(gameObject, GetRandomTimeAlive());
		}

		// Update is called once per frame
		void Update()
		{
        
		}

		private float GetRandomTimeAlive()
		{
			return Random.Range(minTimeAlive, maxTimeAlive);
		}
	}
}
