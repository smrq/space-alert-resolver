﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External.Serious.White
{
	public class SpacecraftCarrier : SeriousWhiteExternalThreat
	{
		public SpacecraftCarrier()
			: base(3, 6, 2)
		{
		}

		protected override void PerformXAction(int currentTurn)
		{
			Attack(2, ThreatDamageType.ReducedByTwoAgainstInterceptors);
		}

		protected override void PerformYAction(int currentTurn)
		{
			AttackOtherTwoZones(3, ThreatDamageType.ReducedByTwoAgainstInterceptors);
		}

		protected override void PerformZAction(int currentTurn)
		{
			AttackAllZones(4, ThreatDamageType.ReducedByTwoAgainstInterceptors);
		}

		public static string GetDisplayName()
		{
			return "Spacecraft Carrier";
		}

		public static string GetId()
		{
			return "SE1-101";
		}
	}
}
