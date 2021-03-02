import React, { Fragment } from 'react';
import { API_URL, API_URL_LAST } from '../apiurl';
import InputForm from './InputForm';
import OutputForm from './OutputForm';

class ShellForm extends React.Component {
    state = {
        items: [],
        max: '',
        current: '',
        input: '',
        output: ''
    }
    componentDidMount() {
        this.getItems();
    }
    getItems = () => {
        fetch(`${API_URL}`)
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
                items: [...this.state.items, res],
                max: res.id,
                current: res.id,
                output: res.output
            }))
            .catch(err => console.log(err));
    }
    onKeyDown = e => {
        if (e.key === "ArrowUp" && this.state.current > 1) {
            this.setState(previous => ({
                current: previous.current - 1,
                input: (this.state.items.map(item => item.input))[this.state.current - 2],
                output: (this.state.items.map(item => item.output))[this.state.current - 2]
            }));

        }
        else if (e.key === "ArrowDown" && this.state.current < this.state.max) {
            this.setState(previous => ({
                current: previous.current + 1,
                input: (this.state.items.map(item => item.input))[this.state.current],
                output: (this.state.items.map(item => item.output))[this.state.current]
            }));
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
