import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { ZoneList } from './ZoneList';
import { ZoneRepository } from '../../../data/ZoneRepository';
import { ZoneApiModel } from '../../../data/api-models/ZoneApiModel';


export class Zones extends React.Component<RouteComponentProps<{}>, {}> {
    state: {
        zones: Array<ZoneApiModel>
    }
    constructor(props: RouteComponentProps<{}>) {
        super(props);
        this.state = {
            zones: new Array<ZoneApiModel>()
        }
    }

    private loadZones() {
        let repo = new ZoneRepository('');
        repo.getAll().then((data) => {
            this.setState({ zones: data });
        });
    }

    componentDidMount() {
        this.loadZones();
    };
    
    componentWillReceiveProps(nextProps: any) {
        this.loadZones();
    };



    public render() {
        return <ZoneList zones={this.state.zones} />
    }
}