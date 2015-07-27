﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External.Minor.Red
{
	public class PolarizedFighter : MinorRedExternalThreat
	{
		public PolarizedFighter()
			: base(1, 4, 3)
		{
		}

		protected override void PerformXAction(int currentTurn)
		{
			Attack(1);
		}

		protected override void PerformYAction(int currentTurn)
		{
			Attack(2);
		}

		protected override void PerformZAction(int currentTurn)
		{
			Attack(4);
		}

		public override void TakeDamage(IList<PlayerDamage> damages)
		{
			var laserDamages = damages.Where(damage => damage.PlayerDamageType == PlayerDamageType.LightLaser || damage.PlayerDamageType == PlayerDamageType.HeavyLaser).ToList();
			var otherDamages = damages.Except(laserDamages).ToList();
			var laserDamageSum = (int)Math.Ceiling(laserDamages.Sum(laserDamage => laserDamage.Amount) / 2.0);
			var otherDamageSum = otherDamages.Sum(damage => damage.Amount);
			TakeDamage(laserDamageSum + otherDamageSum, null);
		}

		public static string GetDisplayName()
		{
			return "Polarized Fighter";
		}

		public static string GetId()
		{
			return "E3-108";
		}
	}
}
