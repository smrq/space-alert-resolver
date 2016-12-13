﻿using System;
using System.Collections.Generic;
using BLL.Threats.External.Minor.Red;
using BLL.Threats.External.Minor.White;
using BLL.Threats.External.Minor.Yellow;
using BLL.Threats.External.Serious.Red;
using BLL.Threats.External.Serious.White;
using BLL.Threats.External.Serious.Yellow;
using BLL.Threats.Internal.Minor.Red;
using BLL.Threats.Internal.Minor.White;
using BLL.Threats.Internal.Minor.Yellow;
using BLL.Threats.Internal.Serious.Red;
using BLL.Threats.Internal.Serious.White;
using BLL.Threats.Internal.Serious.Yellow;

namespace BLL.Threats
{
	public class ThreatFactory
	{
		private static readonly Dictionary<Type, string> ThreatTypesByDisplayName = new Dictionary<Type, string>
		{
			{typeof (EnergySnake), "Energy Snake"},
			{typeof (JumperB), "Jumper E3-106"},
			{typeof (JumperA), "Jumper E3-105"},
			{typeof (MegashieldFighter), "Megashield Fighter"},
			{typeof (PhasingPulser), "Phasing Pulser"},
			{typeof (PolarizedFighter), "Polarized Fighter"},
			{typeof (PhasingDestroyer), "Phasing Destroyer"},
			{typeof (PlasmaticNeedleship), "Plasmatic Needleship"},
			{typeof (SealedCapsule), "Sealed Capsule"},
			{typeof (Amoeba), "Amoeba"},
			{typeof (Gunship), "Gunship"},
			{typeof (Jellyfish), "Jellyfish"},
			{typeof (Kamikaze), "Kamikaze"},
			{typeof (Marauder), "Marauder"},
			{typeof (MegashieldDestroyer), "Megashield Destroyer"},
			{typeof (Meteoroid), "Meteoroid"},
			{typeof (MinorAsteroid), "Minor Asteroid"},
			{typeof (PhasingFighter), "Phasing Fighter"},
			{typeof (EnergyDragon), "Energy Dragon"},
			{typeof (Leaper), "Leaper"},
			{typeof (MegashieldManOfWar), "Megashield Man-Of-War"},
			{typeof (Overlord), "Overlord"},
			{typeof (Planetoid), "Planetoid"},
			{typeof (PolarizedFrigate), "Polarized Frigate"},
			{typeof (SuperCarrier), "Super-Carrier"},
			{typeof (TransmitterSatellite), "Transmitter Satellite"},
			{typeof (Asteroid), "Asteroid"},
			{typeof (CryoshieldFighter), "Cryoshield Fighter"},
			{typeof (CryoshieldFrigate), "Cryoshield Frigate"},
			{typeof (Destroyer), "Destroyer"},
			{typeof (DimensionSpider), "Dimension Spider"},
			{typeof (EnergyCloud), "Energy Cloud"},
			{typeof (Frigate), "Frigate"},
			{typeof (InterstellarOctopus), "Interstellar Octopus"},
			{typeof (LeviathanTanker), "Leviathan Tanker"},
			{typeof (Maelstrom), "Maelstrom"},
			{typeof (ManOfWar), "Man-Of-War"},
			{typeof (MiniCarrier), "Mini-Carrier"},
			{typeof (PhantomFighter), "Phantom Fighter"},
			{typeof (PlasmaticFighter), "Plasmatic Fighter"},
			{typeof (PulseBall), "Pulse Ball"},
			{typeof (PulseSatellite), "Pulse Satellite"},
			{typeof (Scout), "Scout"},
			{typeof (Behemoth), "Behemoth"},
			{typeof (Juggernaut), "Juggernaut"},
			{typeof (MajorAsteroid), "Major Asteroid"},
			{typeof (MotherSwarm), "Mother Swarm"},
			{typeof (NebulaCrab), "Nebula Crab"},
			{typeof (Nemesis), "Nemesis"},
			{typeof (PhasingManOfWar), "Phasing Man-Of-War"},
			{typeof (PhasingFrigate), "Phasing Frigate"},
			{typeof (PlasmaticFrigate), "Plasmatic Frigate"},
			{typeof (PsionicSatellite), "Psionic Satellite"},
			{typeof (SpacecraftCarrier), "Spacecraft Carrier"},
			{typeof (SpinningSaucer), "Spinning Saucer"},
			{typeof (StealthFighter), "Stealth Fighter"},
			{typeof (Swarm), "Swarm"},
			{typeof (BreachedAirlock), "Breached Airlock"},
			{typeof (Driller), "Driller"},
			{typeof (LateralLaserJam), "Lateral Laser Jam"},
			{typeof (PhasingTroopersB), "Phasing Troopers I3-105"},
			{typeof (PhasingTroopersA), "Phasing Troopers I3-106"},
			{typeof (PulseCannonShortCircuit), "Pulse Cannon Short Circuit"},
			{typeof (ReversedShields), "Reversed Shields"},
			{typeof (Ninja), "Ninja"},
			{typeof (OverheatedReactor), "Overheated Reactor"},
			{typeof (PowerPackOverload), "Power Pack Overload"},
			{typeof (SlimeA), "Slime I2-01"},
			{typeof (SlimeB), "Slime I2-02"},
			{typeof (TroopersA), "Troopers I2-04"},
			{typeof (TroopersB), "Troopers I2-03"},
			{typeof (Virus), "Virus"},
			{typeof (CyberGremlin), "Cyber Gremlin"},
			{typeof (HiddenTransmitterB), "Hidden Transmitter SI3-102"},
			{typeof (HiddenTransmitterA), "Hidden Transmitter SI3-103"},
			{typeof (Parasite), "Cyber Gremlin"},
			{typeof (RabidBeast), "Rabid Beast"},
			{typeof (Siren), "Siren"},
			{typeof (SpaceTimeVortex), "Space Time Vortex"},
			{typeof (Alien), "Alien"},
			{typeof (BattleBotUprising), "BattleBot Uprising"},
			{typeof (CentralLaserJam), "Central Laser Jam"},
			{typeof (CommandosB), "Commandos SI1-02"},
			{typeof (CommandosA), "Commandos SI1-01"},
			{typeof (CrossedWires), "Crossed Wires"},
			{typeof (Fissure), "Fissure"},
			{typeof (HackedShieldsA), "Hacked Shields I1-06"},
			{typeof (HackedShieldsB), "Hacked Shields I1-05"},
			{typeof (SaboteurA), "Saboteur I1-04"},
			{typeof (SaboteurB), "Saboteur I1-03"},
			{typeof (Contamination), "Contamination"},
			{typeof (Executioner), "Executioner"},
			{typeof (NuclearDevice), "Nuclear Device"},
			{typeof (PhasingAnomaly), "Phasing Anomaly"},
			{typeof (PhasingMineLayer), "Phasing Mine Layer"},
			{typeof (PowerSystemOverload), "Power System Overload"},
			{typeof (Seeker), "Seeker"},
			{typeof (Shambler), "Shambler"},
			{typeof (SkirmishersA), "Skirmishers I1-01"},
			{typeof (SkirmishersB), "Skirmishers I1-02"},
			{typeof (UnstableWarheads), "Unstable Warheads"},
			{typeof (ArmoredGrappler), "Armored Grappler"},
			{typeof (Fighter), "Fighter"}
		};

