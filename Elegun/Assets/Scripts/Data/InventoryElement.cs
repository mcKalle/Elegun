using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Data
{
	public class InventoryElement
	{
		public InventoryElement(Element element, int count)
		{
			Count = count;
			Element = element;	
		}

		public int Count { get; set; }
		public Element Element { get; set; }
	}
}
