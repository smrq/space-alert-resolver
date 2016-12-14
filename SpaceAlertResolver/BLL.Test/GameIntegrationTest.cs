﻿using BLL.ShipComponents;
using BLL.Threats.External;
using BLL.Threats.External.Minor.White;
using BLL.Threats.Internal;
using BLL.Tracks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Threats;
using BLL.Threats.External.Serious.White;
using BLL.Threats.Internal.Minor.White;
using BLL.Threats.Internal.Serious.White;
using BLL.Threats.Internal.Serious.Yellow;

namespace BLL.Test
{
	[TestClass]
	public class GameIntegrationTest
	{
		[TestMethod]
		public void JustAFighterNoActions()
		{
			var players = Enumerable.Range(0, 1).Select(index => new Player (new List<PlayerAction>())).ToList();

			var externalTracksByZone = new Dictionary<ZoneLocation, TrackConfiguration>
			{
				{ZoneLocation.Blue, TrackConfiguration.Track1},
				{ZoneLocation.Red, TrackConfiguration.Track5},
				{ZoneLocation.White, TrackConfiguration.Track3}
			};
			var internalTrack = TrackConfiguration.Track4;

			var fighter = new Fighter { TimeAppears = 5, CurrentZone = ZoneLocation.Red };
			var externalThreats = new ExternalThreat[] { fighter };

			var internalThreats = new InternalThreat[0];
			var bonusThreats = new Threat[0];

			var game = new Game(players, internalThreats, externalThreats, bonusThreats, externalTracksByZone, internalTrack);

			for (var currentTurn = 0; currentTurn < game.NumberOfTurns; currentTurn++)
				game.PerformTurn();

			Assert.IsFalse(game.HasLost);
			Assert.AreEqual(0, game.SittingDuck.BlueZone.TotalDamage);
			Assert.AreEqual(5, game.SittingDuck.RedZone.TotalDamage);
			Assert.AreEqual(0, game.SittingDuck.WhiteZone.TotalDamage);
			Assert.AreEqual(0, game.ThreatController.DefeatedThreats.Count());
			Assert.AreEqual(1, game.ThreatController.SurvivedThreats.Count());
			Assert.AreEqual(2, game.TotalPoints);
			Assert.AreEqual(5, game.SittingDuck.Zones.ElementAt(0).AllDamageTokensTaken.Count());
			Assert.AreEqual(0, game.SittingDuck.Zones.ElementAt(1).AllDamageTokensTaken.Count());
			Assert.AreEqual(0, game.SittingDuck.Zones.ElementAt(2).AllDamageTokensTaken.Count());

			foreach (var zone in game.SittingDuck.Zones)
			{
				foreach (var token in zone.AllDamageTokensTaken)
					Console.WriteLine("{0} damage token taken in zone {1}. Still damaged: {2}", token, zone.ZoneLocation, zone.CurrentDamageTokens.Contains(token));
			}
		}

		[TestMethod]
		public void SixBasicThreats()
		{
			var players = GetPlayers();

			var externalTracksByZone = new Dictionary<ZoneLocation, TrackConfiguration>
			{
				{ZoneLocation.Blue, TrackConfiguration.Track1},
				{ZoneLocation.Red, TrackConfiguration.Track2},
				{ZoneLocation.White, TrackConfiguration.Track3},
			};
			var internalTrack = TrackConfiguration.Track4;

			var destroyer = new Destroyer { TimeAppears = 4, CurrentZone = ZoneLocation.Blue };
			var fighter1 = new Fighter { TimeAppears = 5, CurrentZone = ZoneLocation.Red };
			var fighter2 = new Fighter { TimeAppears = 6, CurrentZone = ZoneLocation.White };
			var externalThreats = new ExternalThreat[] { destroyer, fighter1, fighter2 };

			var skirmishers = new SkirmishersA { TimeAppears = 4 };
			var fissure = new Fissure { TimeAppears = 2 };
			var nuclearDevice = new NuclearDevice { TimeAppears = 6 };
			var internalThreats = new InternalThreat[] { skirmishers, fissure, nuclearDevice };

			var bonusThreats = new Threat[0];

			var game = new Game(players, internalThreats, externalThreats, bonusThreats, externalTracksByZone, internalTrack);

			for (var currentTurn = 0; currentTurn < game.NumberOfTurns; currentTurn++)
				game.PerformTurn();

			Assert.IsFalse(game.HasLost);
			Assert.AreEqual(4, game.SittingDuck.BlueZone.TotalDamage);
			Assert.AreEqual(3, game.SittingDuck.RedZone.TotalDamage);
			Assert.AreEqual(3, game.SittingDuck.WhiteZone.TotalDamage);
			Assert.AreEqual(3, game.ThreatController.DefeatedThreats.Count());
			Assert.AreEqual(3, game.ThreatController.SurvivedThreats.Count());
			Assert.AreEqual(30, game.TotalPoints);
			Assert.AreEqual(3, game.SittingDuck.Zones.ElementAt(0).AllDamageTokensTaken.Count());
			Assert.AreEqual(3, game.SittingDuck.Zones.ElementAt(1).AllDamageTokensTaken.Count());
			Assert.AreEqual(4, game.SittingDuck.Zones.ElementAt(2).AllDamageTokensTaken.Count());
		}

