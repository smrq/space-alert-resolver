import EnergyContainer from './EnergyContainer';

export default abstract class Reactor extends EnergyContainer {
    constructor(capacity: number, energy: number) {
        super(capacity, energy);
    }

    drain(amount?: number): number
    {
        const oldEnergy = this.energy;
        this.energy -= amount == null ? 0 : this.energy;
        const currentEnergy = this.energy;
        return oldEnergy - currentEnergy;
    }
}
