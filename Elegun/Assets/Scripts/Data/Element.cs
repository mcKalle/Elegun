using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
	[Serializable]
	public class Element
	{

		public int ElementId;
		public string Name;
		public Material ElementColorMaterial;
		public Material ElementBackgroundMaterial;

		public Sprite MunitionSprite;
		public Sprite ProjectileSprite;

		public Sprite ShieldSprite;

		/// <summary>
		/// The element, which IS effective to the current element.
		/// </summary>
		public int InferiorElementId;
		/// <summary>
		/// The element, which is NOT effective to the current element.
		/// </summary>
		public int SuperiorElementId;
	}
}
