import React from 'react';
import { Form, FormGroup, Input, Button, Col, Row } from 'reactstrap';
import { API_URL } from '../apiurl';


class InputForm extends React.Component {
    state = {
        id: '',
        input: ''
    }
    onChange = e => {
        this.setState({ input: e.target.value })
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
        .catch(err => console.log(err));
    }

    render() {
        return (
            <Form onSubmit={this.onSubmit} className="inputForm">
                <FormGroup>
                    <Row>
                        <Col md={6}>
                            <Input
                                type="text"
                                name="input"
                                placeholder="Type a PowerShell command here"
                                onChange={this.onChange}
                            />
                        </Col>
                        <Col>
                            <Button>execute</Button>
                        </Col>
                    </Row>
                </FormGroup>
            </Form>
        );
    }
}

export default InputForm;