import Reactor from './Reactor';
import Shield from './Shield';
import CentralReactor from './CentralReactor';
import CentralShield from './CentralShield';

let reactor: Reactor;
let shield: Shield;

beforeEach(() => {
    reactor = new CentralReactor();
    shield = new CentralShield(reactor);
});

describe('Shield', () => {
    it('Energy_Get_NoBonusEnergy', () => {
        shield.energy = 3;
        shield.bonusShield = 0;
        expect(shield.energy).toBe(3);
        expect(shield.bonusShield).toBe(0);
    });

    it('Energy_Get_IncludeBonusEnergy', () => {
        shield.energy = 3;
        shield.bonusShield = 1;
        expect(shield.energy).toBe(4);
        expect(shield.bonusShield).toBe(1);
    });

    it('Energy_Add_NoBonusEnergy', () => {
        shield.energy = 3;
        shield.bonusShield = 0;
        expect(shield.energy).toBe(3);
        expect(shield.bonusShield).toBe(0);
        shield.energy += 1;
        expect(shield.energy).toBe(4);
        expect(shield.bonusShield).toBe(0);
    });

    it('Energy_Add_IncludeBonusEnergy', () => {
        shield.energy = 3;
        shield.bonusShield = 2;
        expect(shield.energy).toBe(5);
        expect(shield.bonusShield).toBe(2);
        shield.energy += 1;
        expect(shield.energy).toBe(6);
        expect(shield.bonusShield).toBe(2);
    });

    it('Energy_SetToZero_HadBonusEnergy', () => {
        shield.energy = 3;
        shield.bonusShield = 1;
        expect(shield.energy).toBe(4);
        expect(shield.bonusShield).toBe(1);
        shield.energy = 0;
        expect(shield.energy).toBe(0);
        expect(shield.bonusShield).toBe(0);
    });

    it('Energy_SetToZero_NoBonusEnergy', () => {
        shield.energy = 3;
        shield.bonusShield = 0;
        expect(shield.energy).toBe(3);
        expect(shield.bonusShield).toBe(0);
        shield.energy = 0;
        expect(shield.energy).toBe(0);
        expect(shield.bonusShield).toBe(0);
    });

    it('Energy_SubtractedLessThanBonusEnergy', () => {
        shield.energy = 4;
        shield.bonusShield = 2;
        expect(shield.energy).toBe(6);
        expect(shield.bonusShield).toBe(2);
        shield.energy -= 1;
        expect(shield.energy).toBe(5);
        expect(shield.bonusShield).toBe(1);
    });

    it('Energy_SubtractedMoreThanBonusEnergy', () => {
        shield.energy = 4;
        shield.bonusShield = 2;
        expect(shield.energy).toBe(6);
        expect(shield.bonusShield).toBe(2);
        shield.energy -= 3;
        expect(shield.energy).toBe(3);
        expect(shield.bonusShield).toBe(0);
    });

    it('Energy_Subtracted_NoBonusEnergy', () => {
        shield.energy = 4;
        shield.bonusShield = 0;
        expect(shield.energy).toBe(4);
        expect(shield.bonusShield).toBe(0);
        shield.energy -= 3;
        expect(shield.energy).toBe(1);
        expect(shield.bonusShield).toBe(0);
    });

    it('Energy_SubtractedMoreThanTotal_NoBonusEnergy', () => {
        shield.energy = 3;
        shield.bonusShield = 0;
        expect(shield.energy).toBe(3);
        expect(shield.bonusShield).toBe(0);
        shield.energy -= 5;
        expect(shield.energy).toBe(0);
        expect(shield.bonusShield).toBe(0);
    });

    it('Energy_SubtractedMoreThanTotal_HasBonusEnergy', () => {
        shield.energy = 3;
        shield.bonusShield = 1;
        expect(shield.energy).toBe(4);
        expect(shield.bonusShield).toBe(1);
        shield.energy -= 5;
        expect(shield.energy).toBe(0);
        expect(shield.bonusShield).toBe(0);
    });

    it('FillToCapacity_RemainingCapacityGreaterThanSource', () => {
        reactor.energy = 2;
        shield.energy = 0;
        shield.fillToCapacity(false);
        expect(reactor.energy).toBe(0);
        expect(shield.energy).toBe(2);
    });

    it('FillToCapacity_RemainingCapacityLessThanSource', () => {
        reactor.energy = 3;
        shield.energy = 2;
        shield.fillToCapacity(false);
        expect(reactor.energy).toBe(2);
        expect(shield.energy).toBe(3);
    });

    it('FillToCapacity_SourceEmpty', () => {
        reactor.energy = 0;
        shield.energy = 2;
        shield.fillToCapacity(false);
        expect(reactor.energy).toBe(0);
        expect(shield.energy).toBe(2);
    });

    it('FillToCapacity_AtCapacity', () => {
        reactor.energy = 2;
        shield.energy = 3;
        shield.fillToCapacity(false);
        expect(reactor.energy).toBe(2);
        expect(shield.energy).toBe(3);
    });

    it('FillToCapacity_RemainingCapacityGreaterThanSource_Heroic', () => {
        reactor.energy = 2;
        shield.energy = 0;
        shield.fillToCapacity(true);
        expect(reactor.energy).toBe(0);
        expect(shield.energy).toBe(3);
    });

    it('FillToCapacity_RemainingCapacityLessThanSource_Heroic', () => {
        reactor.energy = 3;
        shield.energy = 2;
        shield.fillToCapacity(true);
        expect(reactor.energy).toBe(2);
        expect(shield.energy).toBe(4);
    });

    it('FillToCapacity_SourceEmpty_Heroic', () => {
        reactor.energy = 0;
        shield.energy = 2;
        shield.fillToCapacity(true);
        expect(reactor.energy).toBe(0);
        expect(shield.energy).toBe(2);
    });

    it('FillToCapacity_AtCapacity_Heroic', () => {
        reactor.energy = 2;
        shield.energy = 3;
        shield.fillToCapacity(true);
        expect(reactor.energy).toBe(2);
        expect(shield.energy).toBe(3);
    });

    it('ShieldThroughAttack_IneffectiveShields_DamageGreaterThanBonusShield', () => {
        shield.setIneffectiveShields(true);
        shield.energy = 5;
        shield.bonusShield = 2;
        var result = shield.shieldThroughAttack(3);
        expect(result).toBe(2);
        expect(shield.bonusShield).toBe(0);
        expect(shield.energy).toBe(5);
    });

    it('ShieldThroughAttack_IneffectiveShields_DamageLessThanBonusShield', () => {
        shield.setIneffectiveShields(true);
        shield.energy = 5;
        shield.bonusShield = 2;
        var result = shield.shieldThroughAttack(1);
        expect(result).toBe(1);
        expect(shield.bonusShield).toBe(1);
        expect(shield.energy).toBe(6);
    });

    it('ShieldThroughAttack_ReversedShields_HasBonusShields', () => {
        shield.setReversedShields(true);
        shield.energy = 3;
        shield.bonusShield = 2;
        var result = shield.shieldThroughAttack(6);
        expect(result).toBe(-1);
        expect(shield.bonusShield).toBe(0);
        expect(shield.energy).toBe(0);
    });


    it('ShieldThroughAttack_ReversedShields_NoBonusShields', () => {
        shield.setReversedShields(true);
        shield.energy = 3;
        shield.bonusShield = 0;
        var result = shield.shieldThroughAttack(6);
        expect(result).toBe(-3);
        expect(shield.bonusShield).toBe(0);
        expect(shield.energy).toBe(0);
    });

    it('ShieldThroughAttack_WorkingShieldsGreaterThanAttack_NoBonusShields', () => {
        shield.energy = 3;
        shield.bonusShield = 0;
        var result = shield.shieldThroughAttack(2);
        expect(result).toBe(2);
        expect(shield.bonusShield).toBe(0);
        expect(shield.energy).toBe(1);
    });

    it('ShieldThroughAttack_WorkingShieldsLessThanAttack_NoBonusShields', () => {
        shield.energy = 3;
        shield.bonusShield = 0;
        var result = shield.shieldThroughAttack(5);
        expect(result).toBe(3);
        expect(shield.bonusShield).toBe(0);
        expect(shield.energy).toBe(0);
    });

    it('ShieldThroughAttack_WorkingShieldsGreaterThanAttack_HasBonusShields', () => {
        shield.energy = 4;
        shield.bonusShield = 2;
        var result = shield.shieldThroughAttack(5);
        expect(result).toBe(5);
        expect(shield.bonusShield).toBe(0);
        expect(shield.energy).toBe(1);
    });

    it('ShieldThroughAttack_WorkingShieldsLessThanAttack_HasBonusShields', () => {
        shield.energy = 4;
        shield.bonusShield = 2;
        var result = shield.shieldThroughAttack(7);
        expect(result).toBe(6);
        expect(shield.bonusShield).toBe(0);
        expect(shield.energy).toBe(0);
    });
});
