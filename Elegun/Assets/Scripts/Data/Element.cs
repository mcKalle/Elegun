using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
	[Serializable]
	public class Element
	{

		public int ElementId;
		public string Name;
		public Color ElementColor;
		public Material ElementMaterial;

		public Sprite MunitionSprite;
		public Sprite ProjectileSprite;

		public Sprite ShieldSprite;

		public int CounterPartElementId;
	}
}
