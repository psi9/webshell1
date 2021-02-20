import React from 'react';
import { Input } from 'reactstrap';

class OutputForm extends React.Component {

    render() {
        return (
            <Input
                readOnly
                className="outputForm"
                type="textarea"
                name="output"
                rows="15"
            />
        );
    }
}

export default OutputForm;