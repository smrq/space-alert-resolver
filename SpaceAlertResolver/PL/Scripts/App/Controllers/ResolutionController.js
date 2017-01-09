﻿"use strict";

angular.module("spaceAlertModule")
	.controller("ResolutionController",
	[
		"$scope", "gameData", '$interval', '$animate', function($scope, gameData, $interval, $animate) {
			$animate.enabled(false);
			$scope.gameData = gameData;
			$scope.playing = null;
			$scope.turnsPerTwoSeconds = 5;
			var selectPhase = function(phase) {
				$scope.currentPhase = phase;
			}
			var selectTurn = function(turn) {
				$scope.currentTurn = turn;
				selectPhase(0);
			}
			var stop = function() {
				if ($scope.playing != null) {
					$interval.cancel($scope.playing);
					$scope.playing = null;
				}
			};
			var play = function() {
				$scope.playing = $interval(function() {
						if (!$scope.isAtEndOfTurn())
							$scope.selectPhaseAutomatically($scope.currentPhase + 1);
						else if (!$scope.isAtEndOfGame())
							$scope.selectTurnAutomatically($scope.currentTurn + 1);
						else {
							stop();
						}
					},
					2000 / $scope.turnsPerTwoSeconds);
			};
			$scope.$watch('turnsPerTwoSeconds',
				function() {
					if ($scope.playing != null) {
						stop();
						play();
					}
				});

			$scope.selectTurnManually = function(turn) {
				stop();
				selectTurn(turn);
			}
			$scope.selectTurnAutomatically = function(turn) {
				selectTurn(turn);
			}

			$scope.selectPhaseManually = function(phase) {
				stop();
				selectPhase(phase);
			}
			$scope.selectPhaseAutomatically = function(phase) {
				selectPhase(phase);
			}

			$scope.isAtStartOfTurn = function() {
				return $scope.currentPhase === 0;
			}
			$scope.isAtStartOfGame = function() {
				return $scope.currentTurn === 0 && $scope.isAtStartOfTurn();
			}
			$scope.isAtEndOfTurn = function() {
				return $scope.currentPhase === $scope.gameData[$scope.currentTurn].length - 1;
			}
			$scope.isAtEndOfGame = function() {
				return $scope.currentTurn === $scope.gameData.length - 1 && $scope.isAtEndOfTurn();
			}
			$scope.goBackOnePhase = function() {
				if (!$scope.isAtStartOfTurn())
					$scope.selectPhaseManually($scope.currentPhase - 1);
				else if (!$scope.isAtStartOfGame()) {
					$scope.selectTurnManually($scope.currentTurn - 1);
					$scope.selectPhaseManually($scope.gameData[$scope.currentTurn].length - 1);
				}
			}
			$scope.goForwardOnePhase = function() {
				if (!$scope.isAtEndOfTurn())
					$scope.selectPhaseManually($scope.currentPhase + 1);
				else if (!$scope.isAtEndOfGame()) {
					$scope.selectTurnManually($scope.currentTurn + 1);
				}
			}
			$scope.playPause = function() {
				if ($scope.playing)
					stop();
				else
					play();
			}
			$scope.goToStart = function() {
				if (!$scope.isAtStartOfGame()) {
					stop();
					$scope.selectTurnManually(0);
				}
			}
			$scope.goToEnd = function() {
				if (!$scope.isAtEndOfGame()) {
					stop();
					$scope.selectTurnManually($scope.gameData.length - 1);
					$scope.selectPhaseManually($scope.gameData[$scope.currentTurn].length - 1);
				}
			}
			$scope.$on('$destroy',
				function() {
					stop();
				});

			$scope.selectTurnManually(0);
		}
	]);
