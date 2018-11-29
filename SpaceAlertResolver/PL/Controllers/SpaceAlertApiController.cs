﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using BLL.Players;
using BLL.ShipComponents;
using BLL.Threats.External;
using BLL.Threats.Internal;
using BLL.Tracks;
using PL.Models;

namespace PL.Controllers
{
    // [Route("api/[controller]")]
    public class SpaceAlertApiController : Controller
    {
        [HttpGet("[action]")]
        public InputModel NewGameInput()
        {
            var allExternalThreats = ExternalThreatFactory.AllExternalThreats
                .Select(threat => new ExternalThreatModel(threat))
                .ToList();
            var allInternalThreats = InternalThreatFactory.AllInternalThreats
                .Select(threat => new InternalThreatModel(threat))
                .ToList();
            var inputModel = new InputModel
            {
                SingleActions = ActionModel.AllSingleActionModels.OrderBy(action => action.FirstAction).ThenBy(action => action.SecondAction),
                DoubleActions = ActionModel.AllSelectableDoubleActionModels.OrderBy(action => action.FirstAction).ThenBy(action => action.SecondAction),
                SpecializationActions = PlayerSpecializationActionModel.AllPlayerSpecializationActionModels
                    .OrderBy(action => action.PlayerSpecialization)
                    .ThenBy(player => player.Hotkey),
                Tracks = TrackFactory.CreateAllTracks()
                    .Select(track => new TrackSnapshotModel(track, new List<int>()))
                    .ToList(),
                AllInternalThreats = new AllThreatsModel(allInternalThreats),
                AllExternalThreats = new AllThreatsModel(allExternalThreats),
                PlayerSpecializations = EnumFactory.All<PlayerSpecialization>().ToList(),
                AllDamageTokens = EnumFactory.All<DamageToken>(),
                DamageableZones = EnumFactory.All<ZoneLocation>()
            };
            return inputModel;
        }

        [HttpPost("[action]")]
        public IList<GameTurnModel> ProcessGame([FromBody]NewGameModel newGameModel)
        {
            var game = newGameModel.ConvertToGame();
            if (game.Players.First().Actions.All(action => action.FirstActionSegment.SegmentType == PlayerActionType.BattleBots))
                throw new InvalidOperationException("Successfully triggered test Exception. You can't do that many battle bots!");

            game.StartGame();
            var turnModels = new List<GameTurnModel>();

            game.PhaseStarting += (sender, eventArgs) =>
            {
                var lastPhase = turnModels.Last().Phases.LastOrDefault();
                lastPhase?.SubPhases.Add(new GameSnapshotModel(game, "End of Phase"));
                turnModels.Last().Phases.Add(new GamePhaseModel {Description = eventArgs.PhaseHeader});
                turnModels.Last().Phases.Last().SubPhases.Add(new GameSnapshotModel(game, "Start of Phase"));
            };
            game.EventMaster.EventTriggered += (sender, eventArgs) =>
            {
                turnModels.Last().Phases.Last().SubPhases.Add(new GameSnapshotModel(game, eventArgs.PhaseHeader));
            };
            game.LostGame += (sender, args) =>
            {
                turnModels.Last().Phases.Last().SubPhases.Add(new GameSnapshotModel(game, "Lost!"));
            };

			while(game.GameStatus == GameStatus.InProgress)
			{ 
                turnModels.Add(new GameTurnModel { Turn = game.CurrentTurn });
                game.PerformTurn();
                turnModels.Last().Phases.Last().SubPhases.Add(new GameSnapshotModel(game, "End of Phase"));
            }

            return turnModels;
        }

        [HttpPost("[action]")]
        public void SendGameMessage([FromBody]SendGameMessageModel model, string senderEmailAddress)
        {
            EmailService.SendEmail(model.MessageText, senderEmailAddress);
        }
    }
}
