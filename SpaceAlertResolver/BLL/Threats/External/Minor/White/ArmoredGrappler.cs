﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External.Minor.White
{
	public class ArmoredGrappler : MinorWhiteExternalThreat
	{
		public ArmoredGrappler()
			: base(3, 4, 2)
		{
		}

		public override void PerformXAction(int currentTurn)
		{
			Attack(1);
		}

		public override void PerformYAction(int currentTurn)
		{
			Repair(1);
		}

		public override void PerformZAction(int currentTurn)
		{
			Attack(4);
		}

		public static string GetDisplayName()
		{
			return "Armored Grappler";
		}
	}
}
