import * as React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';

interface IScheduleTableRowProps {
    id: string;
    name: string;
    description: string;
    channel: number;
    isEnabled: boolean;
    isStarted: boolean;
    onStartStopClick: any;
    onEditScheduleClick: any;
}

export class ScheduleTableRow extends React.Component<IScheduleTableRowProps, {}> {

    public render() {
        return (
            <tr>
                <td>{this.props.name}</td>
                <td>{this.props.description}</td>
                <td>{this.props.channel}</td>
                <td>{this.props.isEnabled.toString()}</td>
                <td>{this.props.isStarted ? 'Started' : 'Stopped'}
                    <Button bsStyle={this.props.isStarted ? 'danger' : 'primary'} onClick={(e) => { this.props.onStartStopClick(e, this.props.id) }} > {this.props.isStarted ? 'STOP' : 'START'}
                    </Button>
                </td>
                <td>
                    <Button  onClick={(e) => { this.props.onEditScheduleClick(e, this.props.id) }} >Edit</Button>
                </td>
            </tr>
        );
    }
}
