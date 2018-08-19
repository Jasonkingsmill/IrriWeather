import * as React from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { ScheduleList } from './ScheduleList';
import { FormEvent, ChangeEvent } from 'react';
import Schedule from 'src/data/irrigation/schedule/Schedule';
import AddScheduleDialog, { IAddScheduleDialogProps } from 'src/components/irrigation/schedule/AddScheduleDialog';
import EditScheduleDialog, { IEditScheduleDialogProps } from 'src/components/irrigation/schedule/EditScheduleDialog';
import { ScheduleRepository } from 'src/data/irrigation/schedule/ScheduleRepository';
import { ScheduleService } from 'src/data/irrigation/schedule/ScheduleService';
import AddScheduleApiModel from 'src/data/irrigation/schedule/api-models/AddScheduleApiModel';
import UpdateScheduleApiModel from 'src/data/irrigation/schedule/api-models/UpdateScheduleApiModel';


export class Schedules extends React.Component<RouteComponentProps<{}>, {}> {
    state: {
        schedules: Schedule[],
        addScheduleDialogProps: IAddScheduleDialogProps,
        editScheduleDialogProps: IEditScheduleDialogProps
    }

    constructor(props: RouteComponentProps<{}>) {
        super(props);
        this.repo = new ScheduleRepository();
        this.state = {
            schedules: new Array<Schedule>(),
            addScheduleDialogProps: this.getInitialAddScheduleDialogState(),
            editScheduleDialogProps: this.getInitialEditScheduleDialogState()
        };
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
            scheduleChannel: "",
            scheduleDescription: "",
            scheduleEnabled: true,
            scheduleName: ""
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
            scheduleChannel: "",
            scheduleDescription: "",
            scheduleEnabled: true,
            scheduleName: "",
            scheduleId: ""
        }
    }


    private repo: ScheduleRepository;



    private loadSchedules() {
        this.repo.getAll().then((data) => {
            this.setState({ schedules: data });
        });
    }





    private handleOnStartStopClick(e: any, id: string) {
        e.preventDefault();
        let schedule = this.state.schedules.find((a) => a.id == id) as Schedule;
        let service = new ScheduleService();
        if (schedule.isStarted) {
            service.stopSchedule(id)
                .catch()
                .then(() => this.loadSchedules());
        }
        else {
            service.startSchedule(id)
                .catch()
                .then(() => this.loadSchedules());
        }
    }





    private onAddScheduleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        var addScheduleDialogProps = { ...this.state.addScheduleDialogProps };
        addScheduleDialogProps[e.target.name] = e.target.value;
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
            channel: parseInt(form.scheduleChannel),
            description: form.scheduleDescription,
            isEnabled: form.scheduleEnabled,
            name: form.scheduleName
        } as AddScheduleApiModel;
        this.repo.add(model)
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
        editScheduleDialogProps.scheduleChannel = schedule.channel.toString();
        editScheduleDialogProps.scheduleDescription = schedule.description;
        editScheduleDialogProps.scheduleEnabled = schedule.isEnabled;
        editScheduleDialogProps.scheduleName = schedule.name;
        this.setState({ editScheduleDialogProps });
    }
    private handleEditScheduleSubmit(event: React.FormEvent<HTMLInputElement>) {
        event.preventDefault();
        let form = this.state.editScheduleDialogProps;

        let model = {
            channel: parseInt(form.scheduleChannel),
            description: form.scheduleDescription,
            isEnabled: form.scheduleEnabled,
            name: form.scheduleName
        } as UpdateScheduleApiModel;
        this.repo.update(form.scheduleId, model)
            .catch(() => alert("Error updating schedule"))
            .then(() => {
                this.resetEditScheduleDialog();
                this.loadSchedules();
            });
    }

    private handleRemoveScheduleClick() {
        let form = this.state.editScheduleDialogProps;

        this.repo.remove(form.scheduleId)
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

    componentDidMount() {
        this.loadSchedules();
    };

    //componentWillReceiveProps(nextProps: any) {
    //    this.loadSchedules();
    //};



    public render() {
        return <div className="row">
            <ScheduleList
                schedules={this.state.schedules}
                onAddScheduleClick={() => this.onAddScheduleClick()}
                onStartStopClick={(e: any, id: string) => this.handleOnStartStopClick(e, id)}
                onEditScheduleClick={(e: any, id: string) => this.handleOnEditScheduleClick(e, id)}
            />
            <AddScheduleDialog  {...this.state.addScheduleDialogProps} />
            <EditScheduleDialog  {...this.state.editScheduleDialogProps} />
        </div>

    }
}