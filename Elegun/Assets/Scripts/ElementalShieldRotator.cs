using Assets.Scripts.Global;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class ElementalShieldRotator : MonoBehaviour
	{
		public GameObject ShieldPrefab;
	
		public float ShieldDistanceFromCenter = 1.2f;
		public float ElementalShieldMoveSpeed = 1.9f;

		[Range(3, 12) ] 
		public int ShieldCount = 6;

		// Start is called before the first frame update
		void Start()
		{
			PlaceShieldObjects();
		}

		// Update is called once per frame
		void Update()
		{
			transform.Rotate(new Vector3(0, 0, 1), ElementalShieldMoveSpeed);
		}

		void PlaceShieldObjects()
		{
			for (int i = 0; i < ShieldCount; i++)
			{
				float angle = i * Mathf.PI * 2 / ShieldCount;
				float x = Mathf.Cos(angle) * ShieldDistanceFromCenter;
				float y = Mathf.Sin(angle) * ShieldDistanceFromCenter;
				Vector3 pos = transform.position + new Vector3(x, y, 0);
				float angleDegrees = -angle * Mathf.Rad2Deg;

				GameObject shieldGameObject = Instantiate(ShieldPrefab, pos, Quaternion.identity, transform);

				// distribute shield elements evenly
				SpriteRenderer spriteRenderer = shieldGameObject.GetComponentsInChildren<SpriteRenderer>().FirstOrDefault(g => g.tag == "BackgroundShader");
				
				if (spriteRenderer != null)
				{
					int elementIndex = i % 3;

					spriteRenderer.material =
						GameManager.Instance.Elements[elementIndex].ElementMaterial;
				}
			}
		}

		#region collision detection
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.tag == Constants.Tags.PROJECTILE)
			{

			}
		}
		#endregion

		#region external control
		// only used for balance testing & prototyping
		public void UpdateSpeed(float newRotationSpeed)
		{
			ElementalShieldMoveSpeed = newRotationSpeed;
		}
		#endregion
	}
}
