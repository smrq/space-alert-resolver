import EnergyContainer from './EnergyContainer';
import Reactor from './Reactor';

export default abstract class Shield extends EnergyContainer {
    private _source: EnergyContainer;
    private _bonusShield: number;
    private _ineffectiveShields: boolean;
    private _reversedShields: boolean;

    constructor(source: Reactor, capacity: number, energy: number) {
        super(capacity, energy);
        this._source = source;
        this._bonusShield = 0;
        this._ineffectiveShields = false;
        this._reversedShields = false;
    }

    get energy(): number { return super.energy + this.bonusShield; }
    set energy(value: number) {
        if (this.energy <= value) {
            super.energy = value - this.bonusShield;
        } else if (value <= 0) {
            this.bonusShield = 0;
            super.energy = 0;
        } else {
            let energyToDrain = this.energy - value;
            const oldBonusShield = this.bonusShield;
            this.bonusShield -= energyToDrain;
            const newBonusShield = this.bonusShield;
            const bonusEnergyDrained = oldBonusShield - newBonusShield;
            energyToDrain -= bonusEnergyDrained;
            super.energy -= energyToDrain;
        }
    }

    get bonusShield(): number { return this._bonusShield; }
    set bonusShield(value: number) { this._bonusShield = value > 0 ? value : 0; }

    performBAction(isHeroic: boolean): void {
        this.fillToCapacity(isHeroic);
    }

    fillToCapacity(isHeroic: boolean): void {
        const energyToPull = this.capacity - this.energy;
        const oldSourceEnergy = this._source.energy;
        this._source.energy -= energyToPull;
        const newSourceEnergy = this._source.energy;
        const energyPulled = oldSourceEnergy - newSourceEnergy;
        this.energy += energyPulled;
        if (energyPulled > 0 && isHeroic) {
            this.energy++;
        }
    }

    performEndOfTurn(): void {
        super.performEndOfTurn();
        this.bonusShield = 0;
    }

    setIneffectiveShields(ineffectiveShields: boolean) {
        this._ineffectiveShields = ineffectiveShields;
    }

    setReversedShields(reversedShields: boolean): void {
        this._reversedShields = reversedShields;
    }

    shieldThroughAttack(amount: number): number {
        let amountUnshielded = amount;
        const oldBonusShield = this.bonusShield;
        this.bonusShield -= amount;
        const newBonusShield = this.bonusShield;
        const amountShielded = oldBonusShield - newBonusShield;
        amountUnshielded -= amountShielded;

        if (this._reversedShields) {
            const energyAddedToAttack = this.energy;
            this.energy = 0;
            return amountShielded - energyAddedToAttack;
        }

        if (this._ineffectiveShields) {
            return amountShielded;
        }

        const oldShields = this.energy;
        this.energy -= amountUnshielded;
        const newShields = this.energy;
        return (oldShields - newShields) + amountShielded;
    }

    drain(amount?: number): number {
        const oldEnergy = this.energy;
        this.energy -= amount == null ? this.energy : amount;
        const currentEnergy = this.energy;
        return oldEnergy - currentEnergy;
    }
}
