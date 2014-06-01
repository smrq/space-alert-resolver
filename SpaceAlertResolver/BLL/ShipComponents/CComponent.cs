﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.ShipComponents
{
	public abstract class CComponent
	{
		public abstract void PerformCAction(Player performingPlayer);
	}
}
