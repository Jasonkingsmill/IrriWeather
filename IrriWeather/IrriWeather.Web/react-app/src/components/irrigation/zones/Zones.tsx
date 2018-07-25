import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { ZoneList } from './ZoneList';
import { ZoneRepository as repo } from '../../../data/zoneRepository';
import { ZoneApiModel } from '../../../data/api-models/ZoneApiModel';


export class Zones extends React.Component<RouteComponentProps<{}>, {}> {
    state: {
        zones: ZoneApiModel[]
    }

    constructor(props: RouteComponentProps<{}>) {
        super(props);
        this.state.zones.push(;
        //var res = repo.prototype.getAll();
        //res.then((data) => {
        //    this.state.zones = data;
        //});
        //res.catch((err) => {
        //    throw new Error('');
        //});
    }
    componentWillMount() {

    }
    componentDidMount() {
        repo.default.prototype.getAll();
    }
    public render() {
        return <ZoneList {this.state.zones}/>
    }
}