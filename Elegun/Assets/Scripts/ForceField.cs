using Assets.Scripts;
using Assets.Scripts.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
	[HideInInspector]
	public string PlayerId { get; set; }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag(Constants.Tags.PROJECTILE))
		{
			var projectile = col.gameObject.GetComponent<Projectile>();

			if (projectile != null)
			{
				// ignore the collision, if the projectile is coming from the same player
				if (projectile.PlayerId != PlayerId)
				{
					Munition munitionToBeAbsorbed = new Munition();
					munitionToBeAbsorbed.Element = projectile.Element;
					munitionToBeAbsorbed.Capacity = 1;

					MunitionAbsorbed?.Invoke(this, new MunitionAbsorbedEventArgs(munitionToBeAbsorbed));

					Destroy(col.gameObject);
				}
			}
		}
	}

	#region Events
	public event EventHandler<MunitionAbsorbedEventArgs> MunitionAbsorbed;

	public class MunitionAbsorbedEventArgs : EventArgs
	{
		public MunitionAbsorbedEventArgs(Munition munition)
		{
			Munition = munition;
		}

		public Munition Munition { get; set; }
	}
	#endregion
}
