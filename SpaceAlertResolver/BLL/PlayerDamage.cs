﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.ShipComponents;

namespace BLL
{
	public class PlayerDamage
	{
		public int Range { get; private set; }
		public IList<ZoneLocation> ZoneLocations { get; private set; }
		public int Amount { get; private set; }
		public PlayerDamageType PlayerDamageType { get; private set; }
		public bool RequiresTargetingAssistance { get; private set; }

		public PlayerDamage(int amount, PlayerDamageType playerDamageType, int range, IList<ZoneLocation> zoneLocations, bool requiresTargetingAssistance = false)
		{
			Amount = amount;
			PlayerDamageType = playerDamageType;
			Range = range;
			ZoneLocations = zoneLocations;
			RequiresTargetingAssistance = requiresTargetingAssistance;
		}
	}
}
