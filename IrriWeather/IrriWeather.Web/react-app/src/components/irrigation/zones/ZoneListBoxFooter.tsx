import * as React from 'react';
import Button from 'reactstrap/lib/Button';

interface IZoneListBoxFooterProps {
    onAddZoneClick: any;
}

export const ZoneListBoxFooter = (props: IZoneListBoxFooterProps) => {
    return (
        <div className="box-footer clearfix">
            <button type="button" className="btn btn-default pull-right" onClick={props.onAddZoneClick}>
                Add New Zone
            </button>
        </div>
    );
}