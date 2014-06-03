﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
	public class ThreatDamage
	{
		public int Amount { get; set; }
		public ThreatDamageType ThreatDamageType { get; private set; }
		public IList<ZoneLocation> ZoneLocations { get; private set; } 

		public ThreatDamage(int amount, ThreatDamageType threatDamageType, IList<ZoneLocation> zoneLocations)
		{
			Amount = amount;
			ThreatDamageType = threatDamageType;
			ZoneLocations = zoneLocations;
		}
	}
}
