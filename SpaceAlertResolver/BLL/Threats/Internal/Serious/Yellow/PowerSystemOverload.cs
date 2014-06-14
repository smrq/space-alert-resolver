﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.ShipComponents;

namespace BLL.Threats.Internal.Serious.Yellow
{
	public class PowerSystemOverload : SeriousYellowInternalThreat
	{
		private ISet<StationLocation> StationsHitThisTurn { get; set; }

		public PowerSystemOverload()
			: base(
				7,
				3,
				new List<StationLocation> {StationLocation.LowerRed, StationLocation.LowerWhite, StationLocation.LowerBlue},
				PlayerAction.B)
		{
			StationsHitThisTurn = new HashSet<StationLocation>();
		}

		public override void Initialize(ISittingDuck sittingDuck, ThreatController threatController, int timeAppears)
		{
			base.Initialize(sittingDuck, threatController, timeAppears);
			ThreatController.EndOfPlayerActions += PerformEndOfPlayerActions;
		}

		public static string GetDisplayName()
		{
			return "Power System Overload";
		}

		protected override void PerformXAction(int currentTurn)
		{
			SittingDuck.DrainReactors(new[] { ZoneLocation.White }, 2);
		}

		protected override void PerformYAction(int currentTurn)
		{
			SittingDuck.DrainReactors(EnumFactory.All<ZoneLocation>(), 1);
		}

		protected override void PerformZAction(int currentTurn)
		{
			DamageAllZones(3);
		}

		private void PerformEndOfPlayerActions()
		{
			if (CurrentStations.All(station => StationsHitThisTurn.Contains(station)))
				base.TakeDamage(2, null, false, CurrentStation);
			StationsHitThisTurn.Clear();
		}

		public override void TakeDamage(int damage, Player performingPlayer, bool isHeroic, StationLocation stationLocation)
		{
			StationsHitThisTurn.Add(stationLocation);
			base.TakeDamage(damage, performingPlayer, isHeroic, stationLocation);
		}

		protected override void OnHealthReducedToZero()
		{
			ThreatController.EndOfPlayerActions -= PerformEndOfPlayerActions;
			base.OnHealthReducedToZero();
		}

		protected override void OnReachingEndOfTrack()
		{
			ThreatController.EndOfPlayerActions -= PerformEndOfPlayerActions;
			base.OnReachingEndOfTrack();
		}
	}
}
