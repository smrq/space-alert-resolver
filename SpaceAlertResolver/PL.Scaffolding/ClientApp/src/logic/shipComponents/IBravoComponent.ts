export default interface IBravoComponent extends IDamageableComponent, IEnergyProvider {
    performBAction(isHeroic: boolean): void;
	readonly energyInComponent: number;
    readonly capacity: number;
}
