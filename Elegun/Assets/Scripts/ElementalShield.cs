using UnityEngine;

namespace Assets.Scripts
{
	public class ElementalShield : MonoBehaviour
	{
		public float ElementalShieldMoveSpeed = 3f;


		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			transform.Rotate(new Vector3(0, 0, 1), ElementalShieldMoveSpeed);
		}

		#region external control
		// only used for balance testing & prototyping
		public void UpdateSpeed(float newRotationSpeed)
		{
			ElementalShieldMoveSpeed = newRotationSpeed;
		}
		#endregion
	}
}
