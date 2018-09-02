import * as React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import ScheduleType from 'src/data/irrigation/schedule/api-models/ScheduleType';
import { TimeSpan } from 'src/data/TimeSpan';
import Zone from 'src/data/irrigation/zones/Zone';

interface IScheduleTableRowProps {
    id: string;
    name: string;
    description: string;
    scheduleType: ScheduleType;
    startTime: string;
    startDate: string;
    duration: string;
    enabledUntil: string;
    days: Array<number>;
    getZones: () => Array<Zone>;
    zoneIds: Array<string>;
    isEnabled: boolean;
    onEditScheduleClick: any;
}

export class ScheduleTableRow extends React.Component<IScheduleTableRowProps, {}> {

    public render() {
        return (
            <tr>
                <td>{this.props.name}</td>
                <td>{this.props.description}</td>
                <td>{this.props.scheduleType}</td>
                <td>
                    {
                        this.props.getZones().map((zone: Zone) => {
                            if (this.props.zoneIds.find((id) => id == zone.id)) {
                                return <div>{zone.name}</div>;
                            }
                            else
                                return <div></div>
                        })
                    }
                </td>

                {this.props.scheduleType == ScheduleType.DateTime &&
                    <td>{this.props.startDate}</td>
                }
                {(this.props.scheduleType == ScheduleType.DaysOfMonth || this.props.scheduleType == ScheduleType.DaysOfWeek) &&
                    <td>{this.props.days.join(",")}</td>
                }
                {this.props.scheduleType == ScheduleType.EvenDays &&
                    <td>Even Days</td>
                }
                {this.props.scheduleType == ScheduleType.OddDays &&
                    <td>Odd Days</td>
                }
                <td>{this.props.startTime}</td>
                <td>{this.props.duration}</td>
                <td>{this.props.isEnabled.toString()}</td>
                <td>
                    <Button onClick={(e) => { this.props.onEditScheduleClick(e, this.props.id) }} >Edit</Button>
                </td>
            </tr>
        );
    }
}
