import * as React from 'react';
import { ZoneSummaryViewModel } from '../../view-models/ZoneSummaryViewModel';
import { ZoneTableHeader } from './ZoneTableHeader';
import { ZoneTableRow } from './ZoneTableRow';
import { ZoneTableFooter } from './ZoneTableFooter';

interface ZoneListBodyProps {
    zones: Array<ZoneSummaryViewModel >;
}

export const ZoneListBoxBody = (props: ZoneListBodyProps) => {
    return (
        <div className="box-body table-responsive no-padding">
            <table className="table table-hover">
                <tbody>
                    <ZoneTableHeader />
                    {props.zones.map(zone =>
                        <ZoneTableRow key={zone.id}
                            id={zone.id}
                            name={zone.name}
                            email={zone.email}
                            status={zone.isActive ? 'Active' : 'Inactive'}
                            agentCount={zone.agentCount}
                            workflowCount={zone.workflowCount}
                        />
                    )}
                    <ZoneTableFooter />
                </tbody>
            </table>
        </div>
    );
}