import * as React from 'react';
import { ScheduleTableHeader } from './ScheduleTableHeader';
import { ScheduleTableRow } from './ScheduleTableRow';
import { ScheduleTableFooter } from './ScheduleTableFooter';
import Schedule from 'src/data/irrigation/schedule/Schedule';

interface IScheduleListProps {
    schedules: Schedule[];
    onEditScheduleClick: any;
}

export const ScheduleListBoxBody = (props: IScheduleListProps) => {
    return (
        <div className="box-body table-responsive no-padding">
            <table className="table table-hover">
                <tbody>
                    <ScheduleTableHeader />
                    {
                        props.schedules.map((schedule) =>
                            <ScheduleTableRow {...schedule}
                                onEditScheduleClick={(e: any, id: string) => props.onEditScheduleClick(e, id)}
                            />
                    )}
                    <ScheduleTableFooter />
                </tbody>
            </table>
        </div>
    );
}