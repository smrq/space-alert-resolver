﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.Internal
{
	public class CentralLaserJam : MinorWhiteInternalThreat
	{
		public CentralLaserJam(int timeAppears, SittingDuck sittingDuck)
			: base(2, 2, timeAppears, sittingDuck.WhiteZone.UpperStation, PlayerAction.A, sittingDuck)
		{
		}

		public override void PeformXAction()
		{
			CurrentStation.OppositeDeckStation.EnergyContainer.Energy -= 1;
		}

		public override void PerformYAction()
		{
			Damage(1);
		}

		public override void PerformZAction()
		{
			Damage(3);
			DamageOtherTwoZones(1);
		}

		public override InternalPlayerDamageResult TakeDamage(int damage)
		{
			if (RemainingHealth == 1)
			{
				var reactor = CurrentStation.OppositeDeckStation.EnergyContainer;
				if (reactor.Energy > 1)
				{
					reactor.Energy--;
					return base.TakeDamage(damage);
				}
				return null;
			}
			return base.TakeDamage(damage);
		}
	}
}
