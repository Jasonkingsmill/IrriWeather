import * as React from 'react';
import { ZoneListBoxHeader } from './ZoneListBoxHeader';
import { ZoneListBoxBody } from './ZoneListBoxBody';
import { ZoneListBoxFooter } from './ZoneListBoxFooter';
import { ZoneApiModel } from '../../../data/api-models/ZoneApiModel';
import Zone from 'src/data/Zone';


interface IZoneListProps {
    zones: Zone[],
    onAddZoneClick: any,
    onStartStopClick: any,
    onEditZoneClick: any
}

export class ZoneList extends React.Component<IZoneListProps, {}> {
    constructor(props: IZoneListProps) {
        super(props);
    }

    public componentDidMount() {
        document.title = 'Zone List';
    }

    public render() {
        return (
            <div className="row">
                <div className="col-xs-12 col-md-12">
                    <div className="col-xs-12">
                        <div className="box">
                            <ZoneListBoxHeader />
                            <ZoneListBoxBody
                                zones={this.props.zones}
                                onStartStopClick={(e: any, id: string) => this.props.onStartStopClick(e, id)}
                                onEditZoneClick={(e: any, id: string) => this.props.onEditZoneClick(e, id)}
                            />
                            <ZoneListBoxFooter onAddZoneClick={this.props.onAddZoneClick} />
                        </div>
                    </div>
                </div>
            </div>
        );
    }


}

