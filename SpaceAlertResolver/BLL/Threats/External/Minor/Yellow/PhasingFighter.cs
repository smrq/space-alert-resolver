﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External.Minor.Yellow
{
	public class PhasingFighter : MinorYellowExternalThreat
	{
		private bool isPhased;
		private bool wasPhasedAtStartOfTurn;

		public PhasingFighter(int timeAppears, ZoneLocation currentZone, ISittingDuck sittingDuck)
			: base(2, 4, 3, timeAppears, currentZone, sittingDuck)
		{
		}

		public override void PeformXAction()
		{
			Attack(1);
		}

		public override void PerformYAction()
		{
			Attack(wasPhasedAtStartOfTurn ? 1 : 2);
		}

		public override void PerformZAction()
		{
			Attack(wasPhasedAtStartOfTurn ? 2 : 3);
		}

		public override void BeforeMove()
		{
			base.BeforeMove();
			isPhased = false;
		}

		public override void AfterMove()
		{
			base.AfterMove();
			isPhased = !wasPhasedAtStartOfTurn;
		}

		public override void PerformEndOfTurn()
		{
			wasPhasedAtStartOfTurn = isPhased;
			base.PerformEndOfTurn();
		}

		//TODO: Rules clarification: Can this be hit by the leviathan tanker?

		public override bool CanBeTargetedBy(PlayerDamage damage)
		{
			return !isPhased && base.CanBeTargetedBy(damage);
		}

		public static string GetDisplayName()
		{
			return "Phasing Fighter";
		}
	}
}
