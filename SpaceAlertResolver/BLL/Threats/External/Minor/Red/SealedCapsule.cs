﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External.Minor.Red
{
	public class SealedCapsule : MinorRedExternalThreat
	{
		public SealedCapsule()
			: base(4, 4, 4)
		{
		}

		protected override void PerformXAction(int currentTurn)
		{
			Speed++;
			shields = 3;
		}

		protected override void PerformYAction(int currentTurn)
		{
			Speed++;
		}

		protected override void PerformZAction(int currentTurn)
		{
			var extraSpeed = TotalHealth - RemainingHealth < 2;
			//TODO: Calls in internal threat, possibly with an extra speed
			//TODO: Points
			//Killed before z: worth internal threat points
			//Hits Z: worth no points, internal threat worth normal points (0 for leaving on track, reg surviving or killing)
			throw new NotImplementedException();
		}

		public static string GetDisplayName()
		{
			return "Sealed Capsule";
		}
	}
}