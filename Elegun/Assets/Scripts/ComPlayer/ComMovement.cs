using UnityEngine;

namespace Assets.Scripts
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class ComMovement : MonoBehaviour
	{

		public float moveSpeed = 12f;
		public float smoothFactor = 0.8f;

		private Rigidbody2D rb;

		void Start()
		{
			rb = GetComponent<Rigidbody2D>();
		}

		// Update is called once per frame
		void Update()
		{
		}

		// FixedUpdate can run once, zero, or several times per frame,
		// depending on how many physics frames per second are set in the time settings, and how fast/slow the frame rate is.
		void FixedUpdate()
		{
			
		}

		
		#region external control
		// only used for balance testing & prototyping
		public void UpdateSpeed(float newMoveSpeed)
		{
			moveSpeed = newMoveSpeed;
		}
		#endregion
	}
}
