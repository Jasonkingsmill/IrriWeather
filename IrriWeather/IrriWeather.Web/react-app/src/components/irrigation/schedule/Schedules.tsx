import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { ScheduleList } from './ScheduleList';
import { FormEvent, ChangeEvent } from 'react';
import Schedule from 'src/data/irrigation/schedule/Schedule';
import AddScheduleDialog, { IAddScheduleDialogProps } from 'src/components/irrigation/schedule/AddScheduleDialog';
import EditScheduleDialog, { IEditScheduleDialogProps } from 'src/components/irrigation/schedule/EditScheduleDialog';
import { ScheduleRepository } from 'src/data/irrigation/schedule/ScheduleRepository';
import AddScheduleApiModel from 'src/data/irrigation/schedule/api-models/AddScheduleApiModel';
import UpdateScheduleApiModel from 'src/data/irrigation/schedule/api-models/UpdateScheduleApiModel';
import ScheduleType from 'src/data/irrigation/schedule/api-models/ScheduleType';
import Zone from 'src/data/irrigation/zones/Zone';
import { ZoneRepository } from 'src/data/irrigation/zones/ZoneRepository';
import { TimeSpan } from 'src/data/TimeSpan';


export class Schedules extends React.Component<RouteComponentProps<{}>, {}> {
    state: {
        zones: Zone[],
        schedules: Schedule[],
        addScheduleDialogProps: IAddScheduleDialogProps,
        editScheduleDialogProps: IEditScheduleDialogProps
    }

    private scheduleRepo: ScheduleRepository;
    private zoneRepo: ZoneRepository;


    constructor(props: RouteComponentProps<{}>) {
        super(props);
        this.scheduleRepo = new ScheduleRepository();
        this.zoneRepo = new ZoneRepository();
        this.state = {
            zones: new Array<Zone>(),
            schedules: new Array<Schedule>(),
            addScheduleDialogProps: this.getInitialAddScheduleDialogState(),
            editScheduleDialogProps: this.getInitialEditScheduleDialogState()
        };
    }

    componentDidMount() {
        this.loadSchedules();
        this.loadZones();
    };

    private loadSchedules() {
        this.scheduleRepo.getAll().then((data) => {
            this.setState({ schedules: data });
        });
    }
    private loadZones() {
        this.zoneRepo.getAll().then((data) => {
            this.setState({ zones: data });
        });
    }

    private getInitialAddScheduleDialogState(): IAddScheduleDialogProps {
        return {
            visible: false,
            handleSubmit: (e: any) => this.handleAddScheduleSubmit(e),
            pristine: true,
            reset: () => this.resetAddScheduleDialog(),
            submitting: false,
            closeDialog: () => this.closeAddScheduleDialog(),
            handleOnChange: this.onAddScheduleChange,
            handleOnZoneSelectChange: this.onAddScheduleZoneChange,
            getZones: () => this.state.zones,
            scheduleName: "",
            scheduleDescription: "",
            scheduleType: ScheduleType.DaysOfWeek,
            scheduleStartTime: "",
            scheduleStartDate: "",
            scheduleDuration: "00:00:00",
            scheduleEnabledUntil: "2099-12-31",
            scheduleDays: "",
            scheduleZoneIds: new Array<string>(),
            scheduleIsEnabled: true
        }
    }

    private getInitialEditScheduleDialogState(): IEditScheduleDialogProps {
        return {
            visible: false,
            handleSubmit: (e: any) => this.handleEditScheduleSubmit(e),
            pristine: true,
            submitting: false,
            closeDialog: () => this.closeEditScheduleDialog(),
            removeSchedule: () => this.handleRemoveScheduleClick(),
            handleOnChange: this.onEditScheduleChange,
            handleOnZoneSelectChange: this.onAddScheduleZoneChange,
            getZones: () => this.state.zones,
            scheduleId: "",
            scheduleName: "",
            scheduleDescription: "",
            scheduleType: ScheduleType.DaysOfWeek,
            scheduleStartTime: "",
            scheduleStartDate: "",
            scheduleDuration: "00:00:00",
            scheduleEnabledUntil: "2099-12-31",
            scheduleDays: "",
            scheduleZoneIds: new Array<string>(),
            scheduleIsEnabled: true
        }
    }






