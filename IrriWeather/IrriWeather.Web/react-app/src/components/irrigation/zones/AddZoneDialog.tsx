import * as React from 'react';
import { Button, Modal } from 'react-bootstrap';

export interface IAddZoneDialogProps {
    visible: boolean;
    handleSubmit: any;
    pristine: boolean;
    reset: any;
    submitting: boolean;
    closeDialog: any;
}

let AddZoneDialog: any = (props: IAddZoneDialogProps) => {
    return (
        <div className='box'>
            <Modal bsSize='large' show={props.visible} onHide={props.closeDialog} >
                <form onSubmit={props.handleSubmit} className='form-horizontal' >
                    <Modal.Header closeButton>
                        <Modal.Title>Add New Zone</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <div className='form-group' >
                            <label htmlFor='zoneName' className='col-sm-4 control-label'>Zone Name</label>
                            <div>
                                <input
                                    name="zoneName"
                                    type="text"
                                    placeholder="Client"
                                />
                            </div> 
                        </div>
                        <div className='form-group' >
                            <label htmlFor='description' className='col-sm-4 control-label'>Description</label>
                            <div>
                                <input
                                    name="description"
                                    type="text"
                                    placeholder="Description"
                                />
                            </div>
                        </div>
                        <div className='form-group' >
                            <label htmlFor='channel' className='col-sm-4 control-label'>Channel Number</label>
                            <div>
                                <input
                                    name="channel"
                                    type="number"
                                    placeholder="0-31"
                                />
                            </div>
                        </div>
                    </Modal.Body>
                    <Modal.Footer>
                        <button className="btn btn-default pull-left" type='button' onClick={props.closeDialog}>Close</button>
                        <button className="btn btn-primary" type='submit' disabled={props.pristine || props.submitting}>Save</button>
                    </Modal.Footer>
                </form>
            </Modal>
        </div>
    );
}

export default AddZoneDialog;


