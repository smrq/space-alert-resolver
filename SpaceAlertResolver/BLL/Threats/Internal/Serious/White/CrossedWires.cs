﻿using BLL.Players;
using BLL.ShipComponents;

namespace BLL.Threats.Internal.Serious.White
{
    public class CrossedWires : SeriousWhiteInternalThreat
    {
        internal CrossedWires()
            : base(4, 3, StationLocation.UpperWhite, PlayerActionType.Bravo)
        {
        }

        protected override void PerformXAction(int currentTurn)
        {
            SittingDuck.TransferEnergyToShields(new [] {CurrentZone});
            EnergyLeaksOutFromReactor(CurrentZone);
        }

        protected override void PerformYAction(int currentTurn)
        {
            EnergyLeaksOutFromShield(CurrentZone);
        }

        protected override void PerformZAction(int currentTurn)
        {
            foreach (var zoneLocation in EnumFactory.All<ZoneLocation>())
                EnergyLeaksOutFromReactor(zoneLocation);
        }

        public override string Id { get; } = "SI1-05";
        public override string DisplayName { get; } = "Crossed Wires";
        public override string FileName { get; } = "CrossedWires";

        private void EnergyLeaksOutFromShield(ZoneLocation zoneLocation)
        {
            var energyDrained = SittingDuck.DrainShields(new [] {zoneLocation});
            Attack(energyDrained);
        }

        private void EnergyLeaksOutFromReactor(ZoneLocation zoneLocation)
        {
            var energyDrained = SittingDuck.DrainReactors(new [] {zoneLocation});
            Attack(energyDrained);
        }
    }
}
