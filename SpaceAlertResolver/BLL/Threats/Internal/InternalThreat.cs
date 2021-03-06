﻿using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Players;
using BLL.ShipComponents;
using BLL.Tracks;

namespace BLL.Threats.Internal
{
    public abstract class InternalThreat : Threat
    {
        public InternalThreat Parent { get; protected set; }

        public IList<StationLocation> CurrentStations { get; private set; }
        public virtual IList<StationLocation> DisplayOnTrackStations => CurrentStations.Concat(WarningIndicatorStations).ToList();
        public virtual IList<StationLocation> DisplayOnShipStations => CurrentStations;
        public IList<StationLocation> WarningIndicatorStations { get; } = new List<StationLocation>();

        public void SetInitialPlacement(int timeAppears)
        {
            TimeAppears = timeAppears;
        }

        public override ThreatDamageType StandardDamageType { get; } = ThreatDamageType.IgnoresShields;
        public override int? DamageDistanceToSource => null;
        public override void PlaceOnTrack(Track track, int trackPosition)
        {
            base.PlaceOnTrack(track, trackPosition);
            SetThreatStatus(ThreatStatus.OnShip, true);
        }

        public virtual bool IsDamageable => IsOnShip;

        public bool IsOnShip => GetThreatStatus(ThreatStatus.OnShip);

        public int? TotalInaccessibility {get; protected set; }
        private int? RemainingInaccessibility { get; set; }

        internal StationLocation CurrentStation
        {
            get { return CurrentStations.Single(); }
            set { CurrentStations = new List<StationLocation>{value}; }
        }

        public override ZoneLocation CurrentZone => CurrentStation.ZoneLocation();

        protected IList<ZoneLocation> CurrentZones
        {
            get { return CurrentStations.Select(station => station.ZoneLocation()).ToList(); }
        }

        protected PlayerActionType? ActionType { get; }

        protected InternalThreat(ThreatType threatType, ThreatDifficulty difficulty, int health, int speed, IList<StationLocation> currentStations, PlayerActionType? actionType) :
            base(threatType, difficulty, health, speed)
        {
            ActionType = actionType;
            CurrentStations = currentStations;
        }

        protected InternalThreat(ThreatType threatType, ThreatDifficulty difficulty, int health, int speed, StationLocation currentStation, PlayerActionType? actionType) :
            this(threatType, difficulty, health, speed, new List<StationLocation> {currentStation}, actionType)
        {
        }

        protected InternalThreat(ThreatType threatType, ThreatDifficulty difficulty, int health, int speed, StationLocation currentStation, PlayerActionType? actionType, int? inaccessibility) :
            this(threatType, difficulty, health, speed, new List<StationLocation> {currentStation}, actionType)
        {
            TotalInaccessibility = RemainingInaccessibility = inaccessibility;
        }

        public virtual void TakeDamage(int damage, Player performingPlayer, bool isHeroic, StationLocation? stationLocation)
        {
            var damageRemaining = damage;
            if (RemainingInaccessibility.HasValue)
            {
                var previousInaccessibility = RemainingInaccessibility.Value;
                RemainingInaccessibility -= damageRemaining;
                RemainingInaccessibility = RemainingInaccessibility < 0 ? 0 : RemainingInaccessibility;
                damageRemaining -= previousInaccessibility;
            }
            if (damageRemaining > 0)
                RemainingHealth -= damage;
            CheckDefeated();
        }

        private void MoveToNewStation(StationLocation? newStation)
        {
            if (CurrentStations.Count != 1)
                throw new InvalidOperationException("Cannot move a threat that exists in more than 1 zone.");
            if (!newStation.HasValue)
                throw new InvalidOperationException("Moved wrongly.");
            CurrentStations.Remove(CurrentStation);
            CurrentStations.Add(newStation.Value);
        }

        internal void MoveRed()
        {
            if (CanMoveRed())
                MoveToNewStation(CurrentStation.RedwardStationLocation());
        }

        private bool CanMoveRed()
        {
            switch (CurrentZone)
            {
                case ZoneLocation.Red:
                    return false;
                case ZoneLocation.White:
                    return !SittingDuck.RedDoorIsSealed;
                case ZoneLocation.Blue:
                    return !SittingDuck.BlueDoorIsSealed;
                default:
                    throw new InvalidOperationException("Invalid zone location encountered");
            }
        }

        internal void MoveBlue()
        {
            if (CanMoveBlue())
                MoveToNewStation(CurrentStation.BluewardStationLocation());
        }

        private bool CanMoveBlue()
        {
            switch (CurrentZone)
            {
                case ZoneLocation.Blue:
                    return false;
                case ZoneLocation.White:
                    return !SittingDuck.BlueDoorIsSealed;
                case ZoneLocation.Red:
                    return !SittingDuck.RedDoorIsSealed;
                default:
                    throw new InvalidOperationException("Invalid zone location encountered");
            }
        }

        internal void ChangeDecks()
        {
            MoveToNewStation(CurrentStation.OppositeStationLocation());
        }

        protected override void OnReachingEndOfTrack()
        {
            base.OnReachingEndOfTrack();
            var malfunctionTypes = new[]
            {
                PlayerActionType.Alpha,
                PlayerActionType.Bravo,
                PlayerActionType.Charlie
            };
            if (ActionType != null && malfunctionTypes.Contains(ActionType.Value))
                SittingDuck.AddIrreparableMalfunctionToStations(
                    CurrentStations,
                    new IrreparableMalfunction { ActionType = ActionType.Value });
        }

        protected override void OnThreatTerminated()
        {
            base.OnThreatTerminated();
            SetThreatStatus(ThreatStatus.OnShip, false);
        }

        protected override void OnTurnEnded(object sender, EventArgs args)
        {
            base.OnTurnEnded(sender, args);
            RemainingInaccessibility = TotalInaccessibility;
        }

        public virtual bool CanBeTargetedBy(StationLocation stationLocation, PlayerActionType playerActionType, Player performingPlayer)
        {
            return IsDamageable && ActionType == playerActionType && CurrentStations.Contains(stationLocation);
        }

        public bool NextDamageWillDestroyThreat()
        {
            return RemainingInaccessibility == 0 && RemainingHealth == 1;
        }
    }
}
