import React from 'react';
import { Input } from 'reactstrap';
import { API_URL } from '../apiurl';

class OutputForm extends React.Component {
    state = {
        output: ''
    }
    componentDidMount() {
        this.getOutput();
    }
    getOutput = () => {
        fetch(`${API_URL}/${this.props.max}`)
            .then(res => res.json())
            .then(res => this.setState({ output: res.output }))
            .catch(err => console.log(err));
    }
    render() {
        return (
            <Input
                readOnly
                className="outputForm"
                type="textarea"
                name="output"
                rows="15"
                value={this.state.output}
            />
        );
    }
}

export default OutputForm;