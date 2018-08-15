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


export class Zones extends React.Component<RouteComponentProps<{}>, {}> {
    state: {
        zones: ZoneApiModel[],
        addZoneDialogProps: IAddZoneDialogProps
    }

    private repo: ZoneRepository;

    constructor(props: RouteComponentProps<{}>) {
        super(props);
        this.repo = new ZoneRepository();
        this.state = {
            zones: new Array<Zone>(),
            addZoneDialogProps: {
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
        };
    }

    private loadZones() {
        this.repo.getAll().then((data) => {
            this.setState({ zones: data });
        });
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
                this.closeAddZoneDialog();
                this.loadZones();
            });

    }

    private resetAddZoneDialog() {

    }

    private closeAddZoneDialog() {
        var addZoneDialogProps = { ...this.state.addZoneDialogProps }
        addZoneDialogProps.visible = false;
        this.setState({ addZoneDialogProps });
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
            <ZoneList zones={this.state.zones} onAddZoneClick={() => this.onAddZoneClick()} onStartStopClick={(e: any, id: string) => this.handleOnStartStopClick(e,id)} />
            <AddZoneDialog  {...this.state.addZoneDialogProps} />
        </div>

    }
}