import * as React from 'react';
import { Button, Modal } from 'react-bootstrap';

export interface IEditZoneDialogProps {
    visible: boolean;
    handleSubmit: any;
    pristine: boolean;
    submitting: boolean;
    closeDialog: any;
    removeZone: any;
    handleOnChange: any;
    zoneId: string;
    zoneName: string;
    zoneDescription: string;
    zoneChannel: string;
    zoneEnabled: boolean;
}

export let EditZoneDialog: any = (props: IEditZoneDialogProps) => {
    return (
        <div className='box'>
            <Modal bsSize='large' show={props.visible} onHide={props.closeDialog} >
                <form onSubmit={props.handleSubmit} className='form-horizontal' >
                    <input name="zoneId" type="text" value={props.zoneId} hidden />
                    <Modal.Header closeButton>
                        <Modal.Title>Edit Zone</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <div className='form-group' >
                            <label htmlFor='zoneName' className='col-sm-4 control-label'>Zone Name</label>
                            <div>
                                <input
                                    name="zoneName"
                                    type="text"
                                    placeholder="Enter zone name"
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.zoneName}
                                />
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='zoneDescription' className='col-sm-4 control-label'>Description</label>
                            <div>
                                <textarea
                                    name="zoneDescription"
                                    placeholder="Enter a description"
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.zoneDescription}
                                />
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='zoneChannel' className='col-sm-4 control-label'>Channel</label>
                            <div>
                                <input
                                    name="zoneChannel"
                                    type="number"
                                    placeholder="Channel No. between 0-31"
                                    autoComplete="off"
                                    onChange={props.handleOnChange}
                                    value={props.zoneChannel}
                                />
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='zoneEnabled' className='col-sm-4 control-label'>Enabled</label>
                            <div>
                                <input
                                    name="zoneEnabled"
                                    type="checkbox"
                                    onChange={props.handleOnChange}
                                    checked={props.zoneEnabled}
                                />
                            </div>
                        </div>
                        <div>
                            <Button bsStyle="danger" onClick={props.removeZone} >
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

export default EditZoneDialog;


//disabled={props.pristine || props.submitting}