import * as React from 'react';
import { Link } from 'react-router-dom';

interface IZoneTableRowProps {
    id: number;
    name: string;
    description: string;
    channel: number;
    isEnabled: boolean;
}

export class ZoneTableRow extends React.Component<IZoneTableRowProps, {}> {
    
    public render() {
        return (
            <tr>
                <td>{this.props.name}</td>
                <td>{this.props.description}</td>
                <td>{this.props.channel}</td>
                <td>{this.props.isEnabled}</td>
                <td><span className="btn btn-default" ><Link to={'/irrigation/zones/' + this.props.id}>Edit</Link></span></td>
            </tr>
            );
    }
}
