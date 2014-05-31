﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External
{
	public class SpinningSaucer : MinorWhiteExternalThreat
	{
		private bool hitByRocket;

		public SpinningSaucer(int timeAppears, Zone currentZone, SittingDuck sittingDuck)
			: base(4, 3, 3, timeAppears, currentZone, sittingDuck)
		{
		}

		public override void PeformXAction()
		{
			Attack(1);
		}

		public override void PerformYAction()
		{
			Attack(1);
		}

		public override void PerformZAction()
		{
			if (!hitByRocket)
				Attack(5);
		}

		public override void TakeDamage(IList<PlayerDamage> damages)
		{
			if (damages.Any(damage => damage.DamageType == DamageType.Rocket))
				hitByRocket = true;
			base.TakeDamage(damages);
		}
	}
}
