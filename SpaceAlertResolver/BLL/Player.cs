﻿using System.Collections.Generic;
using System.Linq;
using BLL.ShipComponents;

namespace BLL
{
	public class Player
	{
		private bool isKnockedOut;
		public bool IsKnockedOut {
			get { return isKnockedOut; }
			set
			{
				if (value || !CurrentStation.Players.Any(player => player.PreventsKnockOut))
					isKnockedOut = value;
			}
		}
		public Interceptors Interceptors { get; set; }
		public IList<PlayerAction> Actions { get; set; }
		public Station CurrentStation { get; set; }
		public BattleBots BattleBots { get; set; }
		public int Index { get; set; }
		public bool IsCaptain { get; set; }
		public PlayerSpecialization? BasicSpecialization { get; set; }
		public PlayerSpecialization? AdvancedSpecialization { get; set; }
		public int BonusPoints { get; set; }
		public bool PlayerToTeleport { get; set; }
		public bool TeleportDestination { get; set; }
		private bool PreventsKnockOut { get; set; }

		public void SetPreventsKnockOut(bool preventsKnockOut)
		{
			PreventsKnockOut = preventsKnockOut;
		}

		public bool HasSpecialOpsProtection { get; set; }

		public void Shift(int turn)
		{
			Shift(turn, null);
		}

		public void ShiftAndRepeatPreviousAction(int turn)
		{
			var actionToRepeat = turn <= 0 ? null : Actions[turn - 1].ActionType;
			Shift(turn, actionToRepeat);
		}

		private void Shift(int turn, PlayerActionType? actionToInsert)
		{
			var endTurn = turn;
			while (endTurn + 1 < Actions.Count && Actions[endTurn].ActionType.HasValue)
				endTurn++;
			Actions.Insert(turn, PlayerActionFactory.CreateSingleAction(this, actionToInsert));
			Actions.RemoveAt(endTurn + 1);
		}

		public bool IsPerformingMedic(int currentTurn)
		{
			return IsPerformingAdvancedMedic(currentTurn) || IsPerformingBasicMedic(currentTurn);
		}

		public bool IsPerformingAdvancedMedic(int currentTurn)
		{
			return AdvancedSpecialization == PlayerSpecialization.Medic && (Actions[currentTurn].HasAdvancedSpecializationAttached);
		}

		public bool IsPerformingBasicMedic(int currentTurn)
		{
			return AdvancedSpecialization == PlayerSpecialization.Medic && (Actions[currentTurn].HasBasicSpecializationAttached);
		}

		public bool IsPerformingAdvancedSpecialOps(int currentTurn)
		{
			var currentAction = Actions[currentTurn];
			return AdvancedSpecialization == PlayerSpecialization.SpecialOps && currentAction.HasAdvancedSpecializationAttached;
		}
	}
}
