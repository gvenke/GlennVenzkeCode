using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectPooling;
using System.Drawing;

namespace GameOfLife.ObjectPooling
{
	public class GraphicsObjectPool : ObjectPool<Graphics>
	{

		public GraphicsObjectPool(Graphics baseObj, int maxItems) : base(baseObj, maxItems)
		{
			//
		}

		//protected override Graphics ItemInit() => _initDelegate();

		protected override void ItemCleanup(Graphics item)
		{
			item.Dispose();
		}
	}
}
