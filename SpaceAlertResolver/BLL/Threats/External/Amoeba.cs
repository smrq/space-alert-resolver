﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External
{
	public class Amoeba : MinorWhiteExternalThreat
	{
		public Amoeba(int timeAppears, Zone currentZone, SittingDuck sittingDuck)
			: base(0, 8, 2, timeAppears, currentZone, sittingDuck)
		{
		}

		public override void PeformXAction()
		{
			Repair(2);
		}

		public override void PerformYAction()
		{
			Repair(2);
		}

		public override void PerformZAction()
		{
			Attack(5);
		}

		//TODO: Cannot be targeted by rockets
	}
}