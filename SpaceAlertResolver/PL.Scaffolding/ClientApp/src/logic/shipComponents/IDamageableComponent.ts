export default interface IDamageableComponent {
	setDamaged(isCampaignDamage: boolean): void;
    repair(): void;
}