		private static readonly IDictionary<string, Type> ThreatTypesById = new Dictionary<string, Type>
		{
			{"E3-109", typeof (EnergySnake)},
			{"E3-106", typeof (JumperB)},
			{"E3-105", typeof (JumperA)},
			{"E3-104", typeof (MegashieldFighter)},
			{"E3-102", typeof (PhasingPulser)},
			{"E3-108", typeof (PolarizedFighter)},
			{"E3-103", typeof (PhasingDestroyer)},
			{"E3-101", typeof (PlasmaticNeedleship)},
			{"E3-107", typeof (SealedCapsule)},
			{"E1-09", typeof (Amoeba)},
			{"E1-05", typeof (Gunship)},
			{"E2-05", typeof (Jellyfish)},
			{"E2-01", typeof (Kamikaze)},
			{"E2-06", typeof (Marauder)},
			{"E2-103", typeof (MegashieldDestroyer)},
			{"E1-10", typeof (Meteoroid)},
			{"E2-07", typeof (MinorAsteroid)},
			{"E2-102", typeof (PhasingFighter)},
			{"SE3-108", typeof (EnergyDragon)},
			{"SE3-106", typeof (Leaper)},
			{"SE3-102", typeof (MegashieldManOfWar)},
			{"SE3-105", typeof (Overlord)},
			{"SE3-107", typeof (Planetoid)},
			{"SE3-109", typeof (PolarizedFrigate)},
			{"SE3-103", typeof (SuperCarrier)},
			{"SE3-104", typeof (TransmitterSatellite)},
			{"SE1-08", typeof (Asteroid)},
			{"E1-06", typeof (CryoshieldFighter)},
			{"SE1-05", typeof (CryoshieldFrigate)},
			{"E1-02", typeof (Destroyer)},
			{"SE1-102", typeof (DimensionSpider)},
			{"E1-04", typeof (EnergyCloud)},
			{"SE1-01", typeof (Frigate)},
			{"SE1-06", typeof (InterstellarOctopus)},
			{"SE1-03", typeof (LeviathanTanker)},
			{"SE1-07", typeof (Maelstrom)},
			{"SE1-02", typeof (ManOfWar)},
			{"E2-101", typeof (MiniCarrier)},
			{"E2-03", typeof (PhantomFighter)},
			{"E1-101", typeof (PlasmaticFighter)},
			{"E1-01", typeof (PulseBall)},
			{"SE1-04", typeof (PulseSatellite)},
			{"E2-02", typeof (Scout)},
			{"SE2-01", typeof (Behemoth)},
			{"SE2-02", typeof (Juggernaut)},
			{"SE2-06", typeof (MajorAsteroid)},
			{"SE2-103", typeof (MotherSwarm)},
			{"SE2-04", typeof (NebulaCrab)},
			{"SE2-05", typeof (Nemesis)},
			{"SE3-101", typeof (PhasingManOfWar)},
			{"SE2-102", typeof (PhasingFrigate)},
			{"SE2-101", typeof (PlasmaticFrigate)},
			{"SE2-03", typeof (PsionicSatellite)},
			{"SE1-101", typeof (SpacecraftCarrier)},
			{"E1-102", typeof (SpinningSaucer)},
			{"E1-03", typeof (StealthFighter)},
			{"E2-04", typeof (Swarm)},
			{"I3-104", typeof (BreachedAirlock)},
			{"I3-107", typeof (Driller)},
			{"I3-101", typeof (LateralLaserJam)},
			{"I3-105", typeof (PhasingTroopersB)},
			{"I3-106", typeof (PhasingTroopersA)},
			{"I3-102", typeof (PulseCannonShortCircuit)},
			{"I3-103", typeof (ReversedShields)},
			{"I2-102", typeof (Ninja)},
			{"I2-06", typeof (OverheatedReactor)},
			{"I2-101", typeof (PowerPackOverload)},
			{"I2-01", typeof (SlimeA)},
			{"I2-02", typeof (SlimeB)},
			{"I2-04", typeof (TroopersA)},
			{"I2-03", typeof (TroopersB)},
			{"I2-05", typeof (Virus)},
			{"SI3-106", typeof (CyberGremlin)},
			{"SI3-102", typeof (HiddenTransmitterB)},
			{"SI3-103", typeof (HiddenTransmitterA)},
			{"SI3-107", typeof (Parasite)},
			{"SI3-101", typeof (RabidBeast)},
			{"SI3-105", typeof (Siren)},
			{"SI3-104", typeof (SpaceTimeVortex)},
			{"SI1-03", typeof (Alien)},
			{"SI1-06", typeof (BattleBotUprising)},
			{"I1-101", typeof (CentralLaserJam)},
			{"SI1-02", typeof (CommandosB)},
			{"SI1-01", typeof (CommandosA)},
			{"SI1-05", typeof (CrossedWires)},
			{"SI1-04", typeof (Fissure)},
			{"I1-06", typeof (HackedShieldsA)},
			{"I1-05", typeof (HackedShieldsB)},
			{"I1-04", typeof (SaboteurA)},
			{"I1-03", typeof (SaboteurB)},
			{"SI2-04", typeof (Contamination)},
			{"SI2-01", typeof (Executioner)},
			{"SI2-05", typeof (NuclearDevice)},
			{"SI2-101", typeof (PhasingAnomaly)},
			{"SI2-102", typeof (PhasingMineLayer)},
			{"SI2-03", typeof (PowerSystemOverload)},
			{"SI2-02", typeof (Seeker)},
			{"SI1-101", typeof (Shambler)},
			{"I1-01", typeof (SkirmishersA)},
			{"I1-02", typeof (SkirmishersB)},
			{"I1-07", typeof (UnstableWarheads)},
			{"E1-08", typeof (ArmoredGrappler)},
			{"E1-07", typeof (Fighter)},
		};

		public static T CreateThreat<T>(string id) where T: Threat
		{
			if (!ThreatTypesById.ContainsKey(id))
				return null;
			return Activator.CreateInstance(ThreatTypesById[id]) as T;
		}
	}
}
