import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { ZoneList } from './ZoneList';
import { ZoneRepository } from '../../../data/ZoneRepository';
import { ZoneApiModel } from '../../../data/api-models/ZoneApiModel';
import { Zone } from '../../../data/Zone';
import AddZoneDialog, { IAddZoneDialogProps } from 'src/components/irrigation/zones/AddZoneDialog';


export class Zones extends React.Component<RouteComponentProps<{}>, {}> {
    state: {
        zones: ZoneApiModel[],
        addZoneDialogProps: IAddZoneDialogProps
    }
    constructor(props: RouteComponentProps<{}>) {
        super(props);
        this.state = {
            zones: new Array<ZoneApiModel>(),
            addZoneDialogProps: {
                visible: false,
                handleSubmit: this.submitAddZone,
                pristine: true,
                reset: this.resetAddZoneDialog,
                submitting: false,
                closeDialog: this.closeAddZoneDialog
            }
        };
        let repo = new ZoneRepository();
        repo.getAll().then((data) => {
            this.state.zones = data;
        });

    }

    private loadZones() {
        let repo = new ZoneRepository();
        repo.getAll().then((data) => {
            this.setState({ zones: data });
        });
    }

    private onAddZoneClick() {
        this.setState({});
    }


    private submitAddZone() {

    }

    private resetAddZoneDialog() {

    }

    private closeAddZoneDialog() {

    }

    //componentDidMount() {
    //    this.loadZones();
    //};
    
    //componentWillReceiveProps(nextProps: any) {
    //    this.loadZones();
    //};



    public render() {
        return <ZoneList zones={this.state.zones} onAddZoneClick={() => this.onAddZoneClick} />
            <AddZoneDialog props={this.state.addZoneDialogProps} />
    }
}