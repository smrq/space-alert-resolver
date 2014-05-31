﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.External
{
	public abstract class SeriousExternalThreat : ExternalThreat
	{
		protected SeriousExternalThreat(int pointsForSurviving, int pointsForDefeating, int shields, int health, int speed, int timeAppears, Zone currentZone)
			: base(pointsForSurviving, pointsForDefeating, shields, health, speed, timeAppears, currentZone)
		{
			threatType = ThreatType.SeriousExternal;
		}
	}
}