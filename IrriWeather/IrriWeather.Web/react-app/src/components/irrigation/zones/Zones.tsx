import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { ZoneList } from './ZoneList';
import { ZoneRepository } from '../../../data/ZoneRepository';
import { ZoneApiModel } from '../../../data/api-models/ZoneApiModel';
import { Zone } from '../../../data/Zone';
import { AddZoneDialog, IAddZoneDialogProps } from 'src/components/irrigation/zones/AddZoneDialog';
import { AddZoneApiModel } from 'src/data/api-models/AddZoneApiModel';
import { FormEvent, ChangeEvent } from 'react';
import { ZoneService } from 'src/data/ZoneService';
import EditZoneDialog, { IEditZoneDialogProps } from 'src/components/irrigation/zones/EditZoneDialog';
import UpdateZoneApiModel from 'src/data/api-models/UpdateZoneApiModel';


export class Zones extends React.Component<RouteComponentProps<{}>, {}> {
    state: {
        zones: Zone[],
        addZoneDialogProps: IAddZoneDialogProps,
        editZoneDialogProps: IEditZoneDialogProps
    }

    constructor(props: RouteComponentProps<{}>) {
        super(props);
        this.repo = new ZoneRepository();
        this.state = {
            zones: new Array<Zone>(),
            addZoneDialogProps: this.getInitialAddZoneDialogState(),
            editZoneDialogProps: this.getInitialEditZoneDialogState()
        };
    }


    private getInitialAddZoneDialogState(): IAddZoneDialogProps {
        return {
            visible: false,
            handleSubmit: (e: any) => this.handleAddZoneSubmit(e),
            pristine: true,
            reset: () => this.resetAddZoneDialog(),
            submitting: false,
            closeDialog: () => this.closeAddZoneDialog(),
            handleOnChange: this.onAddZoneChange,
            zoneChannel: "",
            zoneDescription: "",
            zoneEnabled: true,
            zoneName: ""
        }
    }

    private getInitialEditZoneDialogState(): IEditZoneDialogProps {
        return {
            visible: false,
            handleSubmit: (e: any) => this.handleEditZoneSubmit(e),
            pristine: true,
            submitting: false,
            closeDialog: () => this.closeEditZoneDialog(),
            removeZone: () => this.handleRemoveZoneClick(),
            handleOnChange: this.onEditZoneChange,
            zoneChannel: "",
            zoneDescription: "",
            zoneEnabled: true,
            zoneName: "",
            zoneId: ""
        }
    }


    private repo: ZoneRepository;



    private loadZones() {
        this.repo.getAll().then((data) => {
            this.setState({ zones: data });
        });
    }





    private handleOnStartStopClick(e: any, id: string) {
        e.preventDefault();
        let zone = this.state.zones.find((a) => a.id == id) as Zone;
        let service = new ZoneService();
        if (zone.isStarted) {
            service.stopZone(id)
                .catch()
                .then(() => this.loadZones());
        }
        else {
            service.startZone(id)
                .catch()
                .then(() => this.loadZones());
        }
    }





    private onAddZoneChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        var addZoneDialogProps = { ...this.state.addZoneDialogProps };
        addZoneDialogProps[e.target.name] = e.target.value;
        this.setState({ addZoneDialogProps });
    }



    private onAddZoneClick() {
        var addZoneDialogProps = { ...this.state.addZoneDialogProps }
        addZoneDialogProps.visible = true;
        this.setState({ addZoneDialogProps });
    }

    private handleAddZoneSubmit(event: React.FormEvent<HTMLInputElement>) {
        event.preventDefault();
        let form = this.state.addZoneDialogProps;

        let model = {
            channel: parseInt(form.zoneChannel),
            description: form.zoneDescription,
            isEnabled: form.zoneEnabled,
            name: form.zoneName
        } as AddZoneApiModel;
        this.repo.add(model)
            .catch(() => alert("Error"))
            .then(() => {
                this.resetAddZoneDialog();
                this.loadZones();
            });
    }
    

    private resetAddZoneDialog() {
        var addZoneDialogProps = this.getInitialAddZoneDialogState();
        this.setState({ addZoneDialogProps });
    }



    private closeAddZoneDialog() {
        var addZoneDialogProps = { ...this.state.addZoneDialogProps }
        addZoneDialogProps.visible = false;
        this.setState({ addZoneDialogProps });
        this.loadZones();
    }







    private onEditZoneChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        var editZoneDialogProps = { ...this.state.editZoneDialogProps };
        editZoneDialogProps[e.target.name] = e.target.value;
        this.setState({ editZoneDialogProps });
    }
    private handleOnEditZoneClick(e: any, id: string) {
        var editZoneDialogProps = { ...this.state.editZoneDialogProps }
        let zone = this.state.zones.find(z => z.id == id) as Zone;
        editZoneDialogProps.visible = true;
        editZoneDialogProps.zoneId = zone.id;
        editZoneDialogProps.zoneChannel = zone.channel.toString();
        editZoneDialogProps.zoneDescription = zone.description;
        editZoneDialogProps.zoneEnabled = zone.isEnabled;
        editZoneDialogProps.zoneName = zone.name;
        this.setState({ editZoneDialogProps });
    }
    private handleEditZoneSubmit(event: React.FormEvent<HTMLInputElement>) {
        event.preventDefault();
        let form = this.state.editZoneDialogProps;

        let model = {
            channel: parseInt(form.zoneChannel),
            description: form.zoneDescription,
            isEnabled: form.zoneEnabled,
            name: form.zoneName
        } as UpdateZoneApiModel;
        this.repo.update(form.zoneId, model)
            .catch(() => alert("Error updating zone"))
            .then(() => {
                this.resetEditZoneDialog();
                this.loadZones();
            });
    }

    private handleRemoveZoneClick() {
        let form = this.state.editZoneDialogProps;

        this.repo.remove(form.zoneId)
            .catch(() => alert("Error removing zone"))
            .then(() => {
                this.resetEditZoneDialog();
                this.loadZones();
            });
    }
    private resetEditZoneDialog() {
        var editZoneDialogProps = this.getInitialEditZoneDialogState();
        this.setState({ editZoneDialogProps });
    }
    private closeEditZoneDialog() {
        var editZoneDialogProps = { ...this.state.editZoneDialogProps }
        editZoneDialogProps.visible = false;
        this.setState({ editZoneDialogProps });
        this.loadZones();
    }

    componentDidMount() {
        this.loadZones();
    };

    //componentWillReceiveProps(nextProps: any) {
    //    this.loadZones();
    //};



    public render() {
        return <div className="row">
            <ZoneList
                zones={this.state.zones}
                onAddZoneClick={() => this.onAddZoneClick()}
                onStartStopClick={(e: any, id: string) => this.handleOnStartStopClick(e, id)}
                onEditZoneClick={(e: any, id: string) => this.handleOnEditZoneClick(e, id)}
            />
            <AddZoneDialog  {...this.state.addZoneDialogProps} />
            <EditZoneDialog  {...this.state.editZoneDialogProps} />
        </div>

    }
}