    private onAddScheduleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        var addScheduleDialogProps = { ...this.state.addScheduleDialogProps };
        addScheduleDialogProps[e.target.name] = e.target.value;
        this.setState({ addScheduleDialogProps });
    }
    private onAddScheduleZoneChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        var addScheduleDialogProps = { ...this.state.addScheduleDialogProps };
        addScheduleDialogProps.scheduleZoneIds = new Array<string>();
        for (let i = 0; i < e.target.selectedOptions.length; i++) {
            addScheduleDialogProps.scheduleZoneIds.push(e.target.selectedOptions[i].value);
        }
        this.setState({ addScheduleDialogProps });
    }



    private onAddScheduleClick() {
        var addScheduleDialogProps = { ...this.state.addScheduleDialogProps }
        addScheduleDialogProps.visible = true;
        this.setState({ addScheduleDialogProps });
    }

    private handleAddScheduleSubmit(event: React.FormEvent<HTMLInputElement>) {
        event.preventDefault();
        let form = this.state.addScheduleDialogProps;

        let model = {
            name: form.scheduleName,       
            description: form.scheduleDescription,
            scheduleType: form.scheduleType,
            duration: form.scheduleDuration,
            enabledUntil: new Date(Date.parse(form.scheduleEnabledUntil)).toISOString(),
            isEnabled: form.scheduleIsEnabled,
            startTime: form.scheduleStartTime,
            zoneIds: form.scheduleZoneIds
        } as Schedule;

        switch (form.scheduleType) {
            case ScheduleType.DateTime:
                model.startDate = new Date(Date.parse(form.scheduleStartDate)).toISOString();
                break;
            case ScheduleType.DaysOfMonth:
            case ScheduleType.DaysOfWeek:
                model.days =  form.scheduleDays.split(",").map<number>((value) => { return parseInt(value); });
                break;
        }

        this.scheduleRepo.add(model)
            .catch(() => alert("Error"))
            .then(() => {
                this.resetAddScheduleDialog();
                this.loadSchedules();
            });
    }


    private resetAddScheduleDialog() {
        var addScheduleDialogProps = this.getInitialAddScheduleDialogState();
        this.setState({ addScheduleDialogProps });
    }



    private closeAddScheduleDialog() {
        var addScheduleDialogProps = { ...this.state.addScheduleDialogProps }
        addScheduleDialogProps.visible = false;
        this.setState({ addScheduleDialogProps });
        this.loadSchedules();
    }







    private onEditScheduleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        var editScheduleDialogProps = { ...this.state.editScheduleDialogProps };
        editScheduleDialogProps[e.target.name] = e.target.value;
        this.setState({ editScheduleDialogProps });
    }
    private handleOnEditScheduleClick(e: any, id: string) {
        var editScheduleDialogProps = { ...this.state.editScheduleDialogProps }
        let schedule = this.state.schedules.find(z => z.id == id) as Schedule;
        editScheduleDialogProps.visible = true;
        editScheduleDialogProps.scheduleId = schedule.id;
        editScheduleDialogProps.scheduleDescription = schedule.description;
        editScheduleDialogProps.scheduleDays = schedule.days.join(",");
        editScheduleDialogProps.scheduleDuration = schedule.duration.toString();
        editScheduleDialogProps.scheduleEnabledUntil = schedule.enabledUntil.toString();
        editScheduleDialogProps.scheduleIsEnabled = schedule.isEnabled;
        editScheduleDialogProps.scheduleName = schedule.name;
        editScheduleDialogProps.scheduleStartDate = schedule.startDate.toString();
        editScheduleDialogProps.scheduleStartTime = schedule.startTime.toString();
        editScheduleDialogProps.scheduleType = schedule.scheduleType;
        editScheduleDialogProps.scheduleZoneIds = schedule.zoneIds;
        this.setState({ editScheduleDialogProps });
    }
    private handleEditScheduleSubmit(event: React.FormEvent<HTMLInputElement>) {
        event.preventDefault();
        let form = this.state.editScheduleDialogProps;

        let model = {
            name: form.scheduleName,
            description: form.scheduleDescription,
            scheduleType: form.scheduleType,
            duration: form.scheduleDuration,
            enabledUntil: new Date(Date.parse(form.scheduleEnabledUntil)).toISOString(),
            isEnabled: form.scheduleIsEnabled,
            startTime: form.scheduleStartTime,
            zoneIds: form.scheduleZoneIds
        } as Schedule;

        switch (form.scheduleType) {
            case ScheduleType.DateTime:
                model.startDate = new Date(Date.parse(form.scheduleStartDate)).toISOString();
                break;
            case ScheduleType.DaysOfMonth:
            case ScheduleType.DaysOfWeek:
                model.days = form.scheduleDays.split(",").map<number>((value) => { return parseInt(value); });
                break;
        }

        this.scheduleRepo.update(form.scheduleId, model)
            .catch(() => alert("Error"))
            .then(() => {
                this.resetEditScheduleDialog();
                this.loadSchedules();
            });
    }

    private handleRemoveScheduleClick() {
        let form = this.state.editScheduleDialogProps;

        this.scheduleRepo.remove(form.scheduleId)
            .catch(() => alert("Error removing schedule"))
            .then(() => {
                this.resetEditScheduleDialog();
                this.loadSchedules();
            });
    }
    private resetEditScheduleDialog() {
        var editScheduleDialogProps = this.getInitialEditScheduleDialogState();
        this.setState({ editScheduleDialogProps });
    }
    private closeEditScheduleDialog() {
        var editScheduleDialogProps = { ...this.state.editScheduleDialogProps }
        editScheduleDialogProps.visible = false;
        this.setState({ editScheduleDialogProps });
        this.loadSchedules();
    }


    public render() {
        return <div className="row">
            <ScheduleList
                schedules={this.state.schedules}
                getZones={() => this.state.zones}
                onAddScheduleClick={() => this.onAddScheduleClick()}
                onEditScheduleClick={(e: any, id: string) => this.handleOnEditScheduleClick(e, id)}
            />
            <AddScheduleDialog  {...this.state.addScheduleDialogProps} />
            <EditScheduleDialog  {...this.state.editScheduleDialogProps} />
        </div>

    }
}