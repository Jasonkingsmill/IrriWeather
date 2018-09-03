import * as React from 'react';
import { Button, Modal } from 'react-bootstrap';
import { ScheduleType } from 'src/data/irrigation/schedule/api-models/ScheduleType';
import Zone from 'src/data/irrigation/zones/Zone';
import * as Cleave from 'cleave.js/react';
import { ChangeEvent } from 'react';


export interface IAddScheduleDialogProps {
    visible: boolean;
    handleSubmit: any;
    pristine: boolean;
    reset: any;
    submitting: boolean;
    closeDialog: any;
    handleOnChange: any;
    handleOnZoneSelectChange: any;
    getZones: () => Array<Zone>;
    scheduleName: string;
    scheduleDescription: string;
    scheduleType: ScheduleType;
    scheduleStartTime: string;
    scheduleStartDate: string;
    scheduleDuration: string;
    scheduleEnabledUntil: string;
    scheduleDays: string;
    scheduleZoneIds: Array<string>;
    scheduleIsEnabled: boolean;

}

export let AddScheduleDialog: any = (props: IAddScheduleDialogProps) => {
    let getForm = (scheduleType: ScheduleType): any => {
        switch (scheduleType) {
            case ScheduleType.DaysOfMonth:
            case ScheduleType.DaysOfWeek:
                return (
                    <div className='form-group' >
                        <label htmlFor='scheduleDays' className='col-sm-4 control-label'>Days</label>
                        <div>
                            <input
                                name="scheduleDays"
                                type="text"
                                placeholder="Day numbers: '1,2,3'"
                                autoComplete="off"
                                onChange={props.handleOnChange}
                                value={props.scheduleDays}
                            />
                        </div>
                    </div>
                );
            default:
                break;
        }
    }


    return (
        <div className='box'>
            <Modal bsSize='large' show={props.visible} onHide={props.closeDialog} >
                <form onSubmit={props.handleSubmit} className='form-horizontal' >
                    <Modal.Header closeButton>
                        <Modal.Title>Add New Schedule</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <div className='form-group' >
                            <label htmlFor='scheduleName' className='col-sm-4 control-label'>Name</label>
                            <div>
                                <input
                                    name="scheduleName"
                                    type="text"
                                    placeholder=""
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.scheduleName}
                                />
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='scheduleDescription' className='col-sm-4 control-label'>Description</label>
                            <div>
                                <input
                                    name="scheduleDescription"
                                    type="text"
                                    placeholder=""
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.scheduleDescription}
                                />
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='scheduleType' className='col-sm-4 control-label'>Type</label>
                            <div>
                                <select
                                    name="scheduleType"
                                    onChange={props.handleOnChange}
                                    
                                >
                                    {Object.keys(ScheduleType).map((value: string, index: number) => {
                                        return <option key={index} value={value}>{value}</option>
                                    })}
                                </select>
                            </div>
                        </div>


                        {getForm(props.scheduleType)}


                        <div className='form-group' >
                            <label htmlFor='scheduleStartDate' className='col-sm-4 control-label'>Start Date</label>
                            <div>
                                <input
                                    name="scheduleStartDate"
                                    type="date"
                                    placeholder=""
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.scheduleStartDate}
                                />
                            </div>
                        </div>

                        <div className='form-group' >
                            <label htmlFor='scheduleStartTime' className='col-sm-4 control-label'>Start Time</label>
                            <div>
                                <input
                                    name="scheduleStartTime"
                                    type="time"
                                    placeholder=""
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.scheduleStartTime}
                                />
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='scheduleDuration' className='col-sm-4 control-label'>Duration</label>
                            <div>
                                <Cleave
                                    name="scheduleDuration"
                                    placeholder="Enter duration"
                                    options={{ time: true, timePattern: ['h', 'm', 's'] }}
                                    onChange={props.handleOnChange}
                                    value={props.scheduleDuration}
                                />
                                
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='scheduleZoneIds' className='col-sm-4 control-label'>Zones</label>
                            <div>
                                <select
                                    name="scheduleZoneIds"
                                    multiple
                                    onChange={props.handleOnZoneSelectChange} >
                                    <option key="none" disabled selected style={{ display: "none" }} ></option>
                                    {props.getZones().map((zone: Zone) => {
                                        return <option key={zone.id} value={zone.id}>{zone.name}</option>
                                    })}
                                    </select>
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='scheduleEnabledUntil' className='col-sm-4 control-label'>Enable Until</label>
                            <div>
                                <input
                                    name="scheduleEnabledUntil"
                                    type="date"
                                    placeholder=""
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.scheduleEnabledUntil}
                                />
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='scheduleIsEnabled' className='col-sm-4 control-label'>Enabled</label>
                            <div>
                                <input
                                    name="scheduleIsEnabled"
                                    type="checkbox"
                                    onChange={props.handleOnChange}
                                    checked={props.scheduleIsEnabled}
                                />
                            </div>
                        </div>
                    </Modal.Body>
                    <Modal.Footer>
                        <button className="btn btn-default pull-left" type='button' onClick={props.closeDialog}>Close</button>
                        <button className="btn btn-primary" type='submit' >Save</button>
                    </Modal.Footer>
                </form>
            </Modal>
        </div>
    );
}

export default AddScheduleDialog;


//disabled={props.pristine || props.submitting}