		private static IList<Player> GetPlayers()
		{
			var players = new List<Player>
			{
				new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
				{
					null,
					PlayerActionType.ChangeDeck,
					PlayerActionType.Bravo,
					PlayerActionType.ChangeDeck,
					PlayerActionType.Alpha,
					PlayerActionType.Alpha,
					PlayerActionType.Alpha,
					PlayerActionType.Alpha,
					PlayerActionType.Alpha,
					PlayerActionType.Alpha
				}))
			};
			players.Add(new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
			{
				PlayerActionType.MoveRed,
				PlayerActionType.ChangeDeck,
				PlayerActionType.Charlie,
				PlayerActionType.ChangeDeck,
				null,
				PlayerActionType.Charlie,
				PlayerActionType.BattleBots,
				PlayerActionType.Charlie,
				PlayerActionType.BattleBots,
				PlayerActionType.Alpha
			})));
			players.Add(new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
			{
				null,
				PlayerActionType.Charlie,
				null,
				null,
				PlayerActionType.Charlie,
				PlayerActionType.ChangeDeck,
				PlayerActionType.Charlie
			})));
			players.Add(new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
			{
				PlayerActionType.ChangeDeck,
				null,
				null,
				null,
				null,
				null,
				PlayerActionType.Charlie
			})));
			players.Add(new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
			{
				null,
				PlayerActionType.ChangeDeck,
				null,
				null,
				null,
				null,
				PlayerActionType.Charlie
			})));
			players.Add(new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
			{
				PlayerActionType.TeleportBlueLower,
				PlayerActionType.TeleportRedUpper,
				PlayerActionType.TeleportWhiteLower,
				PlayerActionType.TeleportWhiteUpper
			})));
			for (var i = 0; i < players.Count; i++)
				players[i].Index = i;
			return players;
		}

		[TestMethod]
		public void EzraCampaign1Mission1()
		{
			var players = new List<Player>();
			players.Add(new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
			{
				PlayerActionType.MoveRed,
				null,
				PlayerActionType.BasicSpecialization,
				PlayerActionType.Alpha, 
				PlayerActionType.Alpha, 
				PlayerActionType.Alpha,
				PlayerActionType.MoveBlue, 
				PlayerActionType.MoveBlue,
				PlayerActionType.Alpha, 
				PlayerActionType.Alpha, 
				PlayerActionType.Alpha,
				null
			}))
			{
				BasicSpecialization = PlayerSpecialization.EnergyTechnician
			});

			players.Add(new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
			{
				PlayerActionType.MoveRed,
				PlayerActionType.ChangeDeck, 
				PlayerActionType.Alpha,
				PlayerActionType.Alpha,
				PlayerActionType.Bravo,
				PlayerActionType.MoveBlue,
				PlayerActionType.MoveBlue,
				null,
				PlayerActionType.Alpha,
				PlayerActionType.Bravo,
				PlayerActionType.Alpha,
				null
			})));

			players.Add(new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
			{
				null,
				null,
				null,
				PlayerActionType.MoveBlue, 
				PlayerActionType.Charlie, 
				PlayerActionType.ChangeDeck, 
				PlayerActionType.BattleBots, 
				PlayerActionType.BattleBots, 
				null,
				PlayerActionType.MoveRed, 
				null,
				PlayerActionType.Charlie
			})));

			players.Add(new Player(PlayerActionFactory.CreateSingleActionList(null, null, new PlayerActionType?[]
			{
				PlayerActionType.Charlie,
				null,
				null,
				PlayerActionType.BasicSpecialization,
				null,
				PlayerActionType.ChangeDeck,
				null,
				PlayerActionType.BasicSpecialization,
				null,
				null,
				null,
				PlayerActionType.Charlie
			}))
			{
				BasicSpecialization = PlayerSpecialization.DataAnalyst
			});

			for (var i = 0; i < players.Count; i++)
				players[i].Index = i;

			var externalTracksByZone = new Dictionary<ZoneLocation, TrackConfiguration>
			{
				{ZoneLocation.Blue, TrackConfiguration.Track3},
				{ZoneLocation.Red, TrackConfiguration.Track2},
				{ZoneLocation.White, TrackConfiguration.Track7},
			};
			var internalTrack = TrackConfiguration.Track4;

			var dimensionSpider = new DimensionSpider { TimeAppears = 6, CurrentZone = ZoneLocation.Blue };
			var asteroid = new Asteroid { TimeAppears = 4, CurrentZone = ZoneLocation.Red };
			var cryoshieldFighter = new CryoshieldFighter { TimeAppears = 1, CurrentZone = ZoneLocation.Red };
			var externalThreats = new ExternalThreat[] { dimensionSpider, asteroid, cryoshieldFighter };

			var shambler = new Shambler { TimeAppears = 5 };
			var internalThreats = new InternalThreat[] { shambler };

			var bonusThreats = new Threat[0];

			var game = new Game(players, internalThreats, externalThreats, bonusThreats, externalTracksByZone, internalTrack);

			for (var currentTurn = 0; currentTurn < game.NumberOfTurns; currentTurn++)
				game.PerformTurn();
			Assert.IsFalse(game.HasLost);
			Assert.AreEqual(0, game.SittingDuck.BlueZone.TotalDamage);
			Assert.AreEqual(1, game.SittingDuck.RedZone.TotalDamage);
			Assert.AreEqual(0, game.SittingDuck.WhiteZone.TotalDamage);
			Assert.AreEqual(3, game.ThreatController.DefeatedThreats.Count());
			Assert.AreEqual(1, game.ThreatController.SurvivedThreats.Count());
			Assert.AreEqual(20 + 4, game.TotalPoints);
			Assert.AreEqual(1, game.SittingDuck.Zones.ElementAt(0).AllDamageTokensTaken.Count());
			Assert.AreEqual(0, game.SittingDuck.Zones.ElementAt(1).AllDamageTokensTaken.Count());
			Assert.AreEqual(0, game.SittingDuck.Zones.ElementAt(2).AllDamageTokensTaken.Count());
		}
	}
}
