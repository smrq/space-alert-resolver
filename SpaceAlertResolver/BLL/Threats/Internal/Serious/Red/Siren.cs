﻿using System.Linq;
using BLL.Players;
using BLL.ShipComponents;

namespace BLL.Threats.Internal.Serious.Red
{
    public class Siren : SeriousRedInternalThreat
    {
        internal Siren()
            : base(2, 2, StationLocation.UpperRed, PlayerActionType.BattleBots)
        {
        }

        protected override void PerformXAction(int currentTurn)
        {
            var stationsWithOnePlayer = EnumFactory.All<StationLocation>()
                .Where(stationLocation => stationLocation.IsOnShip())
                .Where(stationLocation => SittingDuck.GetPlayerCount(stationLocation) == 1);
            SittingDuck.KnockOutPlayers(stationsWithOnePlayer);
        }

        protected override void PerformYAction(int currentTurn)
        {
            var stationsWithMultiplePlayers = EnumFactory.All<StationLocation>()
                .Where(stationLocation => stationLocation.IsOnShip())
                .Where(stationLocation => SittingDuck.GetPlayerCount(stationLocation) > 1);
            SittingDuck.KnockOutPlayers(stationsWithMultiplePlayers);
        }

        protected override void PerformZAction(int currentTurn)
        {
            SittingDuck.KnockOutPlayers(EnumFactory.All<StationLocation>());
        }

        public override string Id { get; } = "SI3-105";
        public override string DisplayName { get; } = "Siren";
        public override string FileName { get; } = "Siren";

        public override void TakeDamage(int damage, Player performingPlayer, bool isHeroic, StationLocation? stationLocation)
        {
            base.TakeDamage(damage, performingPlayer, isHeroic, stationLocation);
            if (!isHeroic)
                performingPlayer.BattleBots.IsDisabled = true;
            CurrentStation = StationLocation.LowerBlue;
        }
    }
}
