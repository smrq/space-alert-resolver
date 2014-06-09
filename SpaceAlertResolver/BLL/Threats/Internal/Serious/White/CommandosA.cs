﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.Internal.Serious.White
{
	public class CommandosA : Commandos
	{
		public CommandosA()
			: base(StationLocation.LowerRed)
		{
		}

		public override void PerformYAction(int currentTurn)
		{
			if (IsDamaged)
				MoveBlue();
			else
				Damage(2);
		}

		public static string GetDisplayName()
		{
			return "Commandos SI1-01";
		}
	}
}
