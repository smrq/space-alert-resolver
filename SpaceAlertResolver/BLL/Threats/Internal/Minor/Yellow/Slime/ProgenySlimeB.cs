﻿using BLL.ShipComponents;

namespace BLL.Threats.Internal.Minor.Yellow.Slime
{
	public class ProgenySlimeB : ProgenySlime
	{
		public ProgenySlimeB(NormalSlime parent, StationLocation stationLocation)
			: base(parent, stationLocation)
		{
		}

		public override string Id { get; } = "I2-02";
		public override string DisplayName { get; } = "Slime";
		public override string FileName { get; } = "SlimeB";
	}
}