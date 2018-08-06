import * as React from 'react';
import Button from 'reactstrap/lib/Button';

interface ZoneListBoxFooterProps {
    onAddZone: any;
}

export const ZoneListBoxFooter = (props: ZoneListBoxFooterProps) => {
    return (
        <div className="box-footer clearfix">
            <button type="button" className="btn btn-default pull-right" onClick={() => props.onAddZone()}>
                Add New Zone
            </button>
        </div>
    );
}