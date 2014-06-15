﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.ShipComponents
{
	public class UpperStation : StandardStation
	{
		public virtual Shield Shield { get; set; }
		protected override void RefillEnergy(bool isHeroic)
		{
			Shield.PerformBAction(isHeroic);
		}
	}
}