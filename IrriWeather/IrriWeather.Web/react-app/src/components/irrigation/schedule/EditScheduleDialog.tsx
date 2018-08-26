import * as React from 'react';
import { Button, Modal } from 'react-bootstrap';

export interface IEditScheduleDialogProps {
    visible: boolean;
    handleSubmit: any;
    pristine: boolean;
    submitting: boolean;
    closeDialog: any;
    removeSchedule: any;
    handleOnChange: any;
    scheduleId: string;
    scheduleName: string;
    scheduleDescription: string;
    scheduleType: string;
    scheduleStartTime: string;
    scheduleStartDate: string;
    scheduleDuration: string;
    scheduleEnabledUntil: string;
    scheduleDays: string;
    scheduleZoneIds: Array<string>;
    scheduleIsEnabled: boolean;
}

export let EditScheduleDialog: any = (props: IEditScheduleDialogProps) => {
    return (
        <div className='box'>
            <Modal bsSize='large' show={props.visible} onHide={props.closeDialog} >
                <form onSubmit={props.handleSubmit} className='form-horizontal' >
                    <input name="scheduleId" type="text" value={props.scheduleId} hidden />
                    <Modal.Header closeButton>
                        <Modal.Title>Edit Schedule</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <div className='form-group' >
                            <label htmlFor='scheduleName' className='col-sm-4 control-label'>Schedule Name</label>
                            <div>
                                <input
                                    name="scheduleName"
                                    type="text"
                                    placeholder="Enter Schedule name"
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
                                    placeholder="Enter a description"
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.scheduleDescription}
                                />
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='scheduleEnabled' className='col-sm-4 control-label'>Enabled</label>
                            <div>
                                <input
                                    name="scheduleEnabled"
                                    type="checkbox"
                                    onChange={props.handleOnChange}
                                    checked={props.scheduleIsEnabled}
                                />
                            </div>
                        </div>
                        <div>
                            <Button bsStyle="danger" onClick={props.removeSchedule} >
                                Remove
                            </Button>
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

export default EditScheduleDialog;


//disabled={props.pristine || props.submitting}