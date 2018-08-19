import * as React from 'react';
import { Button, Modal } from 'react-bootstrap';

export interface IAddScheduleDialogProps {
    visible: boolean;
    handleSubmit: any;
    pristine: boolean;
    reset: any;
    submitting: boolean;
    closeDialog: any;
    handleOnChange: any;
    scheduleName: string;
    scheduleDescription: string;
    scheduleChannel: string;
    scheduleEnabled: boolean;
}

export let AddScheduleDialog: any = (props: IAddScheduleDialogProps) => {
    return (
        <div className='box'>
            <Modal bsSize='large' show={props.visible} onHide={props.closeDialog} >
                <form onSubmit={props.handleSubmit} className='form-horizontal' >
                    <Modal.Header closeButton>
                        <Modal.Title>Add New Schedule</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <div className='form-group' >
                            <label htmlFor='scheduleName' className='col-sm-4 control-label'>Schedule Name</label>
                            <div>
                                <input
                                    name="scheduleName"
                                    type="text"
                                    placeholder="Enter schedule name"
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
                            <label htmlFor='scheduleChannel' className='col-sm-4 control-label'>Channel</label>
                            <div>
                                <input
                                    name="scheduleChannel"
                                    type="number"
                                    placeholder="Channel No. between 0-31"
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.scheduleChannel}
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
                                    checked={props.scheduleEnabled}
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