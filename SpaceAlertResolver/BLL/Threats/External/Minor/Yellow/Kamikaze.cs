﻿namespace BLL.Threats.External.Minor.Yellow
{
	public class Kamikaze : MinorYellowExternalThreat
	{
		public Kamikaze()
			: base(2, 5, 4)
		{
		}

		protected override void PerformXAction(int currentTurn)
		{
			Speed++;
			Shields = 1;
		}

		protected override void PerformYAction(int currentTurn)
		{
			Speed++;
			Shields = 0;
		}

		protected override void PerformZAction(int currentTurn)
		{
			AttackCurrentZone(6);
		}
	}
}
