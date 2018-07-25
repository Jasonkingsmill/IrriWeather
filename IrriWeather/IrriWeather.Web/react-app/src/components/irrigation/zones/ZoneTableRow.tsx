import * as React from 'react';
import { Link } from 'react-router-dom';

interface ZoneTableRowProps {
    id: number;
    name: string;
    description: string;
    channel: string;
    isEnabled: string;
}

export class ZoneTableRow extends React.Component<ZoneTableRowProps, {}> {

    constructor() {
        super();
    }

    public render() {
        return (
            <tr>
                <td>{this.props.name}</td>
                <td>{this.props.description}</td>
                <td>{this.props.channel}</td>
                <td>{this.props.isEnabled}</td>
                <td><span className="btn btn-default" ><Link to={'/zone/' + this.props.id}>Edit</Link></span></td>
            </tr>
            );
    }
}
