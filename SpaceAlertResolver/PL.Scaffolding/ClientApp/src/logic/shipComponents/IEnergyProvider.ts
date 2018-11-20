export default interface IEnergyProvider
{
    useEnergy(amount: number): void;
    canUseEnergy(amount: number): bool;
    get energyType() : EnergyType;
    performEndOfTurn(): void;
}
