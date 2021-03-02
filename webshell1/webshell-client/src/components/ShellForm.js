import React, { Fragment } from 'react';
import { API_URL, API_URL_LAST } from '../apiurl';
import InputForm from './InputForm';
import OutputForm from './OutputForm';

class ShellForm extends React.Component {
    state = {
        current: '',
        input: '',
        output: '',
        max: ''
    }
    componentDidMount() {
        this.getItems();
    }
    getItem = (id) => {
        fetch(`${API_URL}/${id}`)
            .then(res => res.json())
            .then(res => this.setState({
                items: res,
                max: res.length,
                current: res.length,
                input: (res.map(item => item.input))[res.length - 1],
                output: (res.map(item => item.output))[res.length - 1]
            }))
            .catch(err => console.log(err));
    }
    onChange = input => {
        this.setState({ input })
    }
    onSubmit = e => {
        e.preventDefault();
        fetch(`${API_URL}`, {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(this.state.input)
        })
            .then(res => res.json())
            .then(res => this.setState({
                output: res.output,
                max: res.id,
                current: res.id
            }))
            .catch(err => console.log(err));
    }
    onKeyDown = e => {
        if (e.key === "ArrowUp" && this.state.current > 1) {
            this.setState(previous => ({
                current: previous.current - 1
            }));
            this.getItem(this.state.current - 1);
        }
        else if (e.key === "ArrowDown" && this.state.current < this.state.max) {
            this.setState(previous => ({
                current: previous.current + 1
            }));
            this.getItem(this.state.current + 1);
        }
        
    }
    render() {
        return (
            <Fragment>
                <InputForm
                    onInputChange={this.onChange}
                    onInputSubmit={this.onSubmit}
                    onKeyDown={this.onKeyDown}
                    input={this.state.input}
                />
                <OutputForm
                    output={this.state.output}
                />
            </Fragment>
        );
    }
}
export default ShellForm;
