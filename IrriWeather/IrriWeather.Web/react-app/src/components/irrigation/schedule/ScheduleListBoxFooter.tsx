import * as React from 'react';
import Button from 'reactstrap/lib/Button';

interface IScheduleListBoxFooterProps {
    onAddScheduleClick: any;
}

export const ScheduleListBoxFooter = (props: IScheduleListBoxFooterProps) => {
    return (
        <div className="box-footer clearfix">
            <button type="button" className="btn btn-default pull-right" onClick={props.onAddScheduleClick}>
                Add New Schedule
            </button>
        </div>
    );
}