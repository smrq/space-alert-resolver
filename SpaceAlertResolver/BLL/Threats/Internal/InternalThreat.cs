﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Threats.Internal
{
	public abstract class InternalThreat : Threat
	{
		public IList<StationLocation> CurrentStations { get; private set; }

		protected StationLocation CurrentStation
		{
			get { return CurrentStations.Single(); }
			set { CurrentStations = new[] {value}; }
		}

		//TODO: Set track, to allow to know position and add threats

		protected ZoneLocation CurrentZone
		{
			get { return CurrentStation.ZoneLocation(); }
		}

		protected IList<ZoneLocation> CurrentZones
		{
			get { return CurrentStations.Select(station => station.ZoneLocation()).ToList(); }
		}

		public PlayerAction ActionType { get; protected set; }

		protected InternalThreat(ThreatType type, ThreatDifficulty difficulty, int health, int speed, int timeAppears, StationLocation currentStation, PlayerAction actionType, ISittingDuck sittingDuck) :
			base(type, difficulty, health, speed, timeAppears, sittingDuck)
		{
			CurrentStation = currentStation;
			sittingDuck.StationByLocation[currentStation].Threats.Add(this);
			ActionType = actionType;
		}

		protected InternalThreat(ThreatType type, ThreatDifficulty difficulty, int health, int speed, int timeAppears, IList<StationLocation> currentStations, PlayerAction actionType, ISittingDuck sittingDuck) :
			base(type, difficulty, health, speed, timeAppears, sittingDuck)
		{
			CurrentStations = currentStations;
			foreach (var currentStation in CurrentStations)
				sittingDuck.StationByLocation[currentStation].Threats.Add(this);
			ActionType = actionType;
		}

		//TODO: Respect isHeroic here instead of in the ship
		public virtual void TakeDamage(int damage, Player performingPlayer, bool isHeroic)
		{
			RemainingHealth -= damage;
			CheckForDestroyed();
		}

		private void MoveToNewStation(StationLocation newStation)
		{
			if (CurrentStations.Count != 1)
				throw new InvalidOperationException("Cannot move a threat that exists in more than 1 zone.");
			sittingDuck.StationByLocation[CurrentStation].Threats.Remove(this);
			CurrentStation = newStation;
			sittingDuck.StationByLocation[CurrentStation].Threats.Add(this);
		}

		protected void MoveRed()
		{
			MoveToNewStation(sittingDuck.StationByLocation[CurrentStation].OppositeDeckStation.StationLocation);
		}

		protected void MoveBlue()
		{
			MoveToNewStation(sittingDuck.StationByLocation[CurrentStation].BluewardStation.StationLocation);
		}

		protected void ChangeDecks()
		{
			MoveToNewStation(sittingDuck.StationByLocation[CurrentStation].OppositeDeckStation.StationLocation);
		}

		protected void Damage(int amount)
		{
			Damage(amount, new [] {CurrentZone});
		}

		protected void DamageOtherTwoZones(int amount)
		{
			Damage(amount, EnumFactory.All<ZoneLocation>().Except(new[] { CurrentZone }).ToList());
		}

		protected void Damage(int amount, IList<ZoneLocation> zones)
		{
			var result = sittingDuck.TakeAttack(new ThreatDamage(amount, ThreatDamageType.Internal, zones));
			if (result.ShipDestroyed)
				throw new LoseException(this);
		}

		public virtual void PerformEndOfPlayerActions()
		{
		}

		public IrreparableMalfunction GetIrreparableMalfunction()
		{
			if (ActionType == PlayerAction.BattleBots)
				return null;
			return new IrreparableMalfunction
			{
				ActionType = ActionType
			};
		}
	}
}
