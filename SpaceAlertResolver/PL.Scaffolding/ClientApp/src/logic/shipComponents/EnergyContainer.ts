import IBravoComponent from './IBravoComponent';
import EnergyType from './EnergyType';

export default abstract class EnergyContainer implements IBravoComponent
{
    private _capacity: number;
    private _energy: number;
    private _isDamaged: boolean;

    constructor(capacity: number, energy: number) {
        this.capacity = capacity;
        this.energy = energy;
    }

    abstract performBAction(isHeroic: boolean): void;

    get capacity(): number { return this._capacity; }
    set capacity(value: number) { this._capacity = value; }

    get energy(): number { return this._energy; }
    set energy(value: number) { this._energy = value > 0 ? value : 0; }

    get isDamaged(): boolean { return this._isDamaged; }
    set isDamaged(value: boolean) { this._isDamaged = value; }

    get energyInComponent(): number { return this.energy; }

    get energyType(): EnergyType { return EnergyType.Standard; }

    setDamaged(isCampaignDamage: boolean): void {
        const wasAlreadyDamaged = this.isDamaged;
        this.isDamaged = true;
        if (!wasAlreadyDamaged) {
            this.capacity--;
            if (this.energy > this.capacity) {
                this.energy = this.capacity;
            }
            if (isCampaignDamage) {
                this.energy--;
            }
        }
    }

    repair(): void {
        const wasAlreadyDamaged = this.isDamaged;
        this.isDamaged = false;
        if (wasAlreadyDamaged) {
            this.capacity++;
        }
    }

    useEnergy(amount: number): void {
        this.energy -= amount;
    }

    canUseEnergy(amount: number): void {
        return this.energy >= amount;
    }

    performEndOfTurn() {}
}
