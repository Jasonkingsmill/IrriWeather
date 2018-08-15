import * as React from 'react';
import { ZoneTableHeader } from './ZoneTableHeader';
import { ZoneTableRow } from './ZoneTableRow';
import { ZoneTableFooter } from './ZoneTableFooter';
import Zone from 'src/data/Zone';

interface IZoneListProps {
    zones: Zone[];
    onStartStopClick: any;
}

export const ZoneListBoxBody = (props: IZoneListProps) => {
    return (
        <div className="box-body table-responsive no-padding">
            <table className="table table-hover">
                <tbody>
                    <ZoneTableHeader />
                    {
                        props.zones.map((zone) =>
                            <ZoneTableRow {...zone} onStartStopClick={(e: any, id: string) => props.onStartStopClick(e,id)} />
                    )}
                    <ZoneTableFooter />
                </tbody>
            </table>
        </div>
    );
}