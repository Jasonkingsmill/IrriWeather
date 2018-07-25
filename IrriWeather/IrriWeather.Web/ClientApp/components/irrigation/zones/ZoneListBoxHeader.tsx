import * as React from 'react';


export const ZoneListBoxHeader = () => {
    return (
        <div className="box-header">
            <h3 className="box-title">Zone List</h3>

            <div className="box-tools">
                <div className="input-group input-group-sm" style={{ width: 150 }}>
                    <input type="text" name="table_search" className="form-control pull-right" placeholder="Search" />

                    <div className="input-group-btn">
                        <button type="submit" className="btn btn-default"><i className="fa fa-search"></i></button>
                    </div>
                </div>
            </div>
        </div>
    );
}
