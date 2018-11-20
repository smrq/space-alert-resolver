import Reactor from './Reactor';
import Shield from './Shield';

export default class CentralShield extends Shield
{
	constructor(source: Reactor) {
		super(source, 3, 1);
	}
}
