using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts
{
	public class Munition : MonoBehaviour
	{
		public int Capacity;
		public Element Element;

		public float MinTimeAlive;
		public float MaxTimeAlive;

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
			return Random.Range(MinTimeAlive, MaxTimeAlive);
		}
	}
}
