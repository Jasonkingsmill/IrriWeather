import * as React from 'react';
import { ZoneListBoxHeader } from './ZoneListBoxHeader';
import { ZoneListBoxBody } from './ZoneListBoxBody';
import { ZoneListBoxFooter } from './ZoneListBoxFooter';
import { ZoneApiModel } from '../../../data/api-models/ZoneApiModel';


interface IZoneListProps {
    zones: ZoneApiModel[]
}

export class ZoneList extends React.Component<IZoneListProps, {}> {
    constructor(props: IZoneListProps) {
        super(props);
        this.addZone = this.addZone.bind(this);
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
                            <ZoneListBoxBody zones={this.props.zones} />
                            <ZoneListBoxFooter onAddZone={this.addZone} />
                        </div>
                    </div>
                </div>
            </div>
        );
    }

    public addZone() {
        //this.props.addZone();
    }

}

