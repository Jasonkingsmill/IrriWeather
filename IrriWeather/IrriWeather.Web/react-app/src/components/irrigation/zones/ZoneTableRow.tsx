import * as React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'react-bootstrap';

interface IZoneTableRowProps {
    id: number;
    name: string;
    description: string;
    channel: number;
    isEnabled: boolean;
    running: boolean;
}

export class ZoneTableRow extends React.Component<IZoneTableRowProps, {}> {
    
    public render() {
        return (
            <tr>
                <td>{this.props.name}</td>
                <td>{this.props.description}</td>
                <td>{this.props.channel}</td>
                <td>{this.props.isEnabled.toString()}</td>
                <td>{this.props.running ? 'OFF' : 'ON'}</td>
                <td><span className="btn btn-default" ><Link to={'/irrigation/zones/' + this.props.id}>Edit</Link></span></td>
            </tr>
            );
    }
}
