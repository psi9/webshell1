import React, { Fragment } from 'react';
import { API_URL } from '../apiurl';
import InputForm from './InputForm';
import OutputForm from './OutputForm';

class ShellForm extends React.Component {
    state = {
        max: ''
    }
    componentDidMount() {
        this.getMaxID();
    }
    getMaxID = () => {
        fetch(`${API_URL}/last`)
            .then(res => res.json())
            .then(res => this.setState({ max: res.id }))
            .catch(err => console.log(err));
    }
    render() {
        return (
            <Fragment>
                <InputForm/>
                <OutputForm
                    max={this.state.max}
                />
            </Fragment>
        );
    }
}
export default ShellForm;