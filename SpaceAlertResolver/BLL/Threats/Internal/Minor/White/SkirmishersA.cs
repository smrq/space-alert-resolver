﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.Internal.Minor.White
{
	public class SkirmishersA : Skirmishers
	{
		public SkirmishersA()
			: base(StationLocation.UpperRed)
		{
		}

		public override void PerformXAction(int currentTurn)
		{
			MoveBlue();
		}

		public static string GetDisplayName()
		{
			return "Skirmishers I1-01";
		}
	}
}
