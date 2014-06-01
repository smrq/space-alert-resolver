﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.Internal
{
	public abstract class Saboteur : MinorWhiteInternalThreat
	{
		protected Saboteur(int timeAppears, SittingDuck sittingDuck)
			: base(1, 4, timeAppears, sittingDuck.WhiteZone.LowerStation, PlayerAction.BattleBots, sittingDuck)
		{
		}

		public override void PerformYAction()
		{
			var currentReactor = CurrentStation.EnergyContainer;
			var reactorHasEnergy = currentReactor.Energy > 1;
			if (reactorHasEnergy)
				currentReactor.Energy--;
			else
				Damage(1);
		}

		public override void PerformZAction()
		{
			Damage(2);
		}
	}
}
