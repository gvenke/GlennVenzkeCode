using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectPooling;
using System.Drawing;

namespace GameOfLife.ObjectPooling
{
	public class PenObjectPool : ObjectPool<Pen>
	{
		//private Func<Pen> _initDelegate;
				
		public PenObjectPool(Pen baseObj, int maxItems) : base(baseObj, maxItems)
		{
			//
		}

		//protected override Pen ItemInit() => _initDelegate();
				
		protected override void ItemCleanup(Pen item)
		{
			item.Dispose();
		}
	}
}
