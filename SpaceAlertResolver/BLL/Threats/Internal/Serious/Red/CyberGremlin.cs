﻿using System;
using BLL.ShipComponents;
using BLL.Tracks;

namespace BLL.Threats.Internal.Serious.Red
{
	public class CyberGremlin : SeriousRedInternalThreat
	{
		public CyberGremlin()
			: base(1, 2, StationLocation.UpperRed, PlayerActionType.BattleBots, 1)
		{
		}

		public override void PlaceOnBoard(Track track, int? trackPosition)
		{
			base.PlaceOnBoard(track, trackPosition);
			ThreatController.JumpingToHyperspace += OnJumpingToHyperspace;
		}

		private void OnJumpingToHyperspace(object sender, EventArgs args)
		{
			SittingDuck.KnockOutPlayers(EnumFactory.All<StationLocation>());
		}

		protected override void PerformXAction(int currentTurn)
		{
			var energyDrained = SittingDuck.DrainReactor(CurrentZone);
			Speed += energyDrained;
		}

		protected override void PerformYAction(int currentTurn)
		{
			SabotageAllSystems();
			MoveBlue();
		}

		protected override void PerformZAction(int currentTurn)
		{
			SabotageAllSystems();
		}

		private void SabotageAllSystems()
		{
			var newThreats = new[]
			{
				new Sabotage(ThreatType, Difficulty, CurrentStation, PlayerActionType.Alpha),
				new Sabotage(ThreatType, Difficulty, CurrentStation, PlayerActionType.Bravo),
				new Sabotage(ThreatType, Difficulty, CurrentStation, PlayerActionType.Charlie)
			};
			foreach (var newThreat in newThreats)
			{
				newThreat.Initialize(SittingDuck, ThreatController);
				ThreatController.AddInternalTracklessThreat(newThreat);
			}
		}

		protected override void OnHealthReducedToZero()
		{
			OnThreatTerminated();
			ThreatController.JumpingToHyperspace -= OnJumpingToHyperspace;
		}

		public override string Id { get; } = "SI3-106";
		public override string DisplayName { get; } = "Cyber Gremlin";
		public override string FileName { get; } = "CyberGremlin";

		private class Sabotage : InternalThreat
		{
			public Sabotage(ThreatType threatType, ThreatDifficulty threatDifficulty, StationLocation currentStation, PlayerActionType actionType)
				: base(threatType, threatDifficulty, 1, 0, currentStation, actionType)
			{
			}

			protected override void PerformXAction(int currentTurn)
			{
			}

			protected override void PerformYAction(int currentTurn)
			{
			}

			protected override void PerformZAction(int currentTurn)
			{
			}

			public override string Id { get; } = "SI3-106-X";
			public override string DisplayName { get { return null; } }

			public override bool IsTrackless { get { return true; } }

			public override string FileName
			{
				get
				{
					switch (ActionType)
					{
						case PlayerActionType.Alpha:
							return "Alpha";
						case PlayerActionType.Bravo:
							return "Bravo";
						case PlayerActionType.Charlie:
							return "Charlie";
						default:
							throw new InvalidOperationException();
					}
				}
			}

			public override bool ShowOnTrack { get; } = false;

			public override int Points
			{
				get { return 0; }
			}

			public override bool IsDefeated
			{
				get { return false; }
			}

			public override bool IsSurvived
			{
				get { return false; }
			}

			public override bool IsDamageable { get; } = true;

			public override void PlaceOnBoard(Track track, int? trackPosition)
			{
				base.PlaceOnBoard(null, null);
			}
		}
	}
}
