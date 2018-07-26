import * as React from 'react';
import { ZoneApiModel } from '../../../data/api-models/ZoneApiModel';
import { ZoneTableHeader } from './ZoneTableHeader';
import { ZoneTableRow } from './ZoneTableRow';
import { ZoneTableFooter } from './ZoneTableFooter';

interface IZoneListProps {
    zones: ZoneApiModel[]
}

export const ZoneListBoxBody = (props: IZoneListProps) => {
    return (
        <div className="box-body table-responsive no-padding">
            <table className="table table-hover">
                <tbody>
                    <ZoneTableHeader />
                    {
                        props.zones.map((zone) =>
                            <ZoneTableRow {...zone} />
                    )}
                    <ZoneTableFooter />
                </tbody>
            </table>
        </div>
    );
}