﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External
{
	public class DimensionSpider : SeriousWhiteExternalThreat
	{
		public DimensionSpider(int speed, int timeAppears, ZoneLocation currentZone, ISittingDuck sittingDuck)
			: base(0, 13, speed, timeAppears, currentZone, sittingDuck)
		{
		}

		public override void PeformXAction()
		{
			Shields = 1;
		}

		public override void PerformYAction()
		{
			Shields++;
		}

		public override void PerformZAction()
		{
			AttackAllZones(4);
		}

		public override void OnJumpingToHyperspace()
		{
			PerformZAction();
		}

		public static string GetDisplayName()
		{
			return "Dimension Spider";
		}

		public override bool CanBeTargetedBy(PlayerDamage damage)
		{
			return damage.PlayerDamageType != PlayerDamageType.Rocket && base.CanBeTargetedBy(damage);
		}
	}
}
