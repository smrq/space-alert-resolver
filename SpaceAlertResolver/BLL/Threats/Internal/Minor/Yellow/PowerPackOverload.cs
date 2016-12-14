﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL.ShipComponents;
using BLL.Tracks;

namespace BLL.Threats.Internal.Minor.Yellow
{
	public class PowerPackOverload : MinorYellowInternalThreat
	{
		private ISet<StationLocation> StationsHitThisTurn { get; set; }

		public PowerPackOverload()
			: base(
				4,
				3,
				new List<StationLocation> {StationLocation.LowerBlue, StationLocation.LowerRed},
				PlayerActionType.Alpha)
		{
			StationsHitThisTurn = new HashSet<StationLocation>();
		}

		public override void PlaceOnBoard(Track track, int? trackPosition)
		{
			base.PlaceOnBoard(track, trackPosition);
			ThreatController.PlayerActionsEnding += OnPlayerActionsEnding;
		}
		protected override void PerformXAction(int currentTurn)
		{
			SittingDuck.DisableLowerRedInactiveBattleBots();
			SittingDuck.RemoveRocket();
		}

		protected override void PerformYAction(int currentTurn)
		{
			Repair(1);
		}

		protected override void PerformZAction(int currentTurn)
		{
			SittingDuck.KnockOutPlayers(CurrentStations);
			Damage(3, CurrentZones);
		}

		private void OnPlayerActionsEnding(object sender, EventArgs args)
		{
			if (CurrentStations.All(station => StationsHitThisTurn.Contains(station)))
				base.TakeDamage(1, null, false, CurrentStation);
			StationsHitThisTurn.Clear();
		}

		public override void TakeDamage(int damage, Player performingPlayer, bool isHeroic, StationLocation? stationLocation)
		{
			if (stationLocation != null)
				StationsHitThisTurn.Add(stationLocation.Value);
			base.TakeDamage(damage, performingPlayer, isHeroic, stationLocation);
		}

		protected override void OnThreatTerminated()
		{
			ThreatController.PlayerActionsEnding -= OnPlayerActionsEnding;
			base.OnThreatTerminated();
		}
	}
}
