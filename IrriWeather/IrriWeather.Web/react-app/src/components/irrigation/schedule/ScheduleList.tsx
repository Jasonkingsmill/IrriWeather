import * as React from 'react';
import { ScheduleListBoxHeader } from './ScheduleListBoxHeader';
import { ScheduleListBoxBody } from './ScheduleListBoxBody';
import { ScheduleListBoxFooter } from './ScheduleListBoxFooter';
import Schedule from 'src/data/irrigation/schedule/Schedule';
import Zone from 'src/data/irrigation/zones/Zone';


interface IScheduleListProps {
    schedules: Schedule[],
    getZones: () => Array<Zone>;
    onAddScheduleClick: any,
    onEditScheduleClick: any
}

export class ScheduleList extends React.Component<IScheduleListProps, {}> {
    constructor(props: IScheduleListProps) {
        super(props);
    }

    public componentDidMount() {
        document.title = 'Schedule List';
    }

    public render() {
        return (
            <div className="row">
                <div className="col-xs-12 col-md-12">
                    <div className="col-xs-12">
                        <div className="box">
                            <ScheduleListBoxHeader />
                            <ScheduleListBoxBody
                                schedules={this.props.schedules}
                                getZones={this.props.getZones}
                                onEditScheduleClick={(e: any, id: string) => this.props.onEditScheduleClick(e, id)}
                            />
                            <ScheduleListBoxFooter onAddScheduleClick={this.props.onAddScheduleClick} />
                        </div>
                    </div>
                </div>
            </div>
        );
    }


}

