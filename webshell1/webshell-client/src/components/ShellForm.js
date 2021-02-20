import React, { Fragment } from 'react';
import { API_URL } from '../apiurl';
import InputForm from './InputForm';
import OutputForm from './OutputForm';

class ShellForm extends React.Component {
    state = {
        item: ''
    }
    componentDidMount() {
        this.getItem();
    }
    getItem = () => {
        fetch(`${API_URL}/last`)
            .then(res => res.json())
            .then(res => this.setState({ item: res }))
            .catch(err => console.log(err));
    }
    render() {
        return (
            <Fragment>
                <InputForm/>
                <OutputForm
                    item={this.state.item}
                />
            </Fragment>
        );
    }
}
export default ShellForm;