﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External
{
	public class SpacecraftCarrier : SeriousWhiteExternalThreat
	{
		public SpacecraftCarrier(int timeAppears, ZoneLocation currentZone, SittingDuck sittingDuck)
			: base(3, 6, 2, timeAppears, currentZone, sittingDuck)
		{
		}

		public override void PeformXAction()
		{
			Attack(2, ThreatDamageType.ReducedByTwoAgainstInterceptors);
		}

		public override void PerformYAction()
		{
			AttackOtherTwoZones(3, ThreatDamageType.ReducedByTwoAgainstInterceptors);
		}

		public override void PerformZAction()
		{
			AttackAllZones(4, ThreatDamageType.ReducedByTwoAgainstInterceptors);
		}

		public static string GetDisplayName()
		{
			return "Spacecraft Carrier";
		}
	}
}
