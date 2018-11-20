import Reactor from './Reactor';

export default class CentralReactor extends Reactor
{
    private _fuelCapsules: number;

    constructor() {
        super(5, 3);
        this._fuelCapsules = 3;
    }

    get fuelCapsules(): number { return this._fuelCapsules; }
    set fuelCapsules(value: number) { this._fuelCapsules = value < 0 ? 0 : value; }

    performBAction(isHeroic: boolean): void {
        if (this.fuelCapsules <= 0) {
            return;
        }

        const oldEnergy = this.energy;
        fuelCapsules--;
        this.energy = this.capacity;

        if (isHeroic && this.energy > oldEnergy) {
            this.energy++;
        }
    }
}
