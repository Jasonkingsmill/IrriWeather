import * as React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';

interface IZoneTableRowProps {
    id: string;
    name: string;
    description: string;
    channel: number;
    isEnabled: boolean;
    isStarted: boolean;
    onStartStopClick: any;
    onEditZoneClick: any;
}

export class ZoneTableRow extends React.Component<IZoneTableRowProps, {}> {

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
                    <Button  onClick={(e) => { this.props.onEditZoneClick(e, this.props.id) }} >Edit</Button>
                </td>
            </tr>
        );
    }
}
