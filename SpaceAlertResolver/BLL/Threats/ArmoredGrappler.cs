﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats
{
	public class ArmoredGrappler : MinorExternalThreat
	{
		public ArmoredGrappler(int timeAppears, Zone currentZone)
			: base(2, 4, 3, 4, 2, timeAppears, currentZone)
		{
		}

		public override void PeformXAction(SittingDuck sittingDuck)
		{
			sittingDuck.TakeDamage(1, CurrentZone);
		}

		public override void PerformYAction(SittingDuck sittingDuck)
		{
			if (RemainingHealth < TotalHealth)
				remainingHealth++;
		}

		public override void PerformZAction(SittingDuck sittingDuck)
		{
			sittingDuck.TakeDamage(4, CurrentZone);
		}
	}
}
