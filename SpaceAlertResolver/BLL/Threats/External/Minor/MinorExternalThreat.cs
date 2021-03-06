﻿namespace BLL.Threats.External.Minor
{
    public abstract class MinorExternalThreat : ExternalThreat
    {
        protected MinorExternalThreat(ThreatDifficulty difficulty, int shields, int health, int speed) : 
            base(ThreatType.MinorExternal, difficulty, shields, health, speed)
        {
        }
    }
}
