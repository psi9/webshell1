import React from 'react';
import { Form, FormGroup, Input, Button, Col, Row } from 'reactstrap';
import { API_URL } from '../apiurl';


class InputForm extends React.Component {
    handleChange = e => {
        this.props.onInputChange(e.target.value)
    }
    handleSubmit = e => {
        this.props.onInputSubmit(e)
    }
    handleKeyDown = e => {
        this.props.onKeyDown(e)
    }
    render() {
        return (
            <Form onSubmit={this.handleSubmit} className="inputForm">
                <FormGroup>
                    <Row>
                        <Col md={6}>
                            <Input
                                type="text"
                                name="input"
                                value={this.props.input}
                                onChange={this.handleChange}
                                onKeyDown={this.handleKeyDown}
                            />
                        </Col>
                        <Col>
                            <Button type="submit">execute</Button>
                        </Col>
                    </Row>
                </FormGroup>
            </Form>
        );
    }
}

export default InputForm;